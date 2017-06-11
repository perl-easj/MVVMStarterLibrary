using Persistency.Interfaces;
using Persistency.Types;

namespace Persistency.Implementation
{
    public class PersistencyManager : IPersistencyManager
    {
        private static IPersistencyManager _instance;
        public static IPersistencyManager Instance
        {
            get { return _instance ?? (_instance = new PersistencyManager()); }
        }

        public event PersistencyTypes.SourceDelegate LoadDelegate;
        public event PersistencyTypes.SourceDelegate SaveDelegate;

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