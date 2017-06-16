using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using DataClass.Interfaces;

namespace ViewModel.Interfaces
{
    public interface IMasterDetailsViewModel<T>
    {
        ObservableCollection<IDTOWrapper<T>> ItemViewModelCollection { get; }
        IDTOWrapper<T> ItemViewModelSelected { get; set; }
        IDTOWrapper<T> DetailsViewModel { get; set; }
        bool ItemSelectorEnabled { get; }
        Visibility ItemSelectorVisible { get; }
    }
}