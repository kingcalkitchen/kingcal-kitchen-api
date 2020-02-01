using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("MealOrdersHistory", Schema = "Food")]
    public class MealOrderHistory
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MealId { get; set; }

        public int Quantity { get; set; }

        public Guid Purchasedby { get; set; }

        public DateTime PurchasedDate { get; set; }


        [ForeignKey("MealId")]
        public virtual Meal Meal { get; set; }

        [ForeignKey("Purchasedby")]
        public virtual User Purchasor { get; set; }
    }
}
