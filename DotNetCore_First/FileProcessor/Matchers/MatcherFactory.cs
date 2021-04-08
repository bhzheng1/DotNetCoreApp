using System;
using System.Collections.Generic;
using System.Linq;
using FileProcessor.Config;
using FileProcessor.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FileProcessor.Matchers
{
    public class MatcherFactory
    {
        private readonly AppConfig _appConfig;
        private readonly IServiceProvider _provider;

        public MatcherFactory(IOptionsSnapshot<AppConfig> appConfig, IServiceProvider provider)
        {
            _appConfig = appConfig.Value;
            _provider = provider;
        }

        public IEnumerable<IFileHandler> GetHandlers() => _appConfig.Handlers.Select(Make);

        private IFileHandler Make(IConfiguration config)
        {
            var kind = config.GetValue<string>("Kind");
            var type = Type.GetType(kind);
            if(type==null)
                throw new Exception($"handler type `{kind}` not found in assembly - did you spell it correctly?");
            _provider.GetRequiredService<ScopedConfigHolder>().Configuration = config;
            return (IFileHandler) _provider.GetRequiredService(type);
        }
    }
}