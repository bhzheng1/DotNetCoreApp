using FileProcessor.Config;
using Microsoft.Extensions.Options;

namespace FileProcessor.ApiCaller.ApiCallerHelper
{
    public interface IRequestConfig
    {
        string BaseUri { get; set; }
        string ValidateAndUploadBulkDocumentsUri { get; set; }
        string ProcessEntitlementsFromSftpUri { get; set; }
    }
    public class RequestConfig:IRequestConfig
    {
        private string _baseUri;
        private string _validateAndUploadBulkDocumentUri;
        private string _processEntitlementsFromSftpUri;

        public RequestConfig(IOptions<AppConfig> appConfig)
        {
            _baseUri = appConfig.Value.ApiEndpoint;
            _validateAndUploadBulkDocumentUri = "/api/v1/UploadBulkDocs/ValidateAndUploadBulkDocuments";
            _processEntitlementsFromSftpUri = "/api/v1/UploadBulkDocs/ProcessEntitlementsFromSFTP";
        }
        public string BaseUri { get => _baseUri; set => _baseUri = value; }
        public string ValidateAndUploadBulkDocumentsUri { get=>_validateAndUploadBulkDocumentUri; set=>_validateAndUploadBulkDocumentUri=value; }
        public string ProcessEntitlementsFromSftpUri { get=>_processEntitlementsFromSftpUri; set=>_processEntitlementsFromSftpUri=value; }
    }
}