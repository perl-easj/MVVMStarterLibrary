using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistency.Interfaces
{
    /// <summary>
    /// Interface for any persistent source 
    /// from/to which a  List of objects can be 
    /// loaded/saved. The interface allows 
    /// for an asynchronous implementation.
    /// </summary>
    /// <typeparam name="TDTO">
    /// Type of loaded/saved objects.
    /// </typeparam>
    public interface IPersistentSource<TDTO>
    {
        /// <summary>
        /// Save the given List of objects to the source
        /// </summary>
        /// <param name="objects">
        /// List of objects to save
        /// </param>
        Task Save(List<TDTO> objects);

        /// <summary>
        /// Load a List of objects from the source.
        /// </summary>
        /// <returns>
        /// List of loaded objects, wrapped in an awaitable Task object.
        /// </returns>
        Task<List<TDTO>> Load();

        /// <summary>
        /// Create the given object in the persistent source
        /// </summary>
        /// <param name="obj">Object to create</param>
        /// <returns>The resulting key for the created object.</returns>
        Task<int> Create(TDTO obj);

        /// <summary>
        /// Reads a single object from the source,
        /// i.e. the object matching the given key.
        /// </summary>
        /// <param name="key">Key for object to read</param>
        /// <returns>
        /// Object matching the provided key, wrapped 
        /// in an awaitable Task object.
        /// </returns>
        Task<TDTO> Read(int key);

        /// <summary>
        /// Updates a single object in the source,
        /// i.e. the object matching the given key.
        /// </summary>
        /// <param name="key">Key for object to update</param>
        /// <param name="obj">Object to update</param>
        Task Update(int key, TDTO obj);

        /// <summary>
        /// Deletes a single object from the source,
        /// i.e. the object matching the given key.
        /// </summary>
        /// <param name="key">Key for object to delete</param>
        Task Delete(int key);
    }
}