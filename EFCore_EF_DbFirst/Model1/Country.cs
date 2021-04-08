using System;
using System.Collections.Generic;

namespace example.Model1
{
    public partial class Country
    {
        public Country()
        {
            Locations = new HashSet<Location>();
        }

        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public int? RegionId { get; set; }

        public Region Region { get; set; }
        public ICollection<Location> Locations { get; set; }
    }
}
