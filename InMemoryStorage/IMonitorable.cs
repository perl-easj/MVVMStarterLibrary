using System;

namespace InMemoryStorage.Interfaces
{
    public interface IMonitorable
    {
        void AddOnObjectCreatedCallback(Action callback);
        void AddOnObjectUpdatedCallback(Action callback);
        void AddOnObjectDeletedCallback(Action callback);
    }
}