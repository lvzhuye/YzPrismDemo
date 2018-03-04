using Microsoft.Practices.Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularityWithUnityDemo.Desktop
{
    public class ModuleTrackingState : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }

        private string moduleName;

        private ModuleInitializationStatus moduleInitializationStatus;

        private DiscoveryMethod expectedDiscoveryMethod;

        private InitializationMode expectedInitialzationMode;

        private DownLoadTiming expectedDownLoadTiming;

        private string configuredDependencies = "(none)";

        private long bytesReceived;

        private long totalBytesToReceive;

        public string ModuleName
        {
            get { return this.moduleName; }
            set
            {
                if(this.moduleName != value)
                {
                    this.moduleName = value;
                    this.RaisePropertyChanged(PropertyNames.ModuleName);
                }
            }
        }

        public ModuleInitializationStatus ModuleInitializationStatus
        {
            get
            {
                return this.moduleInitializationStatus;
            }
            set
            {
                if(this.moduleInitializationStatus != value)
                {
                    this.moduleInitializationStatus = value;
                    this.RaisePropertyChanged(PropertyNames.ModuleInitializationStatus);
                }
            }
        }

        public DiscoveryMethod ExpectedDiscoveryMethod
        {
            get
            {
                return this.expectedDiscoveryMethod;
            }
            set
            {
                if(this.expectedDiscoveryMethod != value)
                {
                    this.expectedDiscoveryMethod = value;
                    this.RaisePropertyChanged(PropertyNames.ExpectedDiscoveryMethod);
                }
            }
        }

        public InitializationMode ExpectedInitializationMode
        {
            get
            {
                return this.expectedInitialzationMode;
            }
            set
            {
                if(this.expectedInitialzationMode != value)
                {
                    this.expectedInitialzationMode = value;
                    this.RaisePropertyChanged(PropertyNames.ExpectedInitializationMode);
                }
            }
        }

        public DownLoadTiming ExpectedDownLoadTiming
        {
            get
            {
                return expectedDownLoadTiming;
            }
            set
            {
                if(expectedDownLoadTiming != value)
                {
                    this.expectedDownLoadTiming = value;
                    this.RaisePropertyChanged(PropertyNames.ExpectedDownLoadTiming);
                }
            }
        }

        public string ConfiguredDependencies
        {
            get
            {
                return this.configuredDependencies;
            }
            set
            {
                if (this.configuredDependencies != value)
                {
                    this.configuredDependencies = value;
                    this.RaisePropertyChanged(PropertyNames.ConfiguredDependencies);
                }
            }
        }

        public long BytesReceived
        {
            get
            {
                return this.bytesReceived;
            }
            set
            {
                if(this.bytesReceived != value)
                {
                    this.bytesReceived = value;
                    this.RaisePropertyChanged(PropertyNames.BytesReceived);
                    this.RaisePropertyChanged(PropertyNames.DownLoadProgressPercentage);
                }
            }
        }

        public long TotalBytesToReceive
        {
            get
            {
                return this.totalBytesToReceive;
            }
            set
            {
                if(this.totalBytesToReceive != value)
                {
                    this.totalBytesToReceive = value;
                    this.RaisePropertyChanged(PropertyNames.TotalBytesToReceive);
                    this.RaisePropertyChanged(PropertyNames.DownLoadProgressPercentage);
                }
            }
        }

        public int DownLoadProgressPercentage
        {
            get
            {
                if(this.bytesReceived < this.totalBytesToReceive)
                {
                    return (int)(this.bytesReceived * 100.0 / this.totalBytesToReceive);
                }
                else
                {
                    return 100;
                }
            }
        }

    }

    public static class PropertyNames
    {
        public const string ModuleName = "ModuleName";

        public const string ModuleInitializationStatus = "ModuleInitializationStatus";

        public const string ExpectedDiscoveryMethod = "ExpectedDiscoveryMethod";

        public const string ExpectedInitializationMode = "ExpectedInitializationMode";

        public const string ExpectedDownLoadTiming = "ExpectedDownLoadTiming";

        public const string ConfiguredDependencies = "ConfiguredDependencies";

        public const string BytesReceived = "BytesReceived";

        public const string TotalBytesToReceive = "TotalBytesToReceive";

        public const string DownLoadProgressPercentage = "DownLoadProgressPercentag";


    }
}
