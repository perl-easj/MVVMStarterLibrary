using System;

namespace DTO.Implementation
{
    /// <summary>
    /// Adds type information to the DTO wrapper, such that it can be
    /// used in e.g. ViewModel classes for View property binding .
    /// </summary>
    /// <typeparam name="TDTO">
    /// Actual type of wrapped DTO
    /// </typeparam>
    //public abstract class TypedDTOWrapper<TDTO> : DTOWrapper
    //    where TDTO : class
    //{
    //    protected TypedDTOWrapper(IDTO obj) : base(obj)
    //    {
    //    }

    //    /// <summary>
    //    /// Returns the DTO as a object of type TDTO. If the cast of a 
    //    /// non-null object produces a null value, an exception is thrown. 
    //    /// </summary>
    //    public TDTO TypedDataObject
    //    {
    //        get
    //        {
    //            if (DataObject == null)
    //            {
    //                return null;
    //            }

    //            TDTO tObj = DataObject as TDTO;

    //            if (tObj == null)
    //            {
    //                throw new ArgumentException(nameof(TypedDataObject));    
    //            }

    //            return tObj;
    //        }
    //    }
    //}
}