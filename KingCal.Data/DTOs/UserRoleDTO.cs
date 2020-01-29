using System;

namespace KingCal.Data.DTOs
{
    public class UserRoleDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid DeletedBy { get; set; }

        public DateTime DeletedDated { get; set; }
    }
}
