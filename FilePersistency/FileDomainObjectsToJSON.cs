using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace FilePersistency.Implementation
{
    /// <summary>
    /// This class provides file-based persistency for objects.
    /// The in-memory objects are serialised from/to a JSON string,
    /// which is read/written to a text file.
    /// </summary>
    public static class FileDomainObjectsToJSON<T>
    {
        /// <summary>
        /// Save objects to file.
        /// </summary>
        /// <param name="objects">
        /// Storable objects to save.
        /// </param>
        /// <param name="fileName">
        /// Data is saved to this text file.
        /// </param>
        public static async void Save(List<T> objects, string fileName)
        {
            var saveFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            string collectionAsString = JsonConvert.SerializeObject(objects);
            await FileIO.WriteTextAsync(saveFile, collectionAsString);
        }

        /// <summary>
        /// Read objects from file. 
        /// The objects are returned by the method.
        /// </summary>
        /// <param name="fileName">
        /// Data is read from this text file.
        /// </param>
        public static async Task<List<T>> Load(string fileName)
        {
            string objectsAsString = await LoadFromFileAsync(fileName);
            List<T> objects = new List<T>();
            if (objectsAsString != null)
            {
                objects = (List<T>)JsonConvert.DeserializeObject(objectsAsString, typeof(List<T>));
            }
            return objects;
        }

        /// <summary>
        /// Performs the actual read from the specified file.
        /// Note that if no file is found (first attempt to load),
        /// a new file is created.
        /// </summary>
        /// <param name="fileName">
        /// Data is read from this file.
        /// </param>
        /// <returns>
        /// The raw string read from the text file.
        /// </returns>
        private static async Task<string> LoadFromFileAsync(string fileName)
        {
            {
                try
                {
                    StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                    return await FileIO.ReadTextAsync(localFile);
                }
                catch (FileNotFoundException)
                {
                    var saveFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                    await FileIO.WriteTextAsync(saveFile, "");
                    return null;
                }
            }
        }
    }
}