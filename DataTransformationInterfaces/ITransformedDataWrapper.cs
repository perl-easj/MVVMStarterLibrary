namespace DataTransformation.Interfaces
{
    /// <summary>
    /// Minimal interface for a class wrapping a transformed
    /// data object. This could e.g. be a view model class.
    /// </summary>
    public interface ITransformedDataWrapper
    {
        /// <summary>
        /// Returns the wrapper transformed data object.
        /// Note that the return type is ITransformedData,
        /// i.e. not type-specific
        /// </summary>
        ITransformedData DataObject { get; }
    }
}