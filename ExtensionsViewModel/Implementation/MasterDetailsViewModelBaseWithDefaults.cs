using System.Collections.Generic;
using InMemoryStorage.Interfaces;
using ModelClass.Implementation;
using ViewModel.Implementation;

namespace ExtensionsViewModel.Implementation
{
    public abstract class MasterDetailsViewModelBaseWithDefaults<TDomainClass> : MasterDetailsViewModelBase<TDomainClass> 
        where TDomainClass : DomainClassBase, new()
    {
        protected MasterDetailsViewModelBaseWithDefaults(
            ViewModelFactoryBase<TDomainClass> viewModelFactory, 
            IObservableInMemoryCollection<TDomainClass> catalog,
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