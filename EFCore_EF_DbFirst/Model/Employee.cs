using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.Model
{
    [Table("Employees", Schema = "hr")]
    public partial class Employee
    {
        public Employee()
        {
            InverseManager = new HashSet<Employee>();
            JobHistories = new HashSet<JobHistory>();
        }

        [Column("employee_id")]
        public int EmployeeId { get; set; }
        [Column("first_name")]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(25)]
        public string LastName { get; set; }
        [Required]
        [Column("email")]
        [StringLength(25)]
        public string Email { get; set; }
        [Column("phone_number")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [Column("hire_date", TypeName = "datetime")]
        public DateTime HireDate { get; set; }
        [Required]
        [Column("job_id")]
        [StringLength(10)]
        public string JobId { get; set; }
        [Column("salary", TypeName = "money")]
        public decimal? Salary { get; set; }
        [Column("commission_pct", TypeName = "decimal(2, 2)")]
        public decimal? CommissionPct { get; set; }
        [Column("manager_id")]
        public int? ManagerId { get; set; }
        [Column("department_id")]
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("Employees")]
        public Department Department { get; set; }
        [ForeignKey("JobId")]
        [InverseProperty("Employees")]
        public Job Job { get; set; }
        [ForeignKey("ManagerId")]
        [InverseProperty("InverseManager")]
        public Employee Manager { get; set; }
        [InverseProperty("Manager")]
        public ICollection<Employee> InverseManager { get; set; }
        [InverseProperty("Employee")]
        public ICollection<JobHistory> JobHistories { get; set; }
    }
}
