using System;
using System.Collections.Generic;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;

namespace DTO.Implementation
{
    /// <summary>
    /// Standard implementation of conversion to DTOs.
    /// </summary>
    /// <typeparam name="T">
    /// Type of object to convert
    /// </typeparam>
    public class DTOConverter<T>
    {
        private IInMemoryCollection<T> _collection;
        private IDTOFactory<T> _dtoFactory;

        public DTOConverter(IInMemoryCollection<T> collection, IDTOFactory<T> dtoFactory)
        {
            // Sanity checks, so no need to null-check later
            if (collection == null || dtoFactory == null)
            {
                throw new ArgumentException(nameof(DTOConverter<T>));
            }

            _collection = collection;
            _dtoFactory = dtoFactory;
        }

        /// <summary>
        /// Convert all objects to DTOs
        /// </summary>
        public List<IDTO> AllDTO
        {
            get
            {
                List<IDTO> dtoCollection = new List<IDTO>();
                foreach (T obj in _collection.All)
                {
                    dtoCollection.Add(_dtoFactory.Create(obj));
                }

                return dtoCollection;
            }
        }

        /// <summary>
        /// Read object corresponding to given key.
        /// </summary>
        /// <param name="key">
        /// Key for object to read.
        /// </param>
        /// <returns>
        /// Read object converted to DTO.
        /// </returns>
        public IDTO ReadDTO(int key)
        {
            T obj = _collection.Read(key);
            if (obj == null)
            {
                return null;
            }

            return _dtoFactory.Create(obj);
        }

        /// <summary>
        /// Delete object corresponding to given key.
        /// </summary>
        /// <param name="key">
        /// Key for object to delete.
        /// </param>
        public void DeleteDTO(int key)
        {
            _collection.Delete(key);
        }
    }
}