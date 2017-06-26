using DTO.Interfaces;
using ExtensionsCommands.Types;
using ViewModel.Implementation;
using ViewModel.Interfaces;

namespace ExtensionsViewModel.Implementation
{
    public class MasterDetailsViewModelMediator<TDTO> : IMasterDetailsViewModelMediator 
        where TDTO : IDTO, new()
    {
        private MasterDetailsViewModelWithState<TDTO> _masterDetailsViewModel;
        private ViewModelFactoryBase<TDTO> _viewModelFactory;

        public MasterDetailsViewModelMediator(
            MasterDetailsViewModelWithState<TDTO> masterDetailsViewModel,
            ViewModelFactoryBase<TDTO> viewModelFactory)
        {
            _masterDetailsViewModel = masterDetailsViewModel;
            _viewModelFactory = viewModelFactory;
        }

        public void OnItemSelectionChanged(IDTOWrapper dtoWrapper)
        {
            if (dtoWrapper == null)
            {
                _masterDetailsViewModel.ItemDetails = null;
            }
            else
            {
                _masterDetailsViewModel.ItemDetails = (_masterDetailsViewModel.ViewState == CRUDStates.UpdateState) ?
                    _viewModelFactory.CreateDetailsViewModelFromExisting(dtoWrapper.DataObject) :
                    _viewModelFactory.CreateDetailsViewModel(dtoWrapper.DataObject);
            }

            NotifyCommands();
        }

        public void OnModelChanged()
        {
            _masterDetailsViewModel.ItemSelected = null;
            _masterDetailsViewModel.OnPropertyChanged(nameof(_masterDetailsViewModel.ItemCollection));

            if (_masterDetailsViewModel.ViewState == CRUDStates.CreateState)
            {
                _masterDetailsViewModel.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromNew();
            }
        }

        public void OnViewStateChanged(string state)
        {
            // Set DetailsViewModel according to new state
            if (_masterDetailsViewModel.ViewState == CRUDStates.CreateState)
            {
                _masterDetailsViewModel.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromNew();
            }
            if (_masterDetailsViewModel.ViewState == CRUDStates.UpdateState && _masterDetailsViewModel.ItemSelected != null)
            {
                _masterDetailsViewModel.ItemDetails = _viewModelFactory.CreateDetailsViewModelFromExisting(_masterDetailsViewModel.ItemSelected.DataObject);
            }

            // All commands are notified
            NotifyCommands();

            // Control states should be re-read
            _masterDetailsViewModel.OnPropertyChanged(nameof(_masterDetailsViewModel.ControlStates));
        }

        private void NotifyCommands()
        {
            // All commands are notified
            _masterDetailsViewModel.DataCommandManager.Notify();
            _masterDetailsViewModel.StateCommandManager.Notify();
        }
    }
}