namespace Second.DataAccess.Entities
{
    public class Department : BaseEntity
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? ManagerId { get; set; }
        public int? LocationId { get; set; }
    }
}