using System.Collections.Generic;
using System.Threading.Tasks;
using Persistency.Interfaces;
using StringPersistency.Interfaces;

namespace FilePersistency.Implementation
{
    /// <summary>
    /// File-specific implementation of IDataSource... interfaces. 
    /// Ties a IStringPersistence implementation to a IDataConverter 
    /// implementation.
    /// </summary>
    /// <typeparam name="TDTO">
    /// Type of objects to load/save.
    /// </typeparam>
    public class FileSource<TDTO> : IDataSourceLoad<TDTO>, IDataSourceSave<TDTO>
    {
        private string _fileName;
        private IStringPersistence _stringPersistence;
        private IStringConverter<TDTO> _stringConverter;

        /// <summary>
        /// If nothing else is specified, data is stored 
        /// in a text file  called (NameOfClass)Model.dat, 
        /// for instance CarModel.dat.
        /// </summary>
        public FileSource(IStringPersistence stringPersistence, IStringConverter<TDTO> stringConverter, string fileSuffix = "Model.dat")
        {
            _stringPersistence = stringPersistence;
            _stringConverter = stringConverter;
            _fileName = typeof(TDTO).Name + fileSuffix;
        }

        /// <summary>
        /// Loads objects from file
        /// </summary>
        /// <returns>
        /// List of loaded objects, wrapped in an awaitable Task.
        /// </returns>
        public async Task<List<TDTO>> Load()
        {
            string data = await _stringPersistence.LoadAsync(_fileName);
            return _stringConverter.ConvertFromString(data);
        }

        /// <summary>
        /// Saves objects to file
        /// </summary>
        /// <param name="objects">
        /// List of objects to save
        /// </param>
        public Task Save(List<TDTO> objects)
        {
            string data = _stringConverter.ConvertToString(objects);
            return _stringPersistence.SaveAsync(_fileName, data);
        }
    }
}