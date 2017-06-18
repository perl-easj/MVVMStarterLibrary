using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Commands.Implementation;
using DTO.Interfaces;
using Persistency.Interfaces;
using ViewActionState.Interfaces;
using ViewActionState.Types;
using ViewCommands.Interfaces;
using ViewControlState.Interfaces;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    public abstract class MasterDetailsViewModelBase<T, TDTO> : 
        INotifyPropertyChanged, 
        IHasViewActionState,
        IDTOWrapper,
        IMasterDetailsViewModel 
        where TDTO : IDTO, new()
    {
        #region Instance fields
        private ICollectionAggregate<T> _collection;
        private ViewModelFactoryBase<TDTO> _viewModelFactory;

        private IDTOWrapper _detailsViewModel;
        private IDTOWrapper _itemViewModelSelected;

        private IViewActionService _actionService;
        private IViewActionStateService _actionStateService;
        private IViewControlStateService _controlStateService;
        #endregion

        #region IHasViewActionState implementation
        public ViewActionStateType ViewActionState
        {
            get { return _actionStateService.ViewActionState; }
        }

        private void ViewActionStateChanged(object sender, EventArgs eventArgs)
        {
            if (_actionStateService.ViewActionState == ViewActionStateType.Create)
            {
                DetailsViewModel = _viewModelFactory.CreateDetailsViewModelFromNew();
            }
            if (_actionStateService.ViewActionState == ViewActionStateType.Update && ItemViewModelSelected != null)
            {
                DetailsViewModel = _viewModelFactory.CreateDetailsViewModelFromExisting(ItemViewModelSelected.DataObject);
            }

            _actionService.Notify();
            OnPropertyChanged(nameof(ViewControlStates));
            OnPropertyChanged();
        }
        #endregion

        #region IDataObjectWrapper implementation
        public IDTO DataObject
        {
            get { return DetailsViewModel?.DataObject; }
        }
        #endregion

        #region IMasterDetailsViewModel implementation
        public virtual ObservableCollection<IDTOWrapper> ItemViewModelCollection
        {
            get { return _viewModelFactory.CreateItemViewModelCollection(_collection.AllDTO); }
        }

        public virtual IDTOWrapper ItemViewModelSelected
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
                    IDTO obj = _itemViewModelSelected.DataObject;
                    DetailsViewModel = (ViewActionState == ViewActionStateType.Update) ?
                        _viewModelFactory.CreateDetailsViewModelFromExisting(obj) :
                        _viewModelFactory.CreateDetailsViewModel(obj);
                }

                _actionService.Notify();
                OnPropertyChanged(nameof(DetailsViewModel));
                OnPropertyChanged();
            }
        }

        public virtual IDTOWrapper DetailsViewModel
        {
            get { return _detailsViewModel; }
            set
            {
                _detailsViewModel = value;
                _actionService.Notify();
                OnPropertyChanged();
            }
        }
        #endregion

        #region Properties for view binding and derived classes
        public virtual IViewControlStateService ControlStateService
        {
            get { return _controlStateService; }
        }

        public virtual Dictionary<string, IViewControlState> ViewControlStates
        {
            get { return _controlStateService.GetViewControlStates(ViewActionState); }
        }
        #endregion

        #region Initialisation
        protected MasterDetailsViewModelBase(
            ViewModelFactoryBase<TDTO> viewModelFactory,
            ICollectionAggregate<T> collection,
            IViewActionStateService actionStateService,
            IViewControlStateService controlStateService)
        {
            // Sanity checks, so we don't need null checks elsewhere
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            _collection.OnObjectCreated += AfterModelModified;
            _collection.OnObjectUpdated += AfterModelModified;
            _collection.OnObjectDeleted += AfterModelModified;

            _detailsViewModel = null;
            _itemViewModelSelected = null;

            _controlStateService = controlStateService;
            _actionStateService = actionStateService;
            _actionStateService.OnViewActionStateChanged += ViewActionStateChanged;

            _actionService = new ViewActionService(this, this, _collection);
        }
        #endregion

        #region ICommand properties
        public virtual Dictionary<string, ICommand> ViewActionCommand
        {
            get { return _actionService.Commands; }
        }

        public virtual Dictionary<string, ICommand> ViewActionStateCommand
        {
            get { return _actionStateService.Commands; }
        }
        #endregion

        #region AfterModelModified code (called on collection change events)
        private void AfterModelModified(object sender, EventArgs eventArgs)
        {
            ItemViewModelSelected = null;
            OnPropertyChanged(nameof(ItemViewModelCollection));

            if (ViewActionState == ViewActionStateType.Create)
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