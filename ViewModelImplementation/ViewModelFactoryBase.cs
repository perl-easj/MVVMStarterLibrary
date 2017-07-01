using System.Collections.Generic;
using System.Collections.ObjectModel;
using DTO.Interfaces;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    /// <summary>
    /// Base class for ViewModel factory classes. The scope of the
    /// factory class is to create ViewModel objects (Item and Details),
    /// based on corresponding DTOs.
    /// </summary>
    /// <typeparam name="TDTO">
    /// Actual type of DTO
    /// </typeparam>
    public abstract class ViewModelFactoryBase<TDTO> : IViewModelFactory
        where TDTO : IDTO, new()
    {
        /// <summary>
        /// Specific details for how to create a Details ViewModel object
        /// based on a DTO is deferred to sub-classes.
        /// </summary>
        public abstract IDTOWrapper CreateDetailsViewModel(IDTO obj);

        /// <summary>
        /// Specific details for how to create an Item ViewModel object
        /// based on a DTO is deferred to sub-classes.
        /// </summary>
        public abstract IDTOWrapper CreateItemViewModel(IDTO obj);

        /// <summary>
        /// Use brand new DTO as source for creating a Details ViewModel object.
        /// </summary>
        public IDTOWrapper CreateDetailsViewModelFromNewDTO()
        {
            return CreateDetailsViewModel(new TDTO());
        }

        /// <summary>
        /// Use given DTO as source for creating a Details ViewModel object.
        /// Note that the given DTO is cloned, and the given DTO will thus
        /// not be referred to by the new Details ViewModel object.
        /// </summary>
        public IDTOWrapper CreateDetailsViewModelFromClonedDTO(IDTO obj)
        {
            return CreateDetailsViewModel(obj.Clone());
        }

        /// <summary>
        /// Create a collection of Item ViewModel objects, from a
        /// collection of DTOs. 
        /// </summary>
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