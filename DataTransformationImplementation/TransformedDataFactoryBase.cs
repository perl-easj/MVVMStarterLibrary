using DataTransformation.Interfaces;

namespace DataTransformation.Implementation
{
    public abstract class TransformedDataFactoryBase<T, TTDO> : ITransformedDataFactory<T>
        where TTDO : ITransformedData, new()
    {
        public ITransformedData CreateTransformedObject(T obj)
        {
            if (obj == null)
            {
                return null;
            }

            ITransformedData dtoObj = new TTDO();
            dtoObj.SetValuesFromObject(obj);
            return dtoObj;
        }

        public abstract T CreateDomainObject(ITransformedData tObj);
    }
}