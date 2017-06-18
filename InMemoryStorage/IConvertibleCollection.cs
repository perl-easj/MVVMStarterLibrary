using System.Collections.Generic;
using DTO.Interfaces;

namespace InMemoryStorage.Interfaces
{
    public interface IConvertibleCollection
    {
        List<IDTO> AllDTO { get; }
        IDTO ReadDTO(int key);
        void DeleteDTO(int key);
        void InsertDTO(IDTO obj, bool replaceKey = true);
    }
}