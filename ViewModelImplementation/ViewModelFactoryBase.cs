using System.Collections.Generic;
using System.Collections.ObjectModel;
using DataClass.Interfaces;

namespace ViewModel.Implementation
{
    public abstract class ViewModelFactoryBase<T> 
        where T : new()
    {
        public abstract IDTOWrapper<T> CreateDetailsViewModel(T obj);
        public abstract IDTOWrapper<T> CreateItemViewModel(T obj);

        public IDTOWrapper<T> CreateDetailsViewModelFromNew()
        {
            return CreateDetailsViewModel(new T());
        }

        public IDTOWrapper<T> CreateDetailsViewModelFromExisting(T obj)
        {
            return CreateDetailsViewModel(obj);
        }

        public virtual ObservableCollection<IDTOWrapper<T>> CreateItemViewModelCollection(IEnumerable<T> dataObjects)
        {
            var itemViewModels = new ObservableCollection<IDTOWrapper<T>>();
            foreach (T obj in dataObjects)
            {
                itemViewModels.Add(CreateItemViewModel(obj));
            }

            return itemViewModels;
        }
    }
}