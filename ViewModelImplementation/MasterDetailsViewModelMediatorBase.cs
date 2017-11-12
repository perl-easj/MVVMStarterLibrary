using System;
using DataTransformation.Interfaces;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    /// <summary>
    /// This class implements a common mediation strategy, which can
    /// be extended in derived classes.
    /// No assumtions about view states or control states are made.
    /// </summary>
    public abstract class MasterDetailsViewModelMediatorBase<T, TVMO> : IMasterDetailsViewModelMediator<TVMO> 
        where TVMO : class, ITransformed<T>
    {
        #region Instance fields
        protected MasterDetailsViewModelBase<T, TVMO> MasterDetailsViewModel;
        protected IViewModelFactory<TVMO> ViewModelFactory;
        #endregion

        #region Constructor
        protected MasterDetailsViewModelMediatorBase(
            MasterDetailsViewModelBase<T, TVMO> masterDetailsViewModel,
            IViewModelFactory<TVMO> viewModelFactory)
        {
            MasterDetailsViewModel = masterDetailsViewModel ?? throw new ArgumentNullException(nameof(MasterDetailsViewModel));
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(ViewModelFactory));
        }
        #endregion

        #region Interface implementation
        /// <summary>
        /// When the Item selection changes, these steps should be invoked:
        /// 1a) If the wrapper is null, set the Item Details to null
        /// 1b) Else: set Item Details by calling abstract method (subclasses
        ///     must specify details for setting Item Details)
        /// 2)  Notify all commands, by calling abstract method (subclasses
        ///     must specify commands to notify.
        /// </summary>
        public virtual void OnItemSelectionChanged(IDataWrapper<TVMO> tdoWrapper)
        {
            if (tdoWrapper == null)
            {
                MasterDetailsViewModel.ItemDetails = null;
            }
            else
            {
                SetItemDetailsOnItemSelectionChanged(tdoWrapper.DataObject);
            }

            NotifyCommands();
        }

        /// <summary>
        /// When the underlying catalog changes, these steps should be invoked:
        /// 1) Item selection is set to null (no selection)
        /// 2) ItemCollection property is notified, to invoke re-read by
        ///    View properties binding to this property.
        /// 3) Set Item Details, by calling abstract method (subclasses
        ///    must specify details for setting Item Details)
        /// </summary>
        public virtual void OnCatalogChanged()
        {
            MasterDetailsViewModel.ItemSelected = null;
            MasterDetailsViewModel.OnPropertyChanged(nameof(MasterDetailsViewModel.ItemCollection));

            SetItemDetailsOnCatalogChanged();
        }
        #endregion

        #region Abstract methods - override in derived classes
        /// <summary>
        /// Override this method to specify behavior for 
        /// Item Details when Item selection changes.
        /// </summary>
        /// <param name="vmObj">New selection</param>
        public abstract void SetItemDetailsOnItemSelectionChanged(TVMO vmObj);

        /// <summary>
        /// Override this method to specify behavior for 
        /// Item Details when catalog changes.
        /// </summary>
        public abstract void SetItemDetailsOnCatalogChanged();

        /// <summary>
        /// Override this method to notify a specific set
        /// of Command objects when Item selection changes.
        /// </summary>
        public abstract void NotifyCommands();
        #endregion
    }
}