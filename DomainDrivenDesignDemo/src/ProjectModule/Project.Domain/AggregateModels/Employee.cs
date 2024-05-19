using SharedKernel;

namespace Domain.AggregateModels;

public class Employee : Entity<int>
{
    public Employee(int id, string fullName, DateTime dateHired, Guid internalEmployeeId) : base(id)
    {
        FullName = fullName;
        DateHired = dateHired;
        InternalEmployeeId = internalEmployeeId;
    }

    public string? FullName { get; set; }
    public DateTime DateHired { get; set; }
    public Guid InternalEmployeeId { get; set; }
}