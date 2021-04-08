using System;
using System.Collections.Generic;

namespace example.Model1
{
    public partial class JobHistory
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string JobId { get; set; }
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }
        public Employee Employee { get; set; }
        public Job Job { get; set; }
    }
}
