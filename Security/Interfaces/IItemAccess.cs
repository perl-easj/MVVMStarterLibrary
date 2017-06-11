using System.Collections.Generic;
using Security.Types;

namespace Security.Interfaces
{
    public interface IItemAccess
    {
        void AddAccessType(string itemName, AccessType accessType);
        List<AccessType> GetAccessTypes(string itemName);
    }
}