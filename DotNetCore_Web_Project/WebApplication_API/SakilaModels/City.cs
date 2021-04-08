using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.SakilaModels
{
    [Table("city")]
    public partial class City
    {
        public City()
        {
            Address = new HashSet<Address>();
        }

        [Key]
        [Column("city_id")]
        public int CityId { get; set; }
        [Required]
        [Column("city")]
        [StringLength(50)]
        public string City1 { get; set; }
        [Column("country_id")]
        public short CountryId { get; set; }
        [Column("last_update", TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [ForeignKey(nameof(CountryId))]
        [InverseProperty("City")]
        public virtual Country Country { get; set; }
        [InverseProperty("City")]
        public virtual ICollection<Address> Address { get; set; }
    }
}
