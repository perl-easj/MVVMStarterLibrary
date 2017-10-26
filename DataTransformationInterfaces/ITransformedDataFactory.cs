namespace DataTransformation.Interfaces
{
    /// <summary>
    /// Interface for a factory class, able to produce a
    /// transformed data object from a domain data object,
    /// and vice versa.
    /// </summary>
    /// <typeparam name="T">Type for original domain data</typeparam>
    public interface ITransformedDataFactory<T>
    {
        /// <summary>
        /// Create a transformed data object from an
        /// original domain data object. 
        /// </summary>
        ITransformedData CreateTransformedObject(T obj);

        /// <summary>
        /// Create a domain data object from a transformed
        /// data object.
        /// </summary>
        T CreateDomainObject(ITransformedData tObj);
    }
}