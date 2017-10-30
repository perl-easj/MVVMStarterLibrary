using System.Collections.Generic;

namespace DataTransformation.Interfaces
{
    /// <summary>
    /// Interface for a factory class, able to produce a
    /// transformed data object from a domain data object,
    /// and vice versa.
    /// </summary>
    /// <typeparam name="T">Type for original domain object</typeparam>
    /// <typeparam name="TTDO">Type for transformed object</typeparam>
    public interface IFactory<T, TTDO>
    {
        /// <summary>
        /// Create a transformed data object from an
        /// original domain data object. 
        /// </summary>
        TTDO CreateTransformedObject(T obj);

        /// <summary>
        /// Create a domain data object from a transformed
        /// data object.
        /// </summary>
        T CreateDomainObject(TTDO tObj);

        List<TTDO> CreateTransformedObjects(List<T> objects);
        List<T> CreateDomainObjects(List<TTDO> tObjects);
    }
}