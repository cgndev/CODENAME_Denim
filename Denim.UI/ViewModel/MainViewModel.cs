using Autofac.Features.Indexed;
using Denim.Model;
using Denim.UI.Data.Lookups;
using Denim.UI.Data.Repositories;
using Denim.UI.Event;
using Denim.UI.Properties;
using Denim.UI.View.Services;
using Denim.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Denim.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private IIndex<string, IDetailViewModel> _detailViewModelCreator;
        private IMessageDialogService _messageDialogService;
        private IDetailViewModel _selectedDetailViewModel;

        //
        private IMemberLookupDataService _memberLookupDataService;
        private IMemberRepository _memberRepository;

        public MemberWrapper _loggedMember;

        public string LoginOfficeId { get; set; }

        public MainViewModel(INavigationViewModel navigationViewModel,
            IIndex<string, IDetailViewModel> detailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IMemberRepository memberRepository,
            IMemberLookupDataService memberLookupDataService
            )
        {
            _eventAggregator = eventAggregator;
            _detailViewModelCreator = detailViewModelCreator;
            _messageDialogService = messageDialogService;

            DetailViewModels = new ObservableCollection<IDetailViewModel>();

            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Subscribe(OnOpenDetailView);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
                .Subscribe(AfterDetailDeleted);
            _eventAggregator.GetEvent<AfterDetailClosedEvent>()
                .Subscribe(AfterDetailClosed);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            OpenSingleDetailViewCommand = new DelegateCommand<Type>(OnOpenSingleDetailViewExecute);

            //
            LogInCommand = new DelegateCommand(OnLoginExecute);

            NavigationViewModel = navigationViewModel;

            //
            Members = new ObservableCollection<LookupItem>();
            _memberLookupDataService = memberLookupDataService;
            _memberRepository = memberRepository;

            LoginOfficeId = Settings.Default.currentUserOfficeId;
        }

        public async Task LoadAsync()
        {
            //await LoadMemberLookupAsync();
            //await NavigationViewModel.LoadAsync();

            if (5 < LoginOfficeId.Length)
            {
                try
                {
                    var temp = await _memberRepository.GetByOfficeIdAsync(LoginOfficeId);
                    LoggedMember = new MemberWrapper(temp);

                    if (LoggedMember.IpNumber == GetIpNumber())
                    {
                        LoginOfficeId = LoggedMember.OfficeId;
                        Settings.Default.currentUserOfficeId = LoginOfficeId;
                        Settings.Default.Save();
                    }
                }
                catch
                {
                    _messageDialogService.ShowInfoDialog("로그인 실패");

                    return;
                }
            }
        }

        //
        public MemberWrapper LoggedMember
        {
            get { return _loggedMember; }
            private set
            {
                _loggedMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogInCommand { get; }

        public ICommand CreateNewDetailCommand { get; }

        public ICommand OpenSingleDetailViewCommand { get; }

        //
        public IMemberDetailViewModel MemberDetailViewModel { get; }

        public INavigationViewModel NavigationViewModel { get; }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }
        
        //
        public ObservableCollection<LookupItem> Members { get; }

        //
        private async Task LoadMemberLookupAsync()
        {
            Members.Clear();
            Members.Add(new LookupItem { Id = 0, DisplayMember = "사용자를 선택하세요..." });

            var lookup = await _memberLookupDataService.GetMemberLookupAsync();
            foreach (var lookupItem in lookup)
            {
                Members.Add(lookupItem);
            }
        }


        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            var detailViewModel = DetailViewModels
                .SingleOrDefault(vm => vm.Id == args.Id
                && vm.GetType().Name == args.ViewModelName);
            
            if(detailViewModel == null)
            {
                detailViewModel = _detailViewModelCreator[args.ViewModelName];
                try
                {
                    await detailViewModel.LoadAsync(args.Id);
                }
                catch
                {
                    _messageDialogService.ShowInfoDialog("Could not load the entity, " +
                        "maybe it was deleted in the meantime by another user. " + 
                        "The navigation is refreshed for you.");
                    await NavigationViewModel.LoadAsync();

                    return;

                }
                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;
        }

        private int nextNewItemId = 0;

        private async void OnLoginExecute()
        {
            try
            {
                var temp = await _memberRepository.GetByOfficeIdAsync(LoginOfficeId);
                LoggedMember = new MemberWrapper(temp);

                if (LoggedMember.IpNumber == GetIpNumber())
                {
                    Settings.Default.currentUserOfficeId = LoginOfficeId;
                    Settings.Default.Save();
                }
            }
            catch
            {
                _messageDialogService.ShowInfoDialog("로그인 실패");

                return;
            }



        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs { Id = nextNewItemId--,
            ViewModelName = viewModelType.Name });
        }

        private void OnOpenSingleDetailViewExecute(Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    Id = -1,
                    ViewModelName = viewModelType.Name
                });
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            var detailViewModel = DetailViewModels
                .SingleOrDefault(vm => vm.Id == id
                && vm.GetType().Name == viewModelName);

            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }

        private int GetIpNumber()
        {
            string hostName = Dns.GetHostName();

            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);

            int ipNumber_D = 0;

            foreach (IPAddress ipAddress in ipEntry.AddressList)
            {
                if (ipAddress.ToString().StartsWith("10.136.23."))
                    ipNumber_D = Int32.Parse(ipAddress.ToString().Split('.')[3]);

            }

            return ipNumber_D;
        }

    }
}
