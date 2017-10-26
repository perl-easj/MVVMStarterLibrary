namespace Persistency.Interfaces
{
    /// <summary>
    /// Interface for any entity that is 
    /// persistable, i.e. can be saved to 
    /// and loaded from persistent storage.
    /// </summary>
    public interface IPersistable
    {
        /// <summary>
        /// Invoke a Load operation on the entity,
        /// meaning that all existing items in the
        /// entity are replaced with the loaded items.
        /// </summary>
        void Load(bool suppressException = true);

        /// <summary>
        /// Invoke a Save operation on the entity,
        /// meaning that all existing items in the
        /// entity are saved to persistent storage,
        /// thereby replacing the items present in
        /// persistent storage.
        /// </summary>
        void Save(bool suppressException = true);
    }
}