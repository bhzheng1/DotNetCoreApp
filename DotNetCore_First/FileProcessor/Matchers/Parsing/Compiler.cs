using System.Collections.Generic;
using FileProcessor.Extensions;
using FileProcessor.Matchers.Criteria;

namespace FileProcessor.Matchers.Parsing
{
    public class Compiler
    {
        private readonly Tokenizer _tokenizer;

        public Compiler(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public NextDelegate Apply(string criteria) => MakeDelegate(MakeCriterias(criteria));

        private IEnumerable<ICriteria> MakeCriterias(string criteria)
        {
            foreach (var token in _tokenizer.Apply(criteria))
            {
                
            }
            yield return new End();
        }

        private NextDelegate MakeDelegate(IEnumerable<ICriteria> criterias)
        {
            return (text, parameters) =>
            {
                var (head, tails) = criterias;
                return head == null ? MatchResult.None() : head.Apply(text, parameters, MakeDelegate(tails));
            };
        }
    }
}