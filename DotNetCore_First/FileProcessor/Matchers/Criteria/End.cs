using System.Collections.Generic;

namespace FileProcessor.Matchers.Criteria
{
    public class End : ICriteria
    {
        public IMatchResult Apply(string text, IDictionary<string, string> parameters, NextDelegate next)
        {
            return text.Length == 0 ? MatchResult.Found(parameters) : MatchResult.None();
        }
    }
}