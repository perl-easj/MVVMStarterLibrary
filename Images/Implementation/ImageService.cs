using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Images.Interfaces;
using Images.Types;

namespace Images.Implementation
{
    public class ImageService : IImageService
    {
        private ITaggedImageCollection _collection;
        private Dictionary<AppImageType, string> _appImages;

        public ImageService()
        {
            _collection = new TaggedImageCollection();
            _appImages = new Dictionary<AppImageType, string>();
        }

        public void SetAppImageSource(AppImageType imageType, string source)
        {
            if (_appImages.ContainsKey(imageType))
            {
                throw new ArgumentException(nameof(SetAppImageSource));
            }

            _appImages.Add(imageType, source);
        }

        public string GetAppImageSource(AppImageType imageType)
        {
            if (!_appImages.ContainsKey(imageType))
            {
                throw new ArgumentException(nameof(GetAppImageSource));
            }

            return _appImages[imageType];
        }

        public void AddImage(IImage image)
        {
            _collection.AddImage(image);
        }

        public void SetImages(List<IImage> imageList)
        {
            _collection.SetImages(imageList);
        }

        public List<IImage> All
        {
            get { return _collection.All; }
        }

        public IImage Read(int key, IImage defaultImage = null)
        {
            return _collection.Read(key, defaultImage);
        }

        public List<IImage> AllWithTag(string tag)
        {
            return _collection.AllWithTag(tag);
        }

        public List<string> AllTags
        {
            get { return _collection.AllTags; }
        }

        public ObservableCollection<IImage> GetObservableImageCollection(string tag)
        {
            var collection = new ObservableCollection<IImage>();
            AllWithTag(tag).ForEach(collection.Add);
            return collection;
        }
    }
}