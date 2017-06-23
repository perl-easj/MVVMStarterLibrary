using System.Collections.Generic;

namespace ControlState.Interfaces
{
    public interface IControlStateService
    {
        Dictionary<string, IControlState> GetControlStates(string state);
        void AddControlState(IControlState controlState);
        void AddControlState(string state, IControlState controlState);
    }
}