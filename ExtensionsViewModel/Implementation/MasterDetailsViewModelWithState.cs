using System;
using System.Collections.Generic;
using Command.Interfaces;
using ControlState.Interfaces;
using DTO.Interfaces;
using InMemoryStorage.Interfaces;
using ViewModel.Implementation;
using ViewModel.Interfaces;
using ViewState.Interfaces;

namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// This class adds several functionalities to the Master/Details base class.
    /// 1) View state service
    /// 2) Control state service
    /// 3) Data commands (commands for invoking CRUD operations)
    /// 4) State commands (commands for setting the view in a specific view state)
    /// 5) A Mediator implementation, which manages the actions performed when a
    ///    certain aspect of the object state changes (i,e, a change in collection, 
    ///    item selection, or view state).
    /// </summary>
    public abstract class MasterDetailsViewModelWithState : 
        MasterDetailsViewModelBase, 
        IHasViewState, 
        IHasControlStates
    {
        #region Instance fields
        protected IMonitorable _collection;

        private IControlStateService _controlStateService;
        private IViewStateService _viewStateService;

        private ICommandManager _dataCommands;
        private ICommandManager _stateCommands;

        private IMasterDetailsViewModelWithStateMediator _mediator; 
        #endregion

        #region Constructor
        protected MasterDetailsViewModelWithState(
            IViewModelFactory viewModelFactory,
            IMonitorable collection,
            IDTOCollection dtoCollection)
            : base(viewModelFactory, dtoCollection)
        {
            // Sanity checks, to avoid null-checking later.
            _collection = collection ?? throw new ArgumentNullException(nameof(MasterDetailsViewModelWithState));
            _mediator = new MasterDetailsViewModelWithStateMediator(this, viewModelFactory);

            // Let Mediator be notified when collection changes.
            _collection.AddOnObjectCreatedCallback(_mediator.OnModelChanged);
            _collection.AddOnObjectUpdatedCallback(_mediator.OnModelChanged);
            _collection.AddOnObjectDeletedCallback(_mediator.OnModelChanged);

            // Let Mediator be notified when item selection changes.
            ItemSelectionChanged += _mediator.OnItemSelectionChanged;

            // These are set in sub-classes, since they involve references to the
            // Master/Details ViewModel object itself.
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
            protected set
            {
                _viewStateService = value;
                _viewStateService.ViewState = "ReadState";

                // Let Mediator be notified when view state changes.
                _viewStateService.ViewStateChanged += _mediator.OnViewStateChanged;
            }
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