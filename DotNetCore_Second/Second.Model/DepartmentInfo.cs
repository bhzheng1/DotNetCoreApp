using System;

namespace Second.Model
{
    [Serializable]
    public class DepartmentInfo
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int LocationId { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }

        public string CountryId { get; set; }
        public string CountryName { get; set; }

        public int RegionId { get; set; }
        public string RegionName { get; set; }
    }
}
