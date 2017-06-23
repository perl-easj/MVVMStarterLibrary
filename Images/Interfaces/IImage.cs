namespace Images.Interfaces
{
    public interface IImage
    {
        int Key { get; set; }
        string Source { get; }
        string Description { get; }
    }
}