using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.SakilaModels
{
    [Table("address")]
    public partial class Address
    {
        public Address()
        {
            Customer = new HashSet<Customer>();
            Staff = new HashSet<Staff>();
            Store = new HashSet<Store>();
        }

        [Key]
        [Column("address_id")]
        public int AddressId { get; set; }
        [Required]
        [Column("address")]
        [StringLength(50)]
        public string Address1 { get; set; }
        [Column("address2")]
        [StringLength(50)]
        public string Address2 { get; set; }
        [Required]
        [Column("district")]
        [StringLength(20)]
        public string District { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("postal_code")]
        [StringLength(10)]
        public string PostalCode { get; set; }
        [Required]
        [Column("phone")]
        [StringLength(20)]
        public string Phone { get; set; }
        [Column("last_update", TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Address")]
        public virtual City City { get; set; }
        [InverseProperty("Address")]
        public virtual ICollection<Customer> Customer { get; set; }
        [InverseProperty("Address")]
        public virtual ICollection<Staff> Staff { get; set; }
        [InverseProperty("Address")]
        public virtual ICollection<Store> Store { get; set; }
    }
}
