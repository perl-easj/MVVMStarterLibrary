using System.Collections.Generic;

namespace DataTransformation.Interfaces
{
    /// <summary>
    /// Interface for a collection of transformed data objects.
    /// Note that the class implementing the interface does not
    /// need to physically stored transformed objects; it only
    /// needs to be able to provide transformed objects when
    /// required, e.g. by using a factory class
    /// </summary>
    public interface ITransformedDataCollection
    {
        /// <summary>
        /// Returns all transformed data objects
        /// </summary>
        List<ITransformedData> AllTransformed { get; }

        /// <summary>
        /// Returns the transformed data object with the specified key (if any)
        /// </summary>
        ITransformedData ReadTransformed(int key);

        /// <summary>
        /// Deletes the transformed data object with the specified key (if any)
        /// </summary>
        void DeleteTransformed(int key);

        /// <summary>
        /// Inserts the given transformed data object into the collection
        /// </summary>
        void InsertTransformed(ITransformedData obj, bool replaceKey = true);
    }
}