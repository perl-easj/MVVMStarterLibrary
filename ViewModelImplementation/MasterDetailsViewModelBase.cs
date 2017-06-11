using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml;
using Commands.Implementation;
using InMemoryStorage.Interfaces;
using ModelClass.Implementation;
using ModelClass.Interfaces;
using ViewActionState.Interfaces;
using ViewActionState.Types;
using ViewControlState.Implementation;
using ViewControlState.Interfaces;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    /// <summary>
    /// This class is a base class for any colllection-oriented view model, 
    /// i.e. a view model that wraps a collection of domain objects.
    /// </summary>
    /// <typeparam name="TDomainClass">
    /// Type of domain objects wrapped by the class.
    /// </typeparam>
    public abstract class MasterDetailsViewModelBase<TDomainClass> : 
        INotifyPropertyChanged, 
        IHasActionViewState,
        IDomainObjectWrapper<TDomainClass>,
        IMasterDetailsViewModel<TDomainClass>
        where TDomainClass : DomainClassBase, new()
    {
        #region Instance fields
        private IObservableInMemoryCollection<TDomainClass> _collection;
        private ViewModelFactoryBase<TDomainClass> _viewModelFactory;

        private IDomainObjectWrapper<TDomainClass> _detailsViewModel;
        private IDomainObjectWrapper<TDomainClass> _itemViewModelSelected;

        private ViewActionStateType _viewState;
        private IViewControlStateService _stateService;

        private CRUDCommandBase<TDomainClass> _deleteCommand;
        private CRUDCommandBase<TDomainClass> _updateCommand;
        private CRUDCommandBase<TDomainClass> _createCommand;

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
                    DetailsViewModel = _viewModelFactory.CreateDetailsViewModelFromClone(ItemViewModelSelected.DomainObject);
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
        public virtual ObservableCollection<IDomainObjectWrapper<TDomainClass>> ItemViewModelCollection
        {
            get { return _viewModelFactory.CreateItemViewModelCollection(_collection); }
        }

        public virtual IDomainObjectWrapper<TDomainClass> ItemViewModelSelected
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
                    TDomainClass obj = _itemViewModelSelected.DomainObject;
                    DetailsViewModel = (ActionViewState == ViewActionStateType.Update) ?
                        _viewModelFactory.CreateDetailsViewModelFromClone(obj) :
                        _viewModelFactory.CreateDetailsViewModel(obj);
                }

                NotifyCommands();
                OnPropertyChanged(nameof(DetailsViewModel));
                OnPropertyChanged();
            }
        }

        public virtual IDomainObjectWrapper<TDomainClass> DetailsViewModel
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

        #region IDomainObjectWrapper implementation
        public TDomainClass DomainObject
        {
            get
            {
                return DetailsViewModel?.DomainObject;
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
            ViewModelFactoryBase<TDomainClass> viewModelFactory,
            IObservableInMemoryCollection<TDomainClass> catalog)
        {
            // Sanity checks, so we don't need null checks elsewhere
            _collection = catalog ?? throw new ArgumentNullException(nameof(catalog));
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
            _deleteCommand = new DeleteCommandBase<TDomainClass>(this, this, _collection);
            _updateCommand = new UpdateCommandBase<TDomainClass>(this, this, _collection);
            _createCommand = new CreateCommandBase<TDomainClass>(this, this, _collection);
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