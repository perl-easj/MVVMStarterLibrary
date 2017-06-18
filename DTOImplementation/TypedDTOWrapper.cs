using System;
using DTO.Interfaces;

namespace DTO.Implementation
{
    public abstract class TypedDTOWrapper<TDTO> : DTOWrapper
        where TDTO : class
    {
        private TDTO _typedDataObject;

        protected TypedDTOWrapper(IDTO obj) : base(obj)
        {
            _typedDataObject = DataObject as TDTO;
            if (_typedDataObject == null)
            {
                throw new ArgumentException();
            }
        }

        public TDTO TypedDataObject
        {
            get { return _typedDataObject; }
        }
    }
}