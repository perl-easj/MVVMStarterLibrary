using System;

namespace InMemoryStorage.Interfaces
{
    /// <summary>
    /// A collection that enables a client to be notified about changes
    /// in the collection should implement this interface. Clients can
    /// then register callbacks at the collection.
    /// </summary>
    public interface IMonitorable
    {
        void AddOnObjectCreatedCallback(Action callback);
        void AddOnObjectUpdatedCallback(Action callback);
        void AddOnObjectDeletedCallback(Action callback);
    }
}