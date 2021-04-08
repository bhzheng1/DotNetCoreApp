using System.Collections.Generic;
using FileProcessor.Matchers.Parsing;

namespace FileProcessor.Matchers
{
    public class PatternMatcher
    {
        private readonly Compiler _compiler;

        public PatternMatcher(Compiler compiler)
        {
            _compiler = compiler;
        }

        public IMatchResult Apply(string criteria, string fullPath) =>
            _compiler.Apply(criteria)(fullPath, new Dictionary<string, string>());
    }
}
