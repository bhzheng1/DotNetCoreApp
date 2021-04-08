using System;
using System.Collections.Generic;

namespace example.Model1
{
    public partial class Employee
    {
        public Employee()
        {
            InverseManager = new HashSet<Employee>();
            JobHistories = new HashSet<JobHistory>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string JobId { get; set; }
        public decimal? Salary { get; set; }
        public decimal? CommissionPct { get; set; }
        public int? ManagerId { get; set; }
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }
        public Job Job { get; set; }
        public Employee Manager { get; set; }
        public ICollection<Employee> InverseManager { get; set; }
        public ICollection<JobHistory> JobHistories { get; set; }
    }
}
