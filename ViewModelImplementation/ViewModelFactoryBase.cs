using System.Collections.Generic;
using System.Collections.ObjectModel;
using DataTransformation.Interfaces;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    /// <summary>
    /// Base class for ViewModel factory classes. 
    /// The scope of the factory class is to create 
    /// ViewModel objects (Item and Details), based on 
    /// corresponding transformed data objects.
    /// </summary>
    /// <typeparam name="TDO">Type of transformed data object</typeparam>
    public abstract class ViewModelFactoryBase<TDO> : IViewModelFactory
        where TDO : ITransformedData, new()
    {
        /// <summary>
        /// Specific details for how to create a Details 
        /// ViewModel object based on a transformed 
        /// data object is deferred to sub-classes.
        /// </summary>
        public abstract ITransformedDataWrapper CreateDetailsViewModel(ITransformedData obj);

        /// <summary>
        /// Specific details for how to create an 
        /// Item ViewModel object based on a transformed 
        /// data object is deferred to sub-classes.
        /// </summary>
        public abstract ITransformedDataWrapper CreateItemViewModel(ITransformedData obj);

        /// <summary>
        /// Use brand new transformed data object as 
        /// source for creating a Details ViewModel object.
        /// </summary>
        public ITransformedDataWrapper CreateDetailsViewModelFromNewTDO()
        {
            return CreateDetailsViewModel(new TDO());
        }

        /// <summary>
        /// Use given transformed data object as source 
        /// for creating a Details ViewModel object.
        /// Note that the given transformed data object 
        /// is cloned, and the given transformed data 
        /// object will thus not be referred to by the 
        /// new Details ViewModel object.
        /// </summary>
        public ITransformedDataWrapper CreateDetailsViewModelFromClonedTDO(ITransformedData obj)
        {
            return CreateDetailsViewModel(obj.Clone());
        }

        /// <summary>
        /// Create a collection of Item ViewModel objects, 
        /// from a collection of transformed data objects. 
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