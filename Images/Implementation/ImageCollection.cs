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
        /// Get all image objects currently in the collection.
        /// </summary>
        public List<IImage> All
        {
            get { return _images.Values.ToList(); }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Add a single image object to the collection
        /// </summary>
        /// <param name="image">
        /// Image object to add.
        /// </param>
        /// <returns>
        /// Key for image object
        /// </returns>
        public int AddImage(IImage image)
        {
            image.Key = FindNewKey();
            _images.Add(image.Key, image);
            return image.Key;
        }

        /// <summary>
        /// Sets the collection to contain the given 
        /// set of image objects. All existing image 
        /// objects in the collection should be removed.
        /// </summary>
        /// <param name="imageList">
        /// List of new image objects.
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
        /// Retrieve a single image object, 
        /// matching the given key.
        /// </summary>
        /// <param name="key">
        /// Key of image object to retrieve.
        /// </param>
        /// <param name="defaultImage">
        /// A fall-back image can be specified; 
        /// this is returned if no image in the 
        /// collection matches the given key.
        /// </param>
        /// <returns>
        /// Image object matching the key, 
        /// or fall-back image.
        /// </returns>
        public IImage Read(int key, IImage defaultImage = null)
        {
            return _images.ContainsKey(key) ? _images[key] : defaultImage;
        }

        /// <summary>
        /// Calculates the next key for a new image object.
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