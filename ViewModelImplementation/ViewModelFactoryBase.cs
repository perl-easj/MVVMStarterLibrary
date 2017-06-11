using System.Collections.ObjectModel;
using InMemoryStorage.Interfaces;
using ModelClass.Interfaces;

namespace ViewModel.Implementation
{
    public abstract class ViewModelFactoryBase<TDomainClass>
        where TDomainClass : class, IDomainClass, new()
    {
        public abstract IDomainObjectWrapper<TDomainClass> CreateDetailsViewModel(TDomainClass obj);
        public abstract IDomainObjectWrapper<TDomainClass> CreateItemViewModel(TDomainClass obj);

        public IDomainObjectWrapper<TDomainClass> CreateDetailsViewModelFromNew()
        {
            return CreateDetailsViewModel(new TDomainClass());
        }

        public IDomainObjectWrapper<TDomainClass> CreateDetailsViewModelFromClone(IDomainClass obj)
        {
            return CreateDetailsViewModel(obj.Clone() as TDomainClass);
        }

        public virtual ObservableCollection<IDomainObjectWrapper<TDomainClass>> CreateItemViewModelCollection(
            IInMemoryCollectionReadOnly<TDomainClass> collection)
        {
            var itemViewModels = new ObservableCollection<IDomainObjectWrapper<TDomainClass>>();
            foreach (TDomainClass obj in collection.All)
            {
                itemViewModels.Add(CreateItemViewModel(obj));
            }

            return itemViewModels;
        }
    }
}