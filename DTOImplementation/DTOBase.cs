using System;
using DTO.Interfaces;

namespace DTO.Implementation
{
    public abstract class DTOBase : IDTO
    {
        public const int NullKey = -1;

        private int _key;

        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }

        protected DTOBase()
        {
            _key = NullKey;
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