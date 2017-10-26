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
        /// Creates a filter collection with the given ID. 
        /// An exception is thrown if a collection with 
        /// the given ID already exists.
        /// </summary>
        /// <param name="filterCollId">
        /// Filter collection identifier
        /// </param>
        public void Add(string filterCollId)
        {
            if (_filterCollections.ContainsKey(filterCollId))
            {
                throw new ArgumentException(nameof(Add));
            }

            _filterCollections.Add(filterCollId, new FilterCollection<T>());
        }

        /// <summary>
        /// Removes the filter collection with the given ID.
        /// </summary>
        /// <param name="filterCollId">
        /// Filter collection identifier
        /// </param>
        public void Remove(string filterCollId)
        {
            _filterCollections.Remove(filterCollId);
        }

        /// <summary>
        /// Retrieves the filter collection with the given ID. 
        /// An exception is thrown if no collection with the 
        /// given ID exists.
        /// </summary>
        /// <param name="filterCollId">
        /// Filter collection identifier
        /// </param>
        public IFilterCollection<T> Get(string filterCollId)
        {
            if (!_filterCollections.ContainsKey(filterCollId))
            {
                throw new ArgumentException(nameof(Get));
            }

            return _filterCollections[filterCollId];
        }
    }
}