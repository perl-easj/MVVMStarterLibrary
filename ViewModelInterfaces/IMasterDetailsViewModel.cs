using System.Collections.ObjectModel;
using DTO.Interfaces;

namespace ViewModel.Interfaces
{
    public interface IMasterDetailsViewModel
    {
        ObservableCollection<IDTOWrapper> ItemCollection { get; }
        IDTOWrapper ItemSelected { get; set; }
        IDTOWrapper ItemDetails { get; set; }
    }
}