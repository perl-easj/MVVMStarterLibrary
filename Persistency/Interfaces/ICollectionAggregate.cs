using InMemoryStorage.Interfaces;

namespace Persistency.Interfaces
{
    public interface ICollectionAggregate<T> : IInMemoryCollection<T>, IPersistable, IObservable, IConvertibleCollection, IManaged
    {
    }
}