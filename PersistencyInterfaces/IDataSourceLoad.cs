using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistency.Interfaces
{
    public interface IDataSourceLoad<TDTO>
    {
        /// <summary>
        /// Load a List of objects from the source.
        /// </summary>
        /// <returns>
        /// List of loaded objects, wrapped in an awaitable Task object.
        /// </returns>
        Task<List<TDTO>> Load();
    }
}