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
using System.Security.Cryptography;
using System.Text;

namespace KingCal.Service.Implementations
{
    public class User : IUser
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<User> _logger;
        private readonly DataContext _context;
        private readonly IUserRoles _userRolesService;

        public User(IOptions<AppSettings> appSettings, ILogger<User> logger, DataContext context, IUserRoles userRolesService)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _context = context;
            _userRolesService = userRolesService;
        }

        public Data.Entities.User Authenticate(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password)) return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);
            if (user == null)  return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;

            return user;
        }

        public IEnumerable<Data.Entities.User> GetAll()
        {
            return _context.Users;
        }

        public Data.Entities.User GetById(Guid id) 
        {
            return _context.Users.Find(id);
        }

        public Data.Entities.User Create(Data.Entities.User user, string password)
        {
            if (String.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username " + user.Username + " is already taken");

            Guid userId = Guid.NewGuid();
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.Id = userId;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);

            Guid roleId = _context.Roles.Where(x => x.Name == "user").FirstOrDefault().Id;
            Guid newUserRoleId = Guid.NewGuid();
            UserRole userRole = new UserRole
            {
                Id = newUserRoleId,
                UserId = userId,
                RoleId = roleId,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                DeletedBy = Guid.Empty,
                DeletedDate = null,
            };
            _context.UserRoles.Add(userRole);
            
            _context.SaveChanges();

            return user;
        }

        public void Update(Data.Entities.User userParam, string password = null) 
        {
            var user = _context.Users.Find(userParam.Id);

            if (user is null)
                throw new AppException("User not found.");

            if (String.IsNullOrWhiteSpace(userParam.Email))
                throw new AppException("Email can not be null.");

            if (String.IsNullOrWhiteSpace(userParam.Username))
                throw new AppException("Username can not be null.");

            if (userParam.Username != user.Username) 
            {
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken.");
            }

            if (userParam.Email != user.Email)
            {
                if (_context.Users.Any(x => x.Email == userParam.Email))
                    throw new AppException("Email " + userParam.Email + " is already taken.");
            }

            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;
            user.Email = userParam.Email;

            if (!String.IsNullOrWhiteSpace(password)) 
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id) 
        {
            var user = _context.Users.Find(id);
            if (user != null) 
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        #region Private Methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password is null) throw new ArgumentNullException("password");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password is null) throw new ArgumentNullException("password");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new HMACSHA512(storedSalt)) 
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        #endregion
    }
}
