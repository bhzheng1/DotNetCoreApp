using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.SakilaModels
{
    [Table("rental")]
    public partial class Rental
    {
        public Rental()
        {
            Payment = new HashSet<Payment>();
        }

        [Key]
        [Column("rental_id")]
        public int RentalId { get; set; }
        [Column("rental_date", TypeName = "datetime")]
        public DateTime RentalDate { get; set; }
        [Column("inventory_id")]
        public int InventoryId { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("return_date", TypeName = "datetime")]
        public DateTime? ReturnDate { get; set; }
        [Column("staff_id")]
        public byte StaffId { get; set; }
        [Column("last_update", TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Rental")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(InventoryId))]
        [InverseProperty("Rental")]
        public virtual Inventory Inventory { get; set; }
        [ForeignKey(nameof(StaffId))]
        [InverseProperty("Rental")]
        public virtual Staff Staff { get; set; }
        [InverseProperty("Rental")]
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
