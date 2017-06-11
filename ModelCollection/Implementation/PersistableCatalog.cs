using System;
using System.Collections.Generic;
using InMemoryStorage.Implementation;
using InMemoryStorage.Interfaces;
using ModelCollection.Interfaces;
using Persistency.Implementation;
using Persistency.Interfaces;

namespace ModelCollection.Implementation
{
    public class PersistableCatalog<T> : ObservableInMemoryCollection<T>, IPersistable
        where T : class, IStorable
    {
        private IPersistentSource<T> _source;

        public PersistableCatalog(IPersistentSource<T> source)
        {
            _source = source;

            PersistencyManager.Instance.LoadDelegate += Load;
            PersistencyManager.Instance.SaveDelegate += Save;
        }

        public PersistableCatalog(Action loadAction, Action saveAction, IPersistentSource<T> source)
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
                    List<T> objects = await _source.Load();
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