using System.Collections.Generic;
using DataClass.Implementation;
using InMemoryStorage.Interfaces;
using ViewModel.Implementation;

namespace ExtensionsViewModel.Implementation
{
    public abstract class MasterDetailsViewModelBaseWithDefaults<TDTO> : 
        MasterDetailsViewModelBase<TDTO> 
        where TDTO : DTOBaseWithKey, new()
    {
        protected MasterDetailsViewModelBaseWithDefaults(
            ViewModelFactoryBase<TDTO> viewModelFactory,
            IConvertibleObservableInMemoryCollection<TDTO> catalog,
            List<string> immutableControls,
            List<string> mutableControls) 
            : base(viewModelFactory, catalog)
        {
            StateService.AddImmutableControlsDefaultStates(immutableControls);
            StateService.AddMutableControlsDefaultStates(mutableControls);
            StateService.AddButtonDefaultStates();
        }
    }
}