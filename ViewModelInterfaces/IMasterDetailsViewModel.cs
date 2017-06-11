using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using ModelClass.Interfaces;

namespace ViewModel.Interfaces
{
    public interface IMasterDetailsViewModel<T>
    {
        ObservableCollection<IDomainObjectWrapper<T>> ItemViewModelCollection { get; }
        IDomainObjectWrapper<T> ItemViewModelSelected { get; set; }
        IDomainObjectWrapper<T> DetailsViewModel { get; set; }
        bool ItemSelectorEnabled { get; }
        Visibility ItemSelectorVisible { get; }
    }
}