using System.Collections.Generic;
using System.Threading.Tasks;
using Persistency.Interfaces;

namespace Persistency.Implementation
{
    public class ConfiguredPersistentSource<TDTO> : IPersistentSource<TDTO>
    {
        private IDataSourceCRUD<TDTO> _dataSourceCRUD;
        private IDataSourceLoad<TDTO> _dataSourceLoad;
        private IDataSourceSave<TDTO> _dataSourceSave;

        public IDataSourceCRUD<TDTO> DataSourceCRUD
        {
            get { return _dataSourceCRUD; }
            protected set { _dataSourceCRUD = value; }
        }

        public IDataSourceLoad<TDTO> DataSourceLoad
        {
            get { return _dataSourceLoad; }
            protected set { _dataSourceLoad = value; }
        }

        public IDataSourceSave<TDTO> DataSourceSave
        {
            get { return _dataSourceSave; }
            protected set { _dataSourceSave = value; }
        }

        public Task Save(List<TDTO> objects)
        {
            return _dataSourceSave.Save(objects);
        }

        public Task<List<TDTO>> Load()
        {
            return _dataSourceLoad.Load();
        }

        public Task Create(TDTO obj)
        {
            return _dataSourceCRUD.Create(obj);
        }

        public Task<TDTO> Read(int key)
        {
            return _dataSourceCRUD.Read(key);
        }

        public Task Update(int key, TDTO obj)
        {
            return _dataSourceCRUD.Update(key, obj);
        }

        public Task Delete(int key)
        {
            return _dataSourceCRUD.Delete(key);
        }
    }
}