using System;
using Persistency.Interfaces;

namespace Persistency.Implementation
{
    /// <summary>
    /// Implementation of the IPersistencyManager 
    /// interface as a Singleton
    /// </summary>
    public class PersistencyManager : IPersistencyManager
    {
        #region Singleton implementation
        private static IPersistencyManager _instance;
        public static IPersistencyManager Instance
        {
            get { return _instance ?? (_instance = new PersistencyManager()); }
        } 
        #endregion

        public event Action<bool> LoadDelegate;
        public event Action<bool> SaveDelegate;

        private PersistencyManager()
        {
            LoadDelegate = null;
            SaveDelegate = null;
        }

        /// <summary>
        /// Invoke all registered Load methods.
        /// </summary>
        public void LoadAll(bool suppressException = true)
        {
            LoadDelegate?.Invoke(suppressException);
        }

        /// <summary>
        /// Invoke all registered Save methods.
        /// </summary>
        public void SaveAll(bool suppressException = true)
        {
            SaveDelegate?.Invoke(suppressException);
        }

        /// <summary>
        /// Shorthand static method for invoking the LoadAll method
        /// </summary>
        public static void Load()
        {
            Instance.LoadAll();
        }

        /// <summary>
        /// Shorthand static method for invoking the SaveAll method
        /// </summary>
        public static void Save()
        {
            Instance.SaveAll();
        }
    }
}