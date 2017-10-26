using System.Collections.Generic;
using DataTransformation.Interfaces;
using InMemoryStorage.Interfaces;
using FilePersistency.Implementation;
using InMemoryStorage.Implementation;
using Persistency.Interfaces;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This class injects specific dependencies into 
    /// the PersistentCollectionWithTransformation class:
    /// 1) Data is read from a file-based source.
    /// 2) Data is stored as a string in the file source.
    /// 3) The string is on JSON format.
    /// 4) The InMemoryCollection implementation is used.
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects
    /// </typeparam>
    public abstract class FilePersistableCatalog<T> : PersistentCollectionWithTransformation<T>
        where T : class, IStorable
    {
        protected FilePersistableCatalog(ITransformedDataFactory<T> viewDataFactory) 
            : base(new FileSource<T>(new FileStringPersistence(), new JSONConverter<T>()), 
                   new InMemoryCollection<T>(),
                   viewDataFactory,
                   new List<PersistencyOperations> {PersistencyOperations.Load, PersistencyOperations.Save})
        {
        }
    }
}