using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularityWithUnityDemo.Desktop
{
    public enum ModuleInitializationStatus
    {
        NotStarted,
        
        Downloading,

        Downloaded,

        Constructed,

        Initialized
    }
}
