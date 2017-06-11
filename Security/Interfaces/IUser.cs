namespace Security.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        string Password { get; }
        string UserType { get; }
    }
}