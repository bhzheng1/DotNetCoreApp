namespace FileProcessor.Matchers.Parsing
{
    public class StringLiteral : IToken
    {
        public string Text { get; }

        public StringLiteral(string text)
        {
            Text = text;
        }
    }
}