using System.Collections.Generic;
using ViewActionState.Types;
using ViewControlState.Interfaces;
using ViewControlState.Types;

namespace ViewControlState.Implementation
{
    public class ViewControlStateService : IViewControlStateService
    {
        private Dictionary<ViewActionStateType, Dictionary<string, IViewControlState>> _viewControlStateMap;
        private List<string> _controlIDs;

        public ViewControlStateService()
        {
            _controlIDs = new List<string>();
            _viewControlStateMap = new Dictionary<ViewActionStateType, Dictionary<string, IViewControlState>>();

            _viewControlStateMap.Add(ViewActionStateType.Create, new Dictionary<string, IViewControlState>());
            _viewControlStateMap.Add(ViewActionStateType.Read, new Dictionary<string, IViewControlState>());
            _viewControlStateMap.Add(ViewActionStateType.Update, new Dictionary<string, IViewControlState>());
            _viewControlStateMap.Add(ViewActionStateType.Delete, new Dictionary<string, IViewControlState>());
        }

        public Dictionary<string, IViewControlState> GetViewControlStates(ViewActionStateType viewStateType)
        {
            return _viewControlStateMap[viewStateType];
        }

        /// <summary>
        /// Call this version if the control state 
        /// is valid for all view states
        /// </summary>
        public void AddViewControlState(IViewControlState controlState)
        {
            AddViewControlState(ViewActionStateType.Create, controlState);
            AddViewControlState(ViewActionStateType.Read, controlState);
            AddViewControlState(ViewActionStateType.Update, controlState);
            AddViewControlState(ViewActionStateType.Delete, controlState);
        }

        /// <summary>
        /// Call this version if the control state 
        /// is valid for a specific view state
        /// </summary>
        public void AddViewControlState(ViewActionStateType viewStateType, IViewControlState controlState)
        {
            // Add name, if not seen before
            if (!_controlIDs.Contains(controlState.ID))
            {
                _controlIDs.Add(controlState.ID);
            }

            _viewControlStateMap[viewStateType].Add(controlState.ID, controlState);
        }

        public void AddControlDefaultStates(string id, MutableType mutable)
        {
            AddViewControlState(ViewActionStateType.Create, new ViewControlState(id, true));
            AddViewControlState(ViewActionStateType.Read, new ViewControlState(id, false));
            AddViewControlState(ViewActionStateType.Delete, new ViewControlState(id, false));
            AddViewControlState(ViewActionStateType.Update, new ViewControlState(id, mutable == MutableType.Mutable));
        }

        public void AddControlDefaultStatesMultiple(List<string> ids, MutableType mutable)
        {
            foreach (string id in ids)
            {
                AddControlDefaultStates(id, mutable);
            }
        }

        public void AddImmutableControlsDefaultStates(List<string> ids)
        {
            AddControlDefaultStatesMultiple(ids, MutableType.Immutable);
        }

        public void AddMutableControlsDefaultStates(List<string> ids)
        {
            AddControlDefaultStatesMultiple(ids, MutableType.Mutable);
        }

        public void AddButtonDefaultStates()
        {
            AddViewControlState(new ViewControlState("Create"));
            AddViewControlState(new ViewControlState("Update"));
            AddViewControlState(new ViewControlState("Delete"));
        }
    }
}