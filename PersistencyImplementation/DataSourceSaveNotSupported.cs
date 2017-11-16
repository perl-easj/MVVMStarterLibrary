using System.Collections.Generic;
using System.Threading.Tasks;
using Persistency.Interfaces;

namespace Persistency.Implementation
{
    /// <summary>
    /// This class represents a "Not Supported" 
    /// strategy object for the Save operation.
    /// </summary>
    public class DataSourceSaveNotSupported<TDTO> : IDataSourceSave<TDTO>
    {
        public Task Save(List<TDTO> objects)
        {
            throw new DataSourceOperationNotSupportedException("Save");
        }
    }
}