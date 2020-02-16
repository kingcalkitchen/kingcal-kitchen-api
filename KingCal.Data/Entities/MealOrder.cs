//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace KingCal.Data.Entities
//{
//    [Table("MealOrders", Schema = "Food")]
//    public class MealOrder
//    {
//        [Key]
//        public Guid Id { get; set; }

//        public Guid OrderId { get; set; }

//        public Guid MealId { get; set; }

//        public Guid MealProcurementId { get; set; }

//        public int Quantity { get; set; }


//        [ForeignKey("OrderId")]
//        public virtual Order Order { get; set; }

//        [ForeignKey("MealId")]
//        public virtual Meal Meal { get; set; }

//        [ForeignKey("MealProcurementId")]
//        public virtual MealProcurementOption MealProcurement { get; set; }

//    }
//}
