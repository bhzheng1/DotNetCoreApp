using Microsoft.EntityFrameworkCore;

namespace One.DAL.Entities.Configurations
{
    public partial class CountriesConfig
        : IEntityTypeConfiguration<Country>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Country> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Countries", "hr");

            // key
            builder.HasKey(t => t.CountryId);

            // properties
            builder.Property(t => t.CountryId)
                .IsRequired()
                .HasColumnName("country_id")
                .HasColumnType("nchar(2)")
                .HasMaxLength(2);

            builder.Property(t => t.CountryName)
                .HasColumnName("country_name")
                .HasColumnType("nvarchar(40)")
                .HasMaxLength(40);

            builder.Property(t => t.RegionId)
                .HasColumnName("region_id")
                .HasColumnType("int");

            #endregion
        }

    }
}
