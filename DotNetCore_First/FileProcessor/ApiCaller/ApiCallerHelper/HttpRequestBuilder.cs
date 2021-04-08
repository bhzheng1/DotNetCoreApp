using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FileProcessor.ApiCaller.ApiCallerHelper
{
    public interface IHttpRequestBuilder { }
    public class HttpRequestBuilder : IHttpRequestBuilder
    {
        private HttpMethod _method = null;
        private string _requestUri = "";
        private HttpContent _content = null;
        private MultipartFormDataContent _multiPartContent = null;
        private string _bearerToken = "";
        private string _acceptHeader = "application/json";
        private TimeSpan _timeout = new TimeSpan(0, 0, 15);
        private readonly Dictionary<string, string> _defaultHeaders = new Dictionary<string, string>();
        public HttpRequestBuilder() { }

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this._method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            _requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this._content = content;
            return this;
        }
        public HttpRequestBuilder AddContent(MultipartFormDataContent multiParContent)
        {
            _multiPartContent = multiParContent;
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this._bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this._acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this._timeout = timeout;
            return this;
        }

        public HttpRequestBuilder AddDefaultHeaders(string name, string value)
        {
            _defaultHeaders[name] = value;
            return this;
        }

        public HttpMethod GetMethod() => _method;
        public string GetRequestUri() => _requestUri;
        public HttpContent GetContent() => _content;
        public MultipartFormDataContent GetMultiPartFormDataContent() => _multiPartContent;
        public string GetBearerToken() => _bearerToken;
        public string GetAcceptHeader() => _acceptHeader;
        public TimeSpan GetTimeSpan() => _timeout;
        public Dictionary<string, string> GetDefaultHeader() => _defaultHeaders;
    }
}