using System.Collections.Generic;

namespace Filtering.Interfaces
{
    public interface IFilterCollection<T>
    {
        void AddFilter(IFilter<T> filter);

        void RemoveFilter(string filterID);

        IFilter<T> GetFilter(string filterID);

        List<T> FilterList(List<T> list);
    }
}