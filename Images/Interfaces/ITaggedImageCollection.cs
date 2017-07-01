using System.Collections.Generic;

namespace Images.Interfaces
{
    /// <summary>
    /// This interface extends the IImageCollection interface with
    /// functionality for tagged images.
    /// </summary>
    public interface ITaggedImageCollection : IImageCollection
    {
        /// <summary>
        /// Retrieves all Image objects tagged with the given tag.
        /// </summary>
        /// <param name="tag">
        /// Tag used for selecting Image objects.
        /// </param>
        /// <returns>
        /// List of Image objects tagged with the given tag.
        /// </returns>
        List<IImage> AllWithTag(string tag);

        /// <summary>
        /// Returns the union of all tags for all Image objects.
        /// </summary>
        List<string> AllTags { get; }
    }
}