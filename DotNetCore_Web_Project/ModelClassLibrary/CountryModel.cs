namespace ModelClassLibrary
{
    public class CountryModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public string NationalFlagPath { get; set; }

        public CountryModel(string code, string name, string continent)
        {
            this.Code = code;
            this.Name = name;
            this.Continent = continent;
            this.NationalFlagPath = "Default.png";
        }
    }
}
