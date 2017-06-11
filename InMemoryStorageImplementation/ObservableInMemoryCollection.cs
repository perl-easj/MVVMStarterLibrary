using System;
using InMemoryStorage.Interfaces;

namespace InMemoryStorage.Implementation
{
    public class ObservableInMemoryCollection<T> : InMemoryCollection<T>, IObservableInMemoryCollection<T>
        where T : class, IStorable
    {
        public event EventHandler OnObjectCreated;
        public event EventHandler OnObjectUpdated;
        public event EventHandler OnObjectDeleted;

        public override void AfterObjectCreated()
        {
            OnObjectCreated?.Invoke(this, EventArgs.Empty);
        }

        public override void AfterObjectDeleted()
        {
            OnObjectUpdated?.Invoke(this, EventArgs.Empty);
        }

        public override void AfterObjectUpdated()
        {
            OnObjectDeleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
