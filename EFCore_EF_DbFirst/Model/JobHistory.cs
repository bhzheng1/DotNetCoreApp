using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.Model
{
    [Table("JobHistory", Schema = "hr")]
    public partial class JobHistory
    {
        [Column("employee_id")]
        public int EmployeeId { get; set; }
        [Column("start_date", TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column("end_date", TypeName = "datetime")]
        public DateTime EndDate { get; set; }
        [Required]
        [Column("job_id")]
        [StringLength(10)]
        public string JobId { get; set; }
        [Column("department_id")]
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("JobHistories")]
        public Department Department { get; set; }
        [ForeignKey("EmployeeId")]
        [InverseProperty("JobHistories")]
        public Employee Employee { get; set; }
        [ForeignKey("JobId")]
        [InverseProperty("JobHistories")]
        public Job Job { get; set; }
    }
}
