namespace WebApi_Client
{
    public record WebApiOptions
    {
        public const string SectionName = "WebApiSection";
        public string BaseUri { get; init; }
        public string GetSomethingUri { get; init; }
    }
}

