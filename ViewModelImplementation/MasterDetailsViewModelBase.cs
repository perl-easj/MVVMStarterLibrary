using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DTO.Interfaces;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    /// <summary>
    /// Base class for Master/Details ViewModel classes. The class is kept as
    /// "context-free" as possible; it only holds together a (DTO) collection
    /// and a ViewModel object factory, and provides properties to which a
    /// Master/Details view can bind. Since a Master/Details ViewModel object
    /// can be used as a source for a data-releted operation, it implements
    /// the IDTOWrapper interface.
    /// </summary>
    public abstract class MasterDetailsViewModelBase : INotifyPropertyChanged, IDTOWrapper, IMasterDetailsViewModel 
    {
        #region Instance fields
        protected IDTOCollection DTOCollection;
        protected IViewModelFactory ViewModelFactory;

        private IDTOWrapper _itemDetails;
        private IDTOWrapper _itemSelected;
        #endregion

        #region Initialisation
        protected MasterDetailsViewModelBase(IViewModelFactory viewModelFactory, IDTOCollection dtoCollection)
        {
            // Sanity checks, so we don't need null checks elsewhere
            DTOCollection = dtoCollection ?? throw new ArgumentNullException(nameof(dtoCollection));
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            _itemDetails = null;
            _itemSelected = null;
        }
        #endregion

        #region IDTOWrapper implementation
        /// <summary>
        /// The object referred to by ItemDetails is considered the "wrapped"
        /// DTO. This can be changed in a sub-class, if needed.
        /// </summary>
        public virtual IDTO DataObject
        {
            get { return ItemDetails?.DataObject; }
        }
        #endregion

        #region IMasterDetailsViewModel implementation
        /// <summary>
        /// Creation of an item collection is delegated to the provided
        /// ViewModel object factory.
        /// </summary>
        public virtual ObservableCollection<IDTOWrapper> ItemCollection
        {
            get { return ViewModelFactory.CreateItemViewModelCollection(DTOCollection.AllDTO); }
        }

        /// <summary>
        /// Standard implementation of bindable property, except the call to
        /// OnItemSelectionChanged. Clients interested in knowing about selection
        /// changes get notified about this change.
        /// </summary>
        public virtual IDTOWrapper ItemSelected
        {
            get { return _itemSelected; }
            set
            {
                _itemSelected = value;
                OnItemSelectionChanged(_itemSelected);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Standard implementation of bindable property.
        /// </summary>
        public virtual IDTOWrapper ItemDetails
        {
            get { return _itemDetails; }
            set
            {
                _itemDetails = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Event implementation
        /// <summary>
        /// Clients interested in knowing about changes in item 
        /// selection can register at this event.
        /// </summary>
        public event Action<IDTOWrapper> ItemSelectionChanged;
        public virtual void OnItemSelectionChanged(IDTOWrapper dtoWrapper)
        {
            ItemSelectionChanged?.Invoke(dtoWrapper);
        }
      
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}