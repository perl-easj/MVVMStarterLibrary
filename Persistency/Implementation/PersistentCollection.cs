using System;
using System.Collections.Generic;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;

namespace Persistency.Implementation
{
    public abstract class PersistentCollection<T> : ICollectionAggregate<T>
    {
        #region Instance fields and events
        private IPersistentSource<T> _source;
        private IInMemoryCollection<T> _collection;
        private IDTOFactory<T> _dtoFactory;

        public event EventHandler OnObjectCreated;
        public event EventHandler OnObjectUpdated;
        public event EventHandler OnObjectDeleted;
        #endregion

        #region Constructor
        protected PersistentCollection(IPersistentSource<T> source, IInMemoryCollection<T> collection, IDTOFactory<T> dtoFactory = null)
        {
            if (source == null || collection == null)
            {
                throw new ArgumentException(nameof(PersistentCollection<T>));
            }

            _source = source;
            _collection = collection;
            _dtoFactory = dtoFactory;

            SetCallbacks(AfterObjectCreated, AfterObjectUpdated, AfterObjectDeleted);
            Manage();
        }
        #endregion

        #region IInMemoryCollection implementation
        public List<T> All
        {
            get { return _collection.All; }
        }

        public T Read(int key)
        {
            return _collection.Read(key);
        }

        public T this[int key]
        {
            get { return _collection[key]; }
        }

        public void Insert(T obj, bool replaceKey = true)
        {
            _collection.Insert(obj, replaceKey);
        }

        public void InsertAll(List<T> objects, bool replaceKey = true)
        {
            _collection.InsertAll(objects, replaceKey);
        }

        public void Delete(int key)
        {
            _collection.Delete(key);
        }

        public void DeleteAll()
        {
            _collection.DeleteAll();
        }

        public void SetCallbacks(Action afterCreate, Action afterUpdate, Action afterDelete)
        {
            _collection.SetCallbacks(afterCreate, afterUpdate, afterDelete);
        }
        #endregion

        #region IPersistable implementation
        /// <summary>
        /// Loads storable objects from the source.
        /// </summary>
        public async void Load()
        {
            if (_source != null)
            {
                List<T> objects = await _source.Load();
                _collection.InsertAll(objects, false);
            }
        }

        /// <summary>
        /// Saves storable objects back to the source.
        /// </summary>
        public void Save()
        {
            _source?.Save(_collection.All);
        }
        #endregion

        #region IObservable implementation
        public void AfterObjectCreated()
        {
            OnObjectCreated?.Invoke(this, EventArgs.Empty);
        }

        public void AfterObjectDeleted()
        {
            OnObjectUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void AfterObjectUpdated()
        {
            OnObjectDeleted?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region IConvertibleCollection implementation
        public List<IDTO> AllDTO
        {
            get
            {
                if (_dtoFactory == null)
                {
                    throw new ArgumentException(nameof(AllDTO));    
                }

                List<IDTO> dtoCollection = new List<IDTO>();
                foreach (T obj in All)
                {
                    IDTO cObj = _dtoFactory.Create(obj);
                    cObj.SetValuesFromObject(obj);
                    dtoCollection.Add(cObj);
                }

                return dtoCollection;
            }
        }

        public IDTO ReadDTO(int key)
        {
            if (_dtoFactory == null)
            {
                throw new ArgumentException(nameof(ReadDTO));
            }

            T obj = Read(key);
            if (obj == null)
            {
                return null;
            }

            IDTO cObj = _dtoFactory.Create(obj);
            cObj.SetValuesFromObject(obj);
            return cObj;
        }

        public void DeleteDTO(int key)
        {
            if (_dtoFactory == null)
            {
                throw new ArgumentException(nameof(ReadDTO));
            }

            Delete(key);
        }
        #endregion

        #region IManaged implementation
        public virtual void Manage()
        {
            PersistencyManager.Instance.LoadDelegate += Load;
            PersistencyManager.Instance.SaveDelegate += Save;
        } 
        #endregion

        public abstract void InsertDTO(IDTO obj, bool replaceKey = true);
    }
}