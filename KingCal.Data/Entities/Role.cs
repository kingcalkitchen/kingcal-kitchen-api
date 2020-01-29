using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("Roles", Schema = "User")]
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

    }
}
