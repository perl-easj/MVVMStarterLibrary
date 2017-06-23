using System.Collections.Generic;

namespace Command.Interfaces
{
    public interface ICommandManager
    {
        void AddCommand(string key, INotifiableCommand command);
        Dictionary<string, INotifiableCommand> Commands { get; }
        void Notify();
    }
}