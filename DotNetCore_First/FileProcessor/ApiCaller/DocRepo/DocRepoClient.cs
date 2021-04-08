using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Optional;
using Optional.Linq;

namespace FileProcessor.ApiCaller.DocRepo
{
    public sealed class DocRepoClient : IDisposable
    {
        private readonly HttpClient _client;

        public DocRepoClient(Uri endpoint, ApiKey apiKey)
        {
            _client = new HttpClient();
            _client.Timeout = new TimeSpan(0, 5, 0);
            _client.BaseAddress = endpoint;
            _client.DefaultRequestHeaders.ConnectionClose = true;
            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(apiKey.Key);
        }

        public DocRepoClient(HttpClient client, ApiKey apiKey)
        {
            _client = client;
            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(apiKey.Key);
        }

        public Task<string> Ping()
        {
            return _client.GetStringAsync(FormatRequestUri("ping"));
        }

        public async Task<bool> Store(string bucket, string fileName, Stream file, string tag = null)
        {
            var request = FormatRequestUri("store", bucket.Some(), fileName.Some(), tag.SomeNotNull());
            return (await _client.PostAsync(request, new StreamContent(file))).IsSuccessStatusCode;
        }

        public Task<Stream> Retrieve(string bucket, string fileName, string tag = null)
        {
            var request = FormatRequestUri("retrieve", bucket.Some(), fileName.Some(), tag.SomeNotNull());
            return _client.GetStreamAsync(request);
        }

        public async Task<bool> Delete(string bucket, string fileName, string tag = null)
        {   
            var request = FormatRequestUri("delete", bucket.Some(), fileName.Some(), tag.SomeNotNull());
            return (await _client.DeleteAsync(request)).IsSuccessStatusCode;
        }
        
        public async Task<bool> Exists(string bucket, string fileName, string tag = null)
        {
            var request = FormatRequestUri("exists", bucket.Some(), fileName.Some(), tag.SomeNotNull());
            return bool.Parse(await _client.GetStringAsync(request));
        }

        private string FormatRequestUri(string action)
        {
            return FormatRequestUri(action, Option.None<string>(), Option.None<string>(), Option.None<string>());
        }
        
        private string FormatRequestUri(string action, Option<string> maybeBucket, Option<string> maybeFilename, Option<string> maybeTag)
        {
            var bucket = maybeBucket.Select(_ => $"bucket={Uri.EscapeDataString(_)}").ValueOr("");
            var fileName = maybeFilename.Select(_ => $"filename={Uri.EscapeDataString(_)}").ValueOr("");
            var tag = maybeTag.Select(_ => $"tag={Uri.EscapeDataString(_)}").ValueOr("");
            var parameters = string.Join("&", bucket, fileName, tag);
            return $"api/v1/{action}?{parameters}";
        }
        
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
