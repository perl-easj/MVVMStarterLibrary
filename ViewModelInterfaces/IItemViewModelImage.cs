using Windows.UI.Xaml;

namespace ViewModel.Interfaces
{
    public interface IItemViewModelImage
    {
        string ImageSource { get; }
        Visibility ImageVisibility { get; }
        int ImageSize { get; }
    }
}