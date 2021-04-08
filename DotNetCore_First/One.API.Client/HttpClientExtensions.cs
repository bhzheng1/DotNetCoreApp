using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace One.API.Client
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetAsync<T>(this HttpClient httpClient, Uri uri)
        {
            var result = await httpClient.GetStringAsync(uri);
            return Deserialize<T>(result);
        }

        public static async Task<Stream> GetStreamAsync(this HttpClient httpClient, string relativeUri)
        {
            var response = await httpClient.GetAsync(relativeUri, new CancellationToken());
            return await response.Content.ReadAsStreamAsync();
        }
        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string relativeUri)
        {
            var response = await httpClient.GetAsync(relativeUri,new CancellationToken());
            var result = await response.Content.ReadAsStringAsync();
            return Deserialize<T>(result);
        }

        public static async Task<Stream> PostStreamAsync(this HttpClient httpClient, Uri uri, object model)
        {
            using (var reqContent = new JsonContent(model))
            {
                var resp = await httpClient.PostAsync(uri, reqContent);
                return await resp.Content.ReadAsStreamAsync();
            }
        }

        public static async Task PostAsync(this HttpClient httpClient, Uri uri, object model)
        {
            using (var reqContent = new JsonContent(model))
                await httpClient.PostAsync(uri, reqContent);
        }

        public static async Task PutAsync(this HttpClient httpClient, Uri uri, object model)
        {
            using (var reqContent = new JsonContent(model))
                await httpClient.PutAsync(uri, reqContent);
        }

        public static async Task<T> PutAsync<T>(this HttpClient httpClient, Uri uri, object model)
        {
            using (var reqContent = new JsonContent(model))
            using (var result = await httpClient.PutAsync(uri, reqContent))
            using (var respContent = result.Content)
            {
                var text = await respContent.ReadAsStringAsync();
                return Deserialize<T>(text);
            }
        }

        private static T Deserialize<T>(string jsonText) => JsonConvert.DeserializeObject<T>(jsonText);
    }
}