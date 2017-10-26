using System;
using InMemoryStorage.Interfaces;

namespace DTO.Interfaces
{
    /// <summary>
    /// Interface for a DTO (Data Transfer Object) class.
    /// A DTO is supposed to be able to transport data from
    /// View and ViewModel layers to In-Memory storage
    /// collections, without knowledge about underlying 
    /// domain classes.
    /// </summary>
    //public interface IDTO : IStorable
    //{
    //    /// <summary>
    //    /// A subclass can assign default values to the DTO properties
    //    /// in this method. These values can e.g. be shown as initial
    //    /// values in GUI controls.
    //    /// </summary>
    //    void SetDefaultValues();

    //    /// <summary>
    //    /// The DTO properties can be assigned new values from a provided object.
    //    /// This object will typically an object of the underlying domain class.
    //    /// </summary>
    //    /// <param name="obj">
    //    /// Object to use for setting property values.
    //    /// </param>
    //    void SetValuesFromObject(Object obj);

    //    /// <summary>
    //    /// Create an identical copy of the object. It is up to the implementing
    //    /// class to decide if the copy is deep or shallow.
    //    /// </summary>
    //    /// <returns>
    //    /// Clone of object.
    //    /// </returns>
    //    IDTO Clone();
    //}
}