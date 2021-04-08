using System.Collections.Generic;
using System.Collections.Immutable;

namespace FileProcessor.Matchers
{
    public interface IMatchResult { }

    public class NoMatch : IMatchResult { }

    public class FoundMatch : IMatchResult
    {
        public FoundMatch(IImmutableDictionary<string, string> parameters)
        {
            Parameters = parameters;
        }

        public IImmutableDictionary<string,string> Parameters { get; }
    }

    public static class MatchResult
    {
        private static readonly IMatchResult StaticNone = new NoMatch();
        private static readonly IMatchResult EmptyFound = new FoundMatch(new Dictionary<string,string>().ToImmutableDictionary());
        public static IMatchResult None() => StaticNone;
        public static IMatchResult Found() => EmptyFound;
        public static IMatchResult Found(IDictionary<string,string> parameters)=>new FoundMatch(parameters.ToImmutableDictionary());
        public static IMatchResult Found(IImmutableDictionary<string,string> parameters)=>new FoundMatch(parameters);
    }
}