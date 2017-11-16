using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataTransformation.Interfaces;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// Interface for a "context-free" Master/Details 
    /// ViewModel class. Only consists of three properties 
    /// relating to transformed data object types.
    /// </summary>
    public interface IMasterDetailsViewModel<TVMO> : INotifyPropertyChanged
    {
        /// <summary>
        /// Collection of transformed data objects corresponding 
        /// to underlying domain data, e.g. to be presented in a 
        /// collection-oriented GUI control.
        /// </summary>
        ObservableCollection<IDataWrapper<TVMO>> ItemCollection { get; }

        /// <summary>
        /// Item currently selected in collection-oriented GUI control.
        /// </summary>
        IDataWrapper<TVMO> ItemSelected { get; set; }

        /// <summary>
        /// Details for a specific item. This will usually be the 
        /// same item as ItemSelected refers to.
        /// </summary>
        IDataWrapper<TVMO> ItemDetails { get; set; }

        void OnPropertyChanged([CallerMemberName] string propertyName = null);
    }
}