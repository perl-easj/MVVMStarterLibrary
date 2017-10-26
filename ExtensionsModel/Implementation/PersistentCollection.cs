using System;
using System.Collections.Generic;
using InMemoryStorage.Interfaces;
using Persistency.Implementation;
using Persistency.Interfaces;
// ReSharper disable NotAccessedField.Local

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This class implements a collection with features 
    /// related to persistency:
    /// 1) The collection is monitorable (clients can get 
    ///    notified when collection changes).
    /// 2) The collection is persistable (can be saved to 
    ///    persistent storage).
    /// 3) The collection is managed (can be activated by an
    ///    external persistency manager).
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects
    /// </typeparam>
    public abstract class PersistentCollection<T> :
        IMonitorable, 
        IPersistable, 
        IManaged
    {
        #region Instance fields
        protected IPersistentSource<T> _source;
        protected List<PersistencyOperations> _supportedOperations;
        protected Action _onObjectCreated;
        protected Action _onObjectUpdated;
        protected Action _onObjectDeleted;
        #endregion

        #region Constructor
        protected PersistentCollection(
            IPersistentSource<T> source, 
            List<PersistencyOperations> supportedOperations)
        {
            _source = source ?? throw new ArgumentException(nameof(PersistentCollection<T>));
            _supportedOperations = supportedOperations;

            _onObjectCreated = null;
            _onObjectUpdated = null;
            _onObjectDeleted = null;

            Manage();
        }
        #endregion

        #region IMonitorable implementation
        /// <summary>
        /// Adds a method to be be invoked when 
        /// a new object is created.
        /// </summary>
        public void AddOnObjectCreatedCallback(Action callback)
        {
            _onObjectCreated += callback;
        }

        /// <summary>
        /// Adds a method to be be invoke when 
        /// an object is updated.
        /// </summary>
        public void AddOnObjectUpdatedCallback(Action callback)
        {
            _onObjectUpdated += callback;
        }

        /// <summary>
        /// Adds a method to be be invoke when 
        /// an object is deleted.
        /// </summary>
        public void AddOnObjectDeletedCallback(Action callback)
        {
            _onObjectDeleted += callback;
        }
        #endregion

        #region IPersistable implementation
        /// <summary>
        /// Will try to load objects from persistent source, using
        /// Load semantics (replace all objects in collection with
        /// persisted objects from source).
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
                    InsertObjectsIntoCollection(objects, false);
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
        /// Will try to save objects to underlying source, using 
        /// Save semantics (replace all persisted objects in 
        /// source with objects in collection).
        /// </summary>
        public void Save(bool suppressException = true)
        {
            if (!_supportedOperations.Contains(PersistencyOperations.Save))
            {
                throw new NotSupportedException();
            }

            try
            {
                _source?.Save(GetObjectsInCollection());
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

        #region Abstract methods to override in collection-specific classes
        public abstract List<T> GetObjectsInCollection();
        public abstract void InsertObjectsIntoCollection(List<T> objects, bool replaceKey = true); 
        #endregion
    }
}