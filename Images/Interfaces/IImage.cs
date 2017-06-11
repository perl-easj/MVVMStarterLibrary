using InMemoryStorage.Interfaces;

namespace Images.Interfaces
{
    public interface IImage : IStorable
    {
        string Source { get; }
        string Description { get; }
    }
}