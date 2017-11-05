using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistency.Interfaces;

namespace Persistency.Implementation
{
    public class DataSourceLoadNotSupported<TDTO> : IDataSourceLoad<TDTO>
    {
        public Task<List<TDTO>> Load()
        {
            throw new NotSupportedException("Load");
        }
    }
}