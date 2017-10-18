using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistency.Interfaces;

namespace FilePersistency.Implementation
{
    /// <summary>
    /// File-specific implementation of the IPersistentSource interface.
    /// Ties a IStringPersistence implementation to a IDataConverter implementation.
    /// </summary>
    /// <typeparam name="T">
    /// Type of objects to load/save.
    /// </typeparam>
    public class FileSource<T> : IPersistentSource<T>
    {
        private string _fileName;
        private IStringPersistence _stringPersistence;
        private IDataConverter<T> _dataConverter;

        /// <summary>
        /// If nothing else is specified, data is stored in a text file 
        /// called (NameOfClass)Model.dat, for instance CarModel.dat.
        /// </summary>
        public FileSource(IStringPersistence stringPersistence, IDataConverter<T> dataConverter, string fileSuffix = "Model.dat")
        {
            _stringPersistence = stringPersistence;
            _dataConverter = dataConverter;
            _fileName = typeof(T).Name + fileSuffix;
        }

        /// <summary>
        /// Loads objects from file
        /// </summary>
        /// <returns>
        /// List of loaded objects, wrapped in an awaitable Task.
        /// </returns>
        public async Task<List<T>> Load()
        {
            string data = await _stringPersistence.LoadAsync(_fileName);
            return _dataConverter.ConvertFromString(data);
        }

        /// <summary>
        /// Saves objects to file
        /// </summary>
        /// <param name="objects">
        /// List of objects to save
        /// </param>
        public Task Save(List<T> objects)
        {
            string data = _dataConverter.ConvertToString(objects);
            return _stringPersistence.SaveAsync(_fileName, data);
        }

        public Task Create(T obj)
        {
            throw new NotSupportedException("Create not supported for File Persistency");
        }

        public Task<T> Read(int key)
        {
            throw new NotSupportedException("Read not supported for File Persistency");
        }

        public Task Update(int key, T obj)
        {
            throw new NotSupportedException("Update not supported for File Persistency");
        }

        public Task Delete(int key)
        {
            throw new NotSupportedException("Delete not supported for File Persistency");
        }
    }
}