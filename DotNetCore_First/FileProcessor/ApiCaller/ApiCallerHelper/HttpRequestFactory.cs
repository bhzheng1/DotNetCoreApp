using System;
using System.Net.Http;
using System.Threading.Tasks;
using FileProcessor.ApiCaller.ApiCallerHelper;
using FileProcessor.Helper;

namespace FileProcessor.ApiCallerHelper
{
    public interface IHttpRequestFactory { }
    public class HttpRequestFactory:IHttpRequestFactory
    {
        public async Task<HttpResponseMessage> Create(string requestUri, Guid userId, Guid clientId, object value)
        {
            var builder = new HttpRequestBuilder()
                .AddMethod(HttpMethod.Post)
                .AddRequestUri(requestUri)
                .AddDefaultHeaders("userId", userId.ToString())
                .AddDefaultHeaders("clientId",clientId.ToString())
                .AddContent(new JsonContent(value));
            SendRequest sendReq = new SendRequest(builder);
            return await sendReq.SendAsync();
        }
    }
}
