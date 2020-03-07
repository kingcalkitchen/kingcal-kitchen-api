using System;

namespace KingCal.Common.DTOs
{
    public class UserRoleDTO
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid DeletedBy { get; set; }

        public DateTime DeletedDated { get; set; }

        public UserDTO User { get; set; }

        public RoleDTO Role { get; set; }
    }
}
