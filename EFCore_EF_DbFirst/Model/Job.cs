using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.Model
{
    [Table("Jobs", Schema = "hr")]
    public partial class Job
    {
        public Job()
        {
            Employees = new HashSet<Employee>();
            JobHistories = new HashSet<JobHistory>();
        }

        [Column("job_id")]
        [StringLength(10)]
        public string JobId { get; set; }
        [Required]
        [Column("job_title")]
        [StringLength(35)]
        public string JobTitle { get; set; }
        [Column("min_salary", TypeName = "money")]
        public decimal? MinSalary { get; set; }
        [Column("max_salary", TypeName = "money")]
        public decimal? MaxSalary { get; set; }

        [InverseProperty("Job")]
        public ICollection<Employee> Employees { get; set; }
        [InverseProperty("Job")]
        public ICollection<JobHistory> JobHistories { get; set; }
    }
}
