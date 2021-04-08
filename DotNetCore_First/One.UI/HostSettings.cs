namespace One.UI
{
    public class HostSettings
    {
        public string DefaultDomain { get; set; }
        public string DefaultSubDomain { get; set; }
        public string UseClientStyles { get; set; }
        public int ActivityTimeout { get; set; }
    }

    public class ClientSettings
    {
        public string ApiHostUrl { get; set; }
        public string ImporterServiceUrl { get; set; }
    }

    public class ActivityTimeoutSettings
    {
        public int ActivityTimeout { get; set; }
    }
}
