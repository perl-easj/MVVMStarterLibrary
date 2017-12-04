using System.Collections.Generic;
using Catalog.Implementation;
using InMemoryStorage.Implementation;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This is a minimal implementation of Catalog functionality,
    /// without any data transformation or data source. 
    /// This class is mostly provided for test purposes.
    /// All four transformation methods will have a trivial implementation.
    /// </summary>
    public class InMemoryCatalog<T> : Catalog<T, T, T> 
        where T : class, IStorable
    {
        public InMemoryCatalog() : base(new InMemoryCollection<T>(), null, new List<PersistencyOperations>())
        {
        }

        public override T CreateDomainObjectFromDTO(T dtoObj)
        {
            return dtoObj;
        }

        public override T CreateDTO(T obj)
        {
            return obj;
        }

        public override T CreateDomainObjectFromVMO(T vmObj)
        {
            return vmObj;
        }

        public override T CreateVMO(T obj)
        {
            return obj;
        }
    }
}