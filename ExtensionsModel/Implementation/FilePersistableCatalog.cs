using DTO.Interfaces;
using InMemoryStorage.Interfaces;
using Persistency.Implementation;
using FilePersistency.Implementation;
using InMemoryStorage.Implementation;

namespace ExtensionsModel.Implementation
{
    public abstract class FilePersistableCatalog<T> : PersistentCollection<T>
        where T : class, IStorable
    {
        protected FilePersistableCatalog(IDTOFactory<T> dtoFactory) 
            : base(new FileSource<T>(new FileStringPersistence(), new JSONConverter<T>()), new InMemoryCollection<T>(), dtoFactory)
        {
        }
    }
}