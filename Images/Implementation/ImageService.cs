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
        /// Get all image objects currently in the collection.
        /// </summary>
        public List<IImage> All
        {
            get { return _collection.All; }
        }

        /// <summary>
        /// Returns the union of all tags for all image objects.
        /// </summary>
        public List<string> AllTags
        {
            get { return _collection.AllTags; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the image source for a given 
        /// application-wide image type. If the image 
        /// source has already been set for the given 
        /// image type, an exception is thrown.
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
        /// Retrieves the image source for a given 
        /// application-wide image type. If the source 
        /// has not been set for the given image type,
        /// an exception is thrown.
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
            return _collection.AddImage(image);
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
            _collection.SetImages(imageList);
        }

        /// <summary>
        /// Retrieve a single image object, 
        /// matching the given key.
        /// </summary>
        /// <param name="key">
        /// Key of image objects to retrieve.
        /// </param>
        /// <param name="defaultImage">
        /// A fall-back image can be specified; 
        /// this is returned if no image in the 
        /// collection matches the given key.
        /// </param>
        /// <returns>
        /// Image object matching the key, or fall-back image
        /// </returns>
        public IImage Read(int key, IImage defaultImage = null)
        {
            return _collection.Read(key, defaultImage);
        }

        /// <summary>
        /// Retrieves all image objects tagged with the given tag.
        /// </summary>
        /// <param name="tag">
        /// Tag used for selecting image objects.
        /// </param>
        /// <returns>
        /// List of image objects tagged with the given tag.
        /// </returns>
        public List<IImage> AllWithTag(string tag)
        {
            return _collection.AllWithTag(tag);
        }

        /// <summary>
        /// Returns the set of image objects 
        /// tagged with the given tag, in the form 
        /// of an ObservableCollection.
        /// </summary>
        /// <param name="tag">
        /// Tag used to select image objects.
        /// </param>
        /// <returns>
        /// ObservableCollection of image objects tagged with the given tag.
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