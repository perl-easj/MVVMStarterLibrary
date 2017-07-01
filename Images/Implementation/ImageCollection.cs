using System.Collections.Generic;
using System.Linq;
using Images.Interfaces;

namespace Images.Implementation
{
    /// <summary>
    /// Implementation of the IImageCollection interface. 
    /// Image objects are stored in a Dictionary.
    /// </summary>
    public class ImageCollection : IImageCollection
    {
        private Dictionary<int, IImage> _images;

        public ImageCollection()
        {
            _images = new Dictionary<int, IImage>();
        }

        #region Properties
        /// <summary>
        /// Get all Image objects currently in the collection.
        /// </summary>
        public List<IImage> All
        {
            get { return _images.Values.ToList(); }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Add a single Image object to the collection
        /// </summary>
        /// <param name="image">
        /// Image object to add.
        /// </param>
        /// <returns>
        /// Key for Image object
        /// </returns>
        public int AddImage(IImage image)
        {
            image.Key = FindNewKey();
            _images.Add(image.Key, image);
            return image.Key;
        }

        /// <summary>
        /// Sets the collection to contain the given set of Image objects.
        /// All existing Image objects in the collection should be removed.
        /// </summary>
        /// <param name="imageList">
        /// List of new Image objects.
        /// </param>
        public void SetImages(List<IImage> imageList)
        {
            _images.Clear();
            foreach (IImage image in imageList)
            {
                AddImage(image);
            }
        }

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
        public IImage Read(int key, IImage defaultImage = null)
        {
            return _images.ContainsKey(key) ? _images[key] : defaultImage;
        }

        /// <summary>
        /// Calculates the next key for a new Image object.
        /// </summary>
        /// <returns>
        /// New key value.
        /// </returns>
        private int FindNewKey()
        {
            return _images.Count;
        } 
        #endregion
    }
}