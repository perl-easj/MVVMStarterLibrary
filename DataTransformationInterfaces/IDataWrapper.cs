namespace DataTransformation.Interfaces
{
    /// <summary>
    /// Interface for objects which "wrap" a data-carrying object.
    /// This will typically be a transformed domain object.
    /// </summary>
    public interface IDataWrapper<out TTDO>
    {
        /// <summary>
        /// Returns the wrapped transformed domain object.
        /// </summary>
        TTDO DataObject { get; }
    }
}