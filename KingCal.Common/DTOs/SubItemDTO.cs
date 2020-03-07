using System;
using System.Collections.Generic;

namespace KingCal.Common.DTOs
{
    public class SubItemDTO
    {
        public Guid Id { get; set; }
        public List<SubItemPropertyDTO> SubItemPropertyList { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
