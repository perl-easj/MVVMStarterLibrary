using System;
using DataTransformation.Interfaces;

namespace DataTransformation.Implementation
{
    public abstract class TypedTransformedDataWrapper<TDO> : TransformedDataWrapper
        where TDO : class
    {
        protected TypedTransformedDataWrapper(ITransformedData obj) : base(obj)
        {
        }

        /// <summary>
        /// Returns the DTO as a object of type TDTO. If the cast of a 
        /// non-null object produces a null value, an exception is thrown. 
        /// </summary>
        public TDO TypedDataObject
        {
            get
            {
                if (DataObject == null)
                {
                    return null;
                }

                TDO tObj = DataObject as TDO;

                if (tObj == null)
                {
                    throw new ArgumentException(nameof(TypedDataObject));
                }

                return tObj;
            }
        }
    }
}