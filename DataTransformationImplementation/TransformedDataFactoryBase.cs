using DataTransformation.Interfaces;

namespace DataTransformation.Implementation
{
    /// <summary>
    /// Base class for a factory class, able to produce a
    /// transformed data object from a domain data object,
    /// and vice versa.
    /// </summary>
    /// <typeparam name="T">Type of original domain data</typeparam>
    /// <typeparam name="TTDO">Type of transformed data</typeparam>
    public abstract class TransformedDataFactoryBase<T, TTDO> : ITransformedDataFactory<T>
        where TTDO : ITransformedData, new()
    {
        /// <summary>
        /// Create a transformed data object from the
        /// given domain data object
        /// </summary>
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

        /// <summary>
        /// Creation of a domain object from a transformed data 
        /// object is deferred to type-specific factory classes.
        /// </summary>
        public abstract T CreateDomainObject(ITransformedData tObj);
    }
}