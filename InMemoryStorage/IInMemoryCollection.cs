using System.Collections.Generic;

namespace InMemoryStorage.Interfaces
{
    /// <summary>
    /// Extends the read-only interface with methods for modifying
    /// the collection of stored objects.
    /// </summary>
    /// <typeparam name="TDO">Type of stored objects</typeparam>
    public interface IInMemoryCollection<TDO> : IInMemoryCollectionReadOnly<TDO>
    {
        void Insert(TDO obj, bool replaceKey = true);
        void InsertAll(List<TDO> objects, bool replaceKey = true);
        void Delete(int key);
        void DeleteAll();
    }
}