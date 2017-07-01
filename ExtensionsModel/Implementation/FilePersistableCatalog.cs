using DTO.Interfaces;
using InMemoryStorage.Interfaces;
using FilePersistency.Implementation;
using InMemoryStorage.Implementation;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This class injects several dependencies into the PersistentCollection class:
    /// 1) Data is read from a file-based source.
    /// 2) Data is stored as a string in the file source.
    /// 3) The string is on JSON format.
    /// 4) The InMemoryCollection implementation is used.
    /// The catalog thus only needs a DTOCollection for completion.
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects
    /// </typeparam>
    public abstract class FilePersistableCatalog<T> : PersistentCollection<T>
        where T : class, IStorable
    {
        protected FilePersistableCatalog(IDTOFactory<T> dtoFactory) 
            : base(new FileSource<T>(new FileStringPersistence(), new JSONConverter<T>()), new InMemoryCollection<T>(), dtoFactory)
        {
        }
    }
}