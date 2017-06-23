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

        private IDTOWrapper _detailsViewModel;
        private IDTOWrapper _itemViewModelSelected;
        #endregion

        #region Initialisation
        protected MasterDetailsViewModelBase(ViewModelFactoryBase<TDTO> viewModelFactory, IDTOCollection dtoCollection)
        {
            // Sanity checks, so we don't need null checks elsewhere
            DTOCollection = dtoCollection ?? throw new ArgumentNullException(nameof(dtoCollection));
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            _detailsViewModel = null;
            _itemViewModelSelected = null;
        }
        #endregion

        #region IDTOWrapper implementation
        public IDTO DataObject
        {
            get { return DetailsViewModel?.DataObject; }
        }
        #endregion

        #region IMasterDetailsViewModel implementation
        public virtual ObservableCollection<IDTOWrapper> ItemViewModelCollection
        {
            get { return ViewModelFactory.CreateItemViewModelCollection(DTOCollection.AllDTO); }
        }

        public virtual IDTOWrapper ItemViewModelSelected
        {
            get { return _itemViewModelSelected; }
            set
            {
                _itemViewModelSelected = value;
                OnItemSelectionChanged(_itemViewModelSelected);
                OnPropertyChanged();
            }
        }

        public virtual IDTOWrapper DetailsViewModel
        {
            get { return _detailsViewModel; }
            set
            {
                _detailsViewModel = value;
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