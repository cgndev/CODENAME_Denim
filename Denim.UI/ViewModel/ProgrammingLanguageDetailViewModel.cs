using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Denim.UI.Data.Repositories;
using Denim.UI.View.Services;
using Denim.UI.Wrapper;
using Prism.Commands;
using Prism.Events;

namespace Denim.UI.ViewModel
{
    public class ProgrammingLanguageDetailViewModel : DetailViewModelBase
    {
        private IProgrammingLanguageRepository _programmingLanguageRepository;
        private ProgrammingLanguageWrapper _selectedProgrammingLanguage;

        public ProgrammingLanguageDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IProgrammingLanguageRepository programmingLanguageRepository) 
            : base(eventAggregator, messageDialogService)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            Title = "Programming Languages";
            ProgrammingLanguages = new ObservableCollection<ProgrammingLanguageWrapper>();

            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
        }

        public async override Task LoadAsync(int id)
        {
            // TODO : Load data here
            Id = id;
            
            foreach (var wrapper in ProgrammingLanguages)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }

            ProgrammingLanguages.Clear();

            var languages = await _programmingLanguageRepository.GetAllAsync();

            foreach (var model in languages)
            {
                var wrapper = new ProgrammingLanguageWrapper(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                ProgrammingLanguages.Add(wrapper);
            }
        }

        private void Wrapper_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _programmingLanguageRepository.HasChanges();
            }

            if (e.PropertyName == nameof(ProgrammingLanguageWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<ProgrammingLanguageWrapper> ProgrammingLanguages { get; }

        public ICommand RemoveCommand { get; }
        public ICommand AddCommand { get;  }

        public ProgrammingLanguageWrapper SelectedProgrammingLanguage
        {
            get { return _selectedProgrammingLanguage; }
            set
            {
                _selectedProgrammingLanguage = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
            }
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            return HasChanges && ProgrammingLanguages.All(p => !p.HasErrors);
        }

        protected async override void OnSaveExecute()
        {
            try
            {
                await _programmingLanguageRepository.SaveAsync();
                HasChanges = _programmingLanguageRepository.HasChanges();
                RaiseCollectionSavedEvent();
            }
            catch(Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageDialogService.ShowInfoDialog("Error while saving the entities, " +
                    "the data will be reloaded. Details : " + ex.Message);
                await LoadAsync(Id);
            }

        }

        private void OnAddExecute()
        {
            var wrapper = new ProgrammingLanguageWrapper(new Model.ProgrammingLanguage());
            wrapper.PropertyChanged += Wrapper_PropertyChanged;
            _programmingLanguageRepository.Add(wrapper.Model);
            ProgrammingLanguages.Add(wrapper);

            // Trigger the calidation
            wrapper.Name = "";
        }

        private async void OnRemoveExecute()
        {
            var isReferenced =
                await _programmingLanguageRepository.IsReferencedByFriendAsync(
                    SelectedProgrammingLanguage.Id);
            if (isReferenced)
            {
                MessageDialogService.ShowInfoDialog($"The language {SelectedProgrammingLanguage.Name}" +
                    $" can't be removed, as it is referenced by at least one friend");

                return;
            }

            SelectedProgrammingLanguage.PropertyChanged -= Wrapper_PropertyChanged;
            _programmingLanguageRepository.Remove(SelectedProgrammingLanguage.Model);
            ProgrammingLanguages.Remove(SelectedProgrammingLanguage);
            SelectedProgrammingLanguage = null;
            HasChanges = _programmingLanguageRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnRemoveCanExecute()
        {
            return SelectedProgrammingLanguage != null;
        }
    }
}
