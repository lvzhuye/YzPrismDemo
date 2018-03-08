using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using ModuleTracking;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ModularityWithUnityDemo.Desktop
{
    /// <summary>
    /// Shell.xaml 的交互逻辑
    /// </summary>
    public partial class Shell : Window
    {
        private IModuleManager moduleManager;
        private IModuleTracker moduleTracker;
        private CallBackLogger logger;

        public Shell(IModuleManager moduleManager, IModuleTracker moduleTracker,CallBackLogger logger)
        {
            if (moduleManager == null)
            {
                throw new ArgumentNullException("moduleManager");
            }
            if(moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }
            if(logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            this.moduleManager = moduleManager;
            this.moduleTracker = moduleTracker;
            this.logger = logger;

            InitializeComponent();
        }

        private void ModuleB_RequestModuleLoad(object sender, EventArgs e)
        {
            this.moduleManager.LoadModule(WellKnownModuleNames.ModuleB);
        }

        private void ModuleC_RequestModuleLoad(object sender, EventArgs e)
        {
            this.moduleManager.LoadModule(WellKnownModuleNames.ModuleC);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.moduleTracker;

            this.logger.CallBack = this.Log;

            this.logger.ReplaySavedLogs();

            this.moduleManager.LoadModuleCompleted += this.ModuleManager_LoadModuleCompleted;
            this.moduleManager.ModuleDownloadProgressChanged += this.ModuleManager_ModuleDownLoadProgressChanged;
        }

        private void ModuleManager_ModuleDownLoadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
            this.moduleTracker.RecordModuleDownloading(e.ModuleInfo.ModuleName,e.BytesReceived,e.TotalBytesToReceive);
        }

        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            this.moduleTracker.RecordModuleLoaded(e.ModuleInfo.ModuleName);
        }

        private void Log(string message, Category category, Priority priority)
        {
            this.TrackTextBox.AppendText(string.Format(CultureInfo.CurrentUICulture,"[{0}][{1}] {2}\r\n",category,priority,message));
        }
    }
}
