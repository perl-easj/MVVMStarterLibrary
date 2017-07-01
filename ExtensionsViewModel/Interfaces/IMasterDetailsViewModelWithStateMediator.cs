namespace ViewModel.Interfaces
{
    /// <summary>
    /// Interface for Mediator that also depend on view state.
    /// </summary>
    public interface IMasterDetailsViewModelWithStateMediator : IMasterDetailsViewModelMediator
    {
        /// <summary>
        /// Invoked when the view state changes
        /// </summary>
        /// <param name="state">
        /// New view state
        /// </param>
        void OnViewStateChanged(string state);
    }
}