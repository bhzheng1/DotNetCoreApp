using System;

namespace One.DAL.Entities
{
    public class JobHistory:BaseEntity
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string JobId { get; set; }
        public int? DepartmentId { get; set; }
    }
}