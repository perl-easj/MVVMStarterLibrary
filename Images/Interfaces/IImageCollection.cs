using System.Collections.Generic;

namespace Images.Interfaces
{
    public interface IImageCollection
    {
        void AddImage(IImage image);
        void SetImages(List<IImage> imageList);
        List<IImage> All { get; }
        IImage Read(int key, IImage defaultImage = null);
    }
}