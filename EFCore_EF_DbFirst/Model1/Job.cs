using System;
using System.Collections.Generic;

namespace example.Model1
{
    public partial class Job
    {
        public Job()
        {
            Employees = new HashSet<Employee>();
            JobHistories = new HashSet<JobHistory>();
        }

        public string JobId { get; set; }
        public string JobTitle { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<JobHistory> JobHistories { get; set; }
    }
}
