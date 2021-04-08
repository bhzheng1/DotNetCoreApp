using System.Collections.Generic;
using System.Linq;
using FileProcessor.Extensions;

namespace FileProcessor.Matchers.Criteria
{
    public class MultiDirectoryCriteria : ICriteria
    {
        public IMatchResult Apply(string text, IDictionary<string, string> parameters, NextDelegate next)
        {
            while (text!=string.Empty)
            {
                var paramCopy = new Dictionary<string,string>(parameters);
                switch (next(text,paramCopy))
                {
                    case NoMatch _:
                        text = text.DropFirstPathComponent();
                        break;
                    case FoundMatch match:
                        match.Parameters.ToList().ForEach(x=>parameters.Add(x.Key,x.Value));
                        return MatchResult.Found(parameters);
                }
            }

            return next(text, parameters);
        }
    }
}