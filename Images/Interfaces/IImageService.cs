using System.Collections.ObjectModel;
using Images.Types;

namespace Images.Interfaces
{
    public interface IImageService : ITaggedImageCollection
    {
        void SetAppImageSource(AppImageType imageType, string source);
        string GetAppImageSource(AppImageType imageType);
        ObservableCollection<IImage> GetObservableImageCollection(string tag);
    }
}