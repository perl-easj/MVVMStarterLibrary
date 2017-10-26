using DataTransformation.Interfaces;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// The Mediator will be associated with a Master/Details ViewModel
    /// object, and manage the relations between various properties.
    /// </summary>
    public interface IMasterDetailsViewModelMediator
    {
        /// <summary>
        /// Invoked when the selected item changes.
        /// </summary>
        /// <param name="dtoWrapper">
        /// Newly selected object
        /// </param>
        void OnItemSelectionChanged(ITransformedDataWrapper dtoWrapper);

        /// <summary>
        /// Invoked when the underlying model changes.
        /// </summary>
        void OnModelChanged();
    }
}