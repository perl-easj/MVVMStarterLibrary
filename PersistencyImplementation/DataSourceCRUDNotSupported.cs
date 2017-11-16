using System.Threading.Tasks;
using Persistency.Interfaces;

namespace Persistency.Implementation
{
    /// <summary>
    /// This class represents a collective "Not Supported" 
    /// strategy object for all CRUD operations.
    /// </summary>
    public class DataSourceCRUDNotSupported<TDTO> : IDataSourceCRUD<TDTO>
    {
        public Task<int> Create(TDTO obj)
        {
            throw new DataSourceOperationNotSupportedException("Create");
        }

        public Task<TDTO> Read(int key)
        {
            throw new DataSourceOperationNotSupportedException("Read");
        }

        public Task Update(int key, TDTO obj)
        {
            throw new DataSourceOperationNotSupportedException("Update");
        }

        public Task Delete(int key)
        {
            throw new DataSourceOperationNotSupportedException("Delete");
        }
    }
}