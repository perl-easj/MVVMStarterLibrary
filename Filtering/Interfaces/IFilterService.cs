namespace Filtering.Interfaces
{
    public interface IFilterService<T>
    {
        IFilterCollection<T> CreateFilterCollection(string filterCollId);
        void RemoveFilterCollection(string filterCollId);
        IFilterCollection<T> GetFilterCollection(string filterCollId);
    }
}