using System;
using System.Collections.Generic;

namespace example.Model1
{
    public partial class Region
    {
        public Region()
        {
            Countries = new HashSet<Country>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public ICollection<Country> Countries { get; set; }
    }
}
