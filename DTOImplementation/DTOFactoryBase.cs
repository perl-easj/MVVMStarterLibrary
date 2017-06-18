using DTO.Interfaces;

namespace DTO.Implementation
{
    public class DTOFactoryBase<T, TDTO> : IDTOFactory<T> 
        where TDTO : IDTO, new()
    {
        public IDTO Create(T obj)
        {
            IDTO dtoObj = new TDTO();
            dtoObj.SetValuesFromObject(obj);
            return dtoObj;
        }
    }
}