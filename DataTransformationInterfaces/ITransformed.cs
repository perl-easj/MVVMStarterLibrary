using InMemoryStorage.Interfaces;

namespace DataTransformation.Interfaces
{
    public interface ITransformed<in T> : IStorable
    {
        /// <summary>
        /// Create an identical copy of the object. 
        /// It is up to the implementing class to 
        /// decide if the copy is deep or shallow.
        /// </summary>
        /// <returns>
        /// Clone of object.
        /// </returns>
        ITransformed<T> Clone();

        /// <summary>
        /// The properties of the transformed data can 
        /// be assigned new values from a provided object.
        /// This object will typically be an object of the 
        /// original domain class.
        /// </summary>
        /// <param name="obj">
        /// Object to use for setting property values.
        /// </param>
        void SetValuesFromObject(T obj);
    }
}