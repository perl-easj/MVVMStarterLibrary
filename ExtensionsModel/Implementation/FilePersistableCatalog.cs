using System.Collections.Generic;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;
using FilePersistency.Implementation;
using InMemoryStorage.Implementation;
using Persistency.Interfaces;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This class injects specific dependencies into the PersistentCollection class:
    /// 1) Data is read from a file-based source.
    /// 2) Data is stored as a string in the file source.
    /// 3) The string is on JSON format.
    /// 4) The InMemoryCollection implementation is used.
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects
    /// </typeparam>
    public abstract class FilePersistableCatalog<T> : PersistentCollection<T>
        where T : class, IStorable
    {
        protected FilePersistableCatalog(IDTOFactory<T> dtoFactory) 
            : base(new FileSource<T>(new FileStringPersistence(), new JSONConverter<T>()), 
                   new InMemoryCollection<T>(), 
                   dtoFactory,
                   new List<PersistencyOperations> {PersistencyOperations.Load, PersistencyOperations.Save})
        {
        }
    }
}