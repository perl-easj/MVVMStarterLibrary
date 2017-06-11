using System.Collections.Generic;

namespace Images.Interfaces
{
    public interface ITaggedImageCollection : IImageCollection
    {
        List<IImage> AllWithTag(string tag);
        List<string> AllTags { get; }
    }
}