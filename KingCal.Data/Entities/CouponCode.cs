using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("CouponCodes", Schema = "Food")]
    public class CouponCode
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Percentage { get; set; }

        public DateTime ExpirationDate { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public Guid? DeletedBy { get; set; }
    }
}
