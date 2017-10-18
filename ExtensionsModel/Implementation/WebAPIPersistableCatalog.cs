using System.Collections.Generic;
using DTO.Interfaces;
using InMemoryStorage.Implementation;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;
using WebAPI;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This class injects specific dependencies into the PersistentCollection class:
    /// 1) Data is read from a RESTful web service, supporting Load, Create and Delete
    /// 2) The InMemoryCollection implementation is used.
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects
    /// </typeparam>
    public abstract class WebAPIPersistableCatalog<T> : PersistentCollection<T>
        where T : class, IStorable
    {
        protected WebAPIPersistableCatalog(string url, string apiID, IDTOFactory<T> dtoFactory)
            : base(new WebAPISource<T>(url, apiID), 
                   new InMemoryCollection<T>(), 
                   dtoFactory,
                   new List<PersistencyOperations> { PersistencyOperations.Load, PersistencyOperations.Create, PersistencyOperations.Delete })
        {
        }
    }
}