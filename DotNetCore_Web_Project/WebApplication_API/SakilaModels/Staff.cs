using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.SakilaModels
{
    [Table("staff")]
    public partial class Staff
    {
        public Staff()
        {
            Payment = new HashSet<Payment>();
            Rental = new HashSet<Rental>();
        }

        [Key]
        [Column("staff_id")]
        public byte StaffId { get; set; }
        [Required]
        [Column("first_name")]
        [StringLength(45)]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(45)]
        public string LastName { get; set; }
        [Column("address_id")]
        public int AddressId { get; set; }
        [Column("picture", TypeName = "image")]
        public byte[] Picture { get; set; }
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Column("store_id")]
        public int StoreId { get; set; }
        [Required]
        [Column("active")]
        public bool? Active { get; set; }
        [Required]
        [Column("username")]
        [StringLength(16)]
        public string Username { get; set; }
        [Column("password")]
        [StringLength(40)]
        public string Password { get; set; }
        [Column("last_update", TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Staff")]
        public virtual Address Address { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty("Staff")]
        public virtual Store Store { get; set; }
        [InverseProperty("ManagerStaff")]
        public virtual Store StoreNavigation { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<Payment> Payment { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<Rental> Rental { get; set; }
    }
}
