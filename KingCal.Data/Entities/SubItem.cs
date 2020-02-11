using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("SubItem", Schema = "Item")]
    public class SubItem
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public List<SubItemProperty> SubItemProperty { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public List<ItemSubItem> ItemSubItems { get; set; }
    }
}