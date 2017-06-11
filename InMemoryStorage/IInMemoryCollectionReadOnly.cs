using System.Collections.Generic;

namespace InMemoryStorage.Interfaces
{
    public interface IInMemoryCollectionReadOnly<T> 
        where T : IStorable
    {
        List<T> All { get; }
        T Read(int key);
    }
}