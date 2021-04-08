using System;

namespace FileProcessor.ApiCaller.Model
{
    public class ValidateAndUploadBulkDocs
    {
        public Guid UploadProcessId { get; set; }
        public string Path { get; set; }
        public string ClientFolderTag { get; set; }
        public string ZipFileName { get; set; }
    }
}