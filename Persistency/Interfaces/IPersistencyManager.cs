using Persistency.Types;

namespace Persistency.Interfaces
{
    public interface IPersistencyManager
    {
        event PersistencyTypes.SourceDelegate LoadDelegate;
        event PersistencyTypes.SourceDelegate SaveDelegate;
        void LoadAll();
        void SaveAll();
    }
}