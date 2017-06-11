using System;
using System.Collections.Generic;
using Security.Interfaces;
using Security.Types;

namespace Security.Implementation
{
    public class SecurityService : ISecurityService
    {
        public const string AdminUserName = "Admin";
        private Dictionary<string, IUser> _users;
        private IUserType _userTypes;
        private IItemAccess _itemAccess;
        private string _currentUserName;
        private bool _useLogin;

        public SecurityService()
        {
            _users = new Dictionary<string, IUser>();
            _userTypes = new UserType();
            _itemAccess = new ItemAccess();
            _currentUserName = "(none)";
            _useLogin = false;
        }

        public bool UseLogin
        {
            get { return _useLogin; }
            set { _useLogin = value; }
        }

        public string CurrentUserName
        {
            get { return _currentUserName; }
            set { _currentUserName = value; }
        }

        protected Dictionary<string, IUser> Users
        {
            get { return _users; }
        }

        protected IUserType UserTypes
        {
            get { return _userTypes; }
        }

        protected IItemAccess ItemAccess
        {
            get { return _itemAccess; }
        }

        public void AddUser(string userName, string password, string userType)
        {
            if (_users.ContainsKey(userName))
            {
                throw new ArgumentException(nameof(AddUser));
            }

            _users.Add(userName, new User(userName, password, userType));
            _userTypes.AddUserType(userType);
        }

        public bool CheckPassword(string userName, string password)
        {
            if (!_users.ContainsKey(userName))
            {
                throw new ArgumentException(nameof(CheckPassword));
            }

            return _users[userName].Password == password;
        }

        public void AddItemAccessRight(string itemName, AccessType accessType)
        {
            _itemAccess.AddAccessType(itemName, accessType);
        }

        public void AddItemAccessRights(string itemName, List<AccessType> accessTypes)
        {
            foreach (AccessType accessType in accessTypes)
            {
                AddItemAccessRight(itemName, accessType);
            }
        }

        public void AddUserAccessRight(string userType, string itemName, AccessType accessType)
        {
            _userTypes.AddAccessRight(userType, itemName, accessType);
        }

        public void AddUserAccessRights(string userType, string itemName, List<AccessType> accessTypes)
        {
            foreach (AccessType accessType in accessTypes)
            {
                AddUserAccessRight(userType, itemName, accessType);
            }
        }

        public bool IsActionAllowedForCurrentUser(string itemName)
        {
            return IsActionAllowed(CurrentUserName, itemName);
        }

        public virtual bool IsActionAllowed(string userName, string itemName)
        {
            if (userName == AdminUserName)
            {
                return true;
            }

            if (!Users.ContainsKey(userName))
            {
                return false;
            }

            foreach (AccessType accessType in ItemAccess.GetAccessTypes(itemName))
            {
                if (UserTypes.HasAccessRight(Users[userName].UserType, itemName, accessType))
                {
                    return true;
                }
            }

            return false;
        }
    }
}