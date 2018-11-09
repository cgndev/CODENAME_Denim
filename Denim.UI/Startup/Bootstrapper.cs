using Autofac;
using Denim.DataAccess;
using Denim.UI.Data;
using Denim.UI.Data.Lookups;
using Denim.UI.Data.Repositories;
using Denim.UI.View.Services;
using Denim.UI.ViewModel;
using Prism.Events;

namespace Denim.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DenimDbContext>().AsSelf();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();


            builder.RegisterType<MainWindow>().AsSelf();


            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();

            builder.RegisterType<FriendDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(FriendDetailViewModel));

            builder.RegisterType<MeetingDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(MeetingDetailViewModel));

            builder.RegisterType<ProgrammingLanguageDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(ProgrammingLanguageDetailViewModel));


            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<FriendRepository>().As<IFriendRepository>();
            builder.RegisterType<MeetingRepository>().As<IMeetingRepository>();
            builder.RegisterType<ProgrammingLanguageRepository>()
                .As<IProgrammingLanguageRepository>();

            //////////////////////////////////////
            builder.RegisterType<MemberRepository>().AsImplementedInterfaces();
            builder.RegisterType<MemberDetailViewModel>().AsImplementedInterfaces();


            return builder.Build();
        }
    }
}
