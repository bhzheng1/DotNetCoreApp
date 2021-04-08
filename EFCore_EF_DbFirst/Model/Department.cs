using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.Model
{
    [Table("Departments", Schema = "hr")]
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
            JobHistories = new HashSet<JobHistory>();
        }

        [Column("department_id")]
        public int DepartmentId { get; set; }
        [Required]
        [Column("department_name")]
        [StringLength(30)]
        public string DepartmentName { get; set; }
        [Column("manager_id")]
        public int? ManagerId { get; set; }
        [Column("location_id")]
        public int? LocationId { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("Departments")]
        public Location Location { get; set; }
        [InverseProperty("Department")]
        public ICollection<Employee> Employees { get; set; }
        [InverseProperty("Department")]
        public ICollection<JobHistory> JobHistories { get; set; }
    }
}
