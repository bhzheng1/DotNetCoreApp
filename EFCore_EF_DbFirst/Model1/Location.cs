using System;
using System.Collections.Generic;

namespace example.Model1
{
    public partial class Location
    {
        public Location()
        {
            Departments = new HashSet<Department>();
        }

        public int LocationId { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string CountryId { get; set; }

        public Country Country { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
