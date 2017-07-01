using System;
using System.Collections.Generic;
using Filtering.Interfaces;

namespace Filtering.Implementation
{
    /// <summary>
    /// Implementation of the IFilterService interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FilterService<T> : IFilterService<T>
    {
        private Dictionary<string, IFilterCollection<T>> _filterCollections;

        public FilterService()
        {
            _filterCollections = new Dictionary<string, IFilterCollection<T>>();
        }

        /// <summary>
        /// Create a filter collection with the given ID. An exception is
        /// thrown if a collection with the given ID already exists.
        /// </summary>
        /// <param name="filterCollId">
        /// Filter collection identifier
        /// </param>
        public void CreateFilterCollection(string filterCollId)
        {
            if (_filterCollections.ContainsKey(filterCollId))
            {
                throw new ArgumentException(nameof(CreateFilterCollection));
            }

            _filterCollections.Add(filterCollId, new FilterCollection<T>());
        }

        /// <summary>
        /// Remove the filter collection with the given ID.
        /// </summary>
        /// <param name="filterCollId">
        /// Filter collection identifier
        /// </param>
        public void RemoveFilterCollection(string filterCollId)
        {
            _filterCollections.Remove(filterCollId);
        }

        /// <summary>
        /// Retrieve the filter collection with the given ID. An exception is
        /// thrown if no collection with the given ID exists.
        /// </summary>
        /// <param name="filterCollId">
        /// Filter collection identifier
        /// </param>
        public IFilterCollection<T> GetFilterCollection(string filterCollId)
        {
            if (!_filterCollections.ContainsKey(filterCollId))
            {
                throw new ArgumentException(nameof(GetFilterCollection));
            }

            return _filterCollections[filterCollId];
        }
    }
}