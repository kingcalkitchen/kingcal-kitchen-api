using KingCal.Common.Helpers;
using KingCal.Common.Models;
using KingCal.Data;
using KingCal.Data.Entities;
using KingCal.Service.User.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async IAsyncEnumerable<Data.Entities.Role> GetRolesByUserId(Guid id)
        {
            List<UserRole> roles = 
                await _context.UserRoles
                    .Include(x => x.Role)
                    .Where(x => x.User.Id == id)
                    .ToListAsync();

            foreach (UserRole role in roles) yield return role.Role;

        }

        public async Task<UserRole> AssignRole(Guid userId, Guid roleId, Guid currentUser) 
        {
            if (Guid.Empty == userId)
                throw new AppException("UserId is required");

            if (Guid.Empty == roleId)
                throw new AppException("RoleId is required");

            if (Guid.Empty == currentUser)
                throw new AppException("Current user is required");

            IAsyncEnumerator<Data.Entities.Role> _role = GetRolesByUserId(userId).GetAsyncEnumerator();
            while (await _role.MoveNextAsync()) 
            {
                if (_role.Current.Id == roleId)
                    throw new AppException("User already assigned this role.");
            }

            Data.Entities.User user = _context.Users.Find(userId);
            Data.Entities.Role role = _context.Roles.Find(roleId);

            Guid newUserRoleId = Guid.NewGuid();
            UserRole userRole = new UserRole
            {
                Id = newUserRoleId,
                User = user,
                Role = role,
                CreatedBy = currentUser != Guid.Empty ? currentUser : userId,
                CreatedDate = DateTime.Now,
                DeletedBy = null,
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

            var userRole = _context.UserRoles.Where(x => x.User.Id == userId && x.Role.Id == roleId).FirstOrDefault();
            if (userRole != null) 
            {
                userRole.DeletedBy = currentUser != Guid.Empty ? currentUser : userId;
                userRole.DeletedDate = DateTime.Now;

                _context.UserRoles.Update(userRole);
                _context.SaveChanges();
            }
        }
    }
}
