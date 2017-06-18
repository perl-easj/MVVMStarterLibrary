using System.Collections.ObjectModel;
using DTO.Interfaces;

namespace ViewModel.Interfaces
{
    public interface IMasterDetailsViewModel
    {
        ObservableCollection<IDTOWrapper> ItemViewModelCollection { get; }
        IDTOWrapper ItemViewModelSelected { get; set; }
        IDTOWrapper DetailsViewModel { get; set; }
    }
}