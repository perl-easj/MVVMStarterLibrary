using System;
using System.Collections.Generic;
using Security.Interfaces;
using Security.Types;

namespace Security.Implementation
{
    public class UserType : IUserType
    {
        private Dictionary<string, IItemAccess> _userTypeAccess;

        public UserType()
        {
            _userTypeAccess = new Dictionary<string, IItemAccess>();
        }

        public void AddUserType(string userType)
        {
            if (!_userTypeAccess.ContainsKey(userType))
            {
                _userTypeAccess.Add(userType, new ItemAccess());
            }
        }

        public void AddAccessRight(string userType, string itemName, AccessType accessType)
        {
            if (!_userTypeAccess.ContainsKey(userType))
            {
                throw new ArgumentException(nameof(AddAccessRight));
            }

            _userTypeAccess[userType].AddAccessType(itemName, accessType);
        }

        public List<AccessType> GetAccessRights(string userType, string itemName)
        {
            if (!_userTypeAccess.ContainsKey(userType))
            {
                throw new ArgumentException(nameof(GetAccessRights));
            }

            return _userTypeAccess[userType].GetAccessTypes(itemName);
        }

        public bool HasAccessRight(string userType, string itemName, AccessType accessType)
        {
            List<AccessType> accessTypes = GetAccessRights(userType, itemName);
            return accessTypes.Contains(accessType);
        }
    }
}