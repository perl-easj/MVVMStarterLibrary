using Windows.UI.Xaml;

namespace ViewControlState.Interfaces
{
    public interface IViewControlState
    {
        string ID { get; }
        string Description { get; set; }
        bool Enabled { get; set; }
        Visibility VisibilityState { get; set; }
    }
}