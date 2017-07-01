namespace Filtering.Interfaces
{
    /// <summary>
    /// Interface for a filtering service. Note that an instance
    /// of the service only applies to objects of type T.
    /// </summary>
    /// <typeparam name="T">
    /// Type of objects to which the service applies.
    /// </typeparam>
    public interface IFilterService<T>
    {
        /// <summary>
        /// Create a filter collection with the given ID.
        /// </summary>
        /// <param name="filterCollId">
        /// Filter collection identifier
        /// </param>
        void CreateFilterCollection(string filterCollId);

        /// <summary>
        /// Remove the filter collection with the given ID.
        /// </summary>
        /// <param name="filterCollId">
        /// Filter collection identifier
        /// </param>
        void RemoveFilterCollection(string filterCollId);

        /// <summary>
        /// Retrieve the filter collection with the given ID.
        /// </summary>
        /// <param name="filterCollId">
        /// Filter collection identifier
        /// </param>
        IFilterCollection<T> GetFilterCollection(string filterCollId);
    }
}