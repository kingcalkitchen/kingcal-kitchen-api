//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace KingCal.Data.Entities
//{
//    [Table("Orders", Schema = "Food")]
//    public class Order
//    {
//        [Key]
//        public Guid Id { get; set; }

//        public Guid Purchasedby { get; set; }

//        public DateTime PurchasedDate { get; set; }

//        public DateTime ProcessedDate { get; set; }

//        public DateTime CompletedDate { get; set; }


//        [ForeignKey("Purchasedby")]
//        public virtual User Purchasor { get; set; }
//    }
//}
