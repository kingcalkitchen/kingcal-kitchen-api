using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("MealTypes", Schema = "Food")]
    public class MealType
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
