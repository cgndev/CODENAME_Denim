using Autofac;
using Denim.UI.Data;
using Denim.UI.Startup;
using System;
using System.Windows;

namespace Denim.UI
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.BootStrap();

            var mainWindow = container.Resolve<MainWindow>();

            mainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexepected error occured. Please inform the admin."
                + Environment.NewLine + e.Exception.Message, "Unexpected Error");

            e.Handled = true;
        }
    }
}
