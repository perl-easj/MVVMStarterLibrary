using Windows.UI.Xaml;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// Interface for Item ViewModel classes
    /// containing an image
    /// </summary>
    public interface IItemViewModelImage
    {
        /// <summary>
        /// Returns the string-based image source
        /// (e.g. a file location or URL) for the
        /// image used in the view model.
        /// </summary>
        string ImageSource { get; }

        /// <summary>
        /// Returns whether or not the image should
        /// be visibile in the item view model.
        /// </summary>
        Visibility ImageVisibility { get; }

        /// <summary>
        /// Returns the image size in pixels
        /// of the image in the item view model.
        /// </summary>
        int ImageSize { get; }
    }
}