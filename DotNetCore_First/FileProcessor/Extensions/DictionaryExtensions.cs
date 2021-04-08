using System.Collections.Generic;
using System.Collections.Immutable;

namespace FileProcessor.Extensions
{
    public static class DictionaryExtensions
    {
        public static IImmutableDictionary<TK, TV> Concat<TK, TV>(this IImmutableDictionary<TK, TV> self,
            IImmutableDictionary<TK, TV> other)
        {
            var result = new Dictionary<TK,TV>();
            foreach (var (key, value) in self) result.Add(key, value);
            //throw ArgumentException if new key exists in dic
            foreach (var (key, value) in other) result.Add(key,value);
            return result.ToImmutableDictionary();
        }

        public static IImmutableDictionary<TK, TV> ConcatReplace<TK, TV>(this IImmutableDictionary<TK, TV> self,
            IImmutableDictionary<TK, TV> other)
        {
            var result = new Dictionary<TK,TV>();
            foreach (var (key, value) in self) result[key] = value;
            foreach (var (key, value) in other) result[key] = value;
            return result.ToImmutableDictionary();
        }
    }
}