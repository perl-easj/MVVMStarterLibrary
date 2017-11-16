using Catalog.Interfaces;
using DataTransformation.Interfaces;
using ExtensionsCommands.Types;
using InMemoryStorage.Interfaces;
using ViewModel.Implementation;
using ViewModel.Interfaces;
using ViewState.Interfaces;

// ReSharper disable UnusedMember.Local

namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// This class implements a specific strategy for mediation 
    /// between elements in a Master/Details ViewModel object, 
    /// with CRUD view state and CRUD operation support.
    /// </summary>
    public class MasterDetailsViewModelCRUDMediator<T, TVMO, TDTO> : MasterDetailsViewModelMediatorBase<T, TVMO>, IViewStateMediator 
        where TVMO : class, ITransformed<T> 
        where T : class, IStorable
    {
        #region Instance fields
        private MasterDetailsViewModelCRUD<T, TVMO, TDTO> _masterDetailsViewModelCRUD;
        private IViewModelFactory<TVMO> _viewModelFactory;
        #endregion

        #region Constructor
        public MasterDetailsViewModelCRUDMediator(
            MasterDetailsViewModelCRUD<T, TVMO, TDTO> masterDetailsViewModelCrud,
            ICatalog<TVMO> catalog,
            IViewModelFactory<TVMO> viewModelFactory) 
            : base(masterDetailsViewModelCrud, catalog, viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            _masterDetailsViewModelCRUD = masterDetailsViewModelCrud;
            _masterDetailsViewModelCRUD.ViewStateService.ViewStateChanged += OnViewStateChanged;
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
            if (_masterDetailsViewModelCRUD.ViewState == CRUDStates.CreateState)
            {
                _masterDetailsViewModelCRUD.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromNewVMO();
            }

            // If in the Update state - and an Item is selected - 
            // the Details ViewModel object will now refer to a 
            // clone of the selected VMO.
            if (_masterDetailsViewModelCRUD.ViewState == CRUDStates.UpdateState && _masterDetailsViewModelCRUD.ItemSelected != null)
            {
                _masterDetailsViewModelCRUD.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromClonedVMO(_masterDetailsViewModelCRUD.ItemSelected.DataObject);
            }

            // All commands are notified
            NotifyCommands();

            // Control states should be re-read, 
            // since they may depend on view state.
            _masterDetailsViewModelCRUD.OnPropertyChanged(nameof(_masterDetailsViewModelCRUD.ControlStates));
        }
        #endregion

        #region Base class overrides
        /// <summary>
        /// All commands are notified, such that the 
        /// CanExecute predicate can be re-evaluated.
        /// </summary>
        public override void NotifyCommands()
        {
            _masterDetailsViewModelCRUD.DataCommandManager.Notify();
            _masterDetailsViewModelCRUD.StateCommandManager.Notify();
        }

        /// <summary>
        /// If in the Update state, the Details ViewModel object will
        /// now refer to a clone of the VMO. Otherwise, the Details
        /// ViewModel object will refer directly to the selected VMO.
        /// </summary>
        public override void SetItemDetailsOnItemSelectionChanged(TVMO vmObj)
        {
            _masterDetailsViewModelCRUD.ItemDetails = (_masterDetailsViewModelCRUD.ViewState == CRUDStates.UpdateState) ?
                _viewModelFactory.CreateDetailsViewModelFromClonedVMO(vmObj) :
                _viewModelFactory.CreateDetailsViewModel(vmObj);
        }

        /// <summary>
        /// If in the Create state, set the Details to refer 
        /// to a fresh Details ViewModel object. This object 
        /// will be populated with default values.
        /// </summary>
        public override void SetItemDetailsOnCatalogChanged()
        {
            if (_masterDetailsViewModelCRUD.ViewState == CRUDStates.CreateState)
            {
                _masterDetailsViewModelCRUD.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromNewVMO();
            }
        }
        #endregion
    }
}