using System.Text.Json.Serialization;
namespace WebApi_Client
{
    public record Token
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}

