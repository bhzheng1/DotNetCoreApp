using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.Model
{
    [Table("Locations", Schema = "hr")]
    public partial class Location
    {
        public Location()
        {
            Departments = new HashSet<Department>();
        }

        [Column("location_id")]
        public int LocationId { get; set; }
        [Column("street_address")]
        [StringLength(40)]
        public string StreetAddress { get; set; }
        [Column("postal_code")]
        [StringLength(12)]
        public string PostalCode { get; set; }
        [Required]
        [Column("city")]
        [StringLength(30)]
        public string City { get; set; }
        [Column("state_province")]
        [StringLength(25)]
        public string StateProvince { get; set; }
        [Column("country_id")]
        [StringLength(2)]
        public string CountryId { get; set; }

        [ForeignKey("CountryId")]
        [InverseProperty("Locations")]
        public Country Country { get; set; }
        [InverseProperty("Location")]
        public ICollection<Department> Departments { get; set; }
    }
}
