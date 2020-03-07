using System;

namespace KingCal.Common.DTOs
{
    public class AddressDTO
    {
        public Guid Id { get; set; }
        public string Country { get; set; } //Country Name
        public string AdministrativeArea { get; set; } // State / Province / Region
        public string SubAdministrativeArea { get; set; } // County / District
        public string Locality { get; set; } //City / Town
        public int PostalCode { get; set; } //Postal code / ZIP Code
        public string StreetAddress { get; set; }
        public string Premise { get; set; } //Apartment, Suite, Box number, etc.
        public string LandMark { get; set; } //Nearby Landmark
        public int AddressType { get; set; } //Store or other Address
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}
