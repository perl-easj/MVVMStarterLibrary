using System.Collections.Generic;

namespace InMemoryStorage.Interfaces
{
    public interface IConvertibleInMemoryCollection<TDTO>
    {
        List<TDTO> AllDTO { get; }
        TDTO ReadDTO(int key);
        void DeleteDTO(int key);
        void InsertDTO(TDTO obj, bool replaceKey = true);
    }
}