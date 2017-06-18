using System.Collections.Generic;
using DTO.Interfaces;
using Persistency.Interfaces;
using ViewActionState.Implementation;
using ViewControlState.Implementation;
using ViewModel.Implementation;

namespace ExtensionsViewModel.Implementation
{
    public abstract class MasterDetailsViewModelDefault<T, TDTO> : MasterDetailsViewModelBase<T, TDTO> 
        where TDTO : IDTO, new()
    {
        protected MasterDetailsViewModelDefault(
            ViewModelFactoryBase<TDTO> viewModelFactory,
            ICollectionAggregate<T> catalog,
            List<string> immutableControls,
            List<string> mutableControls) 
            : base(viewModelFactory, catalog, new ViewActionStateService(), new ViewControlStateService())
        {
            ControlStateService.AddImmutableControlsDefaultStates(immutableControls);
            ControlStateService.AddMutableControlsDefaultStates(mutableControls);
            ControlStateService.AddButtonDefaultStates();
            ControlStateService.AddItemSelectorDefaultStates();
        }
    }
}