using System.Collections.Generic;

namespace Images.Interfaces
{
    public interface ITaggedImage : IImage
    {
        List<string> Tags { get; }
        void AddTag(string tag);
        bool ContainsTag(string tag);
        bool ContainsAnyTag(List<string> tags);
    }
}