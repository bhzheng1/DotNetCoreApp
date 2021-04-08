using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using FileProcessor.ApiCaller.ApiCallerHelper;
using FileProcessor.ApiCaller.Model;
using FileProcessor.ApiCallerHelper;
using FileProcessor.Helper;

namespace FileProcessor.ApiCaller
{
    public interface IDocumentStorageApiCaller
    {
        Task<CommandResult<string>> ValidateAndUploadBulkDocuments(ValidateAndUploadBulkDocs docs);
        Task<CommandResult<string>> ProcessEntitlementsFromSftp(ValidateAndUploadBulkDocs docs);
    }

    public class DocumentStorageApiCaller:IDocumentStorageApiCaller
    {
        private readonly HttpClient _client;
        private readonly IRequestConfig _requestConfig;

        public DocumentStorageApiCaller(HttpClient client, IRequestConfig requestConfig)
        {
            _client = client;
            _requestConfig = requestConfig;
        }

        public async Task<CommandResult<string>> ValidateAndUploadBulkDocuments(ValidateAndUploadBulkDocs docs)
        {
            var url = _requestConfig.BaseUri + _requestConfig.ValidateAndUploadBulkDocumentsUri;
            using (var response = await _client.PostAsync(url, docs, new JsonMediaTypeFormatter()))
                return response.ContentAsType<CommandResult<string>>();
        }

        public async Task<CommandResult<string>> ProcessEntitlementsFromSftp(ValidateAndUploadBulkDocs docs)
        {
            var url = _requestConfig.BaseUri + _requestConfig.ProcessEntitlementsFromSftpUri;
            using (var response = await _client.PostAsync(url, docs, new JsonMediaTypeFormatter()))
                return response.ContentAsType<CommandResult<string>>();
        }
    }
}
