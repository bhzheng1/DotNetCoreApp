using System;
using System.IO;

namespace FileProcessor.Extensions
{
    public static class StringExtensions
    {
        public static bool IsLike(this string self, string other) =>
            self.Equals(other, StringComparison.OrdinalIgnoreCase);

        public static string FirstPathComponent(this string str)
        {
            var index = Math.Min(str.IndexOf(Path.DirectorySeparatorChar), str.IndexOf(Path.AltDirectorySeparatorChar));
            return index == -1 ? str : str.Substring(0, index);
        }

        public static string DropFirstPathComponent(this string str)
        {
            var index = Math.Min(str.IndexOf(Path.DirectorySeparatorChar), str.IndexOf(Path.AltDirectorySeparatorChar));
            return index == -1 ? String.Empty : str.Substring(index + 1);
        }

        public static string SubstringBefore(this string str, string until)
        {
            var index = str.IndexOf(until, StringComparison.Ordinal);
            return index == -1 ? str : str.Substring(0, index);
        }

        public static string SubstringAfter(this string str, string after)
        {
            var index = str.IndexOf(after, StringComparison.Ordinal);
            return index == -1 ? str : str.Substring(index + 1);
        }
    }
}
