using System.Collections.Generic;
using InMemoryStorage.Interfaces;

namespace Catalog.Interfaces
{
    /// <summary>
    /// This is a general interface for a Catalog, which is intended
    /// to manage a collection of domain objects, and provide CRUD
    /// methods which can be called by objects in the View Model layer.
    /// </summary>
    /// <typeparam name="TVMO">Domain View Model class</typeparam>
    public interface ICatalog<TVMO> : ICatalogChangedEvent
    {
        /// <summary>
        /// Returns all objects in the catalog
        /// </summary>
        List<TVMO> All { get; }

        /// <summary>
        /// Creates a new domain object in the catalog, 
        /// based on the data provided in the TVMO object
        /// </summary>
        /// <param name="vmObj">
        /// Object containing data for domain object creation
        /// </param>
        /// <param name="keyManagement">
        /// Strategy for selecting the key for the new domain object
        /// </param>
        void Create(TVMO vmObj, KeyManagementStrategyType keyManagement = KeyManagementStrategyType.CollectionDecides);

        /// <summary>
        /// Reads the object in the catalog which
        /// matches the given key (if any)
        /// </summary>
        TVMO Read(int key);

        /// <summary>
        /// Updates the given object in the catalog.
        /// The provided key value will be the key 
        /// for the (updated) domain object in the catalog.
        /// </summary>
        void Update(TVMO vmObj, int key);

        /// <summary>
        /// Deletes the domain object matching the key 
        /// (if any) from the catalog.
        /// </summary>
        void Delete(int key);
    }
}
