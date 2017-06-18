using System;
using System.Collections.Generic;

namespace InMemoryStorage.Interfaces
{
    /// <summary>
    /// Extends the read-only interface with methods for modifying
    /// the collection of stored objects.
    /// </summary>
    /// <typeparam name="T">Type of stored objects</typeparam>
    public interface IInMemoryCollection<T>
    {
        List<T> All { get; }
        T Read(int key);
        T this[int key] { get; }
        void Insert(T obj, bool replaceKey = true);
        void InsertAll(List<T> objects, bool replaceKey = true);
        void Delete(int key);
        void DeleteAll();
        void SetCallbacks(Action afterCreate, Action afterUpdate, Action afterDelete);
    }
}