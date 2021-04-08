using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FileProcessor.ApiCallerHelper;

namespace FileProcessor.ApiCaller.ApiCallerHelper
{
    public class SendRequest
    {
        private readonly HttpRequestBuilder _builder;

        public SendRequest(HttpRequestBuilder builder)
        {
            _builder = builder;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            var request = new HttpRequestMessage()
            {
                Method = _builder.GetMethod(),
                RequestUri = new Uri(_builder.GetRequestUri())
            };
            if (_builder.GetContent() != null) request.Content = _builder.GetContent();
            if(!string.IsNullOrEmpty(_builder.GetBearerToken())) request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",_builder.GetBearerToken());

            request.Headers.Accept.Clear();
            if(!string.IsNullOrEmpty(_builder.GetAcceptHeader())) request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_builder.GetAcceptHeader()));

            //Setup Client
            var client = new HttpClient()
            {
                Timeout = _builder.GetTimeSpan()
            };

            var defaultHeaders = _builder.GetDefaultHeader();
            foreach (var header in defaultHeaders)
            {
                client.DefaultRequestHeaders.Add(header.Key,header.Value);
            }

            return await client.SendAsync(request);
        }
    }
}
