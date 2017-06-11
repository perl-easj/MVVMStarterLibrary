using System.Collections.Generic;
using Security.Types;

namespace Security.Interfaces
{
    public interface ISecurityService
    {
        bool UseLogin { get; set; }
        string CurrentUserName { get; set; }
        void AddUser(string userName, string password, string userType);
        bool CheckPassword(string userName, string password);
        void AddItemAccessRight(string itemName, AccessType accessType);
        void AddItemAccessRights(string itemName, List<AccessType> accessTypes);
        void AddUserAccessRight(string userType, string itemName, AccessType accessType);
        void AddUserAccessRights(string userType, string itemName, List<AccessType> accessTypes);
        bool IsActionAllowedForCurrentUser(string itemName);
        bool IsActionAllowed(string userName, string itemName);
    }
}