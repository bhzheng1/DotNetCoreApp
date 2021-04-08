using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.SakilaModels
{
    [Table("customer")]
    public partial class Customer
    {
        public Customer()
        {
            Payment = new HashSet<Payment>();
            Rental = new HashSet<Rental>();
        }

        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("store_id")]
        public int StoreId { get; set; }
        [Required]
        [Column("first_name")]
        [StringLength(45)]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(45)]
        public string LastName { get; set; }
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Column("address_id")]
        public int AddressId { get; set; }
        [Required]
        [Column("active")]
        [StringLength(1)]
        public string Active { get; set; }
        [Column("create_date", TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        [Column("last_update", TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Customer")]
        public virtual Address Address { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty("Customer")]
        public virtual Store Store { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<Payment> Payment { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<Rental> Rental { get; set; }
    }
}
