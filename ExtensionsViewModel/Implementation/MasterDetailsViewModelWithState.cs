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
    public abstract class MasterDetailsViewModelWithState<TDTO> : 
        MasterDetailsViewModelBase<TDTO>, 
        IHasViewState, 
        IHasControlStates
        where TDTO : IDTO, new()
    {
        protected IMonitorable _collection;

        private IControlStateService _controlStateService;
        private IViewStateService _viewStateService;

        private ICommandManager _dataCommands;
        private ICommandManager _stateCommands;

        private IMasterDetailsViewModelMediator _masterDetailsViewModelMediator;

        protected MasterDetailsViewModelWithState(
            ViewModelFactoryBase<TDTO> viewModelFactory, 
            IMonitorable collection, 
            IDTOCollection dtoCollection) 
            : base(viewModelFactory, dtoCollection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));

            _masterDetailsViewModelMediator = new MasterDetailsViewModelMediator<TDTO>(this, viewModelFactory);

            _collection.AddOnObjectCreatedCallback(_masterDetailsViewModelMediator.OnModelChanged);
            _collection.AddOnObjectUpdatedCallback(_masterDetailsViewModelMediator.OnModelChanged);
            _collection.AddOnObjectDeletedCallback(_masterDetailsViewModelMediator.OnModelChanged);

            ItemSelectionChanged += _masterDetailsViewModelMediator.OnItemSelectionChanged;

            _controlStateService = null;
            _viewStateService = null;

            _dataCommands = null;
            _stateCommands = null;
        }

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
                _viewStateService.ViewStateChanged += _masterDetailsViewModelMediator.OnViewStateChanged;
            }
        }

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

        public Dictionary<string, INotifiableCommand> StateCommand
        {
            get { return _stateCommands.Commands; }
        }

        public Dictionary<string, INotifiableCommand> DataCommand
        {
            get { return _dataCommands.Commands; }
        }

        public Dictionary<string, IControlState> ControlStates
        {
            get { return _controlStateService.GetControlStates(ViewState); }
        }

        public string ViewState
        {
            get { return _viewStateService.ViewState; }
        }
    }
}