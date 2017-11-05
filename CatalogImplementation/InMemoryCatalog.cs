using System.Collections.Generic;
using DataTransformation.Implementation;
using DataTransformation.Interfaces;
using InMemoryStorage.Implementation;
using Persistency.Interfaces;

namespace Catalog.Implementation
{
    public class InMemoryCatalog<T> : Catalog<T, T, T> 
        where T : class, ITransformed<T>, new()
    {
        public InMemoryCatalog() 
            : base(new InMemoryCollection<T>(), null, 
                   new IdenticalDataFactory<T>(), 
                   new IdenticalDataFactory<T>(), 
                   new List<PersistencyOperations>())
        {
        }
    }
}