using KingCal.Common.Helpers;
using KingCal.Common.Models;
using KingCal.Data;
using KingCal.Data.Entities;
using KingCal.Service.User.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace KingCal.Service.Implementations
{
    public class User : IUser
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<User> _logger;
        private readonly DataContext _context;
        private readonly IUserRoles _userRolesService;
        private readonly IConfiguration _configuration;

        public User(IOptions<AppSettings> appSettings, ILogger<User> logger, DataContext context, IUserRoles userRolesService, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _context = context;
            _userRolesService = userRolesService;
            _configuration = configuration;
        }
















        private Data.Entities.User FindOrAdd(Payload payload)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == payload.Email);
            if (user == null)
            {
                user = new Data.Entities.User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    Name = payload.Name,
                    Email = payload.Email,
                    OauthSubject = payload.Subject,
                    OauthIssuer = payload.Issuer,
                    PhotoUrl = payload.Picture,
                };
                Create(user, "Password1234!", Guid.Empty);
            }
            return user;
        }











        public Data.Entities.User Authenticate(Payload payload)
        {
            return FindOrAdd(payload);
        }

        public Data.Entities.User Authenticate(string email, string password)
        {
            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password)) return null;

            var user = _context.Users.SingleOrDefault(x => x.Email == email);
            if (user == null) return null;

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

        public Data.Entities.User Create(Data.Entities.User user, string password, Guid currentUser)
        {
            if (String.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new AppException("Email " + user.Email + " is already taken");

            Guid userId = Guid.NewGuid();
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.Id = userId;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.CreatedBy = currentUser != Guid.Empty ? currentUser : userId;
            user.CreatedDate = DateTime.Now;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(Data.Entities.User userParam, Guid currentUser, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user is null)
                throw new AppException("User not found.");

            if (String.IsNullOrWhiteSpace(userParam.Email))
                throw new AppException("Email can not be null.");

            if (userParam.Email != user.Email)
            {
                if (_context.Users.Any(x => x.Email == userParam.Email))
                    throw new AppException("Email " + userParam.Email + " is already taken.");
            }

            if (userParam.Username != user.Username)
            {
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken.");
            }

            string name = String.Format(
                "{0}{1}{2}", 
                String.IsNullOrEmpty(userParam.FirstName) ? string.Empty : String.Format("{0} ", userParam.FirstName),
                String.IsNullOrEmpty(userParam.MiddleName) ? string.Empty : String.Format("{0} ", userParam.MiddleName),
                String.IsNullOrEmpty(userParam.LastName) ? string.Empty : String.Format("{0} ", userParam.LastName)
                );

            user.FirstName = userParam.FirstName;
            user.MiddleName = userParam.MiddleName;
            user.LastName = userParam.LastName;
            user.Name = name;
            user.Username = userParam.Username;
            user.Email = userParam.Email;
            user.UpdatedDate = DateTime.Now;
            user.UpdatedBy = currentUser != Guid.Empty ? currentUser : userParam.Id;

            // TODO
            // add photoUrl

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

        public void Delete(Guid id, Guid currentUser)
        {
            var user = _context.Users
                .Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (user != null)
            {
                if (user.UserRoles.Count > 0)
                {
                    foreach (UserRole userRole in user.UserRoles)
                    {
                        userRole.DeletedDate = DateTime.Now;
                        userRole.DeletedBy = currentUser;
                        _context.UserRoles.Update(userRole);
                    }
                }

                user.DeletedBy = currentUser != Guid.Empty ? currentUser : user.Id;
                user.DeletedDate = DateTime.Now;

                _context.Users.Update(user);
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
