namespace DotNetCorePdfService.Models
{
    public class DocSettings
    {
        public DocSettings()
        {
            DocWidth = 792;
            DocHeight = 612;
            MarginLeft = 24;
            MarginRight = 24;
            MarginTop = 25;
            MarginBottom = 15;
            DotPerInches = 150;
            SaveQuality = 100;
        }
        public double DocWidth { get; set; }
        public double DocHeight { get; set; }
        public double MarginLeft { get; set; }
        public double MarginRight { get; set; }
        public double MarginTop { get; set; }
        public double MarginBottom { get; set; }
        public double DotPerInches { get; set; }
        public int SaveQuality { get; set; }
    }
}