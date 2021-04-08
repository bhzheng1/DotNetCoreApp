using Microsoft.EntityFrameworkCore;

namespace One.DAL.Entities.Configurations
{
    public partial class JobHistoryConfig
        : IEntityTypeConfiguration<JobHistory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<JobHistory> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("JobHistory", "hr");

            // key
            builder.HasKey(t => new { t.EmployeeId, t.StartDate });

            // properties
            builder.Property(t => t.EmployeeId)
                .IsRequired()
                .HasColumnName("employee_id")
                .HasColumnType("int");

            builder.Property(t => t.StartDate)
                .IsRequired()
                .HasColumnName("start_date")
                .HasColumnType("datetime");

            builder.Property(t => t.EndDate)
                .IsRequired()
                .HasColumnName("end_date")
                .HasColumnType("datetime");

            builder.Property(t => t.JobId)
                .IsRequired()
                .HasColumnName("job_id")
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(10);

            builder.Property(t => t.DepartmentId)
                .HasColumnName("department_id")
                .HasColumnType("int");
            #endregion
        }

    }
}
