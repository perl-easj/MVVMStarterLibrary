using System.Windows.Input;
using ViewActionState.Types;

namespace ViewActionState.Interfaces
{
    public interface IViewActionStateService
    {
        ViewActionStateType ActionState { get; set; }

        ICommand SetCreateStateCommand { get; }
        ICommand SetReadStateCommand { get; }
        ICommand SetUpdateStateCommand { get; }
        ICommand SetDeleteStateCommand { get; }
    }
}