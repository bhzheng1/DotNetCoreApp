using System.Collections.Generic;
using System.Linq;

namespace FileProcessor.Extensions
{
    public static class EnumerableExtensions
    {
        public static void Deconstruct<T>(this IEnumerable<T> self, out T head, out IEnumerable<T> tails)
        {
            var enumerable = self as T[] ?? self.ToArray();
            head = enumerable.FirstOrDefault();
            tails = enumerable.Skip(1);
        } 
    }
}