using System.Collections.Generic;
using ExtensionsCommands.Implementation;
using ExtensionsModel.Implementation;
using InMemoryStorage.Interfaces;
using ViewModel.Interfaces;

namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// This class injects dependencies into the MasterDetailsViewModelWithState class.
    /// 1) Use of a FilePersistableCatalog (which is both a DTOCollection and InMemoryCollection)
    /// 2) List of immutable controls having default behavior
    /// 3) List of mutable  controls having default behavior
    /// 4) Addition of default control behaviors for CRUD invocation, 
    ///    view state selection and item selection.
    /// </summary>
    /// <typeparam name="T">
    /// Type of objects stored in underlying collection
    /// </typeparam>
    public abstract class MasterDetailsViewModelDefault<T> : MasterDetailsViewModelWithState
        where T : class, IStorable
    {
        protected MasterDetailsViewModelDefault(
            IViewModelFactory viewModelFactory,
            FilePersistableCatalog<T> catalog,
            List<string> immutableControls,
            List<string> mutableControls) 
            : base(viewModelFactory, catalog, catalog)
        {
            CRUDControlStateService CRUDcontrolStateService = new CRUDControlStateService();

            // Set all default behaviors
            CRUDcontrolStateService.AddImmutableControlsDefaultStates(immutableControls);
            CRUDcontrolStateService.AddMutableControlsDefaultStates(mutableControls);
            CRUDcontrolStateService.AddCRUDInvokerDefaultStates();
            CRUDcontrolStateService.AddStateSelectorDefaultStates();
            CRUDcontrolStateService.AddItemSelectorDefaultStates();

            // Set specific state service provides
            ViewStateService = new CRUDViewStateService();
            ControlStateService = CRUDcontrolStateService;

            // Set specific command managers
            DataCommandManager =  new CRUDCommandManagerViewStateDependent(this, catalog , this);
            StateCommandManager = new CRUDViewStateSelectCommandManager(ViewStateService);
        }
    }
}