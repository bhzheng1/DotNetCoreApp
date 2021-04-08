namespace FileProcessor.Config
{
    public class EntitlementHandlerConfig : BaseHandlerConfig
    {
        public string Src { get; set; }
        public string Dst { get; set; }
        public string ClientNameParam { get; set; }
    }
}