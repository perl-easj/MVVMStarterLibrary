﻿using System;
using System.Collections.Generic;
using Catalog.Interfaces;
using InMemoryStorage.Interfaces;
using Persistency.Implementation;
using Persistency.Interfaces;

namespace Catalog.Implementation
{
    /// <summary>
    /// Implementation of a persistable (and managed) Catalog.
    /// Note that the implementation inherits from Catalog, and
    /// thus also contains CRUD methods.
    /// </summary>
    public abstract class PersistableCatalog<T, TVMO, TDTO> : Catalog<T, TVMO, TDTO>, IPersistableCatalog, IManagedCatalog 
        where TVMO : IStorable 
        where T : IStorable
    {
        #region Constructor
        protected PersistableCatalog(
            IInMemoryCollection<T> collection,
            IPersistentSource<TDTO> source,
            List<PersistencyOperations> supportedOperations)
            : base(collection, source, supportedOperations)
        {
            Manage();
        }
        #endregion

        #region IPersistableCatalog implementation
        /// <summary>
        /// Relays call of Load to data source, if the data 
        /// source supports the Load operation
        /// </summary>
        public async void Load(bool suppressException = true)
        {

            if (_supportedOperations.Contains(PersistencyOperations.Load))
            {
                List<TDTO> objects = await _source.Load();
                _collection.ReplaceAll(CreateDomainObjects(objects), KeyManagementStrategyType.DataSourceDecides);
            }
            else
            {
                if (!suppressException)
                {
                    throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Relays call of Save to data source, if the data 
        /// source supports the Load operation
        /// </summary>
        public async void Save(bool suppressException = true)
        {
            if (_supportedOperations.Contains(PersistencyOperations.Save))
            {
                await _source.Save(CreateDTOList(_collection.All));
            }
            else
            {
                if (!suppressException)
                {
                    throw new NotSupportedException();
                }
            }
        } 
        #endregion

        #region IManaged implementation
        /// <summary>
        /// Catalog registers Load/Save methods 
        /// at the PersistencyManager
        /// </summary>
        public virtual void Manage()
        {
            PersistencyManager.Instance.LoadDelegate += Load;
            PersistencyManager.Instance.SaveDelegate += Save;
        }
        #endregion

        #region Private helper methods
        /// <summary>
        /// Transforms a list of domain objects into a 
        /// list of corresponding DTOs.
        /// </summary>
        private List<TDTO> CreateDTOList(List<T> objects)
        {
            List<TDTO> dtoObjects = new List<TDTO>();
            foreach (T obj in objects)
            {
                dtoObjects.Add(CreateDTO(obj));
            }
            return dtoObjects;
        }

        /// <summary>
        /// Transforms a list of DTOs into a list of 
        /// corresponding domain objects.
        /// </summary>
        private List<T> CreateDomainObjects(List<TDTO> dtoObjects)
        {
            List<T> objects = new List<T>();
            foreach (TDTO dtoObj in dtoObjects)
            {
                objects.Add(CreateDomainObjectFromDTO(dtoObj));
            }
            return objects;
        } 
        #endregion
    }
}