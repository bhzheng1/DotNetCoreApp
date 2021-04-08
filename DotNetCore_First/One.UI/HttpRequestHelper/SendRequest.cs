using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace One.UI.HttpRequestHelper
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

            var request = new HttpRequestMessage
            {
                Method = _builder.GetMethod(),

                RequestUri = new Uri(_builder.GetRequestUri())
            };

            if (_builder.GetContent() != null)
                request.Content = _builder.GetContent();
            if (!string.IsNullOrEmpty((_builder.GetBearerToken())))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _builder.GetBearerToken());

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(_builder.GetAcceptHeader()))
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_builder.GetAcceptHeader()));

            // Setup Client
            var client = new HttpClient()
            {
                Timeout = _builder.GetTimeout()
            };

            var defaultHeaders = _builder.GetDefaultHeaders();
            foreach (var str in defaultHeaders)
                client.DefaultRequestHeaders.Add(str.Key, str.Value);

            var result = await client.SendAsync(request);
            return result;
        }

        public async Task<HttpResponseMessage> SendAsyncUploadDoc()
        {

            var request = new HttpRequestMessage
            {
                Method = _builder.GetMethod(),

                RequestUri = new Uri(_builder.GetRequestUri())
            };

            if (_builder.GetContent() != null)
                request.Content = _builder.GetContent();
            if (!string.IsNullOrEmpty((_builder.GetBearerToken())))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _builder.GetBearerToken());

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(_builder.GetAcceptHeader()))
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_builder.GetAcceptHeader()));

            // Setup Client
            var client = new HttpClient()
            {
                Timeout = _builder.GetTimeoutLong()
            };

            var defaultHeaders = _builder.GetDefaultHeaders();
            foreach (var str in defaultHeaders)
                client.DefaultRequestHeaders.Add(str.Key, str.Value);

            var result = await client.SendAsync(request);
            return result;
        }

        public async Task<HttpResponseMessage> PostAsyncForDocumentsUpload()
        {

            // Setup Client
            var client = new HttpClient()
            {
                Timeout = _builder.GetTimeoutLong()
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_builder.GetAcceptHeader()));

            var defaultHeaders = _builder.GetDefaultHeaders();
            foreach (var str in defaultHeaders)
                client.DefaultRequestHeaders.Add(str.Key, str.Value);

            var result = await client.PostAsync(_builder.GetRequestUri(), _builder.GetMultipartFormDataContent());
           // var retValue = await result.Content.ReadAsAsync<CommandResult<DocumentsUploadRequestResult>>();
            return result;
        }

        public async Task<HttpResponseMessage> PostAsyncForMediaUpload()
        {

            // Setup Client
            var client = new HttpClient()
            {
                Timeout = _builder.GetTimeoutLong()
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_builder.GetAcceptHeader()));

            var defaultHeaders = _builder.GetDefaultHeaders();
            foreach (var str in defaultHeaders)
                client.DefaultRequestHeaders.Add(str.Key, str.Value);

            var result = await client.PostAsync(_builder.GetRequestUri(), _builder.GetMultipartFormDataContent());
            // var retValue = await result.Content.ReadAsAsync<CommandResult<DocumentsUploadRequestResult>>();
            return result;
        }

        public async Task<HttpResponseMessage> PutAsyncForDocumentsUpload()
        {

            // Setup Client
            var client = new HttpClient()
            {
                Timeout = _builder.GetTimeout()
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_builder.GetAcceptHeader()));

            var defaultHeaders = _builder.GetDefaultHeaders();
            foreach (var str in defaultHeaders)
                client.DefaultRequestHeaders.Add(str.Key, str.Value);

            var result = await client.PutAsync(_builder.GetRequestUri(), _builder.GetMultipartFormDataContent());
            // var retValue = await result.Content.ReadAsAsync<CommandResult<DocumentsUploadRequestResult>>();
            return result;
        }





        public async Task<Stream> SendAsyncExpectingStream()
        {



            var request = new HttpRequestMessage
            {
                Method = _builder.GetMethod(),

                RequestUri = new Uri(_builder.GetRequestUri())
            };

            if (_builder.GetContent() != null)
                request.Content = _builder.GetContent();
            if (!string.IsNullOrEmpty((_builder.GetBearerToken())))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _builder.GetBearerToken());

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(_builder.GetAcceptHeader()))
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_builder.GetAcceptHeader()));

            // Setup Client
            var client = new HttpClient()
            {
                Timeout = _builder.GetTimeoutLong()
            };

            var defaultHeaders = _builder.GetDefaultHeaders();
            foreach (var str in defaultHeaders)
                client.DefaultRequestHeaders.Add(str.Key, str.Value);

            var resultStream = await client.GetStreamAsync(_builder.GetRequestUri());
            return resultStream;
        }

    }



}

