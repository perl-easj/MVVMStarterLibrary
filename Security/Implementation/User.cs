using Security.Interfaces;

namespace Security.Implementation
{
    public class User : IUser
    {
        public string Name { get; }
        public string Password { get; }
        public string UserType { get; }

        public User() : this("(none)", "(none)", "(none)")
        {
        }

        public User(string name, string password, string userType)
        {
            Name = name;
            Password = password;
            UserType = userType;
        }

        public override string ToString()
        {
            string toStr = "User :  " + Name + "\n";
            return toStr;
        }
    }
}