using SharedKernel;

namespace Domain.AggregateModels;

public class Project : Entity<int>
{
    public Project(int id, string projectName, DateTime startDate, int headCountCap, Customer customer) : base(id)
    {
        ProjectName = projectName;
        StartDate = startDate;
        HeadCountCap = headCountCap;
        Customer = customer;
    }

    public string? ProjectName { get; set; }
    public DateTime StartDate { get; set; }
    public Customer Customer { get; set; }
    public DateTime? CompleteDate { get; set; }
    public bool IsComplete { get; set; }
    public int HeadCountCap { get; set; }
    public List<Employee> Members { get; set; } = new();
}