using System;
using DataTransformation.Interfaces;
using ExtensionsCommands.Types;
using ExtensionsViewModel.Interfaces;
using ViewModel.Implementation;
using ViewModel.Interfaces;

// ReSharper disable UnusedMember.Local

namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// This class implements a specific strategy for
    /// mediation between elements in a Master/Details 
    /// ViewModel object with view state.
    /// </summary>
    public class MasterDetailsViewModelWithStateMediator<T, TVMO> : 
        MasterDetailsViewModelMediatorBase<T, TVMO>, 
        IMasterDetailsViewModelWithStateMediator<TVMO> 
        where TVMO : class, ITransformed<T>
    {
        #region Instance fields
        private MasterDetailsViewModelWithState<T, TVMO> _masterDetailsViewModelWS;
        #endregion

        #region Constructor
        public MasterDetailsViewModelWithStateMediator(
            MasterDetailsViewModelWithState<T, TVMO> masterDetailsViewModelWS,
            IViewModelFactory<TVMO> viewModelFactory) : base(masterDetailsViewModelWS, viewModelFactory)
        {
            _masterDetailsViewModelWS = masterDetailsViewModelWS ?? throw new ArgumentNullException(nameof(_masterDetailsViewModelWS));
        }
        #endregion

        #region Implementation of interface methods
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
            if (_masterDetailsViewModelWS.ViewState == CRUDStates.CreateState)
            {
                MasterDetailsViewModel.ItemDetails = ViewModelFactory.CreateDetailsViewModelFromNewVMO();
            }

            // If in the Update state - and an Item is selected - 
            // the Details ViewModel object will now refer to a 
            // clone of the selected VMO.
            if (_masterDetailsViewModelWS.ViewState == CRUDStates.UpdateState && MasterDetailsViewModel.ItemSelected != null)
            {
                MasterDetailsViewModel.ItemDetails = ViewModelFactory.CreateDetailsViewModelFromClonedVMO(MasterDetailsViewModel.ItemSelected.DataObject);
            }

            // All commands are notified
            NotifyCommands();

            // Control states should be re-read, 
            // since they may depend on view state.
            MasterDetailsViewModel.OnPropertyChanged(nameof(_masterDetailsViewModelWS.ControlStates));
        }
        #endregion

        #region Base class overrides
        /// <summary>
        /// All commands are notified, such that the 
        /// CanExecute predicate can be re-evaluated.
        /// </summary>
        public override void NotifyCommands()
        {
            _masterDetailsViewModelWS.DataCommandManager.Notify();
            _masterDetailsViewModelWS.StateCommandManager.Notify();
        }

        /// <summary>
        /// If in the Update state, the Details ViewModel object will
        /// now refer to a clone of the VMO. Otherwise, the Details
        /// ViewModel object will refer directly to the selected VMO.
        /// </summary>
        public override void SetItemDetailsOnItemSelectionChanged(TVMO vmObj)
        {
            MasterDetailsViewModel.ItemDetails = (_masterDetailsViewModelWS.ViewState == CRUDStates.UpdateState) ?
                ViewModelFactory.CreateDetailsViewModelFromClonedVMO(vmObj) :
                ViewModelFactory.CreateDetailsViewModel(vmObj);
        }

        /// <summary>
        /// If in the Create state, set the Details to refer 
        /// to a fresh Details ViewModel object. This object 
        /// will be populated with default values.
        /// </summary>
        public override void SetItemDetailsOnCatalogChanged()
        {
            if (_masterDetailsViewModelWS.ViewState == CRUDStates.CreateState)
            {
                MasterDetailsViewModel.ItemDetails = ViewModelFactory.CreateDetailsViewModelFromNewVMO();
            }
        }
        #endregion
    }
}