using KingCal.Common.Helpers;
using KingCal.Common.Models;
using KingCal.Data;
using KingCal.Service.User.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KingCal.Service.Implementations
{
    public class Role : IRole
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<Role> _logger;
        private readonly DataContext _context;

        public Role(IOptions<AppSettings> appSettings, ILogger<Role> logger, DataContext context)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _context = context;
        }

        public IEnumerable<Data.Entities.Role> GetAll()
        {
            return _context.Roles;
        }

        public Data.Entities.Role GetById(Guid id)
        {
            return _context.Roles.Find(id);
        }

        public Data.Entities.Role Create(Data.Entities.Role role) 
        {
            if (_context.Roles.Any(x => x.Name == role.Name))
                throw new AppException("Role " + role.Name + " is already in use");

            Guid roleId = new Guid();
            role.Id = roleId;

            _context.Roles.Add(role);
            _context.SaveChanges();

            return role;
        }

        public void Update(Data.Entities.Role roleParam) 
        {
            var role = _context.Roles.Find(roleParam.Id);

            if (role is null)
                throw new AppException("Role not found.");

            if (String.IsNullOrWhiteSpace(roleParam.Name))
                throw new AppException("Name can not be null.");

            if (roleParam.Name != role.Name) 
            {
                if (_context.Roles.Any(x => x.Name == roleParam.Name))
                    throw new AppException("Name " + roleParam.Name + " is already taken.");
            }

            role.Name = roleParam.Name;

            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

    }
}
