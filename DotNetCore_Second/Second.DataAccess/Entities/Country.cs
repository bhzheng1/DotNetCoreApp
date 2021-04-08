namespace Second.DataAccess.Entities
{
    public class Country : BaseEntity
    {
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public int? RegionId { get; set; }
    }
}