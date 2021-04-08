using Microsoft.EntityFrameworkCore;

namespace One.DAL.Entities.Configurations
{
    public partial class EmployeeConfig
        : IEntityTypeConfiguration<Employee>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Employee> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Employees", "hr");

            // key
            builder.HasKey(t => t.EmployeeId);

            // properties
            builder.Property(t => t.EmployeeId)
                .IsRequired()
                .HasColumnName("employee_id")
                .HasColumnType("int");

            builder.Property(t => t.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasColumnName("last_name")
                .HasColumnType("nvarchar(25)")
                .HasMaxLength(25);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("nvarchar(25)")
                .HasMaxLength(25);

            builder.Property(t => t.PhoneNumber)
                .HasColumnName("phone_number")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.HireDate)
                .IsRequired()
                .HasColumnName("hire_date")
                .HasColumnType("datetime");

            builder.Property(t => t.JobId)
                .IsRequired()
                .HasColumnName("job_id")
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(10);

            builder.Property(t => t.Salary)
                .HasColumnName("salary")
                .HasColumnType("money");

            builder.Property(t => t.CommissionPct)
                .HasColumnName("commission_pct")
                .HasColumnType("decimal(2, 2)");

            builder.Property(t => t.ManagerId)
                .HasColumnName("manager_id")
                .HasColumnType("int");

            builder.Property(t => t.DepartmentId)
                .HasColumnName("department_id")
                .HasColumnType("int");
            #endregion
        }

    }
}
