using System;
using System.IO;
using FileProcessor.Config;
using FileProcessor.Matchers;

namespace FileProcessor.Handlers
{
    public class DeleteFileHandler:BaseFileHandler
    {
        private readonly IgnoreHandlerConfig _config;
        private readonly PatternMatcher _patternMatcher;

        public DeleteFileHandler(IgnoreHandlerConfig config, PatternMatcher patternMatcher)
        {
            _config = config;
            _patternMatcher = patternMatcher;
        }

        public override string Description => _config.Description;
        public override IMatchResult Test(string fullPath) => _patternMatcher.Apply(_config.Src, fullPath);

        public override void Handle(Guid processId, string fullPath, FoundMatch match)
        {
            File.Delete(fullPath);
        }
    }
}
