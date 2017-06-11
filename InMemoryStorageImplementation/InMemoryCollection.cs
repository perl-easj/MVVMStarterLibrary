using System.Collections.Generic;
using System.Linq;
using InMemoryStorage.Interfaces;

namespace InMemoryStorage.Implementation
{
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
        /// <param name="obj">
        /// Storable object to insert.
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
            AfterObjectCreated();
        }

        public void InsertAll(List<T> objects, bool replaceKey = true)
        {
            DeleteAll();
            foreach (var obj in objects)
            {
                Insert(obj, replaceKey);
            }
            AfterObjectCreated();
        }

        /// <summary>
        /// Deletes a single storable object from the collection
        /// </summary>
        /// <param name="key">
        /// Key for object to delete from collection
        /// </param>
        /// <returns>
        /// Return true if an object was actually deleted.
        /// </returns>
        public void Delete(int key)
        {
            _collection.Remove(key);
            AfterObjectDeleted();
        }

        /// <summary>
        /// Deletes all storable objects from the collection unconditionally.
        /// </summary>
        public void DeleteAll()
        {
            _collection.Clear();
            AfterObjectDeleted();
        }

        /// <summary>
        /// Retrieves the object corresponding to the given key
        /// </summary>
        /// <param name="key">Key of object to retrieve</param>
        /// <returns>The storable object corresponding to the given key</returns>
        public T Read(int key)
        {
            return _collection.ContainsKey(key) ? _collection[key] : null;
        }

        public virtual void AfterObjectCreated()
        {
            // Derived classes may override this method
        }

        public virtual void AfterObjectUpdated()
        {
            // Derived classes may override this method
        }

        public virtual void AfterObjectDeleted()
        {
            // Derived classes may override this method
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