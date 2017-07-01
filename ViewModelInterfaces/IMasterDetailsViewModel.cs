using System.Collections.ObjectModel;
using DTO.Interfaces;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// Interface for a "context-free" Master/Details ViewModel class.
    /// Only consists of three DTO-based properties.
    /// </summary>
    public interface IMasterDetailsViewModel
    {
        /// <summary>
        /// Collection of DTOs corresponding to underlying data,
        /// e.g. to be presented in a collection-oriented GUI control.
        /// </summary>
        ObservableCollection<IDTOWrapper> ItemCollection { get; }

        /// <summary>
        /// Item currently selected.
        /// </summary>
        IDTOWrapper ItemSelected { get; set; }

        /// <summary>
        /// Details for a specific item. This will usually be the 
        /// same item as ItemSelected refers to.
        /// </summary>
        IDTOWrapper ItemDetails { get; set; }
    }
}