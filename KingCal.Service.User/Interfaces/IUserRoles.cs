using KingCal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingCal.Service.User.Interfaces
{
    public interface  IUserRoles
    {
        IAsyncEnumerable<Role> GetRolesByUserId(Guid id);

        Task<UserRole> AssignRole(Guid userId, Guid roleId, Guid currentUser);

        void RemoveRole(Guid userId, Guid roleId, Guid currentUser);
    }
}
