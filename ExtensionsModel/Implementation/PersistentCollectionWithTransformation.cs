using System;
using System.Collections.Generic;
using DataTransformation.Interfaces;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// Adds implementation of the ITransformedDataCollection 
    /// interface to the PersistentCollectionNoTransformation 
    /// base class.
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects
    /// </typeparam>
    public abstract class PersistentCollectionWithTransformation<T> :
        PersistentCollectionNoTransformation<T>,
        ITransformedDataCollection
    {
        #region Instance fields
        private ITransformedDataFactory<T> _tdoFactory;
        #endregion

        #region Constructor
        protected PersistentCollectionWithTransformation(
            IPersistentSource<T> source,
            IInMemoryCollection<T> collection,
            ITransformedDataFactory<T> tdoFactory,
            List<PersistencyOperations> supportedOperations)
            : base(source, collection, supportedOperations)
        {
            _tdoFactory = tdoFactory ?? throw new ArgumentException(nameof(PersistentCollection<T>));
        }
        #endregion

        #region ITransformedDataCollection implementation
        /// <summary>
        /// Returns all transformed data objects.
        /// </summary>
        public List<ITransformedData> AllTransformed
        {
            get
            {
                List<ITransformedData> tdoCollection = new List<ITransformedData>();
                foreach (T obj in All)
                {
                    tdoCollection.Add(_tdoFactory.CreateTransformedObject(obj));
                }

                return tdoCollection;
            }
        }

        /// <summary>
        /// Returns the transformed data object with 
        /// the specified key (if any).
        /// </summary>
        /// <param name="key">Key for object to read</param>
        /// <returns>Object corresponding to given key</returns>
        public ITransformedData ReadTransformed(int key)
        {
            return _tdoFactory.CreateTransformedObject(Read(key));
        }

        /// <summary>
        /// Delete a single object, by calling base class
        /// implementation of IInMemoryCollection.Delete
        /// </summary>
        /// <param name="key">Key for object to delete</param>
        public void DeleteTransformed(int key)
        {
            Delete(key);
        }

        /// <summary>
        /// Insert a single object, by calling base class
        /// mplementation of IInMemoryCollection.Insert
        /// </summary>
        public void InsertTransformed(ITransformedData obj, bool replaceKey = true)
        {
            Insert(_tdoFactory.CreateDomainObject(obj));
        }
        #endregion
    }
}