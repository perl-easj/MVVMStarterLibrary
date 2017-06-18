using System.Collections.Generic;
using System.Windows.Input;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;
using ViewActionState.Interfaces;
using ViewActionState.Types;
using ViewCommands.Interfaces;

namespace Commands.Implementation
{
    public class ViewActionService: IViewActionService 
    {
        private Dictionary<string, ICommand> _commands;
        private IDTOWrapper _source;
        private IHasViewActionState _viewStateObject;

        public Dictionary<string, ICommand> Commands
        {
            get { return _commands; }
        }

        public ViewActionService(IDTOWrapper source, IHasViewActionState viewStateObject, IConvertibleCollection target)
        {
            _source = source;
            _viewStateObject = viewStateObject;
            _commands = new Dictionary<string, ICommand>();

            _commands.Add("Delete", new DeleteCommandBase(source, target, () => CanDo(ViewActionStateType.Delete)));
            _commands.Add("Update", new UpdateCommandBase(source, target, () => CanDo(ViewActionStateType.Update)));
            _commands.Add("Create", new CreateCommandBase(source, target, () => CanDo(ViewActionStateType.Create)));
        }

        public void Notify()
        {
            foreach (ICommand command in _commands.Values)
            {
                (command as CRUDCommandBase)?.RaiseCanExecuteChanged();
            }
        }

        private bool CanDo(ViewActionStateType state)
        {
            return ViewActionStateOK(state) && SourcePresent();
        }

        private bool SourcePresent()
        {
            return _source.DataObject != null;
        }

        private bool ViewActionStateOK(ViewActionStateType state)
        {
            return _viewStateObject.ViewActionState == state;
        }
    }
}