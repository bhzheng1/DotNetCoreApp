using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace One.UI.HttpRequestHelper
{
    public interface IHttpRequestFactory
    {
        Task<HttpResponseMessage> Get(string requestUri);
        Task<HttpResponseMessage> Get(string requestUri, Guid clientId);
        Task<Stream> GetFile(string requestUri);
        Task<Stream> GetFile(string requestUri, Guid clientId);
        Task<HttpResponseMessage> Create(string requestUri, object value);
        Task<HttpResponseMessage> CreateUpload(string requestUri, object value);
        Task<HttpResponseMessage> PostDocuments(string requestUri, MultipartFormDataContent formDataContent);
        Task<HttpResponseMessage> PostMedia(string requestUri, MultipartFormDataContent formDataContent);
        Task<HttpResponseMessage> GetMediaStreamById(string requestUri);
        Task<HttpResponseMessage> UpdateDocument(string requestUri, MultipartFormDataContent formDataContent);
        Task<HttpResponseMessage> Update(string requestUri, object value);
        Task<HttpResponseMessage> Delete(string requestUri, object value);

        //Task<HttpResponseMessage> CreateRole(string requestUri, object value);

        //Task<HttpResponseMessage> UpdateAdminRole(string requestUri, object value);

        //Task<HttpResponseMessage> UpdateInvestorRole(string requestUri, object value);


        //Task<HttpResponseMessage> UpdateProspectRole(string requestUri, object value);


        //Task<HttpResponseMessage> DeleteRole(string requestUri, object value);

        //Task<HttpResponseMessage> UpdateClient(string requestUri, object value);

        Task<HttpResponseMessage> Get(string requestUri, object value);
    }
}
