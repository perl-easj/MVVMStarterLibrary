using DTO.Interfaces;

namespace ViewModel.Interfaces
{
    public interface IMasterDetailsViewModelMediator
    {
        void OnItemSelectionChanged(IDTOWrapper dtoWrapper);
        void OnModelChanged();
        void OnViewStateChanged(string state);
    }
}