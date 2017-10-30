using System.Collections.Generic;
using Catalog.Implementation;
using Catalog.Interfaces;
using DataTransformation.Interfaces;
using ExtensionsCommands.Implementation;
using ExtensionsCommands.Types;
using ExtensionsViewModel.Interfaces;
using InMemoryStorage.Interfaces;
using ViewModel.Interfaces;

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

            // Finally initialise the Mediator
            InitMediator(catalog, new MasterDetailsViewModelWithStateMediator<T, TVMO>(this, viewModelFactory));
        }

        private void InitMediator(IMonitorableCatalog catalog, IMasterDetailsViewModelWithStateMediator<TVMO> mediator)
        {
            Mediator = mediator;

            // Let Mediator be notified when collection changes.
            catalog.AddOnObjectCreatedCallback(Mediator.OnCatalogChanged);
            catalog.AddOnObjectUpdatedCallback(Mediator.OnCatalogChanged);
            catalog.AddOnObjectDeletedCallback(Mediator.OnCatalogChanged);

            // Set initial View state
            ViewStateService.ViewState = CRUDStates.ReadState;

            // Let Mediator be notified when view state changes
            ViewStateService.ViewStateChanged += Mediator.OnViewStateChanged;

            // Let Mediator be notified when ítem selection changes
            ItemSelectionChanged += Mediator.OnItemSelectionChanged;
        }
    }
}