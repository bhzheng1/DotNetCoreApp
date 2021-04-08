using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.SakilaModels
{
    [Table("inventory")]
    public partial class Inventory
    {
        public Inventory()
        {
            Rental = new HashSet<Rental>();
        }

        [Key]
        [Column("inventory_id")]
        public int InventoryId { get; set; }
        [Column("film_id")]
        public int FilmId { get; set; }
        [Column("store_id")]
        public int StoreId { get; set; }
        [Column("last_update", TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [ForeignKey(nameof(FilmId))]
        [InverseProperty("Inventory")]
        public virtual Film Film { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty("Inventory")]
        public virtual Store Store { get; set; }
        [InverseProperty("Inventory")]
        public virtual ICollection<Rental> Rental { get; set; }
    }
}
