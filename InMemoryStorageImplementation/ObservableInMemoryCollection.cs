using System;
using InMemoryStorage.Interfaces;

namespace InMemoryStorage.Implementation
{
    /// <summary>
    /// This class extends the implementation of an in-memory collection
    /// with events and event invoker methods. A client object can register
    /// an eventy handler at the On.. events.
    /// </summary>
    public abstract class ObservableInMemoryCollection<TDO> : 
        InMemoryCollection<TDO>, 
        IObservableInMemoryCollection
        where TDO : class, IStorable
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
