using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace WebApi_Client
{
    public class TokenApiClient : ITokenApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly TokenOptions _tokenOptions;
        public TokenApiClient(HttpClient httpClient, IMemoryCache cache, IOptions<TokenOptions> options)
        {
            _httpClient = httpClient;
            _cache = cache;
            _tokenOptions = options.Value;
        }

        public async Task<Token> FetchTokenAsync()
        {
            var accessToken = await _cache.GetOrCreateAsync("ApiToken", async entry =>
            {
                var token = await GetNewToken();
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(token.ExpiresIn));
                return token;
            });
            return accessToken;
        }

        private async Task<Token> GetNewToken()
        {
            var content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("clientId", _tokenOptions.ClientId),
                new KeyValuePair<string,string>("clientSecret", _tokenOptions.ClientSecret)
            });
            var responseMessage = await (await _httpClient.PostAsync(_tokenOptions.TokenUri, content)).Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<Token>(responseMessage);
            return token;
        }
    }
}

