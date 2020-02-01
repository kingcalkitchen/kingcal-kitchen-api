using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("MealReviews", Schema = "Food")]
    public class MealReview
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MealId { get; set; }

        public int Stars { get; set; }

        public string Review { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }


        [ForeignKey("MealId")]
        public virtual Meal Meal { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User Creator { get; set; }

        [ForeignKey("ApprovedBy")]
        public virtual User Approvor { get; set; }
    }
}
