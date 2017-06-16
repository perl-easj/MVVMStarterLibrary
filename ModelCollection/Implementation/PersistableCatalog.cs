using System;
using System.Collections.Generic;
using InMemoryStorage.Implementation;
using InMemoryStorage.Interfaces;
using ModelCollection.Interfaces;
using Persistency.Implementation;
using Persistency.Interfaces;

namespace ModelCollection.Implementation
{
    public abstract class PersistableCatalog<TDO> : 
        ObservableInMemoryCollection<TDO>, 
        IPersistable
        where TDO : class, IStorable
    {
        private IPersistentSource<TDO> _source;

        protected PersistableCatalog(IPersistentSource<TDO> source)
        {
            _source = source;

            PersistencyManager.Instance.LoadDelegate += Load;
            PersistencyManager.Instance.SaveDelegate += Save;
        }

        protected PersistableCatalog(Action loadAction, Action saveAction, IPersistentSource<TDO> source)
        {
            _source = source;

            loadAction();
            saveAction();
        }

        /// <summary>
        /// Loads storable objects from the source.
        /// NB: Note that exceptions are catched and ignored
        /// </summary>
        public async void Load()
        {
            try
            {
                if (_source != null)
                {
                    List<TDO> objects = await _source.Load();
                    InsertAll(objects, false);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Saves storable objects back to the source.
        /// </summary>
        public void Save()
        {
            _source?.Save(All);
        }
    }
}