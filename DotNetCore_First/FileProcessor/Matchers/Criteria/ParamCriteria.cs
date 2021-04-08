using System.Collections.Generic;
using FileProcessor.Extensions;

namespace FileProcessor.Matchers.Criteria
{
    public class ParamCriteria : ICriteria
    {
        private readonly string _paramName;

        public ParamCriteria(string paramName)
        {
            _paramName = paramName;
        }

        public IMatchResult Apply(string text, IDictionary<string, string> parameters, NextDelegate next)
        {
            var value = text.FirstPathComponent();
            parameters.Add(_paramName,value);
            return next(text.Substring(value.Length), parameters);
        }
    }
}