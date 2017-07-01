namespace ViewModel.Interfaces
{
    /// <summary>
    /// Minimal interface for text-based item ViewModel classes
    /// </summary>
    public interface IItemViewModelDescription
    {
        string Description { get; }
        int FontSize { get; }
    }
}