using Microsoft.Practices.Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModularityWithUnityDemo.Desktop
{
    /// <summary>
    /// ModuleControl.xaml 的交互逻辑
    /// </summary>
    public partial class ModuleControl : UserControl
    {
        private ModuleTrackingState moduleTrackingState;

        public ModuleControl()
        {
            InitializeComponent();

            this.DataContextChanged += ModuleControl_DataContextChanged;

            this.OnDataContextChanged();
        }

        public event EventHandler RequestModuleLoad;

        private void RaiseRequestModuleLoad()
        {
            if(this.RequestModuleLoad != null)
            {
                this.RequestModuleLoad(this, EventArgs.Empty);
            }
        }

        private void OnDataContextChanged()
        {
            if(this.moduleTrackingState != null)
            {
                this.moduleTrackingState.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(ModuleTrackingState_PropertyChanged);
            }
            this.moduleTrackingState = this.DataContext as ModuleTrackingState;

            if (this.moduleTrackingState != null)
            {
                this.moduleTrackingState.PropertyChanged += ModuleTrackingState_PropertyChanged;
            }
            this.UpdateLoadPropressTextBlockVisibility();
        }

        private void ModuleTrackingState_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateClickToLoadTextBlockVisibility();
            this.UpdateLoadPropressTextBlockVisibility();
        }

        private void ModuleControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.OnDataContextChanged();
        }

        private void UpdateClickToLoadTextBlockVisibility()
        {
            if ((this.moduleTrackingState != null) 
                && (this.moduleTrackingState.ExpectedInitializationMode == InitializationMode.OnDemand)
                && (this.moduleTrackingState.ModuleInitializationStatus == ModuleInitializationStatus.NotStarted))
            {
                this.ClickToLoadTextBlcok.Visibility = Visibility.Visible;
            }
            else
            {
                this.ClickToLoadTextBlcok.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateLoadPropressTextBlockVisibility()
        {
            if((this.moduleTrackingState != null)
                && (this.moduleTrackingState.ExpectedDownLoadTiming == DownLoadTiming.InBackground)
                && (this.moduleTrackingState.ModuleInitializationStatus == ModuleInitializationStatus.Downloading))
            {
                this.LoadProgressPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.LoadProgressPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void ModuleControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateClickToLoadTextBlockVisibility();
            this.UpdateLoadPropressTextBlockVisibility();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (!e.Handled)
            {
                if ((this.moduleTrackingState != null) 
                    && (this.moduleTrackingState.ExpectedInitializationMode == InitializationMode.OnDemand)
                    && (this.moduleTrackingState.ModuleInitializationStatus == ModuleInitializationStatus.NotStarted))
                {
                    this.RaiseRequestModuleLoad();
                    e.Handled = true;
                }
            }
        }
    }
}
