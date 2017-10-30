using System;
using System.Collections.Generic;
using Catalog.Interfaces;
using DataTransformation.Interfaces;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;
#pragma warning disable 67

namespace Catalog.Implementation
{
    public class Catalog<T, TVMO, TDTO> : ICatalog<TVMO>, IMonitorableCatalog
    {
        protected IInMemoryCollection<T> _collection;
        protected IPersistentSource<TDTO> _source;
        protected IFactory<T, TVMO> _vmoFactory;
        protected IFactory<T, TDTO> _dtoFactory;
        protected List<PersistencyOperations> _supportedOperations;

        public event Action OnObjectCreated;
        public event Action OnObjectDeleted;
        public event Action OnObjectUpdated;

        public Catalog(
            IInMemoryCollection<T> collection,
            IPersistentSource<TDTO> source,
            IFactory<T, TVMO> vmoFactory,
            IFactory<T, TDTO> dtoFactory,
            List<PersistencyOperations> supportedOperations)
        {
            _collection = collection;
            _source = source;
            _vmoFactory = vmoFactory;
            _dtoFactory = dtoFactory;
            _supportedOperations = supportedOperations;
        }

        public List<TVMO> All
        {
            get
            {
                List<TVMO> transformedAll = new List<TVMO>();
                foreach (T obj in _collection.All)
                {
                    transformedAll.Add(_vmoFactory.CreateTransformedObject(obj));
                }
                return transformedAll;
            }
        }

        public void Create(TVMO tObj, bool replaceKey = true)
        {
            T obj = _vmoFactory.CreateDomainObject(tObj);
            _collection.Insert(obj, replaceKey);

            if (_supportedOperations.Contains(PersistencyOperations.Create))
            {
                _source.Create(_dtoFactory.CreateTransformedObject(obj));
            }

            OnObjectCreated?.Invoke();
        }

        public TVMO Read(int key)
        {
            return _vmoFactory.CreateTransformedObject(_collection[key]);
        }

        public void Update(TVMO obj, int key, bool replaceKey = true)
        {
            Delete(key);
            Create(obj, replaceKey);
        }

        public void Delete(int key)
        {
            _collection.Remove(key);

            if (_supportedOperations.Contains(PersistencyOperations.Delete))
            {
                _source.Delete(key);
            }

            OnObjectDeleted?.Invoke();
        }

        #region IMonitorableCatalog implementation
        /// <summary>
        /// Adds a method to be be invoked when 
        /// a new object is created.
        /// </summary>
        public void AddOnObjectCreatedCallback(Action callback)
        {
            OnObjectCreated += callback;
        }

        /// <summary>
        /// Adds a method to be be invoked when 
        /// an object is updated.
        /// </summary>
        public void AddOnObjectUpdatedCallback(Action callback)
        {
            OnObjectUpdated += callback;
        }

        /// <summary>
        /// Adds a method to be be invoked when 
        /// an object is deleted.
        /// </summary>
        public void AddOnObjectDeletedCallback(Action callback)
        {
            OnObjectDeleted += callback;
        }
        #endregion
    }
}
