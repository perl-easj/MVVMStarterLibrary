using System.Collections.Generic;
using System.Linq;
using Images.Interfaces;

namespace Images.Implementation
{
    public class ImageCollection : IImageCollection
    {
        private Dictionary<int, IImage> _images;

        public ImageCollection()
        {
            _images = new Dictionary<int, IImage>();
        }

        public void AddImage(IImage image)
        {
            image.Key = FindNewKey();
            _images.Add(image.Key, image);
        }

        public void SetImages(List<IImage> imageList)
        {
            _images.Clear();
            foreach (IImage image in imageList)
            {
                AddImage(image);
            }
        }

        public List<IImage> All
        {
            get { return _images.Values.ToList(); }
        }

        public IImage Read(int key, IImage defaultImage = null)
        {
            return _images.ContainsKey(key) ? _images[key] : defaultImage;
        }

        private int FindNewKey()
        {
            return _images.Count;
        }
    }
}