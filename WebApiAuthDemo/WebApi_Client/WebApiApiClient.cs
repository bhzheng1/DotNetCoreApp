using System.Text.Json;
using System.Text.Json.Serialization;
namespace WebApi_Client
{
    public class WebApiApiClient : IWebApiClient
    {
        private readonly HttpClient _httpClient;
        public WebApiApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        private async Task<T> PostAsync<T>(HttpContent content, string uri) where T : class
        {
            var response = await PostAsync(content, uri);
            if (response.IsSuccessStatusCode)
            {
                var rContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(rContent, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            }
            return default(T);
        }

        private async Task<HttpResponseMessage> PostAsync(HttpContent content, string uri)
        {
            var response = await _httpClient.PostAsync(uri, content);
            return response;
        }

        private async Task<T> GetAsync<T>(string uri) where T : class
        {
            var repsonse = await GetAsync(uri);
            if (repsonse.IsSuccessStatusCode)
            {
                var content = await repsonse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            }
            return default(T);
        }

        private async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            return response;
        }
    }
}

