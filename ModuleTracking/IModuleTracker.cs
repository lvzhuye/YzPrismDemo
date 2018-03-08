using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracking
{
    public interface IModuleTracker
    {
        void RecordModuleDownloading(string moduleName, long bytesReceived, long totalBytesToReceive);

        void RecordModuleLoaded(string moduleName);

        void RecordModuleConstracted(string moduleName);

        void RecordModuleInitialized(string moduleName);
    }
}
