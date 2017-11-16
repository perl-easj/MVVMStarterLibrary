using System.Collections.Generic;
using Catalog.Implementation;
using DataTransformation.Interfaces;
using ExtensionsCommands.Implementation;
using ExtensionsCommands.Types;
using InMemoryStorage.Interfaces;
using ViewModel.Interfaces;
// ReSharper disable NotAccessedField.Local

namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// This class injects specific dependencies into 
    /// the MasterDetailsViewModelWithState class, making
    /// it a CRUD-specific implementation
    /// 1) Use of PersistableCatalog
    /// 2) List of immutable controls having default behavior
    /// 3) List of mutable controls having default behavior
    /// 4) Addition of default control behaviors for CRUD invocation, 
    ///    view state selection and item selection.
    /// </summary>
    public abstract class MasterDetailsViewModelCRUD<T, TVMO, TDTO> : MasterDetailsViewModelWithState<T, TVMO>
        where T : class, IStorable 
        where TVMO : class, ITransformed<T>
    {
        private MasterDetailsViewModelCRUDMediator<T, TVMO, TDTO> _mediator;

        protected MasterDetailsViewModelCRUD(
            IViewModelFactory<TVMO> viewModelFactory,
            PersistableCatalog<T, TVMO, TDTO> catalog,
            List<string> immutableControls,
            List<string> mutableControls) 
            : base(viewModelFactory, catalog)
        {
            CRUDControlStateService CRUDcontrolStateService = new CRUDControlStateService();

            // Set all default control behaviors in CRUD-specific state service
            CRUDcontrolStateService.AddImmutableControlsDefaultStates(immutableControls);
            CRUDcontrolStateService.AddMutableControlsDefaultStates(mutableControls);
            CRUDcontrolStateService.AddCRUDInvokerDefaultStates();
            CRUDcontrolStateService.AddStateSelectorDefaultStates();
            CRUDcontrolStateService.AddItemSelectorDefaultStates();

            // Set state services to refer to CRUD-specific services
            ViewStateService = new CRUDViewStateService();
            ControlStateService = CRUDcontrolStateService;

            // Set command managers to CRUD-specific implementations
            DataCommandManager =  new CRUDCommandManagerViewStateDependent<T, TVMO>(this, catalog , this);
            StateCommandManager = new CRUDViewStateSelectCommandManager(ViewStateService);

            // Set initial View state
            ViewStateService.ViewState = CRUDStates.ReadState;

            // Set mediator to a state-aware implementation
            _mediator = new MasterDetailsViewModelCRUDMediator<T, TVMO, TDTO>(this, catalog, viewModelFactory);
        }
    }
}