using System.Collections.Generic;
using Catalog.Interfaces;
using DataTransformation.Interfaces;
using ExtensionsCommands.Implementation;
using ExtensionsCommands.Types;
using InMemoryStorage.Interfaces;
using ViewModel.Interfaces;


namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// This class injects specific dependencies into 
    /// the MasterDetailsViewModelWithState class, making
    /// it a CRUD-specific implementation
    /// 1) List of immutable controls having default behavior
    /// 2) List of mutable controls having default behavior
    /// 3) Addition of CRUD-specific command managers.
    /// 4) Addition of default control behaviors for CRUD invocation, 
    ///    view state selection and item selection.
    /// 5) Addition of CRUD-specific mediator.
    /// </summary>
    public abstract class MasterDetailsViewModelCRUD<TVMO> : MasterDetailsViewModelWithState<TVMO> 
        where TVMO : class, ICopyable, IStorable
    {
        private MasterDetailsViewModelCRUDMediator<TVMO> _mediator;

        protected MasterDetailsViewModelCRUD(
            IViewModelFactory<TVMO> viewModelFactory,
            ICatalog<TVMO> catalog,
            List<string> immutableControls,
            List<string> mutableControls) 
            : base(viewModelFactory, catalog)
        {
            CRUDControlStateService CRUDcontrolStateService = new CRUDControlStateService();

            SetupControlBehaviors(CRUDcontrolStateService, immutableControls, mutableControls);

            // Set state services to refer to CRUD-specific services
            ViewStateService = new CRUDViewStateService();
            ControlStateService = CRUDcontrolStateService;

            SetupCommandManagers(catalog);
            SetupInitialViewState();

            // Set mediator to a state-aware implementation
            _mediator = new MasterDetailsViewModelCRUDMediator<TVMO>(this, catalog, viewModelFactory);
        }

        protected virtual void SetupControlBehaviors(
            CRUDControlStateService stateService, 
            List<string> immutableControls, List<string> mutableControls)
        {
            stateService.AddImmutableControlsDefaultStates(immutableControls);
            stateService.AddMutableControlsDefaultStates(mutableControls);
            stateService.AddCRUDInvokerDefaultStates();
            stateService.AddStateSelectorDefaultStates();
            stateService.AddItemSelectorDefaultStates();
        }

        protected virtual void SetupCommandManagers(ICatalog<TVMO> catalog)
        {
            DataCommandManager = new CRUDCommandManagerViewStateDependent<TVMO>(this, catalog, this);
            StateCommandManager = new CRUDViewStateSelectCommandManager(ViewStateService);
        }

        protected virtual void SetupInitialViewState()
        {
            ViewStateService.ViewState = CRUDStates.ReadState;
        }
    }
}