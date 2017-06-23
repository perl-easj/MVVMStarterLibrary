using System.Collections.Generic;
using ControlState.Interfaces;

namespace ExtensionsViewModel.Interfaces
{
    public interface ICRUDControlStateService : IControlStateService
    {
        void AddImmutableControlsDefaultStates(List<string> ids);
        void AddMutableControlsDefaultStates(List<string> ids);
        void AddCRUDInvokerDefaultStates();
        void AddStateSelectorDefaultStates();
        void AddItemSelectorDefaultStates();
    }
}