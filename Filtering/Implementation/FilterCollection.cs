using System;
using System.Collections.Generic;
using Filtering.Interfaces;

namespace Filtering.Implementation
{
    /// <summary>
    /// Implementation of the IFilterCollection interface.
    /// </summary>
    /// <typeparam name="T">
    /// Type of object to which the filters are applied.
    /// </typeparam>
    public class FilterCollection<T> : IFilterCollection<T>
    {
        private Dictionary<string, IFilter<T>> _filters;

        public FilterCollection()
        {
            _filters = new Dictionary<string, IFilter<T>>();
        }

        /// <summary>
        /// Add a single filter to the filter collection. 
        /// An exception is thrown if the filter is empty 
        /// or already exists.
        /// </summary>
        /// <param name="filter">
        /// Filter to add.
        /// </param>
        public void Add(IFilter<T> filter)
        {
            if (filter == null || _filters.ContainsKey(filter.ID))
            {
                throw new ArgumentException(nameof(Add));
            }

            _filters.Add(filter.ID, filter);
        }

        /// <summary>
        /// Removes a single filter from the collection.
        /// </summary>
        /// <param name="filterID">
        /// Identifier for filter to remove.
        /// </param>
        public void Remove(string filterID)
        {
            _filters.Remove(filterID);
        }

        /// <summary>
        /// Retrieve the filter matching the given ID. 
        /// An exception is thrown if no filter matches the ID.
        /// </summary>
        /// <param name="filterID">
        /// ID for filter to retrieve
        /// </param>
        /// <returns>
        /// Filter matching the given ID.
        /// </returns>
        public IFilter<T> Get(string filterID)
        {
            if (!_filters.ContainsKey(filterID))
            {
                throw new ArgumentException(nameof(Get));
            }

            return _filters[filterID];
        }

        /// <summary>
        /// Apply the filters to all elements in the given list.
        /// Only the objects passing through all the filters are returned.
        /// </summary>
        /// <param name="list">
        /// List of objects to apply the filters to.
        /// </param>
        /// <returns>
        /// List of objects that passed all filters.
        /// </returns>
        public List<T> Apply(List<T> list)
        {
            return list.FindAll(SatisfiesAll);
        }

        /// <summary>
        /// Performs the actual filtering of a single object.
        /// </summary>
        /// <param name="obj">
        /// Object to apply the filters to.
        /// </param>
        /// <returns>
        /// True if object passed all filters, otherwise false.
        /// </returns>
        private bool SatisfiesAll(T obj)
        {
            foreach (IFilter<T> filter in _filters.Values)
            {
                if (!filter.Condition(obj)) return false;
            }

            return true;
        }
    }
}