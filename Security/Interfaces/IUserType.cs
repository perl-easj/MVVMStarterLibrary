using System.Collections.Generic;
using Security.Types;

namespace Security.Interfaces
{
    public interface IUserType
    {
        void AddUserType(string userType);
        void AddAccessRight(string userType, string itemName, AccessType accessType);
        List<AccessType> GetAccessRights(string userType, string itemName);
        bool HasAccessRight(string userType, string itemName, AccessType accessType);
    }
}