using DataTransformation.Interfaces;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// The Mediator will be associated with a 
    /// Master/Details ViewModel object, and manage 
    /// the relations between various properties.
    /// </summary>
    public interface IMasterDetailsViewModelMediator<in TVMO>
    {
        /// <summary>
        /// Invoked when the selected item changes.
        /// </summary>
        /// <param name="tdoWrapper">
        /// Newly selected item
        /// </param>
        void OnItemSelectionChanged(IDataWrapper<TVMO> tdoWrapper);
    }
}