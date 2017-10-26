using System.Collections.Generic;
using System.Collections.ObjectModel;
using DataTransformation.Interfaces;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    /// <summary>
    /// Base class for ViewModel factory classes. The scope of the
    /// factory class is to create ViewModel objects (Item and Details),
    /// based on corresponding DTOs.
    /// </summary>
    public abstract class ViewModelFactoryBase<TDO> : IViewModelFactory
        where TDO : ITransformedData, new()
    {
        /// <summary>
        /// Specific details for how to create a Details ViewModel object
        /// based on a DTO is deferred to sub-classes.
        /// </summary>
        public abstract ITransformedDataWrapper CreateDetailsViewModel(ITransformedData obj);

        /// <summary>
        /// Specific details for how to create an Item ViewModel object
        /// based on a DTO is deferred to sub-classes.
        /// </summary>
        public abstract ITransformedDataWrapper CreateItemViewModel(ITransformedData obj);

        /// <summary>
        /// Use brand new DTO as source for creating a Details ViewModel object.
        /// </summary>
        public ITransformedDataWrapper CreateDetailsViewModelFromNewDTO()
        {
            return CreateDetailsViewModel(new TDO());
        }

        /// <summary>
        /// Use given DTO as source for creating a Details ViewModel object.
        /// Note that the given DTO is cloned, and the given DTO will thus
        /// not be referred to by the new Details ViewModel object.
        /// </summary>
        public ITransformedDataWrapper CreateDetailsViewModelFromClonedDTO(ITransformedData obj)
        {
            return CreateDetailsViewModel(obj.Clone());
        }

        /// <summary>
        /// Create a collection of Item ViewModel objects, from a
        /// collection of DTOs. 
        /// </summary>
        public virtual ObservableCollection<ITransformedDataWrapper> CreateItemViewModelCollection(IEnumerable<ITransformedData> dataObjects)
        {
            var itemViewModels = new ObservableCollection<ITransformedDataWrapper>();
            foreach (ITransformedData obj in dataObjects)
            {
                itemViewModels.Add(CreateItemViewModel(obj));
            }

            return itemViewModels;
        }
    }
}