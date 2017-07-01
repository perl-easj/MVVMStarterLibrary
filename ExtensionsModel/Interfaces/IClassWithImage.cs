namespace ExtensionsModel.Interfaces
{
    /// <summary>
    /// Simple interface for any class containing an Image,
    /// which is referred to by a numeric key.
    /// </summary>
    public interface IClassWithImage
    {
        int ImageKey { get; set; }
    }
}