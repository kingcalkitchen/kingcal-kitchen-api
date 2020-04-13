using System;
using System.Collections.Generic;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace KingCal.Service.User.Interfaces
{
    public interface IUser
    {
        Data.Entities.User Authenticate(string email, string password);
        Data.Entities.User Authenticate(Payload payload);

        IEnumerable<Data.Entities.User> GetAll();
        Data.Entities.User GetById(Guid id);
        Data.Entities.User Create(Data.Entities.User user, string password, Guid currentUser);
        void Update(Data.Entities.User user, Guid currentUser, string password = null);
        void Delete(Guid id, Guid currentUser);
    }
}
