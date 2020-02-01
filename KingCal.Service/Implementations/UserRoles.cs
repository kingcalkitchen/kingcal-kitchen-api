using KingCal.Data;
using KingCal.Data.Entities;
using KingCal.Data.Models;
using KingCal.Service.Helpers;
using KingCal.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KingCal.Service.Implementations
{
    public class UserRoles : IUserRoles
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<UserRoles> _logger;
        private readonly DataContext _context;

        public UserRoles(IOptions<AppSettings> appSettings, ILogger<UserRoles> logger, DataContext context)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _context = context;
        }

        public IEnumerable<Data.Entities.Role> GetRolesByUserId(Guid id) 
        {
            return from role in _context.Roles
                   join userRole in _context.UserRoles on role.Id equals userRole.RoleId
                   where userRole.UserId == id && userRole.DeletedDate == null && userRole.DeletedBy == null
                   select new Data.Entities.Role { Id = role.Id, Name = role.Name };
        }

        public UserRole AssignRole(Guid userId, Guid roleId, Guid currentUser) 
        {
            if (Guid.Empty == userId)
                throw new AppException("UserId is required");

            if (Guid.Empty == roleId)
                throw new AppException("RoleId is required");

            if (Guid.Empty == currentUser)
                throw new AppException("Current user is required");

            List<Data.Entities.Role> roles = GetRolesByUserId(userId).ToList();

            foreach (Data.Entities.Role role in roles)
            {
                if (role.Id == roleId)
                    throw new AppException("User already assigned this role.");
            }

            Guid newUserRoleId = Guid.NewGuid();
            UserRole userRole = new UserRole
            {
                Id = newUserRoleId,
                UserId = userId,
                RoleId = roleId,
                CreatedBy = currentUser,
                CreatedDate = DateTime.Now,
                DeletedBy = Guid.Empty,
                DeletedDate = null,
            };
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

            return userRole;
        }

        public void RemoveRole(Guid userId, Guid roleId, Guid currentUser) 
        {
            if (Guid.Empty == userId)
                throw new AppException("UserId is required");

            if (Guid.Empty == roleId)
                throw new AppException("RoleId is required");

            if (Guid.Empty == currentUser)
                throw new AppException("Current user is required");

            var userRole = _context.UserRoles.Where(x => x.UserId == userId && x.RoleId == roleId).FirstOrDefault();
            if (userRole != null) 
            {
                userRole.DeletedBy = currentUser;
                userRole.DeletedDate = DateTime.Now;

                _context.UserRoles.Update(userRole);
                _context.SaveChanges();
            }
        }
    }
}
