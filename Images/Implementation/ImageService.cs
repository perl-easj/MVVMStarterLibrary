using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Images.Interfaces;
using Images.Types;

namespace Images.Implementation
{
    /// <summary>
    /// Implementation of the IImageService interface
    /// </summary>
    public class ImageService : IImageService
    {
        #region Instance fields
        private ITaggedImageCollection _collection;
        private Dictionary<AppImageType, string> _appImages;
        #endregion

        #region Constructor
        public ImageService()
        {
            _collection = new TaggedImageCollection();
            _appImages = new Dictionary<AppImageType, string>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get all Image objects currently in the collection.
        /// </summary>
        public List<IImage> All
        {
            get { return _collection.All; }
        }

        /// <summary>
        /// Returns the union of all tags for all Image objects.
        /// </summary>
        public List<string> AllTags
        {
            get { return _collection.AllTags; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the image source for a given application-wide image type.
        /// If the image source has already been set for the given image
        /// type, an exception is thrown.
        /// </summary>
        /// <param name="imageType">
        /// Image type to which the given image is used.
        /// </param>
        /// <param name="source">
        /// Specific source for image.
        /// </param>
        public void SetAppImageSource(AppImageType imageType, string source)
        {
            if (_appImages.ContainsKey(imageType))
            {
                throw new ArgumentException(nameof(SetAppImageSource));
            }

            _appImages.Add(imageType, source);
        }

        /// <summary>
        /// Retrieves the image source for a given application-wide image type.
        /// If the source has not been set for the given image type, an
        /// exception is thrown.
        /// </summary>
        /// <param name="imageType">
        /// Image type for which to retrieve the image source.
        /// </param>
        /// <returns>
        /// Image source.
        /// </returns>
        public string GetAppImageSource(AppImageType imageType)
        {
            if (!_appImages.ContainsKey(imageType))
            {
                throw new ArgumentException(nameof(GetAppImageSource));
            }

            return _appImages[imageType];
        }

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
            return _collection.AddImage(image);
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
            _collection.SetImages(imageList);
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
            return _collection.Read(key, defaultImage);
        }

        /// <summary>
        /// Retrieves all Image objects tagged with the given tag.
        /// </summary>
        /// <param name="tag">
        /// Tag used for selecting Image objects.
        /// </param>
        /// <returns>
        /// List of Image objects tagged with the given tag.
        /// </returns>
        public List<IImage> AllWithTag(string tag)
        {
            return _collection.AllWithTag(tag);
        }

        /// <summary>
        /// Returns the set of Image objects tagged with the given tag,
        /// in the form of an ObservableCollection.
        /// </summary>
        /// <param name="tag">
        /// Tag used to select Image objects.
        /// </param>
        /// <returns>
        /// ObservableCollection of Image objects tagged with the given tag.
        /// </returns>
        public ObservableCollection<IImage> GetObservableImageCollection(string tag)
        {
            var collection = new ObservableCollection<IImage>();
            AllWithTag(tag).ForEach(collection.Add);
            return collection;
        }
        #endregion
    }
}