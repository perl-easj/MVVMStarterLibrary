using Command.Implementation;
using ViewState.Interfaces;
using ExtensionsCommands.Types;

namespace ExtensionsCommands.Implementation
{
    public class ViewStateCommandManager : CommandManager
    {
        private IViewStateService _stateService;

        public ViewStateCommandManager(IViewStateService stateService)
        {
            _stateService = stateService;

            AddCommand(CRUDStateCommands.CreateStateCommand, new RelayCommand(() => { _stateService.ViewState = CRUDStates.CreateState; }, () => true));
            AddCommand(CRUDStateCommands.ReadStateCommand, new RelayCommand(() => { _stateService.ViewState = CRUDStates.ReadState; }, () => true));
            AddCommand(CRUDStateCommands.UpdateStateCommand, new RelayCommand(() => { _stateService.ViewState = CRUDStates.UpdateState; }, () => true));
            AddCommand(CRUDStateCommands.DeleteStateCommand, new RelayCommand(() => { _stateService.ViewState = CRUDStates.DeleteState; }, () => true));
        }
    }
}