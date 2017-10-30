namespace DataTransformation.Interfaces
{
    public interface IDataWrapper<out T>
    {
        /// <summary>
        /// Returns the wrapper transformed data object.
        /// Note that the return type is ITransformedData,
        /// i.e. not type-specific
        /// </summary>
        T DataObject { get; }
    }
}