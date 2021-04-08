using System;
using System.Net.Http;
using FileProcessor.ApiCaller.DocRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;

namespace FileProcessor.ApiCaller
{
    class DocClientRepoFactory : ITypedHttpClientFactory<DocRepoClient>
    {
            private readonly ApiKey _apiKey;
            private readonly Uri _baseAddress;

            public DocClientRepoFactory(IConfiguration configuration)
            {
                IConfigurationSection docRepConfig = configuration.GetSection("DocumentRepository");
                _apiKey = new ApiKey(docRepConfig["ApiKey"]);
                _baseAddress = new Uri(docRepConfig["Endpoint"]);
            }

            public DocRepoClient CreateClient(HttpClient httpClient)
            {
                httpClient.BaseAddress = _baseAddress;
                return new DocRepoClient(httpClient,_apiKey);
            }
        }
    }
