using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Persistency.Interfaces;

namespace FilePersistency.Implementation
{
    public class FileStringPersistence : IStringPersistence
    {
        public async Task<string> LoadAsync(string fileName)
        {
            {
                try
                {
                    StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                    return await FileIO.ReadTextAsync(localFile);
                }
                catch (FileNotFoundException)
                {
                    SaveAsync(fileName, "");
                    return null;
                }
            }
        }

        public async void SaveAsync(string fileName, string stringToSave)
        {
            var saveFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(saveFile, stringToSave);
        }
    }
}