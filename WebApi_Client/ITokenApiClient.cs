using System;
namespace WebApi_Client
{
    public interface ITokenApiClient
    {
        public Task<Token> FetchTokenAsync();
    }
}

