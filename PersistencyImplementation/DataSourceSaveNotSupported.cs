using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistency.Interfaces;

namespace Persistency.Implementation
{
    public class DataSourceSaveNotSupported<TDTO> : IDataSourceSave<TDTO>
    {
        public Task Save(List<TDTO> objects)
        {
            throw new NotSupportedException("Save");
        }
    }
}