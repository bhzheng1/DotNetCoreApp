using Microsoft.EntityFrameworkCore;

namespace Second.DataAccess.Entities.Configurations
{
    public partial class RegionsConfig
        : IEntityTypeConfiguration<Region>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Region> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Regions", "hr");

            // key
            builder.HasKey(t => t.RegionId);

            // properties
            builder.Property(t => t.RegionId)
                .IsRequired()
                .HasColumnName("region_id")
                .HasColumnType("int");

            builder.Property(t => t.RegionName)
                .HasColumnName("region_name")
                .HasColumnType("nvarchar(25)")
                .HasMaxLength(25);

            // relationships
            #endregion
        }

    }
}
