using System;
using DataClass.Interfaces;

namespace DataClass.Implementation
{
    public abstract class DTOBase : IDTO
    {
        protected DTOBase()
        {
            SetDefaultValues();
        }

        public IDTO Clone()
        {
            return (MemberwiseClone() as IDTO);
        }

        public abstract void SetDefaultValues();
        public abstract void SetValuesFromObject(Object obj);
    }
}