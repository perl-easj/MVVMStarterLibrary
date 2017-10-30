namespace DataTransformation.Interfaces
{
    public interface ITransformedWithDefault<in T> : ITransformed<T>
    {
        /// <summary>
        /// A subclass can assign default values to
        /// the properties of the transformed data.
        /// These values can e.g. be shown as initial
        /// values in GUI controls.
        /// </summary>
        void SetDefaultValues();
    }
}