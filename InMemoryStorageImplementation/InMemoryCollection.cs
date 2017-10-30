using System.Collections.Generic;
using System.Linq;
using InMemoryStorage.Interfaces;

namespace InMemoryStorage.Implementation
{
    /// <summary>
    /// Implementation of the interface for an in-memory 
    /// collection, typically of domain objects
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects. Type must inherit from IStorable.
    /// </typeparam>
    public class InMemoryCollection<T> : IInMemoryCollection<T>
        where T : class, IStorable
    {
        private Dictionary<int, T> _collection;

        public InMemoryCollection()
        {
            _collection = new Dictionary<int, T>();
        }

        /// <summary>
        /// Returns all objects stored in the collection as a List
        /// </summary>
        public List<T> All
        {
            get { return _collection.Values.ToList(); }
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
            if (replaceKey)
            {
                obj.Key = NextKey();
            }
            _collection.Add(obj.Key, obj);
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
        public T Get(int key)
        {
            return _collection.ContainsKey(key) ? _collection[key] : null;
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
            get { return Get(key); }
        }

        /// <summary>
        /// Deletes a single object from the collection
        /// </summary>
        /// <param name="key">
        /// Key for object to delete from collection
        /// </param>
        public void Remove(int key)
        {
            _collection.Remove(key);
        }

        /// <summary>
        /// Insert a set of objects into the collection. The existing
        /// objects are all deleted.
        /// </summary>
        /// <param name="objects">
        /// Storable objects to insert.
        /// </param>
        /// <param name="replaceKey">
        /// Specifies if the Key value should be overwritten for each object.
        /// </param>
        public void InsertAll(List<T> objects, bool replaceKey = true)
        {
            foreach (var obj in objects)
            {
                Insert(obj, replaceKey);
            }
        }

        public void ReplaceAll(List<T> objects, bool replaceKey = true)
        {
            RemoveAll();
            foreach (var obj in objects)
            {
                Insert(obj, replaceKey);
            }
        }

        /// <summary>
        /// Deletes all objects from the collection unconditionally.
        /// </summary>
        public void RemoveAll()
        {
            _collection.Clear();
        }

        /// <summary>
        /// Finds the next valid key for a new object to be inserted
        /// into the collection.
        /// </summary>
        /// <returns>
        /// Key to assign to new object.
        /// </returns>
        private int NextKey()
        {
            return ((_collection.Count == 0) ? 0 : _collection.Keys.Max() + 1);
        }
    }
}