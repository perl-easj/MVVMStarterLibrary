using InMemoryStorage.Interfaces;
using ModelCollection.Implementation;
using FilePersistency.Implementation;

namespace ExtensionsModel.Implementation
{
    public abstract class FilePersistableCatalog<TDO> : PersistableCatalog<TDO>
        where TDO : class, IStorable
    {
        protected FilePersistableCatalog() : base(new FileSource<TDO>())
        {
        }
    }
}