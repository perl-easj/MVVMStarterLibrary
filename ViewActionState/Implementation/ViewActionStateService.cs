using System.Windows.Input;
using Commands.Implementation;
using ViewActionState.Interfaces;
using ViewActionState.Types;

namespace ViewActionState.Implementation
{
    public class ViewActionStateService : IViewActionStateService
    {
        private ViewActionStateType _actionState;
        private ICommand _setCreateStateCommand;
        private ICommand _setReadStateCommand;
        private ICommand _setUpdateStateCommand;
        private ICommand _setDeleteStateCommand;

        public ViewActionStateType ActionState
        {
            get { return ViewActionStateType.Create; }
            set { }
        }

        public ICommand SetCreateStateCommand
        {
            get { return null; }
        }

        public ICommand SetReadStateCommand
        {
            get { return null; }
        }

        public ICommand SetUpdateStateCommand
        {
            get { return null; }
        }

        public ICommand SetDeleteStateCommand
        {
            get { return null; }
        }

        public ViewActionStateService(ViewActionStateType actionState = ViewActionStateType.Read)
        {
            _actionState = actionState;
            _setCreateStateCommand = new RelayCommand(() => { _actionState = ViewActionStateType.Create; }, () => true);
            _setReadStateCommand = new RelayCommand(() => { _actionState = ViewActionStateType.Read; }, () => true);
            _setUpdateStateCommand = new RelayCommand(() => { _actionState = ViewActionStateType.Update; }, () => true);
            _setDeleteStateCommand = new RelayCommand(() => { _actionState = ViewActionStateType.Delete; }, () => true);
        }
    }
}