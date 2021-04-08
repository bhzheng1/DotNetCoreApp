using Microsoft.EntityFrameworkCore;

namespace One.DAL.Entities.Configurations
{
    public partial class JobsConfig
        : IEntityTypeConfiguration<Job>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Job> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Jobs", "hr");

            // key
            builder.HasKey(t => t.JobId);

            // properties
            builder.Property(t => t.JobId)
                .IsRequired()
                .HasColumnName("job_id")
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(10);

            builder.Property(t => t.JobTitle)
                .IsRequired()
                .HasColumnName("job_title")
                .HasColumnType("nvarchar(35)")
                .HasMaxLength(35);

            builder.Property(t => t.MinSalary)
                .HasColumnName("min_salary")
                .HasColumnType("money");

            builder.Property(t => t.MaxSalary)
                .HasColumnName("max_salary")
                .HasColumnType("money");

            // relationships
            #endregion
        }

    }
}
