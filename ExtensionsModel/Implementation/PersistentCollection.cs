using System;
using System.Collections.Generic;
using DataTransformation.Interfaces;
using InMemoryStorage.Interfaces;
using Persistency.Implementation;
using Persistency.Interfaces;

// ReSharper disable NotAccessedField.Local

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This is a "fully dressed" implementation of a collection, 
    /// with several features:
    /// 1) Implements the IDTOCollection interface.
    /// 2) The collection is monitorable (clients can get notified when collection changes).
    /// 3) The collection is persistable (can be saved to persistent storage).
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects
    /// </typeparam>
    public abstract class PersistentCollection<T> :
        IInMemoryCollection<T>,
        ITransformedDataCollection, 
        IMonitorable, 
        IPersistable<T>, 
        IManaged
    {
        #region Instance fields
        private IPersistentSource<T> _source;
        private IInMemoryCollection<T> _collection;
        private ITransformedDataFactory<T> _dtoFactory;
        private List<PersistencyOperations> _supportedOperations;
        private Action _onObjectCreated;
        private Action _onObjectUpdated;
        private Action _onObjectDeleted;
        #endregion

        #region Constructor
        protected PersistentCollection(
            IPersistentSource<T> source, 
            IInMemoryCollection<T> collection,
            ITransformedDataFactory<T> dtoFactory,
            List<PersistencyOperations> supportedOperations)
        {
            // Sanity checks, so no need to null-check later
            if (source == null || collection == null || dtoFactory == null)
            {
                throw new ArgumentException(nameof(PersistentCollection<T>));
            }

            _source = source;
            _collection = collection;
            _dtoFactory = dtoFactory;
            _supportedOperations = supportedOperations;

            _onObjectCreated = null;
            _onObjectUpdated = null;
            _onObjectDeleted = null;

            Manage();
        }
        #endregion

        #region IPersistable implementation
        /// <summary>
        /// Will try to load objects from underlying source,
        /// using Load semantics (replace all in-memory objects
        /// with persisted objects from source)
        /// </summary>
        public async void Load(bool suppressException = true)
        {
            if (!_supportedOperations.Contains(PersistencyOperations.Load))
            {
                throw new NotSupportedException();
            }

            try
            {
                if (_source != null)
                {
                    List<T> objects = await _source.Load();
                    _collection.InsertAll(objects, false);
                }
            }
            catch (Exception)
            {
                if (!suppressException)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Will try to save objects to underlying source,
        /// using Save semantics (replace all persisted objects
        /// in source with in-memory objects)
        /// </summary>
        public void Save(bool suppressException = true)
        {
            if (!_supportedOperations.Contains(PersistencyOperations.Save))
            {
                throw new NotSupportedException();
            }

            try
            {
                _source?.Save(_collection.All);
            }
            catch (Exception)
            {
                if (!suppressException)
                {
                    throw;
                }
            }
        }
        #endregion

        #region ITransformedDataCollection implementation
        // IDTOCollection methods implemented by delegating to DTOConverter 
        public List<ITransformedData> AllTransformed
        {
            get
            {
                List<ITransformedData> dtoCollection = new List<ITransformedData>();
                foreach (T obj in _collection.All)
                {
                    dtoCollection.Add(_dtoFactory.CreateTransformedObject(obj));
                }

                return dtoCollection;
            }
        }

        /// <summary>
        /// Reads a single object, given its key. Object is
        /// read directly from in-memory.
        /// </summary>
        /// <param name="key">Key for object to read</param>
        /// <returns>Object corresponding to given key</returns>
        public ITransformedData ReadTransformed(int key)
        {
            return _dtoFactory.CreateTransformedObject(_collection.Read(key));
        }

        /// <summary>
        /// Delete a single object with Delete semantics
        /// (delete object from memory and source)
        /// </summary>
        /// <param name="key">Key for object to read</param>
        public void DeleteTransformed(int key)
        {
            _collection.Delete(key);
            if (_supportedOperations.Contains(PersistencyOperations.Delete))
            {
                _source.Delete(key);
            }
            _onObjectDeleted?.Invoke();
        }

        /// <summary>
        /// Insert a single object with Create semantics
        /// (create object in memory and source)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="replaceKey"></param>
        public void InsertTransformed(ITransformedData obj, bool replaceKey = true)
        {
            T newObj = _dtoFactory.CreateDomainObject(obj);

            _collection.Insert(newObj);
            if (_supportedOperations.Contains(PersistencyOperations.Create))
            {
                _source.Create(newObj);
            }
            _onObjectCreated?.Invoke();
        }
        #endregion

        #region IManaged implementation
        // Catalog registers Load/Save methods at the PersistencyManager
        public virtual void Manage()
        {
            PersistencyManager.Instance.LoadDelegate += Load;
            PersistencyManager.Instance.SaveDelegate += Save;
        }
        #endregion

        #region IMonitorable implementation
        // Management of callbacks for collection changes
        public void AddOnObjectCreatedCallback(Action callback)
        {
            _onObjectCreated += callback;
        }

        public void AddOnObjectUpdatedCallback(Action callback)
        {
            _onObjectUpdated += callback;
        }

        public void AddOnObjectDeletedCallback(Action callback)
        {
            _onObjectDeleted += callback;
        }
        #endregion

        #region IInMemoryCollection implementation
        public List<T> All
        {
            get { return _collection.All; }
        }

        public T Read(int key)
        {
            return _collection.Read(key);
        }

        public T this[int key]
        {
            get { return _collection[key]; }
        }

        public void Insert(T obj, bool replaceKey = true)
        {
            _collection.Insert(obj, replaceKey);
        }

        public void InsertAll(List<T> objects, bool replaceKey = true)
        {
            _collection.InsertAll(objects, replaceKey);
        }

        public void Delete(int key)
        {
            _collection.Delete(key);
        }

        public void DeleteAll()
        {
            _collection.DeleteAll();
        } 
        #endregion
    }
}