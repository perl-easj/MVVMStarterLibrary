using System.Collections.Generic;

namespace InMemoryStorage.Interfaces
{
    /// <summary>
    /// Interface for read-only, in-memory collection of objects
    /// implementing the IStorable interface
    /// </summary>
    /// <typeparam name="TDO">Type of stored objects</typeparam>
    public interface IInMemoryCollectionReadOnly<TDO>
    {
        List<TDO> All { get; }
        TDO Read(int key);
        TDO this[int key] { get; }
    }
}