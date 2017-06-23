using System.Collections.Generic;
using System.Threading.Tasks;
using Persistency.Interfaces;

namespace FilePersistency.Implementation
{
    public class FileSource<T> : IPersistentSource<T>
    {
        private string _fileName;
        private IStringPersistence _stringPersistence;
        private IDataConverter<T> _dataConverter;

        /// <summary>
        /// Data is stored in a text file called (NameOfClass)Model.dat,
        /// for instance CarModel.dat
        /// </summary>
        public FileSource(IStringPersistence stringPersistence, IDataConverter<T> dataConverter)
        {
            _stringPersistence = stringPersistence;
            _dataConverter = dataConverter;
            _fileName = typeof(T).Name + "Model.dat";
        }

        /// <summary>
        /// Reads objects from file
        /// </summary>
        /// <returns>
        /// List of objects read from file
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
        public void Save(List<T> objects)
        {
            string data = _dataConverter.ConvertToString(objects);
            _stringPersistence.SaveAsync(_fileName, data);
        }
    }
}