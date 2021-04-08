using Microsoft.EntityFrameworkCore;

namespace One.DAL.Entities.Configurations
{
    public partial class LocationsConfig
        : IEntityTypeConfiguration<Location>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Location> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Locations", "hr");

            // key
            builder.HasKey(t => t.LocationId);

            // properties
            builder.Property(t => t.LocationId)
                .IsRequired()
                .HasColumnName("location_id")
                .HasColumnType("int");

            builder.Property(t => t.StreetAddress)
                .HasColumnName("street_address")
                .HasColumnType("nvarchar(40)")
                .HasMaxLength(40);

            builder.Property(t => t.PostalCode)
                .HasColumnName("postal_code")
                .HasColumnType("nvarchar(12)")
                .HasMaxLength(12);

            builder.Property(t => t.City)
                .IsRequired()
                .HasColumnName("city")
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30);

            builder.Property(t => t.StateProvince)
                .HasColumnName("state_province")
                .HasColumnType("nvarchar(25)")
                .HasMaxLength(25);

            builder.Property(t => t.CountryId)
                .HasColumnName("country_id")
                .HasColumnType("nchar(2)")
                .HasMaxLength(2);
            #endregion
        }

    }
}
