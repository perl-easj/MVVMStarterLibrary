using System;
using System.Collections.Generic;
using Filtering.Interfaces;

namespace Filtering.Implementation
{
    public class FilterService<T> : IFilterService<T>
    {
        private Dictionary<string, IFilterCollection<T>> _filterCollections;

        public FilterService()
        {
            _filterCollections = new Dictionary<string, IFilterCollection<T>>();
        }

        public IFilterCollection<T> CreateFilterCollection(string filterCollId)
        {
            if (_filterCollections.ContainsKey(filterCollId))
            {
                throw new ArgumentException(nameof(CreateFilterCollection));
            }

            _filterCollections.Add(filterCollId, new FilterCollection<T>());
            return _filterCollections[filterCollId];
        }

        public void RemoveFilterCollection(string filterCollId)
        {
            _filterCollections.Remove(filterCollId);
        }

        public IFilterCollection<T> GetFilterCollection(string filterCollId)
        {
            if (!_filterCollections.ContainsKey(filterCollId))
            {
                throw new ArgumentException(nameof(CreateFilterCollection));
            }

            return _filterCollections[filterCollId];
        }
    }
}