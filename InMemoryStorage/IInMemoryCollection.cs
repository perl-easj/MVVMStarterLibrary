using System.Collections.Generic;

namespace InMemoryStorage.Interfaces
{
    public interface IInMemoryCollection<T> : IInMemoryCollectionReadOnly<T>
        where T : IStorable
    {
        void Insert(T obj, bool replaceKey = true);
        void InsertAll(List<T> objects, bool replaceKey = true);
        void Delete(int key);
        void DeleteAll();
        void AfterObjectCreated();
        void AfterObjectUpdated();
        void AfterObjectDeleted();
    }
}