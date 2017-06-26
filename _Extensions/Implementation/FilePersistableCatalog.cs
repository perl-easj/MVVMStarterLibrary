using InMemoryStorage.Interfaces;
using ModelCollection.Implementation;
using FilePersistency.Implementation;

namespace Extensions.Implementation
{
    public class FilePersistableCatalog<TDomainClass> : PersistableCatalog<TDomainClass>
        where TDomainClass : class, IStorable
    {
        public FilePersistableCatalog() : base(new FileSource<TDomainClass>())
        {
        }
    }
}