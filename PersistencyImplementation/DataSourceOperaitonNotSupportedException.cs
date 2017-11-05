using System;

namespace Persistency.Implementation
{
    public class DataSourceOperationNotSupportedException : NotSupportedException
    {
        public DataSourceOperationNotSupportedException(string operation)
            : base($"{operation} is not supported by this data source")
        {
        }
    }
}