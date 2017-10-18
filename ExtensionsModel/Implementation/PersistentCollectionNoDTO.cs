using System;
using System.Collections.Generic;
using InMemoryStorage.Interfaces;
using Persistency.Implementation;
using Persistency.Interfaces;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This is a "fully dressed" implementation of a collection, 
    /// with several features (BUT without DTO):
    /// 1) Implements IInMemoryCollection
    /// 2) The collection is monitorable (clients can get notified when collection changes).
    /// 3) The collection is persistable (can be saved to persistent storage).
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects
    /// </typeparam>
    public abstract class PersistentCollectionNoDTO<T> :
        IInMemoryCollection<T>,
        IMonitorable,
        IPersistable<T>,
        IManaged
    {
        #region Instance fields
        private IPersistentSource<T> _source;
        private IInMemoryCollection<T> _collection;
        private Action _onObjectCreated;
        private Action _onObjectUpdated;
        private Action _onObjectDeleted;
        #endregion

        #region Constructor
        protected PersistentCollectionNoDTO(IPersistentSource<T> source, IInMemoryCollection<T> collection)
        {
            // Sanity checks, so no need to null-check later
            if (source == null || collection == null)
            {
                throw new ArgumentException(nameof(PersistentCollection<T>));
            }

            _source = source;
            _collection = collection;

            _onObjectCreated = null;
            _onObjectUpdated = null;
            _onObjectDeleted = null;

            Manage();
        }
        #endregion

        #region IInMemoryCollection implementation
        // All of the below IInMemoryCollection methods are implemented by
        // 1) Delegating the call to the IInMemoryCollection implementation
        // 2) Invoke relevant callback if collection is changed.
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
            InsertPersistent(obj);
            _collection.Insert(obj, replaceKey);
            _onObjectCreated?.Invoke();
        }

        public void InsertAll(List<T> objects, bool replaceKey = true)
        {
            throw new NotSupportedException("InsertAll not supported for PersistentCollection");
            // _collection.InsertAll(objects, replaceKey);
            // _onObjectCreated?.Invoke();
        }

        public void Delete(int key)
        {
            DeletePersistent(key);
            _collection.Delete(key);
            _onObjectDeleted?.Invoke();
        }

        public void DeleteAll()
        {
            throw new NotSupportedException("DeleteAll not supported for PersistentCollection");
            // _collection.DeleteAll();
            // _onObjectDeleted?.Invoke();
        }
        #endregion

        #region IPersistable implementation
        /// <summary>
        /// Loads objects from the source.
        /// </summary>
        public async void Load(bool suppressException = true)
        {
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
        /// Saves objects back to the source.
        /// </summary>
        public void Save(bool suppressException = true)
        {
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

        public void InsertPersistent(T obj, bool suppressException = true)
        {
            try
            {
                _source?.Create(obj);
            }
            catch (Exception)
            {
                if (!suppressException)
                {
                    throw;
                }
            }
        }

        public void DeletePersistent(int key, bool suppressException = true)
        {
            try
            {
                _source?.Delete(key);
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
    }
}