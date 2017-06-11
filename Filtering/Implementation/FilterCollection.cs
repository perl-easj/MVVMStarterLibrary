using System;
using System.Collections.Generic;
using Filtering.Interfaces;

namespace Filtering.Implementation
{
    public class FilterCollection<T> : IFilterCollection<T>
    {
        private Dictionary<string, IFilter<T>> _filters;

        public FilterCollection()
        {
            _filters = new Dictionary<string, IFilter<T>>();
        }

        public void AddFilter(IFilter<T> filter)
        {
            if (filter == null || _filters.ContainsKey(filter.ID))
            {
                throw new ArgumentException(nameof(AddFilter));
            }

            _filters.Add(filter.ID, filter);
        }

        public void RemoveFilter(string filterID)
        {
            _filters.Remove(filterID);
        }

        public IFilter<T> GetFilter(string filterID)
        {
            if (!_filters.ContainsKey(filterID))
            {
                throw new ArgumentException(nameof(GetFilter));
            }

            return _filters[filterID];
        }

        public List<T> FilterList(List<T> list)
        {
            return list.FindAll(SatisfiesAll);
        }

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