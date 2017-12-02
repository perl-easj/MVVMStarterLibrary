using System.Collections.Generic;
using Catalog.Implementation;
using DataTransformation.Interfaces;
using FilePersistency.Implementation;
using InMemoryStorage.Implementation;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This class injects specific dependencies into 
    /// the FilePersistableCatalog class:
    /// 1) Data is read from a file-based source.
    /// 2) Data is stored as a string in the file source.
    /// 3) The string is on JSON format.
    /// 4) The InMemoryCollection implementation is used.
    /// </summary>
    public abstract class FilePersistableCatalog<T, TVMO, TDTO> : PersistableCatalog<T, TVMO, TDTO>
        where T : class, IStorable, ICopyable, new() 
        where TVMO : IStorable
    {
        protected FilePersistableCatalog() 
            : base(new InMemoryCollection<T>(), 
                   new ConfiguredFileSource<TDTO>(new FileStringPersistence(), new JSONConverter<TDTO>()),
                   new List<PersistencyOperations> {PersistencyOperations.Load, PersistencyOperations.Save})
        {
        }
    }
}