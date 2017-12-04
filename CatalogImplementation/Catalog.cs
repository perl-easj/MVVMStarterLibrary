using System;
using System.Collections.Generic;
using Catalog.Interfaces;
using DataTransformation.Interfaces;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;

namespace Catalog.Implementation
{
    /// <summary>
    /// Main implementation of the ICatalog interface,
    /// which contains CRUD operations. Note that since the
    /// CRUD methods will be called by objects in the View 
    /// Model layer, the methods generally operate on
    /// objects of the TVMO type.
    /// </summary>
    /// <typeparam name="T">Domain class</typeparam>
    /// <typeparam name="TVMO">Domain View Model class</typeparam>
    /// <typeparam name="TDTO">Domain Data Transfer class</typeparam>
    public abstract class Catalog<T, TVMO, TDTO> : ICatalog<TVMO>, IDTOTransform<T, TDTO>, IVMOTransform<T, TVMO>
        where T : IStorable
        where TVMO : IStorable
    {
        protected IInMemoryCollection<T> _collection;
        protected IDataSourceCRUD<TDTO> _source;
        protected List<PersistencyOperations> _supportedOperations;

        public event Action<int> CatalogChanged;

        protected Catalog(
            IInMemoryCollection<T> collection,
            IDataSourceCRUD<TDTO> source,
            List<PersistencyOperations> supportedOperations)
        {
            _collection = collection;
            _source = source;
            _supportedOperations = supportedOperations;
        }

        /// <summary>
        /// Returns all objects in the catalog.
        /// </summary>
        public List<TVMO> All
        {
            get
            {
                List<TVMO> transformedAll = new List<TVMO>();
                foreach (T obj in _collection.All)
                {
                    transformedAll.Add(CreateVMO(obj));
                }
                return transformedAll;
            }
        }

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
        public void Create(TVMO vmObj, KeyManagementStrategyType keyManagement = KeyManagementStrategyType.CollectionDecides)
        {
            // Create the new domain object (this is where it happens :-)).
            T obj = CreateDomainObjectFromVMO(vmObj);

            // Strategy for key selection (DataSource decides)
            // 1) Throw exception if Create operation is not supported,
            //    since choosing DataSourceDecides is then meaningless.
            // 2) Call Create on data source, and use returned key value 
            //    as the new key for the object.
            // 3) Call Insert on the in-memory collection.
            if (keyManagement == KeyManagementStrategyType.DataSourceDecides)
            {
                if (!_supportedOperations.Contains(PersistencyOperations.Create))
                {
                    throw new NotSupportedException("The referenced data source does not support Create.");
                }

                obj.Key = _source.Create(CreateDTO(obj)).Result;
                _collection.Insert(obj, keyManagement);
            }

            // Strategy for key selection (Caller decides)
            // 1) If the data source supports the Create operation,
            //    call Create on data source. It is assumed that it
            //    is NOT an error to call this method, even if the
            //    data source does not support it.
            // 2) Call Insert on the in-memory collection.
            if (keyManagement == KeyManagementStrategyType.CallerDecides)
            {
                if (_supportedOperations.Contains(PersistencyOperations.Create))
                {
                    _source.Create(CreateDTO(obj));
                }
                _collection.Insert(obj, keyManagement);
            }

            // Strategy for key selection (Collection decides)
            // 1) Throw exception if Create operation is not supported,
            //    since choosing DataSourceDecides is then meaningless.
            // 2) If the data source supports the Create operation,
            //    call Create on data source. It is assumed that it
            //    is NOT an error to call this method, even if the
            //    data source does not support it.
            if (keyManagement == KeyManagementStrategyType.CollectionDecides)
            {
                obj.Key = _collection.Insert(obj, keyManagement);
                if (_supportedOperations.Contains(PersistencyOperations.Create))
                {
                    _source.Create(CreateDTO(obj));
                }
            }

            CatalogChanged?.Invoke(obj.Key);
        }

        /// <summary>
        /// Reads the object in the catalog which
        /// matches the given key (if any). Note
        /// that the domain object is transformed
        /// to the TVMO type before being returned.
        /// </summary>
        public TVMO Read(int key)
        {
            return CreateVMO(_collection[key]);
        }

        /// <summary>
        /// Updates the given object in the catalog.
        /// The provided key value will be the key 
        /// for the (updated) domain object in the catalog.
        /// </summary>
        public void Update(TVMO obj, int key)
        {
            Delete(key);
            obj.Key = key;
            Create(obj, KeyManagementStrategyType.CallerDecides);
        }

        /// <summary>
        /// Deletes the domain object matching the key 
        /// (if any) from the catalog.
        /// </summary>
        public void Delete(int key)
        {
            _collection.Remove(key);
            if (_supportedOperations.Contains(PersistencyOperations.Delete))
            {
                _source.Delete(key);
            }

            CatalogChanged?.Invoke(key);
        }

        /// <summary>
        /// Override this method in domain-specific catalog class, 
        /// to define transformation from DTO to domain object.
        /// </summary>
        public abstract T CreateDomainObjectFromDTO(TDTO dtoObj);

        /// <summary>
        /// Override this method in domain-specific catalog class, 
        /// to define transformation from VMO to domain object.
        /// </summary>
        public abstract T CreateDomainObjectFromVMO(TVMO vmObj);

        /// <summary>
        /// Override this method in domain-specific catalog class, 
        /// to define transformation from domain object to DTO.
        /// </summary>
        public abstract TDTO CreateDTO(T obj);

        /// <summary>
        /// Override this method in domain-specific catalog class, 
        /// to define transformation from domain object to VMO.
        /// </summary>
        public abstract TVMO CreateVMO(T obj);
    }
}
