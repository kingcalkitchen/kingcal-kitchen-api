using KingCal.Data.Entities;
using System;
using System.Collections.Generic;

namespace KingCal.Service.User.Interfaces
{
    public interface IRole
    {
        IEnumerable<Role> GetAll();
        Role GetById(Guid id);
        Role Create(Role role);
        void Update(Role role);
        void Delete(Guid id);
    }
}
