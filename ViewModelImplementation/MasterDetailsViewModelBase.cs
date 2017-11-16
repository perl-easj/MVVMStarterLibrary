using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Catalog.Interfaces;
using DataTransformation.Interfaces;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    /// <summary>
    /// Base class for Master/Details ViewModel classes. 
    /// The class is kept as "context-free" as possible; 
    /// it only holds together a collection of transformed 
    /// data objects, a ViewModel object factory, and provides 
    /// properties to which a Master/Details view can bind. 
    /// Since a Master/Details ViewModel object can be used as 
    /// a source for a data-releted operation, it implements
    /// the ITransformedDataWrapper interface.
    /// </summary>
    public abstract class MasterDetailsViewModelBase<T, TVMO> : IDataWrapper<TVMO>, IMasterDetailsViewModel<TVMO>, IItemSelectionChangedEvent<TVMO>
        where TVMO : class, ITransformed<T>
    {
        #region Instance fields
        protected ICatalog<TVMO> Catalog;
        protected IViewModelFactory<TVMO> ViewModelFactory;

        private IDataWrapper<TVMO> _itemDetails;
        private IDataWrapper<TVMO> _itemSelected;
        #endregion

        #region Initialisation
        protected MasterDetailsViewModelBase(IViewModelFactory<TVMO> viewModelFactory, ICatalog<TVMO> catalog)
        {
            // Sanity checks, so we don't need null checks elsewhere
            Catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            _itemDetails = null;
            _itemSelected = null;
        }
        #endregion

        #region ITransformedDataWrapper implementation
        /// <summary>
        /// The object referred to by ItemDetails is considered 
        /// the "wrapped" transformed data object. This can be 
        /// changed in a sub-class, if needed.
        /// </summary>
        public virtual TVMO DataObject
        {
            get { return ItemDetails?.DataObject; }
        }
        #endregion

        #region IMasterDetailsViewModel implementation
        /// <summary>
        /// Creation of an item collection is delegated 
        /// to the provided ViewModel object factory.
        /// </summary>
        public virtual ObservableCollection<IDataWrapper<TVMO>> ItemCollection
        {
            get { return ViewModelFactory.CreateItemViewModelCollection(Catalog.All); }
        }

        /// <summary>
        /// Standard implementation of bindable property, 
        /// except the call to OnItemSelectionChanged. 
        /// Clients interested in knowing about selection
        /// changes get notified about this change.
        /// </summary>
        public virtual IDataWrapper<TVMO> ItemSelected
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
        public virtual IDataWrapper<TVMO> ItemDetails
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
        public event Action<IDataWrapper<TVMO>> ItemSelectionChanged;
        public virtual void OnItemSelectionChanged(IDataWrapper<TVMO> vmoWrapper)
        {
            ItemSelectionChanged?.Invoke(vmoWrapper);
        }
      
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}