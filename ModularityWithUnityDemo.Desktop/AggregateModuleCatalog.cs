using Microsoft.Practices.Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularityWithUnityDemo.Desktop
{
    public class AggregateModuleCatalog : IModuleCatalog
    {
        public List<IModuleCatalog> cataLogs = new List<IModuleCatalog>();

        public ReadOnlyCollection<IModuleCatalog> Catalogs
        {
            get
            {
                return this.cataLogs.AsReadOnly();
            }
        }

        public AggregateModuleCatalog()
        {
            this.cataLogs.Add(new ModuleCatalog());
        }

        public void AddCatalog(IModuleCatalog catalog)
        {
            if(catalog == null)
            {
                throw new ArgumentNullException("catalog");
            }
            this.cataLogs.Add(catalog);
        }

        public IEnumerable<ModuleInfo> CompleteListWithDependencies(IEnumerable<ModuleInfo> modules)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ModuleInfo> GetDependentModules(ModuleInfo moduleInfo)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
