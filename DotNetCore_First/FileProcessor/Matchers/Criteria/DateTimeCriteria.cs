using System;
using System.Collections.Generic;
using System.Globalization;

namespace FileProcessor.Matchers.Criteria
{
    public class DateTimeCriteria : ICriteria
    {
        private readonly string _paramName;
        private readonly string _format;

        public DateTimeCriteria(string paramName, string format)
        {
            _paramName = paramName;
            _format = format;
        }

        public IMatchResult Apply(string text, IDictionary<string, string> parameters, NextDelegate next)
        {
            if (!DateTime.TryParseExact(text.Substring(0, _format.Length), _format, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var result)) return MatchResult.None();
            parameters.Add(_paramName,result.ToString(_format));
            return next(text.Substring(_format.Length), parameters);

        }
    }
}