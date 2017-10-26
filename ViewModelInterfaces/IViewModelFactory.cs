using System.Collections.Generic;
using System.Collections.ObjectModel;
using DataTransformation.Interfaces;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// Interface for a factory for creation of 
    /// Details and Item ViewModel objects, based 
    /// on transformed data objects. The ViewModel 
    /// object may refer to a brand new object, an 
    /// existing object, or a clone of an existing object.
    /// </summary>
    public interface IViewModelFactory
    {
        /// <summary>
        /// Create Details ViewModel object, referring 
        /// to the given transformed data object.
        /// </summary>
        /// <param name="obj">
        /// Transformed data object to which the 
        /// Details ViewModel object will refer.
        /// </param>
        /// <returns>
        /// New Details ViewModel object.
        /// </returns>
        ITransformedDataWrapper CreateDetailsViewModel(ITransformedData obj);

        /// <summary>
        /// Create Item ViewModel object, , referring 
        /// to the given transformed data object.
        /// </summary>
        /// <param name="obj">
        /// Transformed data object to which the 
        /// Item ViewModel object will refer.
        /// </param>
        /// <returns>
        /// New Item ViewModel object.
        /// </returns>
        ITransformedDataWrapper CreateItemViewModel(ITransformedData obj);

        /// <summary>
        /// Create Details ViewModel object, referring 
        /// to a new transformed data object.
        /// </summary>
        /// <returns>
        /// New Details ViewModel object.
        /// </returns>
        ITransformedDataWrapper CreateDetailsViewModelFromNewTDO();

        /// <summary>
        /// Create Details ViewModel object, referring 
        /// to a clone of the given transformed data object.
        /// </summary>
        /// <param name="obj">
        /// Transformed data object. which will be cloned. 
        /// The Details ViewModel object will then refer
        /// to the cloned transformed data object.
        /// </param>
        /// <returns>
        /// New Details ViewModel object.
        /// </returns>
        ITransformedDataWrapper CreateDetailsViewModelFromClonedTDO(ITransformedData obj);

        /// <summary>
        /// Create a collection of Item ViewModel objects, from a
        /// collection of transformed data objects. 
        /// </summary>
        ObservableCollection<ITransformedDataWrapper> CreateItemViewModelCollection(IEnumerable<ITransformedData> dataObjects);
    }
}