using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace FileProcessor.Extensions
{
    public static class LoggingExtensions
    {
        public static IDisposable BeginScopeKv(this ILogger self, params (string, object)[] args)
        {
            var dict = new Dictionary<string, object>();
            foreach (var (key, value) in args)
            {
                dict[key] = value;
            }

            return self.BeginScope(dict);
        }
    }
}