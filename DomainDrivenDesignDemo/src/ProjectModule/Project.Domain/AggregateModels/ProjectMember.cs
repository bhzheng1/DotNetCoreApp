using SharedKernel;

namespace Domain.AggregateModels;

public class ProjectMember : Entity<int>
{
    public ProjectMember(int id) : base(id)
    {
    }
}