using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using ModularityWithUnityDemo.Desktop.Properties;
using ModuleTracking;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularityWithUnityDemo.Desktop
{
    public class ModuleTracker : IModuleTracker
    {
        private readonly ModuleTrackingState moduleATrackingState;
        private readonly ModuleTrackingState moduleBTrackingState;
        private readonly ModuleTrackingState moduleCTrackingState;
        private readonly ModuleTrackingState moduleDTrackingState;

        private ILoggerFacade logger;


        public ModuleTracker(ILoggerFacade logger)
        {
            if(logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            this.logger = logger;

            this.moduleATrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleA,
                ExpectedDiscoveryMethod=DiscoveryMethod.ApplicationReference,
                ExpectedInitializationMode=InitializationMode.WhenAvailable,
                ExpectedDownLoadTiming=DownLoadTiming.WithApplication,
                ConfiguredDependencies=WellKnownModuleNames.ModuleD
            };

            this.moduleBTrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleB,
                ExpectedDiscoveryMethod = DiscoveryMethod.DirectorySweep,
                ExpectedInitializationMode = InitializationMode.OnDemand,
                ExpectedDownLoadTiming = DownLoadTiming.InBackground
            };

            this.moduleCTrackingState = new ModuleTrackingState
            {
                ModuleName=WellKnownModuleNames.ModuleC,
                ExpectedDiscoveryMethod=DiscoveryMethod.ApplicationReference,
                ExpectedInitializationMode=InitializationMode.OnDemand,
                ExpectedDownLoadTiming=DownLoadTiming.WithApplication
            };

            this.moduleDTrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleD,
                ExpectedDiscoveryMethod = DiscoveryMethod.DirectorySweep,
                ExpectedInitializationMode = InitializationMode.WhenAvailable,
                ExpectedDownLoadTiming = DownLoadTiming.InBackground
            };
        }

        public ModuleTrackingState ModuleATrackingState
        {
            get
            {
                return this.moduleATrackingState;
            }
        }

        public ModuleTrackingState ModuleBTrackingState
        {
            get
            {
                return this.moduleBTrackingState;
            }
        }

        public ModuleTrackingState ModuleCTrackingState
        {
            get
            {
                return this.moduleCTrackingState;
            }
        }

        public ModuleTrackingState ModuleDTrackingState
        {
            get
            {
                return this.moduleDTrackingState;
            }
        }

        public void RecordModuleConstracted(string moduleName)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Constructed;
            }

            this.logger.Log(string.Format(CultureInfo.CurrentCulture,Resources.ModuleConstructed,moduleName),Category.Debug,Priority.Low);
        }

        public void RecordModuleDownloading(string moduleName, long bytesReceived, long totalBytesToReceive)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if(moduleTrackingState != null)
            {
                moduleTrackingState.BytesReceived = bytesReceived;
                moduleTrackingState.TotalBytesToReceive = totalBytesToReceive;

                if(bytesReceived < totalBytesToReceive)
                {
                    moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Downloading;
                }
                else
                {
                    moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Downloaded;
                }
            }

            this.logger.Log(string.Format(CultureInfo.CurrentCulture,Resources.ModuleLoadingProgress,moduleName,bytesReceived,totalBytesToReceive),Category.Debug,Priority.Low);
        }

        public void RecordModuleInitialized(string moduleName)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Initialized;
            }

            this.logger.Log(string.Format(CultureInfo.CurrentCulture, Resources.ModuleInitialized), Category.Debug, Priority.Low);
        }

        public void RecordModuleLoaded(string moduleName)
        {
            this.logger.Log(string.Format(CultureInfo.CurrentCulture, Resources.ModuleLoaded, moduleName), Category.Debug, Priority.Low);
        }

        private ModuleTrackingState GetModuleTrackingState(string moduleName)
        {
            switch (moduleName)
            {
                case WellKnownModuleNames.ModuleA:
                    return this.ModuleATrackingState;
                case WellKnownModuleNames.ModuleB:
                    return this.ModuleBTrackingState;
                case WellKnownModuleNames.ModuleC:
                    return this.ModuleCTrackingState;
                case WellKnownModuleNames.ModuleD:
                    return this.ModuleDTrackingState;
                default:
                    return null;
            }
        }
    }
}
