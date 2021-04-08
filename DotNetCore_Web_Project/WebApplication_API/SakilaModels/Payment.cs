using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.SakilaModels
{
    [Table("payment")]
    public partial class Payment
    {
        [Key]
        [Column("payment_id")]
        public int PaymentId { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("staff_id")]
        public byte StaffId { get; set; }
        [Column("rental_id")]
        public int? RentalId { get; set; }
        [Column("amount", TypeName = "decimal(5, 2)")]
        public decimal Amount { get; set; }
        [Column("payment_date", TypeName = "datetime")]
        public DateTime PaymentDate { get; set; }
        [Column("last_update", TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Payment")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(RentalId))]
        [InverseProperty("Payment")]
        public virtual Rental Rental { get; set; }
        [ForeignKey(nameof(StaffId))]
        [InverseProperty("Payment")]
        public virtual Staff Staff { get; set; }
    }
}
