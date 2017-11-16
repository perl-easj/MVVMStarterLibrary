using System.Collections.Generic;
using System.Threading.Tasks;
using Persistency.Interfaces;

namespace Persistency.Implementation
{
    /// <summary>
    /// This class represents a "Not Supported" 
    /// strategy object for the Load operation.
    /// </summary>
    public class DataSourceLoadNotSupported<TDTO> : IDataSourceLoad<TDTO>
    {
        public Task<List<TDTO>> Load()
        {
            throw new DataSourceOperationNotSupportedException("Load");
        }
    }
}