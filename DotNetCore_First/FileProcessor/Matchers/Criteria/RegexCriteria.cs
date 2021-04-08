using System.Collections.Generic;
using System.Text.RegularExpressions;
using FileProcessor.Extensions;

namespace FileProcessor.Matchers.Criteria
{
    public class RegexCriteria : ICriteria
    {
        private readonly string _paramName;
        private readonly Regex _regex;

        public RegexCriteria(string paramName, string regex)
        {
            _paramName = paramName;
            _regex = new Regex(regex,RegexOptions.IgnoreCase);
        }

        public IMatchResult Apply(string text, IDictionary<string, string> parameters, NextDelegate next)
        {
            var result = _regex.Match(text.FirstPathComponent());
            if (!result.Success) return MatchResult.None();
            int matchLength = 0;
            foreach (var resultGroup in result.Groups)
            {
                switch (resultGroup)
                {
                    case Match match:
                        if(_paramName!="_") parameters.Add(_paramName,match.Value);
                        matchLength = match.Value.Length;
                        break;
                    case Group group:
                        parameters.Add(group.Name,group.Value);
                        break;
                }
            }

            return next(text.Substring(matchLength), parameters);
        }
    }
}