using System.Collections.Generic;

namespace ControlState.Interfaces
{
    public interface IHasControlStates
    {
        Dictionary<string, IControlState> ControlStates { get; }
    }
}