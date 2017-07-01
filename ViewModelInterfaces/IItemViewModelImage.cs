using Windows.UI.Xaml;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// Minimal interface for image-based item ViewModel classes
    /// </summary>
    public interface IItemViewModelImage
    {
        string ImageSource { get; }
        Visibility ImageVisibility { get; }
        int ImageSize { get; }
    }
}