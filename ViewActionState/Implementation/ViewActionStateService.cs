using System;
using System.Collections.Generic;
using System.Windows.Input;
using Commands.Implementation;
using ViewActionState.Interfaces;
using ViewActionState.Types;

namespace ViewActionState.Implementation
{
    public class ViewActionStateService : IViewActionStateService
    {
        public event EventHandler OnViewActionStateChanged;
        private ViewActionStateType _actionState;
        private Dictionary<string, ICommand> _commands;

        public ViewActionStateType ViewActionState
        {
            get { return _actionState; }
            set
            {
                _actionState = value;
                OnViewActionStateChanged?.Invoke(this,EventArgs.Empty);
            }
        }

        public Dictionary<string, ICommand> Commands
        {
            get { return _commands; }
        }

        public ViewActionStateService(ViewActionStateType actionState = ViewActionStateType.Read)
        {
            _actionState = actionState;
            _commands = new Dictionary<string, ICommand>();
            _commands.Add("Create", new RelayCommand(() => { ViewActionState = ViewActionStateType.Create; }, () => true));
            _commands.Add("Read", new RelayCommand(() => { ViewActionState = ViewActionStateType.Read; }, () => true));
            _commands.Add("Update", new RelayCommand(() => { ViewActionState = ViewActionStateType.Update; }, () => true));
            _commands.Add("Delete", new RelayCommand(() => { ViewActionState = ViewActionStateType.Delete; }, () => true));
        }
    }
}