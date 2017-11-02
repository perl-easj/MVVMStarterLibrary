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
    ///    supporting Load, Create and Delete
    /// 2) The InMemoryCollection implementation is used.
    /// </summary>
    public abstract class WebAPIPersistableCatalog<T, TVMO, TDTO> : PersistableCatalog<T, TVMO, TDTO>
        where T : class, IStorable 
        where TDTO : ITransformed<T>
    {
        protected WebAPIPersistableCatalog(string url, string apiID, IFactory<T, TVMO> vmFactory, IFactory<T, TDTO> dtoFactory)
            : base(new InMemoryCollection<T>(), new WebAPISource<TDTO>(url, apiID), vmFactory, dtoFactory,
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