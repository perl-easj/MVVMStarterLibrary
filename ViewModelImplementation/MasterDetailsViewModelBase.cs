using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DTO.Interfaces;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    public abstract class MasterDetailsViewModelBase<TDTO> : INotifyPropertyChanged, IDTOWrapper, IMasterDetailsViewModel 
        where TDTO : IDTO, new()
    {
        #region Instance fields
        protected IDTOCollection DTOCollection;
        protected ViewModelFactoryBase<TDTO> ViewModelFactory;

        private IDTOWrapper _itemDetails;
        private IDTOWrapper _itemSelected;
        #endregion

        #region Initialisation
        protected MasterDetailsViewModelBase(ViewModelFactoryBase<TDTO> viewModelFactory, IDTOCollection dtoCollection)
        {
            // Sanity checks, so we don't need null checks elsewhere
            DTOCollection = dtoCollection ?? throw new ArgumentNullException(nameof(dtoCollection));
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            _itemDetails = null;
            _itemSelected = null;
        }
        #endregion

        #region IDTOWrapper implementation
        public IDTO DataObject
        {
            get { return ItemDetails?.DataObject; }
        }
        #endregion

        #region IMasterDetailsViewModel implementation
        public virtual ObservableCollection<IDTOWrapper> ItemCollection
        {
            get { return ViewModelFactory.CreateItemViewModelCollection(DTOCollection.AllDTO); }
        }

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