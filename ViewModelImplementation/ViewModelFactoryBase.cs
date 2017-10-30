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
    public abstract class ViewModelFactoryBase<T, TVMO> : IViewModelFactory<TVMO> 
        where TVMO : class, ITransformed<T>, new()
    {
        /// <summary>
        /// Specific details for how to create a Details 
        /// ViewModel object based on a transformed 
        /// data object is deferred to sub-classes.
        /// </summary>
        public abstract IDataWrapper<TVMO> CreateDetailsViewModel(TVMO obj);

        /// <summary>
        /// Specific details for how to create an 
        /// Item ViewModel object based on a transformed 
        /// data object is deferred to sub-classes.
        /// </summary>
        public abstract IDataWrapper<TVMO> CreateItemViewModel(TVMO obj);

        /// <summary>
        /// Use brand new transformed data object as 
        /// source for creating a Details ViewModel object.
        /// </summary>
        public IDataWrapper<TVMO> CreateDetailsViewModelFromNewVMO()
        {
            return CreateDetailsViewModel(new TVMO());
        }

        /// <summary>
        /// Use given transformed data object as source 
        /// for creating a Details ViewModel object.
        /// Note that the given transformed data object 
        /// is cloned, and the given transformed data 
        /// object will thus not be referred to by the 
        /// new Details ViewModel object.
        /// </summary>
        public IDataWrapper<TVMO> CreateDetailsViewModelFromClonedVMO(TVMO obj)
        {
            return CreateDetailsViewModel(obj.Clone() as TVMO);
        }

        /// <summary>
        /// Create a collection of Item ViewModel objects, 
        /// from a collection of transformed data objects. 
        /// </summary>
        public virtual ObservableCollection<IDataWrapper<TVMO>> CreateItemViewModelCollection(IEnumerable<TVMO> dataObjects)
        {
            var itemViewModels = new ObservableCollection<IDataWrapper<TVMO>>();
            foreach (TVMO obj in dataObjects)
            {
                itemViewModels.Add(CreateItemViewModel(obj));
            }

            return itemViewModels;
        }
    }
}