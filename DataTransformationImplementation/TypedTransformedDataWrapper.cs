using System;
using DataTransformation.Interfaces;

namespace DataTransformation.Implementation
{
    /// <summary>
    /// Extension of the implementation of a wrapper.
    /// The implementation adds type information to the
    /// wrapped object, enabling clients to call 
    /// type-specific methods on the wrapped object.
    /// </summary>
    /// <typeparam name="TTDO">Type of transformed data object</typeparam>
    public abstract class TypedTransformedDataWrapper<TTDO> : TransformedDataWrapper
        where TTDO : class
    {
        protected TypedTransformedDataWrapper(ITransformedData obj) : base(obj)
        {
        }

        /// <summary>
        /// Returns the transformed data object as an object
        /// of type TTDO. If the cast of a non-null object 
        /// produces a null value, an exception is thrown.
        /// </summary>
        public TTDO TypedDataObject
        {
            get
            {
                if (DataObject == null)
                {
                    return null;
                }

                TTDO tObj = DataObject as TTDO;

                if (tObj == null)
                {
                    throw new ArgumentException(nameof(TypedDataObject));
                }

                return tObj;
            }
        }
    }
}