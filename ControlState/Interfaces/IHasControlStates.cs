using System.Collections.Generic;

namespace ControlState.Interfaces
{
    /// <summary>
    /// An object which provides control states can 
    /// expose these through this interface.
    /// </summary>
    public interface IHasControlStates
    {
        Dictionary<string, IControlState> ControlStates { get; }
    }
}