using System;
using InMemoryStorage.Implementation;

namespace DTO.Implementation
{
    /// <summary>
    /// Implementation of IDTO interface, using StorableBase implementation
    /// </summary>
    //public abstract class DTOBase : StorableBase, IDTO
    //{
    //    /// <summary>
    //    /// Constructor. Actual implementation of SetDefaultValues is
    //    /// deferred to sub-classes.
    //    /// </summary>
    //    protected DTOBase()
    //    {
    //        SetDefaultValues();
    //    }

    //    /// <summary>
    //    /// Creates a bit-wise identical copy of the DTO, i.e. a shallow copy.
    //    /// </summary>
    //    /// <returns>
    //    /// Shallow clone of object.
    //    /// </returns>
    //    public IDTO Clone()
    //    {
    //        return (MemberwiseClone() as IDTO);
    //    }

    //    #region Abstract methods to be implemented in sub-classes
    //    public abstract void SetDefaultValues();
    //    public abstract void SetValuesFromObject(Object obj); 
    //    #endregion
    //}
}