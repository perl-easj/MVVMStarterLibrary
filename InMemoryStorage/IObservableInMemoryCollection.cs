using System;

namespace InMemoryStorage.Interfaces
{
    public interface IObservableInMemoryCollection<T> : IInMemoryCollection<T>
        where T : IStorable
    {
        event EventHandler OnObjectCreated;
        event EventHandler OnObjectUpdated;
        event EventHandler OnObjectDeleted;
    }
}