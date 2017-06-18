using System;
using System.Collections.Generic;
using System.Windows.Input;
using ViewActionState.Types;

namespace ViewActionState.Interfaces
{
    public interface IViewActionStateService
    {
        ViewActionStateType ViewActionState { get; set; }
        Dictionary<string, ICommand> Commands { get; }

        event EventHandler OnViewActionStateChanged;
    }
}