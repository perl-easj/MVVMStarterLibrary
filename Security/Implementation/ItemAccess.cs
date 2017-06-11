using System;
using System.Collections.Generic;
using Security.Interfaces;
using Security.Types;

namespace Security.Implementation
{
    public class ItemAccess : IItemAccess
    {
        private Dictionary<string, List<AccessType>> _accessDictionary;

        public ItemAccess()
        {
            _accessDictionary = new Dictionary<string, List<AccessType>>();
        }

        public void AddAccessType(string itemName, AccessType accessType)
        {
            // Add the item, if not already present
            if (!_accessDictionary.ContainsKey(itemName))
            {
                _accessDictionary.Add(itemName, new List<AccessType>());
            }

            // Add the access type, if not already present
            if (_accessDictionary[itemName].Contains(accessType))
            {
                throw new ArgumentException(nameof(AddAccessType));
            }

            _accessDictionary[itemName].Add(accessType);
        }

        public List<AccessType> GetAccessTypes(string itemName)
        {
            return _accessDictionary.ContainsKey(itemName) ? _accessDictionary[itemName] : new List<AccessType>();
        }

        public override string ToString()
        {
            string toStr = "";

            foreach (var item in _accessDictionary)
            {
                toStr = toStr + item.Key + " { ";

                foreach (var access in item.Value)
                {
                    toStr = toStr + access + " ";
                }

                toStr = toStr + "}\n";
            }

            return toStr;
        }
    }
}