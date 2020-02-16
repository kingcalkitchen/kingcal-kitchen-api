using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KingCal.Data.DTOs
{
    public class ItemDTO
    {
        public Guid Id { get; set; }
        public List<PropertyDTO> PropertyList { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public List<SubCategoryItemDTO> SubCategoryItemList { get; set; }
        public List<SubItemDTO> SubItemList { get; set; }
    }
}
