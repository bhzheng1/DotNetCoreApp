using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.Model
{
    [Table("Regions", Schema = "hr")]
    public partial class Region
    {
        public Region()
        {
            Countries = new HashSet<Country>();
        }

        [Column("region_id")]
        public int RegionId { get; set; }
        [Column("region_name")]
        [StringLength(25)]
        public string RegionName { get; set; }

        [InverseProperty("Region")]
        public ICollection<Country> Countries { get; set; }
    }
}
