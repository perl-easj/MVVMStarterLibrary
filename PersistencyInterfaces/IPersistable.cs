namespace Persistency.Interfaces
{
    /// <summary>
    /// Interface for any entity that is persistable, i.e. can be
    /// saved to and loaded from persistent storage.
    /// </summary>
    public interface IPersistable<in T>
    {
        void Load(bool suppressException = true);
        void Save(bool suppressException = true);
    }
}