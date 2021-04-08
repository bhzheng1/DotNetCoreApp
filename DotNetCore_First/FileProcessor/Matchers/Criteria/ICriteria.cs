using System.Collections.Generic;

namespace FileProcessor.Matchers.Criteria
{
    public interface ICriteria
    {
        IMatchResult Apply(string text, IDictionary<string, string> parameters, NextDelegate next);
    }
}