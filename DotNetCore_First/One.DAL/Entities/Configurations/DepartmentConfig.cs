using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace One.DAL.Entities.Configurations
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Departments", "hr");

            // key
            builder.HasKey(t => t.DepartmentId);

            // properties
            builder.Property(t => t.DepartmentId)
                .IsRequired()
                .HasColumnName("department_id")
                .HasColumnType("int");

            builder.Property(t => t.DepartmentName)
                .IsRequired()
                .HasColumnName("department_name")
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30);

            builder.Property(t => t.ManagerId)
                .HasColumnName("manager_id")
                .HasColumnType("int");

            builder.Property(t => t.LocationId)
                .HasColumnName("location_id")
                .HasColumnType("int");
            #endregion
        }

    }
}
