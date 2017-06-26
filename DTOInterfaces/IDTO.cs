using System;
using InMemoryStorage.Interfaces;

namespace DTO.Interfaces
{
    public interface IDTO : IStorable
    {
        void SetDefaultValues();
        void SetValuesFromObject(Object obj);
        IDTO Clone();
    }
}