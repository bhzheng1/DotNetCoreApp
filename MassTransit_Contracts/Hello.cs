//masstransit needs same contract having same namespace
namespace MassTransit_Contracts
{
    public record Hello
    {
        public required string Value { get; init; }
    }
}