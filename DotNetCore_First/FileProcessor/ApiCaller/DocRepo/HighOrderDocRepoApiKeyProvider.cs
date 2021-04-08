using System;
using System.Threading.Tasks;

namespace FileProcessor.ApiCaller.DocRepo
{
    public class HighOrderDocRepoApiKeyProvider : IDocRepoApiKeyProvider
    {
        private readonly Func<string> _reader;
        private ApiKey _apiKey;
        
        public HighOrderDocRepoApiKeyProvider(Func<string> reader)
        {
            _reader = reader;
        }

        public Task<ApiKey> Apply()
        {
            if (_apiKey == null)
                _apiKey = new ApiKey(_reader());

            return Task.FromResult(_apiKey);
        }
    }
}