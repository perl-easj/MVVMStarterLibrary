namespace DataTransformation.Interfaces
{
    /// <summary>
    /// Interface for objects having meaningfful default values.
    /// This will typically be VMO classes, where default values
    /// are shown in a corresponding view.
    /// </summary>
    public interface IDefaultValues
    {
        /// <summary>
        /// A subclass can assign default values to
        /// the properties of the transformed data.
        /// </summary>
        void SetDefaultValues();
    }
}