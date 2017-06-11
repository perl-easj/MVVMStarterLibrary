using System.Collections.Generic;
using ViewActionState.Types;
using ViewControlState.Types;

namespace ViewControlState.Interfaces
{
    public interface IViewControlStateService
    {
        Dictionary<string, IViewControlState> GetViewControlStates(ViewActionStateType viewStateType);
        void AddViewControlState(IViewControlState controlState);
        void AddViewControlState(ViewActionStateType viewStateType, IViewControlState controlState);
        void AddControlDefaultStates(string id, MutableType mutable);
        void AddControlDefaultStatesMultiple(List<string> ids, MutableType mutable);
        void AddImmutableControlsDefaultStates(List<string> ids);
        void AddMutableControlsDefaultStates(List<string> ids);
        void AddButtonDefaultStates();
    }
}