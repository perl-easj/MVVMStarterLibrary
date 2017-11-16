using System.Collections.Generic;
using Catalog.Interfaces;
using Command.Interfaces;
using ControlState.Interfaces;
using DataTransformation.Interfaces;
using ViewModel.Implementation;
using ViewModel.Interfaces;
using ViewState.Interfaces;

namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// This class adds further properties to the 
    /// Master/Details base class.
    /// 1) View state service
    /// 2) Control state service
    /// 3) Data commands (commands for invoking data-related operations)
    /// 4) State commands (commands for setting the view in a specific view state)
    /// Note that the class does not itself choose specific services/commands
    /// </summary>
    public abstract class MasterDetailsViewModelWithState<T, TVMO> : MasterDetailsViewModelBase<T, TVMO>, IHasViewState 
        where TVMO : class, ITransformed<T>
    {
        #region Instance fields
        private IControlStateService _controlStateService;
        private IViewStateService _viewStateService;

        private ICommandManager _dataCommands;
        private ICommandManager _stateCommands;
        #endregion

        #region Constructor
        protected MasterDetailsViewModelWithState(IViewModelFactory<TVMO> viewModelFactory, ICatalog<TVMO> catalog)
            : base(viewModelFactory, catalog)
        {
            // These will be initialised in sub-classes,
            // with specific services/commands
            _controlStateService = null;
            _viewStateService = null;
            _dataCommands = null;
            _stateCommands = null;
        } 
        #endregion

        #region Exposure of State servies (View and Control)
        public IControlStateService ControlStateService
        {
            get { return _controlStateService; }
            protected set { _controlStateService = value; }
        }

        public IViewStateService ViewStateService
        {
            get { return _viewStateService; }
            protected set { _viewStateService = value; }
        } 
        #endregion

        #region Exposure of Command managers (State and Data)
        public ICommandManager DataCommandManager
        {
            get { return _dataCommands; }
            protected set { _dataCommands = value; }
        }

        public ICommandManager StateCommandManager
        {
            get { return _stateCommands; }
            protected set { _stateCommands = value; }
        } 
        #endregion

        #region Exposure of Commands (view state selection and data operations)
        public Dictionary<string, INotifiableCommand> StateCommand
        {
            get { return _stateCommands.Commands; }
        }

        public Dictionary<string, INotifiableCommand> DataCommand
        {
            get { return _dataCommands.Commands; }
        }
        #endregion

        #region Exposure of State (view state and control state)
        public Dictionary<string, IControlState> ControlStates
        {
            get { return _controlStateService.GetControlStates(ViewState); }
        }

        public string ViewState
        {
            get { return _viewStateService.ViewState; }
        }
        #endregion
    }
}