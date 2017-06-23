using System;
using System.Collections.Generic;
using Command.Interfaces;

namespace Command.Implementation
{
    public class CommandManager : ICommandManager
    {
        private Dictionary<string, INotifiableCommand> _commands;

        public Dictionary<string, INotifiableCommand> Commands
        {
            get { return _commands; }
        }

        public CommandManager()
        {
            _commands = new Dictionary<string, INotifiableCommand>();
        }

        public void AddCommand(string key, INotifiableCommand command)
        {
            if (_commands.ContainsKey(key) || command == null)
            {
                throw new ArgumentException(nameof(AddCommand));
            }

            _commands.Add(key, command);
        }

        public void Notify()
        {
            foreach (INotifiableCommand command in _commands.Values)
            {
                command.RaiseCanExecuteChanged();
            }
        }
    }
}