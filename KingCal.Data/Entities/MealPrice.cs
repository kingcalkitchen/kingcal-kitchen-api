using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("MealPrices", Schema = "Food")]
    public class MealPrice
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MealId { get; set; }

        public decimal Price { get; set; }


        [ForeignKey("MealId")]
        public virtual Meal Meal { get; set; }
    }
}
