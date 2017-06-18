using System;

namespace DTO.Interfaces
{
    public interface IDTO
    {
        int Key { get; set; }
        void SetDefaultValues();
        void SetValuesFromObject(Object obj);
        IDTO Clone();
    }
}