using System.Collections.Generic;
using Windows.UI.Xaml;
using ControlState.Implementation;
using ControlState.Types;
using ExtensionsViewModel.Interfaces;
using ExtensionsViewModel.Types;
using ExtensionsCommands.Types;

namespace ExtensionsViewModel.Implementation
{
    public class CRUDControlStateService : ControlStateService, ICRUDControlStateService
    {
        public CRUDControlStateService() 
            : base(new List<string> { CRUDStates.CreateState, CRUDStates.ReadState, CRUDStates.UpdateState, CRUDStates.DeleteState })
        {
        }

        public void AddImmutableControlsDefaultStates(List<string> ids)
        {
            AddControlDefaultStatesMultiple(ids, MutableType.Immutable);
        }

        public void AddMutableControlsDefaultStates(List<string> ids)
        {
            AddControlDefaultStatesMultiple(ids, MutableType.Mutable);
        }

        public void AddCRUDInvokerDefaultStates()
        {
            AddControlState(new GUIControlState(CRUDControls.CreateControl));
            AddControlState(new GUIControlState(CRUDControls.UpdateControl));
            AddControlState(new GUIControlState(CRUDControls.DeleteControl));
        }

        public void AddStateSelectorDefaultStates()
        {
            AddControlState(new GUIControlState(CRUDStateControls.CreateStateControl));
            AddControlState(new GUIControlState(CRUDStateControls.ReadStateControl));
            AddControlState(new GUIControlState(CRUDStateControls.UpdateStateControl));
            AddControlState(new GUIControlState(CRUDStateControls.DeleteStateControl));
        }

        public void AddItemSelectorDefaultStates()
        {
            AddControlState(CRUDStates.CreateState, new GUIControlState(CRUDControls.ItemSelectorControl, false, Visibility.Collapsed));
            AddControlState(CRUDStates.ReadState, new GUIControlState(CRUDControls.ItemSelectorControl, true, Visibility.Visible));
            AddControlState(CRUDStates.UpdateState, new GUIControlState(CRUDControls.ItemSelectorControl, true, Visibility.Visible));
            AddControlState(CRUDStates.DeleteState, new GUIControlState(CRUDControls.ItemSelectorControl, true, Visibility.Visible));            
        }

        private void AddControlDefaultStates(string id, MutableType mutable)
        {
            AddControlState(CRUDStates.CreateState, new GUIControlState(id, true));
            AddControlState(CRUDStates.ReadState, new GUIControlState(id, false));
            AddControlState(CRUDStates.UpdateState, new GUIControlState(id, mutable == MutableType.Mutable));
            AddControlState(CRUDStates.DeleteState, new GUIControlState(id, false));
        }

        private void AddControlDefaultStatesMultiple(List<string> ids, MutableType mutable)
        {
            foreach (string id in ids)
            {
                AddControlDefaultStates(id, mutable);
            }
        }
    }
}