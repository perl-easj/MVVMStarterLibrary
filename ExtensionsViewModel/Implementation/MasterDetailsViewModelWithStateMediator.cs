using System;
using DTO.Interfaces;
using ExtensionsCommands.Types;
using ViewModel.Interfaces;

namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// This class implements a specific strategy for mediation between elements
    /// in a Master/Details ViewModel object with view state.
    /// </summary>
    public class MasterDetailsViewModelWithStateMediator : IMasterDetailsViewModelWithStateMediator
    {
        private MasterDetailsViewModelWithState _masterDetailsViewModel;
        private IViewModelFactory _viewModelFactory;

        public MasterDetailsViewModelWithStateMediator(
            MasterDetailsViewModelWithState masterDetailsViewModel,
            IViewModelFactory viewModelFactory)
        {
            // Sanity checks, to avoid null-checking later.
            _masterDetailsViewModel = masterDetailsViewModel ?? throw new ArgumentNullException(nameof(MasterDetailsViewModelWithStateMediator));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(MasterDetailsViewModelWithStateMediator));
        }

        /// <summary>
        /// Handle change in Item selection.
        /// </summary>
        /// <param name="dtoWrapper">
        /// New selection.
        /// </param>
        public void OnItemSelectionChanged(IDTOWrapper dtoWrapper)
        {
            if (dtoWrapper == null)
            {
                _masterDetailsViewModel.ItemDetails = null;
            }
            else
            {
                // If in the Update state, the Details ViewModel object will now refer to
                // a clone of the selected DTO. Otherwise, the Details ViewModel object 
                // will refer directly to the selected DTO.
                _masterDetailsViewModel.ItemDetails = (_masterDetailsViewModel.ViewState == CRUDStates.UpdateState) ?
                    _viewModelFactory.CreateDetailsViewModelFromClonedDTO(dtoWrapper.DataObject) :
                    _viewModelFactory.CreateDetailsViewModel(dtoWrapper.DataObject);
            }

            // All commands are notified
            NotifyCommands();
        }

        /// <summary>
        /// Handle changes in underlying model.
        /// </summary>
        public void OnModelChanged()
        {
            // If the underlying model changes, the Item selection is set to null (no selection).
            // The ItemCollection property is also notified, such that Views binding to this
            // property can re-read the collection.
            _masterDetailsViewModel.ItemSelected = null;
            _masterDetailsViewModel.OnPropertyChanged(nameof(_masterDetailsViewModel.ItemCollection));

            // If in the Create state, set the Details to refer to a fresh Details ViewModel object.
            // This object will be populated with default values.
            if (_masterDetailsViewModel.ViewState == CRUDStates.CreateState)
            {
                _masterDetailsViewModel.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromNewDTO();
            }
        }

        /// <summary>
        /// Handle changes in view state.
        /// </summary>
        /// <param name="state">
        /// New View State
        /// </param>
        public void OnViewStateChanged(string state)
        {
            // If in the Create state, set the Details to refer to a fresh Details ViewModel object.
            // This object will be populated with default values.
            if (_masterDetailsViewModel.ViewState == CRUDStates.CreateState)
            {
                _masterDetailsViewModel.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromNewDTO();
            }

            // If in the Update state - and an Item is selected - the Details ViewModel object 
            // will now refer to a clone of the selected DTO.
            if (_masterDetailsViewModel.ViewState == CRUDStates.UpdateState && _masterDetailsViewModel.ItemSelected != null)
            {
                _masterDetailsViewModel.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromClonedDTO(_masterDetailsViewModel.ItemSelected.DataObject);
            }

            // All commands are notified
            NotifyCommands();

            // Control states should be re-read, since they may depend on view state.
            _masterDetailsViewModel.OnPropertyChanged(nameof(_masterDetailsViewModel.ControlStates));
        }

        /// <summary>
        /// All commands are notify, such that the CanExecute predicate can be re-evaluated.
        /// </summary>
        private void NotifyCommands()
        {
            _masterDetailsViewModel.DataCommandManager.Notify();
            _masterDetailsViewModel.StateCommandManager.Notify();
        }

        private IDTOWrapper SetDetailsObject(IDTOWrapper itemDtoWrapper)
        {
            if (itemDtoWrapper == null)
            {
                return null;
            }
            else
            {
                if (_masterDetailsViewModel.ViewState == CRUDStates.UpdateState)
                {
                    return _viewModelFactory.CreateDetailsViewModelFromClonedDTO(itemDtoWrapper.DataObject);
                }

                if (_masterDetailsViewModel.ViewState == CRUDStates.CreateState)
                {
                    return _viewModelFactory.CreateDetailsViewModelFromNewDTO();
                }

                return _viewModelFactory.CreateDetailsViewModel(itemDtoWrapper.DataObject); ;
            }
        }
    }
}