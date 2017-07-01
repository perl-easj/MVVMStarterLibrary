namespace Persistency.Interfaces
{
    /// <summary>
    /// Minimal interface for any client that wishes to let a central
    /// manager invoke certain functionality, like e.g. registering
    /// callback methods at a manager delegate, etc..
    /// </summary>
    public interface IManaged
    {
        void Manage();
    }
}