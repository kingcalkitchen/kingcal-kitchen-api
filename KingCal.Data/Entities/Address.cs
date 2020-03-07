using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("Address", Schema = "Address")]
    public class Address
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Country { get; set; } //Country Name

        [Required]
        public string AdministrativeArea { get; set; } // State / Province / Region

        public string SubAdministrativeArea { get; set; } // County / District

        [Required]
        public string Locality { get; set; } //City / Town

        [Required]
        public int PostalCode { get; set; } //Postal code / ZIP Code

        [Required]
        public string StreetAddress { get; set; } 

        public string Premise { get; set; } //Apartment, Suite, Box number, etc.

        public string LandMark { get; set; } //Nearby Landmark

        [Required]
        public int AddressType { get; set; }

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
