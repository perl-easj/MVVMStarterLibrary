using System;
using System.Collections.Generic;
using ControlState.Interfaces;

namespace ControlState.Implementation
{
    public class ControlStateService : IControlStateService
    {
        private Dictionary<string, Dictionary<string, IControlState>> _controlStateMap;
        private List<string> _controlIDs;
        private List<string> _validStates;

        public ControlStateService(List<string> validStates)
        {
            _validStates = validStates;
            _controlIDs = new List<string>();
            _controlStateMap = new Dictionary<string, Dictionary<string, IControlState>>();

            foreach (string viewState in _validStates)
            {
                _controlStateMap.Add(viewState, new Dictionary<string, IControlState>());
            }
        }

        public Dictionary<string, IControlState> GetControlStates(string state)
        {
            if (!_controlStateMap.ContainsKey(state))
            {
                throw new ArgumentException(nameof(GetControlStates));    
            }

            return _controlStateMap[state];
        }

        /// <summary>
        /// Call this version if the control state 
        /// is valid for all view states
        /// </summary>
        public void AddControlState(IControlState controlState)
        {
            foreach (string viewState in _validStates)
            {
                AddControlState(viewState, controlState);
            }
        }

        /// <summary>
        /// Call this version if the control state 
        /// is valid for a specific view state
        /// </summary>
        public void AddControlState(string state, IControlState controlState)
        {
            // Add name, if not seen before
            if (!_controlIDs.Contains(controlState.ID))
            {
                _controlIDs.Add(controlState.ID);
            }

            _controlStateMap[state].Add(controlState.ID, controlState);
        }
    }
}