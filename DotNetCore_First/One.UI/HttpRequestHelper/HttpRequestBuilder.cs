using System;
using System.Collections.Generic;
using System.Net.Http;

namespace One.UI.HttpRequestHelper
{
    public class HttpRequestBuilder
    {
        private HttpMethod method = null;
        private string requestUri = "";
        private HttpContent content = null;
        private MultipartFormDataContent multiPartContent = null;
        private string bearerToken = "";
        private string acceptHeader = "application/json";
        private TimeSpan timeout = new TimeSpan(0, 0, 30);
        private TimeSpan timeoutLong = new TimeSpan(0, 5, 15);
        private Dictionary<string, string> defaultHeaders = new Dictionary<string, string>();
        public HttpRequestBuilder()
        {

        }

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            return this;
        }

        public HttpRequestBuilder AddContent(MultipartFormDataContent multiPartContent)
        {
            this.multiPartContent = multiPartContent;
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this.bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }

        public HttpRequestBuilder AddDefaultHeaders(string name, string value)
        {
            this.defaultHeaders[name] = value;
            return this;
        }

        public HttpMethod GetMethod()
        {
            return this.method;
        }

        public string GetRequestUri()
        {
            return this.requestUri;
        }

        public HttpContent GetContent()
        {
            return this.content;
        }

        public MultipartFormDataContent GetMultipartFormDataContent()
        {
            return this.multiPartContent;
        }

        public string GetBearerToken()
        {
            return this.bearerToken;
        }

        public string  GetAcceptHeader()
        {
            return this.acceptHeader;
        }

        public TimeSpan GetTimeout()
        {
            return this.timeout;
        }

        public TimeSpan GetTimeoutLong()
        {
            return this.timeoutLong;
        }

        public Dictionary<string, string> GetDefaultHeaders()
        {
            return this.defaultHeaders;
        }

    }
}
