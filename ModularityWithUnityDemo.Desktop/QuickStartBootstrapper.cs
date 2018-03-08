using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using ModuleTracking;
using System.Windows;

namespace ModularityWithUnityDemo.Desktop
{
    public class QuickStartBootstrapper:UnityBootstrapper
    {
        private readonly CallBackLogger callbackLogger = new CallBackLogger();

        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override ILoggerFacade CreateLogger()
        {
            return this.callbackLogger;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.RegisterTypeIfMissing(typeof(IModuleTracker), typeof(ModuleTracker), true);

            this.Container.RegisterInstance<CallBackLogger>(this.callbackLogger);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ;
        }
    }
}
