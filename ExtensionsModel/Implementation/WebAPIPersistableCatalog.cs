using System.Collections.Generic;
using Catalog.Implementation;
using DataTransformation.Interfaces;
using InMemoryStorage.Implementation;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;
using WebAPI.Implementation;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This class injects specific dependencies into 
    /// the PersistentCollectionWithTransformation class:
    /// 1) Data is read from a RESTful web service, 
    ///    supporting Load + CRUD
    /// 2) The InMemoryCollection implementation is used.
    /// </summary>
    public class WebAPIPersistableCatalog<T, TVMO, TDTO> : PersistableCatalog<T, TVMO, TDTO>
        where T : class, IStorable 
        where TDTO : ITransformed<T>
        where TVMO : IStorable
    {
        public WebAPIPersistableCatalog(string url, string apiID, IFactory<T, TVMO> vmFactory, IFactory<T, TDTO> dtoFactory)
            : base(new InMemoryCollection<T>(), new ConfiguredWebAPISource<TDTO>(url, apiID), vmFactory, dtoFactory,
                   new List<PersistencyOperations>
                   {
                       PersistencyOperations.Load,
                       PersistencyOperations.Create,
                       PersistencyOperations.Read,
                       PersistencyOperations.Update,
                       PersistencyOperations.Delete
                   })
        {
        }
    }
}