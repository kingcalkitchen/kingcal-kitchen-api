using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("Item", Schema = "Item")]
    public class Item
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public List<Property> Properties { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public List<SubCategoryItem> SubCategoryItems { get; set; }
        public List<ItemSubItem> ItemSubItems { get; set; }
    }
}