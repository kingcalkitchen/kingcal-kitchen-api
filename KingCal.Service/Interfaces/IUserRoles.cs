using KingCal.Data.Entities;
using System;
using System.Collections.Generic;

namespace KingCal.Service.Interfaces
{
    public interface  IUserRoles
    {
        IEnumerable<Role> GetRolesByUserId(Guid id);

        UserRole AssignRole(Guid userId, Guid roleId, Guid currentUser);

        void RemoveRole(Guid userId, Guid roleId, Guid currentUser);
    }
}
