namespace WebApi_Application.Models
{
    public class JwtTokenOptions
    {
        public const string JwtToken = "JwtTokenOptions";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int Expires { get; set; }
        public int RefreshExpires { get; set; }
    }
}
