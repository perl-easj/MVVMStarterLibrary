using System.Collections.Generic;

namespace InMemoryStorage.Interfaces
{
    /// <summary>
    /// Interface for a key-based in-memory collection, supporting
    /// basic insertion, retrieval and deletion
    /// </summary>
    /// <typeparam name="T">
    /// Type of stored objects. The type itself does not need to 
    /// inherit from IStorable, if an alternative key management
    /// strategy is desired.
    /// </typeparam>
    public interface IInMemoryCollection<T>
    {
        List<T> All { get; }
        T Read(int key);
        T this[int key] { get; }
        void Insert(T obj, bool replaceKey = true);
        void InsertAll(List<T> objects, bool replaceKey = true);
        void Delete(int key);
        void DeleteAll();
    }
}