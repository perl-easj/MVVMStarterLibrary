using System.Collections.Generic;
using System.Windows.Input;

namespace ViewCommands.Interfaces
{
    public interface IViewActionService
    {
        Dictionary<string, ICommand> Commands { get; }
        void Notify();
    }
}