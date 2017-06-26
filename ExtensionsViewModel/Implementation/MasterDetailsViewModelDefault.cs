using System.Collections.Generic;
using DTO.Interfaces;
using ExtensionsCommands.Implementation;
using ExtensionsModel.Implementation;
using InMemoryStorage.Interfaces;
using ViewModel.Implementation;

namespace ExtensionsViewModel.Implementation
{
    public abstract class MasterDetailsViewModelDefault<T, TDTO> : MasterDetailsViewModelWithState<TDTO> 
        where TDTO : IDTO, new() 
        where T : class, IStorable
    {
        protected MasterDetailsViewModelDefault(
            ViewModelFactoryBase<TDTO> viewModelFactory,
            FilePersistableCatalog<T> catalog,
            List<string> immutableControls,
            List<string> mutableControls) 
            : base(viewModelFactory, catalog,catalog)
        {
            CRUDControlStateService CRUDcontrolStateService = new CRUDControlStateService();

            CRUDcontrolStateService.AddImmutableControlsDefaultStates(immutableControls);
            CRUDcontrolStateService.AddMutableControlsDefaultStates(mutableControls);
            CRUDcontrolStateService.AddCRUDInvokerDefaultStates();
            CRUDcontrolStateService.AddStateSelectorDefaultStates();
            CRUDcontrolStateService.AddItemSelectorDefaultStates();

            ViewStateService = new CRUDViewStateService();
            ControlStateService = CRUDcontrolStateService;

            DataCommandManager =  new CRUDCommandManagerViewStateDependent(this, catalog , this);
            StateCommandManager = new CRUDViewStateSelectCommandManager(ViewStateService);
        }
    }
}