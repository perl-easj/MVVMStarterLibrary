namespace InMemoryStorage.Interfaces
{
    /// <summary>
    /// If an object is intended to be stored in one of the 
    /// collection-oriented classes in the library, it must
    /// implement this interface.
    /// </summary>
    public interface IStorable
    {
        int Key { get; set; }
    }
}