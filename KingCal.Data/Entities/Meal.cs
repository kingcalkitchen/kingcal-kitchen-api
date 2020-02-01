using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("Meals", Schema = "Food")]
    public class Meal
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid MealTypeId { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }


        [ForeignKey("MealTypeId")]
        public virtual MealType MealType { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User Creator { get; set; }

        [ForeignKey("DeletedBy")]
        public virtual User Deletor { get; set; }
    }
}
