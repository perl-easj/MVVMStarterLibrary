using System;
using DataTransformation.Interfaces;
using ExtensionsCommands.Types;
using ViewModel.Interfaces;
// ReSharper disable UnusedMember.Local

namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// This class implements a specific strategy for
    /// mediation between elements in a Master/Details 
    /// ViewModel object with view state.
    /// </summary>
    public class MasterDetailsViewModelWithStateMediator : IMasterDetailsViewModelWithStateMediator
    {
        #region Instance fields
        private MasterDetailsViewModelWithState _masterDetailsViewModel;
        private IViewModelFactory _viewModelFactory;
        #endregion

        #region Constructor
        public MasterDetailsViewModelWithStateMediator(
    MasterDetailsViewModelWithState masterDetailsViewModel,
    IViewModelFactory viewModelFactory)
        {
            _masterDetailsViewModel = masterDetailsViewModel ?? throw new ArgumentNullException(nameof(MasterDetailsViewModelWithStateMediator));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(MasterDetailsViewModelWithStateMediator));
        }
        #endregion

        #region Implementation of interface methods
        /// <summary>
        /// Handle change in Item selection.
        /// </summary>
        /// <param name="tdoWrapper">
        /// New selection.
        /// </param>
        public void OnItemSelectionChanged(ITransformedDataWrapper tdoWrapper)
        {
            if (tdoWrapper == null)
            {
                _masterDetailsViewModel.ItemDetails = null;
            }
            else
            {
                // If in the Update state, the Details ViewModel object 
                // will now refer to a clone of the selected transformed 
                // data object. Otherwise, the Details ViewModel object 
                // will refer directly to the selected transformed data object.
                _masterDetailsViewModel.ItemDetails = (_masterDetailsViewModel.ViewState == CRUDStates.UpdateState) ?
                    _viewModelFactory.CreateDetailsViewModelFromClonedTDO(tdoWrapper.DataObject) :
                    _viewModelFactory.CreateDetailsViewModel(tdoWrapper.DataObject);
            }

            // All commands are notified
            NotifyCommands();
        }

        /// <summary>
        /// Handle changes in underlying model.
        /// </summary>
        public void OnModelChanged()
        {
            // If the underlying model changes, the Item selection 
            // is set to null (no selection). The ItemCollection 
            // property is also notified, such that Views binding 
            // to this property can re-read the collection.
            _masterDetailsViewModel.ItemSelected = null;
            _masterDetailsViewModel.OnPropertyChanged(nameof(_masterDetailsViewModel.ItemCollection));

            // If in the Create state, set the Details to refer 
            // to a fresh Details ViewModel object. This object 
            // will be populated with default values.
            if (_masterDetailsViewModel.ViewState == CRUDStates.CreateState)
            {
                _masterDetailsViewModel.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromNewTDO();
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
            // If in the Create state, set the Details to refer 
            // to a fresh Details ViewModel object. This object 
            // will be populated with default values.
            if (_masterDetailsViewModel.ViewState == CRUDStates.CreateState)
            {
                _masterDetailsViewModel.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromNewTDO();
            }

            // If in the Update state - and an Item is selected - 
            // the Details ViewModel object will now refer to a 
            // clone of the selected DTO.
            if (_masterDetailsViewModel.ViewState == CRUDStates.UpdateState && _masterDetailsViewModel.ItemSelected != null)
            {
                _masterDetailsViewModel.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromClonedTDO(_masterDetailsViewModel.ItemSelected.DataObject);
            }

            // All commands are notified
            NotifyCommands();

            // Control states should be re-read, 
            // since they may depend on view state.
            _masterDetailsViewModel.OnPropertyChanged(nameof(_masterDetailsViewModel.ControlStates));
        }
        #endregion

        #region Private helper methods
        /// <summary>
        /// All commands are notified, such that the 
        /// CanExecute predicate can be re-evaluated.
        /// </summary>
        private void NotifyCommands()
        {
            _masterDetailsViewModel.DataCommandManager.Notify();
            _masterDetailsViewModel.StateCommandManager.Notify();
        }

        private ITransformedDataWrapper SetDetailsObject(ITransformedDataWrapper itemDtoWrapper)
        {
            if (itemDtoWrapper == null)
            {
                return null;
            }
            else
            {
                if (_masterDetailsViewModel.ViewState == CRUDStates.UpdateState)
                {
                    return _viewModelFactory.CreateDetailsViewModelFromClonedTDO(itemDtoWrapper.DataObject);
                }

                if (_masterDetailsViewModel.ViewState == CRUDStates.CreateState)
                {
                    return _viewModelFactory.CreateDetailsViewModelFromNewTDO();
                }

                return _viewModelFactory.CreateDetailsViewModel(itemDtoWrapper.DataObject);
            }
        } 
        #endregion
    }
}