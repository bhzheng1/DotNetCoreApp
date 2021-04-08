using System;
using System.Collections.Generic;
using System.Text;
using FileProcessor.Matchers;
using Microsoft.AspNetCore.Identity;

namespace FileProcessor.Handlers
{
    public interface IFileHandler
    {
        string Description { get; }
        IMatchResult Test(string fullPath);
        void Handle(Guid processId, string fullPath, FoundMatch match);
    }
    public abstract class BaseFileHandler:IFileHandler
    {
        public abstract string Description { get; }
        public abstract IMatchResult Test(string fullPath);

        public abstract void Handle(Guid processId, string fullPath, FoundMatch match);

        protected string ProcessIdDirectoryId(Guid processId) => processId.ToString();
    }
}
