using System.Windows.Input;

namespace Command.Interfaces
{
    public interface INotifiableCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}