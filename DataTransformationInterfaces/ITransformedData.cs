using System;
using InMemoryStorage.Interfaces;

namespace DataTransformation.Interfaces
{
    /// <summary>
    /// Interface for classes representing a 
    /// transformation of original domain data
    /// </summary>
    public interface ITransformedData : IStorable
    {
        /// <summary>
        /// A subclass can assign default values to
        /// the properties of the transformed data.
        /// These values can e.g. be shown as initial
        /// values in GUI controls.
        /// </summary>
        void SetDefaultValues();

        /// <summary>
        /// The properties of the transformed data can 
        /// be assigned new values from a provided object.
        /// This object will typically be an object of the 
        /// original domain class.
        /// </summary>
        /// <param name="obj">
        /// Object to use for setting property values.
        /// </param>
        void SetValuesFromObject(Object obj);

        /// <summary>
        /// Create an identical copy of the object. 
        /// It is up to the implementing class to 
        /// decide if the copy is deep or shallow.
        /// </summary>
        /// <returns>
        /// Clone of object.
        /// </returns>
        ITransformedData Clone();
    }
}