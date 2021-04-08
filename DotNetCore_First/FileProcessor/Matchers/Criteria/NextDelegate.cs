using System;
using System.Collections.Generic;
using FileProcessor.Extensions;

namespace FileProcessor.Matchers.Criteria
{
    public delegate IMatchResult NextDelegate(string text, IDictionary<string, string> parameters);

    public class StaticCriteria : ICriteria
    {
        private readonly string _fragment;

        public StaticCriteria(string fragment)
        {
            _fragment = fragment;
        }

        public IMatchResult Apply(string text, IDictionary<string, string> parameters, NextDelegate next)
        {
            return text.FirstPathComponent().StartsWith(_fragment, StringComparison.OrdinalIgnoreCase)
                ? next(text.Substring(_fragment.Length), parameters)
                : MatchResult.None();
        }
    }
}
