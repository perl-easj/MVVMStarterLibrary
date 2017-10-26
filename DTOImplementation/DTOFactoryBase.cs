

namespace DTO.Implementation
{
    /// <summary>
    /// Implementation of IDTOFactory. A DTO of type TDTO is produced.
    /// </summary>
    /// <typeparam name="T">
    /// Type of object used for setting values in new DTO. This will
    /// typically be a domain object.
    /// </typeparam>
    /// <typeparam name="TDTO">
    /// Actual type of DTO. The DTO class must declare a 
    /// parameterless constructor.
    /// </typeparam>
    //public abstract class DTOFactoryBase<T, TDTO> : IDTOFactory<T>
    //    where TDTO : IDTO, new()
    //{
    //    public IDTO CreateDTO(T obj)
    //    {
    //        if (obj == null)
    //        {
    //            return null;
    //        }

    //        IDTO dtoObj = new TDTO();
    //        dtoObj.SetValuesFromObject(obj);
    //        return dtoObj;
    //    }

    //    public abstract T CreateT(IDTO dtObj);
    //}
}