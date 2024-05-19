using SharedKernel;

namespace Domain.AggregateModels;

public class Customer : ValueObject
{
    public string? CompanyName { get; set; }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return CompanyName;
    }
}