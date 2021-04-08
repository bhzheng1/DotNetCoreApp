namespace FileProcessor.Matchers.Parsing
{
    public class Parameter:IToken
    {
        public string Name { get; set; }

        public Parameter(string name)
        {
            Name = name;
        }
    }
}
