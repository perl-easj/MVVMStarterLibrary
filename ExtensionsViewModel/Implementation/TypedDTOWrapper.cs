using System;
using DTO.Implementation;
using DTO.Interfaces;

namespace ExtensionsViewModel.Implementation
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