using System.Collections.Generic;
using Catalog.Implementation;
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
    public abstract class WebAPIPersistableCatalog<T, TVMO, TDTO> : PersistableCatalog<T, TVMO, TDTO>
        where T : class, IStorable 
        where TDTO : ICopyable, IStorable
        where TVMO : IStorable
    {
        protected WebAPIPersistableCatalog(string url, string apiID)
            : base(new InMemoryCollection<T>(), new ConfiguredWebAPISource<TDTO>(url, apiID),
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

        /// <summary>
        /// This type of catalog does not opt-in
        /// in a management strategy.
        /// </summary>
        public override void Manage()
        {
        }
    }
}