using System;
using System.Collections.Generic;
using DTO.Implementation;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;
using Persistency.Implementation;
using Persistency.Interfaces;

// ReSharper disable NotAccessedField.Local

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This is a "fully dressed" implementation of a collection, 
    /// with several features:
    /// 1) Implements both the IInMemoryCollection and IDTOCollection interfaces.
    /// 2) The collection is monitorable (clients can get notified when collection changes).
    /// 3) The collection is persistable (can be saved to persistent storage).
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects
    /// </typeparam>
    public abstract class PersistentCollection<T> : 
        IInMemoryCollection<T>, 
        IDTOCollection, 
        IMonitorable, 
        IPersistable, 
        IManaged
    {
        #region Instance fields
        private IPersistentSource<T> _source;
        private IInMemoryCollection<T> _collection;
        private IDTOFactory<T> _dtoFactory;
        private DTOConverter<T> _dtoConverter;
        private Action _onObjectCreated;
        private Action _onObjectUpdated;
        private Action _onObjectDeleted;
        #endregion

        #region Constructor
        protected PersistentCollection(IPersistentSource<T> source, IInMemoryCollection<T> collection, IDTOFactory<T> dtoFactory)
        {
            // Sanity checks, so no need to null-check later
            if (source == null || collection == null || dtoFactory == null)
            {
                throw new ArgumentException(nameof(PersistentCollection<T>));
            }

            _source = source;
            _collection = collection;
            _dtoFactory = dtoFactory;
            _dtoConverter = new DTOConverter<T>(collection, dtoFactory);

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
            _collection.Insert(obj, replaceKey);
            _onObjectCreated?.Invoke();
        }

        public void InsertAll(List<T> objects, bool replaceKey = true)
        {
            _collection.InsertAll(objects, replaceKey);
            _onObjectCreated?.Invoke();
        }

        public void Delete(int key)
        {
            _collection.Delete(key);
            _onObjectDeleted?.Invoke();
        }

        public void DeleteAll()
        {
            _collection.DeleteAll();
            _onObjectDeleted?.Invoke();
        }
        #endregion

        #region IPersistable implementation
        /// <summary>
        /// Loads objects from the source.
        /// </summary>
        public async void Load()
        {
            if (_source != null)
            {
                List<T> objects = await _source.Load();
                _collection.InsertAll(objects, false);
            }
        }

        /// <summary>
        /// Saves objects back to the source.
        /// </summary>
        public void Save()
        {
            _source?.Save(_collection.All);
        }
        #endregion

        #region IDTOCollection implementation
        // IDTOCollection methods implemented by delegating to DTOConverter 
        public List<IDTO> AllDTO
        {
            get { return _dtoConverter.AllDTO;}
        }

        public IDTO ReadDTO(int key)
        {
            return _dtoConverter.ReadDTO(key);
        }

        public void DeleteDTO(int key)
        {
            _dtoConverter.DeleteDTO(key);
            _onObjectDeleted?.Invoke();
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

        // Type-specific Catalog classes will need to implement this method.
        public abstract void InsertDTO(IDTO obj, bool replaceKey = true);
    }
}