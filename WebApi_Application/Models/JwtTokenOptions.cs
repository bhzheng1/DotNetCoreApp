namespace WebApi_Application.Models
{
    public class JwtTokenOptions
    {
        public const string JwtToken = "JwtTokenOptions";
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
        public required string Secret { get; init; }
        public int Expires { get; init; }
        public int RefreshExpires { get; init; }
    }
}
