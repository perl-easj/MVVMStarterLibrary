using System.Collections.Generic;
using System.Threading.Tasks;
using Persistency.Interfaces;

namespace FilePersistency.Implementation
{
    public class FileSource<T> : IPersistentSource<T>
    {
        private string _fileName;

        /// <summary>
        /// Data is stored in a text file called (NameOfClass)Model.dat,
        /// for instance CarModel.dat
        /// </summary>
        public FileSource()
        {
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
            return await FileDomainObjectsToJSON<T>.Load(_fileName);
        }

        /// <summary>
        /// Saves objects to file
        /// </summary>
        /// <param name="objects">
        /// List of objects to save
        /// </param>
        public void Save(List<T> objects)
        {
            FileDomainObjectsToJSON<T>.Save(objects, _fileName);
        }
    }
}