namespace ViewModel.Interfaces
{
    /// <summary>
    /// Interface for Item ViewModel classes
    /// containing a text description.
    /// </summary>
    public interface IItemViewModelDescription
    {
        /// <summary>
        /// Returns the text description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Returns the font size of the text
        /// description in the item view model.
        /// </summary>
        int FontSize { get; }
    }
}