﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Second.DataAccess.sakila
{
    public partial class Inventory
    {
        public Inventory()
        {
            Rentals = new HashSet<Rental>();
        }

        public int InventoryId { get; set; }
        public int FilmId { get; set; }
        public int StoreId { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual Film Film { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}