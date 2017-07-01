using System.Collections.Generic;

namespace Images.Interfaces
{
    /// <summary>
    /// Interface for a collection of Image objects
    /// </summary>
    public interface IImageCollection
    {
        /// <summary>
        /// Add a single Image object to the collection
        /// </summary>
        /// <param name="image">
        /// Image object to add.
        /// </param>
        /// <returns>
        /// Key for Image object
        /// </returns>
        int AddImage(IImage image);

        /// <summary>
        /// Sets the collection to contain the given set of Image objects.
        /// All existing Image objects in the collection should be removed.
        /// </summary>
        /// <param name="imageList">
        /// List of new Image objects.
        /// </param>
        void SetImages(List<IImage> imageList);

        /// <summary>
        /// Get all Image objects currently in the collection.
        /// </summary>
        List<IImage> All { get; }

        /// <summary>
        /// Retrieve a single Image object, matching the given key.
        /// </summary>
        /// <param name="key">
        /// Key of Image objects to retrieve.
        /// </param>
        /// <param name="defaultImage">
        /// A fall-back Image can be specified; this is returned if 
        /// no Image in the collection matches the given key.
        /// </param>
        /// <returns>
        /// Image object matching the key, or fall-back Image
        /// </returns>
        IImage Read(int key, IImage defaultImage = null);
    }
}