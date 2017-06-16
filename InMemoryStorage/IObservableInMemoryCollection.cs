using System;

namespace InMemoryStorage.Interfaces
{
    /// <summary>
    /// Interface with events and methods corresponding to 
    /// CUD functionality. An observer should be able to
    /// tie a callback method to the events.
    /// </summary>
    public interface IObservableInMemoryCollection
    {
        void AfterObjectCreated();
        void AfterObjectUpdated();
        void AfterObjectDeleted();

        event EventHandler OnObjectCreated;
        event EventHandler OnObjectUpdated;
        event EventHandler OnObjectDeleted;
    }
}