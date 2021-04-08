using System.Collections.Generic;
using System.IO;

namespace FileProcessor.Matchers.Criteria
{
    public class PathCriteria : ICriteria
    {
        public IMatchResult Apply(string text, IDictionary<string, string> parameters, NextDelegate next)
        {
            return text.Length > 0 && text[0] == Path.DirectorySeparatorChar
                ? next(text.Substring(1), parameters)
                : MatchResult.None();
        }
    }
}