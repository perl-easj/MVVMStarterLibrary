using System;

namespace ViewState.Interfaces
{
    /// <summary>
    /// Interface for a simple view state service. The view state can be set 
    /// and retrieved, and a client can subscribe to changes in the view state.
    /// No assumption about specific view states are made.
    /// </summary>
    public interface IViewStateService
    {
        /// <summary>
        /// Clients interested in being notified about changes in the view
        /// state of the object can register at this event. 
        /// </summary>
        event Action<string> ViewStateChanged;

        /// <summary>
        /// View state of object.
        /// </summary>
        string ViewState { get; set; }
    }
}