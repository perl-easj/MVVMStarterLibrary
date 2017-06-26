using System;
using Persistency.Interfaces;

namespace Persistency.Implementation
{
    public class PersistencyManager : IPersistencyManager
    {
        private static IPersistencyManager _instance;
        public static IPersistencyManager Instance
        {
            get { return _instance ?? (_instance = new PersistencyManager()); }
        }

        public event Action LoadDelegate;
        public event Action SaveDelegate;

        private PersistencyManager()
        {
            LoadDelegate = null;
            SaveDelegate = null;
        }

        public void LoadAll()
        {
            LoadDelegate?.Invoke();
        }

        public void SaveAll()
        {
            SaveDelegate?.Invoke();
        }

        public static void Load()
        {
            Instance.LoadAll();
        }

        public static void Save()
        {
            Instance.SaveAll();
        }
    }
}