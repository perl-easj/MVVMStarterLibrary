using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml;
using Commands.Implementation;
using DataClass.Implementation;
using DataClass.Interfaces;
using InMemoryStorage.Interfaces;
using ViewActionState.Interfaces;
using ViewActionState.Types;
using ViewControlState.Implementation;
using ViewControlState.Interfaces;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    public abstract class MasterDetailsViewModelBase<TDTO> : 
        INotifyPropertyChanged, 
        IHasActionViewState,
        IDTOWrapper<TDTO>,
        IMasterDetailsViewModel<TDTO>
        where TDTO : DTOBaseWithKey, new()
    {
        #region Instance fields
        private IConvertibleObservableInMemoryCollection<TDTO> _collection;
        private ViewModelFactoryBase<TDTO> _viewModelFactory;

        private IDTOWrapper<TDTO> _detailsViewModel;
        private IDTOWrapper<TDTO> _itemViewModelSelected;

        private ViewActionStateType _viewState;
        private IViewControlStateService _stateService;

        private CRUDCommandBase<TDTO> _deleteCommand;
        private CRUDCommandBase<TDTO> _updateCommand;
        private CRUDCommandBase<TDTO> _createCommand;

        private RelayCommand _selectCreateCommand;
        private RelayCommand _selectReadCommand;
        private RelayCommand _selectUpdateCommand;
        private RelayCommand _selectDeleteCommand;
        #endregion

        #region IHasActionViewState implementation
        public ViewActionStateType ActionViewState
        {
            get { return _viewState; }
            set
            {
                _viewState = value;

                if (_viewState == ViewActionStateType.Create)
                {
                    DetailsViewModel = _viewModelFactory.CreateDetailsViewModelFromNew();
                }
                if (_viewState == ViewActionStateType.Update && ItemViewModelSelected != null)
                {
                    DetailsViewModel = _viewModelFactory.CreateDetailsViewModelFromExisting(ItemViewModelSelected.DataObject);
                }

                NotifyCommands();
                OnPropertyChanged(nameof(ViewControlStates));
                OnPropertyChanged(nameof(ItemSelectorEnabled));
                OnPropertyChanged(nameof(ItemSelectorVisible));
                OnPropertyChanged();
            }
        }
        #endregion

        #region IMasterDetailsViewModel implementation
        public virtual ObservableCollection<IDTOWrapper<TDTO>> ItemViewModelCollection
        {
            get { return _viewModelFactory.CreateItemViewModelCollection(_collection.AllDTO); }
        }

        public virtual IDTOWrapper<TDTO> ItemViewModelSelected
        {
            get { return _itemViewModelSelected; }
            set
            {
                _itemViewModelSelected = value;

                if (_itemViewModelSelected == null)
                {
                    DetailsViewModel = null;
                }
                else
                {
                    TDTO obj = _itemViewModelSelected.DataObject;
                    DetailsViewModel = (ActionViewState == ViewActionStateType.Update) ?
                        _viewModelFactory.CreateDetailsViewModelFromExisting(obj) :
                        _viewModelFactory.CreateDetailsViewModel(obj);
                }

                NotifyCommands();
                OnPropertyChanged(nameof(DetailsViewModel));
                OnPropertyChanged();
            }
        }

        public virtual IDTOWrapper<TDTO> DetailsViewModel
        {
            get { return _detailsViewModel; }
            set
            {
                _detailsViewModel = value;
                NotifyCommands();
                OnPropertyChanged();
            }
        }

        public virtual bool ItemSelectorEnabled
        {
            get { return (ActionViewState != ViewActionStateType.Create); }
        }

        public virtual Visibility ItemSelectorVisible
        {
            get { return Visibility.Visible; }
        }
        #endregion

        #region IDataObjectWrapper implementation
        public TDTO DataObject
        {
            get
            {
                return DetailsViewModel?.DataObject;
            }
        }
        #endregion

        #region Properties for view binding
        public virtual IViewControlStateService StateService
        {
            get { return _stateService; }
        }

        public virtual Dictionary<string, IViewControlState> ViewControlStates
        {
            get { return _stateService.GetViewControlStates(ActionViewState); }
        }
        #endregion

        #region Initialisation
        protected MasterDetailsViewModelBase(
            ViewModelFactoryBase<TDTO> viewModelFactory,
            IConvertibleObservableInMemoryCollection<TDTO> collection)
        {
            // Sanity checks, so we don't need null checks elsewhere

            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            _collection.OnObjectCreated += AfterModelModified;
            _collection.OnObjectUpdated += AfterModelModified;
            _collection.OnObjectDeleted += AfterModelModified;

            _detailsViewModel = null;
            _itemViewModelSelected = null;

            _stateService = new ViewControlStateService();
            _viewState = ViewActionStateType.Read;

            SetupViewStateCommands();
            SetupViewActionControllers();
        }

        protected virtual void SetupViewStateCommands()
        {
            _selectCreateCommand = new RelayCommand(() => { ActionViewState = ViewActionStateType.Create; }, () => true);
            _selectReadCommand = new RelayCommand(() => { ActionViewState = ViewActionStateType.Read; }, () => true);
            _selectUpdateCommand = new RelayCommand(() => { ActionViewState = ViewActionStateType.Update; }, () => true);
            _selectDeleteCommand = new RelayCommand(() => { ActionViewState = ViewActionStateType.Delete; }, () => true);
        }

        protected virtual void SetupViewActionControllers()
        {
            _deleteCommand = new DeleteCommandBase<TDTO>(this, this, _collection);
            _updateCommand = new UpdateCommandBase<TDTO>(this, this, _collection);
            _createCommand = new CreateCommandBase<TDTO>(this, this, _collection);
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Notifies all commands that the CanExecute
        /// status may have changed.
        /// </summary>
        protected virtual void NotifyCommands()
        {
            _createCommand.RaiseCanExecuteChanged();
            _updateCommand.RaiseCanExecuteChanged();
            _deleteCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #region ICommand action controller properties
        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
        }

        public ICommand UpdateCommand
        {
            get { return _updateCommand; }
        }

        public ICommand CreateCommand
        {
            get { return _createCommand; }
        }
        #endregion

        #region ICommand state properties
        public ICommand SelectCreateCommand
        {
            get { return _selectCreateCommand; }
        }

        public ICommand SelectReadCommand
        {
            get { return _selectReadCommand; }
        }

        public ICommand SelectUpdateCommand
        {
            get { return _selectUpdateCommand; }
        }

        public ICommand SelectDeleteCommand
        {
            get { return _selectDeleteCommand; }
        }
        #endregion

        #region AfterModelModified code (called on Catalog change events)
        private void AfterModelModified(object sender, EventArgs eventArgs)
        {
            ItemViewModelSelected = null;
            OnPropertyChanged(nameof(ItemViewModelCollection));

            if (ActionViewState == ViewActionStateType.Create)
            {
                DetailsViewModel = _viewModelFactory.CreateDetailsViewModelFromNew();
            }
        }
        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}