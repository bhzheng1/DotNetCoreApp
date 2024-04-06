namespace MassTransit_WebApplication.Contracts
{
    public record HelloMessage
    {
        public string Value { get; init; }
    }

    public record HelloWorldMessage
    {
        public string Value { get; init; }
    }
}