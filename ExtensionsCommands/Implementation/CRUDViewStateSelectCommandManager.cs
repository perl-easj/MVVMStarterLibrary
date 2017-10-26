using System;
using Command.Implementation;
using ViewState.Interfaces;
using ExtensionsCommands.Types;

namespace ExtensionsCommands.Implementation
{
    /// <summary>
    /// Command manager for selecting a view state 
    /// through a command.
    /// A manager of this type needs a reference 
    /// to an object implementing IViewStateService.
    /// </summary>
    public class CRUDViewStateSelectCommandManager : CommandManager
    {
        private IViewStateService _stateService;

        public CRUDViewStateSelectCommandManager(IViewStateService stateService)
        {
            _stateService = stateService ?? throw new ArgumentException(nameof(CRUDViewStateSelectCommandManager));

            AddCommand(CRUDStateCommands.CreateStateCommand, new RelayCommand(() => { _stateService.ViewState = CRUDStates.CreateState; }, () => true));
            AddCommand(CRUDStateCommands.ReadStateCommand, new RelayCommand(() => { _stateService.ViewState = CRUDStates.ReadState; }, () => true));
            AddCommand(CRUDStateCommands.UpdateStateCommand, new RelayCommand(() => { _stateService.ViewState = CRUDStates.UpdateState; }, () => true));
            AddCommand(CRUDStateCommands.DeleteStateCommand, new RelayCommand(() => { _stateService.ViewState = CRUDStates.DeleteState; }, () => true));
        }
    }
}