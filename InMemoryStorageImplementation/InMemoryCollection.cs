using System.Collections.Generic;
using System.Linq;
using InMemoryStorage.Interfaces;

namespace InMemoryStorage.Implementation
{
    /// <summary>
    /// Implementation of the in-memory collection interface for an
    /// unobserved collection.
    /// </summary>
    /// <typeparam name="T">Type of stored objects</typeparam>
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
        /// Inserts a single storable object into the collection
        /// </summary>
        /// <param name="obj">Storable object to insert.</param>
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
        /// Insert a set of storable objects into the collection. The
        /// existing objects are all deleted.
        /// </summary>
        /// <param name="objects">Storable objects to insert</param>
        /// <param name="replaceKey">
        /// Specifies if the Key value should be overwritten for each object.
        /// </param>
        public void InsertAll(List<T> objects, bool replaceKey = true)
        {
            DeleteAll();
            foreach (var obj in objects)
            {
                Insert(obj, replaceKey);
            }
        }

        /// <summary>
        /// Deletes a single storable object from the collection
        /// </summary>
        /// <param name="key">
        /// Key for object to delete from collection
        /// </param>
        public void Delete(int key)
        {
            _collection.Remove(key);
        }

        /// <summary>
        /// Deletes all storable objects from the collection unconditionally.
        /// </summary>
        public void DeleteAll()
        {
            _collection.Clear();
        }

        /// <summary>
        /// Retrieves the object corresponding to the given key
        /// </summary>
        /// <param name="key">Key of object to retrieve</param>
        /// <returns>
        /// The storable object corresponding to the given key.
        /// Null is returned if no matching object is found.
        /// </returns>
        public T Read(int key)
        {
            return _collection.ContainsKey(key) ? _collection[key] : null;
        }

        /// <summary>
        /// Retrieves the object corresponding to the given key
        /// </summary>
        /// <param name="key">Key of object to retrieve</param>
        /// <returns>
        /// The storable object corresponding to the given key.
        /// Null is returned if no matching object is found.
        /// </returns>
        public T this[int key]
        {
            get { return Read(key); }
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