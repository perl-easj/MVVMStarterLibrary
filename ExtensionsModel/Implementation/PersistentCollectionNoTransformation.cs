using System;
using System.Collections.Generic;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// Adds implementation of the IInMemoryCollection 
    /// interface to the PersistentCollection base class.
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects
    /// </typeparam>
    public class PersistentCollectionNoTransformation<T> :
        PersistentCollection<T>,
        IInMemoryCollection<T>
    {
        #region Instance fields
        protected IInMemoryCollection<T> _collection;
        #endregion

        #region Constructor
        protected PersistentCollectionNoTransformation(
            IPersistentSource<T> source, 
            IInMemoryCollection<T> collection,
            List<PersistencyOperations> supportedOperations) 
            : base(source, supportedOperations)
        {
            _collection = collection ?? throw new ArgumentException(nameof(PersistentCollectionNoTransformation<T>));
        }
        #endregion

        #region IInMemoryCollection implementation
        /// <summary>
        /// Returns all objects stored in the collection as a List
        /// </summary>
        public List<T> All
        {
            get { return _collection.All; }
        }

        /// <summary>
        /// Retrieves the object corresponding to the given key
        /// </summary>
        /// <param name="key">
        /// Key of object to retrieve
        /// </param>
        /// <returns>
        /// The object corresponding to the given key.
        /// Null is returned if no matching object is found.
        /// </returns>
        public T Read(int key)
        {
            return _collection.Read(key);
        }

        /// <summary>
        /// Retrieves the object corresponding to the given key
        /// </summary>
        /// <param name="key">
        /// Key of object to retrieve</param>
        /// <returns>
        /// The object corresponding to the given key.
        /// Null is returned if no matching object is found.
        /// </returns>
        public T this[int key]
        {
            get { return _collection[key]; }
        }

        /// <summary>
        /// Inserts a single object into the collection.
        /// </summary>
        /// <param name="obj">
        /// Object to insert.
        /// </param>
        /// <param name="replaceKey">
        /// Specifies if the Key value should be overwritten.
        /// </param>
        public void Insert(T obj, bool replaceKey = true)
        {
            _collection.Insert(obj, replaceKey);
            if (_supportedOperations.Contains(PersistencyOperations.Create))
            {
                _source.Create(obj);
            }
            _onObjectCreated?.Invoke();
        }

        /// <summary>
        /// Deletes a single object from the collection
        /// </summary>
        /// <param name="key">
        /// Key for object to delete from collection
        /// </param>
        public void Delete(int key)
        {
            _collection.Delete(key);
            if (_supportedOperations.Contains(PersistencyOperations.Delete))
            {
                _source.Delete(key);
            }
            _onObjectDeleted?.Invoke();
        }

        /// <summary>
        /// InsertAll is not supported for a persistent collection
        /// </summary>
        public void InsertAll(List<T> objects, bool replaceKey = true)
        {
            throw new NotSupportedException("InsertAll not supported for PersistentCollection");
        }

        /// <summary>
        /// DeleteAll is not supported for a persistent collection
        /// </summary>
        public void DeleteAll()
        {
            throw new NotSupportedException("DeleteAll not supported for PersistentCollection");
        }
        #endregion

        #region Override of PersistentCollection methods
        public override List<T> GetObjectsInCollection()
        {
            return _collection.All;
        }

        public override void InsertObjectsIntoCollection(List<T> objects, bool replaceKey = true)
        {
            _collection.InsertAll(objects, replaceKey);
        } 
        #endregion
    }
}