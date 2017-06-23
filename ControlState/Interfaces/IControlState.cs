using Windows.UI.Xaml;

namespace ControlState.Interfaces
{
    public interface IControlState
    {
        string ID { get; }
        string Description { get; set; }
        bool Enabled { get; set; }
        Visibility VisibilityState { get; set; }
    }
}