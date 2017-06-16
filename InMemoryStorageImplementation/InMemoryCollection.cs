using System.Collections.Generic;
using System.Linq;
using InMemoryStorage.Interfaces;

namespace InMemoryStorage.Implementation
{
    /// <summary>
    /// Implementation of the in-memory collection interface for an
    /// unobserved collection. Derived classes can choose to override
    /// the After.. methods, if actions need to be invoked after
    /// alteration of the collection.
    /// </summary>
    /// <typeparam name="TDO">Type of stored objects</typeparam>
    public class InMemoryCollection<TDO> : IInMemoryCollection<TDO>
        where TDO : class, IStorable
    {
        private Dictionary<int, TDO> _collection;

        public InMemoryCollection()
        {
            _collection = new Dictionary<int, TDO>();
        }

        /// <summary>
        /// Returns all objects stored in the collection as a List
        /// </summary>
        public List<TDO> All
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
        public void Insert(TDO obj, bool replaceKey = true)
        {
            if (replaceKey)
            {
                obj.Key = NextKey();
            }
            _collection.Add(obj.Key, obj);
            AfterObjectCreated();
        }

        /// <summary>
        /// Insert a set of storable objects into the collection. The
        /// existing objects are all deleted.
        /// </summary>
        /// <param name="objects">Storable objects to insert</param>
        /// <param name="replaceKey">
        /// Specifies if the Key value should be overwritten for each object.
        /// </param>
        public void InsertAll(List<TDO> objects, bool replaceKey = true)
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
        /// <returns>
        /// The storable object corresponding to the given key.
        /// Null is returned if no matching object is found.
        /// </returns>
        public TDO Read(int key)
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
        public TDO this[int key]
        {
            get { return Read(key); }
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