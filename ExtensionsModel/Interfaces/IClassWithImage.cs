namespace ExtensionsModel.Interfaces
{
    /// <summary>
    /// Interface for any class containing an image,
    /// which is referred to by a numeric key.
    /// </summary>
    public interface IClassWithImage
    {
        int ImageKey { get; set; }
    }
}