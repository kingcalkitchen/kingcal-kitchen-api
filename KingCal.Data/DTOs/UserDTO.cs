﻿using System;
using System.Collections.Generic;

namespace KingCal.Data.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        //public List<RoleDTO> Roles { get; set; }
    }
}
