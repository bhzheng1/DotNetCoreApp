using System;
using System.Collections.Generic;

namespace example.Model1
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
            JobHistories = new HashSet<JobHistory>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? ManagerId { get; set; }
        public int? LocationId { get; set; }

        public Location Location { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<JobHistory> JobHistories { get; set; }
    }
}
