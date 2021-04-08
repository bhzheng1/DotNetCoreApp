using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.Model
{
    [Table("Countries", Schema = "hr")]
    public partial class Country
    {
        public Country()
        {
            Locations = new HashSet<Location>();
        }

        [Column("country_id")]
        [StringLength(2)]
        public string CountryId { get; set; }
        [Column("country_name")]
        [StringLength(40)]
        public string CountryName { get; set; }
        [Column("region_id")]
        public int? RegionId { get; set; }

        [ForeignKey("RegionId")]
        [InverseProperty("Countries")]
        public Region Region { get; set; }
        [InverseProperty("Country")]
        public ICollection<Location> Locations { get; set; }
    }
}
