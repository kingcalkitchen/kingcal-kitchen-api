using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("SubCategoryItem", Schema = "Item")]
    public class SubCategoryItem
    {
        public SubCategory SubCategory { get; set; }
        public Guid SubCategoryId { get; set; }
        public Item Item { get; set; }
        public Guid ItemId { get; set; }
    }
}
