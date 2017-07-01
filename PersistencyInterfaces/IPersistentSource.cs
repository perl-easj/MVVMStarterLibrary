using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistency.Interfaces
{
    /// <summary>
    /// Interface for any persistent source from/to which a 
    /// List of objects can be loaded/saved. The interface
    /// allows for an asynchronous implementation.
    /// </summary>
    /// <typeparam name="T">
    /// Type of loaded/saved objects.
    /// </typeparam>
    public interface IPersistentSource<T>
    {
        /// <summary>
        /// Save the given List of objects to the source
        /// </summary>
        /// <param name="objects">
        /// List of objects to save
        /// </param>
        void Save(List<T> objects);

        /// <summary>
        /// Load a List of objects from the source.
        /// </summary>
        /// <returns>
        /// List of loaded objects, wrapped in an awaitable Task object.
        /// </returns>
        Task<List<T>> Load();
    }
}