using System;

namespace ViewState.Interfaces
{
    public interface IViewStateService
    {
        event Action<string> ViewStateChanged;
        string ViewState { get; set; }
    }
}