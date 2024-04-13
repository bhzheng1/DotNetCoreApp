namespace WebApi_Client
{
    public record TokenOptions
    {
        public const string SectionName = "TokenSection";
        public string BaseUri { get; init; }
        public string TokenUri { get; init; }
        public string ClientId { get; init; }
        public string ClientSecret { get; init; }
    }
}

