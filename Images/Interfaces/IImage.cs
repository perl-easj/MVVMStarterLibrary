namespace Images.Interfaces
{
    /// <summary>
    /// Interface for an image managed internally in the application.
    /// </summary>
    public interface IImage
    {
        /// <summary>
        /// Unique identifier for the image.
        /// </summary>
        int Key { get; set; }

        /// <summary>
        /// Source for the image. This could be a 
        /// path to a file, or a URL.
        /// </summary>
        string Source { get; }

        /// <summary>
        /// Description of the image.
        /// </summary>
        string Description { get; }
    }
}