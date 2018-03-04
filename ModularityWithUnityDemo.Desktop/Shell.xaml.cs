using Microsoft.Practices.Prism.Modularity;
using ModuleTracking;
using System;
using System.Collections.Generic;
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
        public Shell(IModuleManager moduleManager, IModuleTracker moduleTracker,CallBackLogger logger)
        {
            if (moduleManager == null)
            {

            }
            InitializeComponent();
        }

        private void ModuleB_RequestModuleLoad(object sender, EventArgs e)
        {

        }

        private void ModuleC_RequestModuleLoad(object sender, EventArgs e)
        {
            this.DataContext=this.
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
