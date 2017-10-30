using System.Collections.Generic;
using System.Collections.ObjectModel;
using DataTransformation.Interfaces;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// Interface for a factory for creation of 
    /// Details and Item ViewModel objects, based 
    /// on a VMO. The ViewModel object may refer 
    /// to a brand new object, an existing object, 
    /// or a clone of an existing object.
    /// </summary>
    public interface IViewModelFactory<TVMO>
    {
        /// <summary>
        /// Create Details ViewModel object, referring to the given VMO.
        /// </summary>
        /// <param name="obj">
        /// VMO to which the Details ViewModel object will refer.
        /// </param>
        /// <returns>
        /// New Details ViewModel object.
        /// </returns>
        IDataWrapper<TVMO> CreateDetailsViewModel(TVMO obj);

        /// <summary>
        /// Create Item ViewModel object, referring to the given VMO.
        /// </summary>
        /// <param name="obj">
        /// VMO to which the Item ViewModel object will refer.
        /// </param>
        /// <returns>
        /// New Item ViewModel object.
        /// </returns>
        IDataWrapper<TVMO> CreateItemViewModel(TVMO obj);

        /// <summary>
        /// Create Details ViewModel object, referring to a new VMO.
        /// </summary>
        /// <returns>
        /// New Details ViewModel object.
        /// </returns>
        IDataWrapper<TVMO> CreateDetailsViewModelFromNewVMO();

        /// <summary>
        /// Create Details ViewModel object, referring to a clone of the given VMO.
        /// </summary>
        /// <param name="obj">
        /// VMO which will be cloned. The Details ViewModel object will then refer to the VMO.
        /// </param>
        /// <returns>
        /// New Details ViewModel object.
        /// </returns>
        IDataWrapper<TVMO> CreateDetailsViewModelFromClonedVMO(TVMO obj);

        /// <summary>
        /// Create a collection of Item ViewModel objects, from a collection of VMOs. 
        /// </summary>
        ObservableCollection<IDataWrapper<TVMO>> CreateItemViewModelCollection(IEnumerable<TVMO> dataObjects);
    }
}