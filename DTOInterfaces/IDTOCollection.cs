using System.Collections.Generic;

namespace DTO.Interfaces
{
    public interface IDTOCollection
    {
        List<IDTO> AllDTO { get; }
        IDTO ReadDTO(int key);
        void DeleteDTO(int key);
        void InsertDTO(IDTO obj, bool replaceKey = true);
    }
}