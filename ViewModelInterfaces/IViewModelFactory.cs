using System.Collections.Generic;
using System.Collections.ObjectModel;
using DTO.Interfaces;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// Interface for a factory for creation of Details and Item ViewModel objects, 
    /// based on DTOs. The ViewModel object may refer to a brand new DTO, an existing
    /// DTO, or a clone of an existing DTO.
    /// </summary>
    public interface IViewModelFactory
    {
        /// <summary>
        /// Create Details ViewModel object, referring to the given DTO.
        /// </summary>
        /// <param name="obj">
        /// DTO to which the Details ViewModel object will refer.
        /// </param>
        /// <returns>
        /// New Details ViewModel object.
        /// </returns>
        IDTOWrapper CreateDetailsViewModel(IDTO obj);

        /// <summary>
        /// Create Item ViewModel object, referring to the given DTO.
        /// </summary>
        /// <param name="obj">
        /// DTO to which the Item ViewModel object will refer.
        /// </param>
        /// <returns>
        /// New Item ViewModel object.
        /// </returns>
        IDTOWrapper CreateItemViewModel(IDTO obj);

        /// <summary>
        /// Create Details ViewModel object, referring to a new DTO.
        /// </summary>
        /// <returns>
        /// New Details ViewModel object.
        /// </returns>
        IDTOWrapper CreateDetailsViewModelFromNewDTO();

        /// <summary>
        /// Create Details ViewModel object, referring to a clone of the given DTO.
        /// </summary>
        /// <param name="obj">
        /// DTO which will be cloned. The Details ViewModel object will then refer
        /// to the cloned DTO.
        /// </param>
        /// <returns>
        /// New Details ViewModel object.
        /// </returns>
        IDTOWrapper CreateDetailsViewModelFromClonedDTO(IDTO obj);

        /// <summary>
        /// Create a collection of Item ViewModel objects, from a
        /// collection of DTOs. 
        /// </summary>
        ObservableCollection<IDTOWrapper> CreateItemViewModelCollection(IEnumerable<IDTO> dataObjects);
    }
}