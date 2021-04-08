namespace FileProcessor.Config
{
    public class DocumentHandlerConfig : BaseHandlerConfig
    {
        public string SrcZip { get; set; }
        public string DstZip { get; set; }
        public string SrcFile { get; set; }
        public string DstFile { get; set; }
        public string ClientNameParam { get; set; }
    }
}