using System;

namespace Persistency.Interfaces
{
    public interface IPersistencyManager
    {
        event Action LoadDelegate;
        event Action SaveDelegate;
        void LoadAll();
        void SaveAll();
    }
}