using System.Collections.Generic;
using System.Collections.ObjectModel;
using DTO.Interfaces;

namespace ViewModel.Implementation
{
    public abstract class ViewModelFactoryBase<TDTO> 
        where TDTO : IDTO, new()
    {
        public abstract IDTOWrapper CreateDetailsViewModel(IDTO obj);
        public abstract IDTOWrapper CreateItemViewModel(IDTO obj);

        public IDTOWrapper CreateDetailsViewModelFromNew()
        {
            return CreateDetailsViewModel(new TDTO());
        }

        public IDTOWrapper CreateDetailsViewModelFromExisting(IDTO obj)
        {
            return CreateDetailsViewModel(obj);
        }

        public virtual ObservableCollection<IDTOWrapper> CreateItemViewModelCollection(IEnumerable<IDTO> dataObjects)
        {
            var itemViewModels = new ObservableCollection<IDTOWrapper>();
            foreach (IDTO obj in dataObjects)
            {
                itemViewModels.Add(CreateItemViewModel(obj));
            }

            return itemViewModels;
        }
    }
}