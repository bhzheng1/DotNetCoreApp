using System.Threading.Tasks;
using FileProcessor.ApiCaller.Encryption.Config;
using VaultSharp;

namespace FileProcessor.ApiCaller.DocRepo
{
    public class VaultDocRepoApiKeyProvider : IDocRepoApiKeyProvider
    {
        private readonly VaultAuthConfig _authConfig;
        private readonly VaultReadConfig _readConfig;
        private ApiKey _apiKey;

        public VaultDocRepoApiKeyProvider(VaultAuthConfig authConfig, VaultReadConfig readConfig)
        {
            _authConfig = authConfig;
            _readConfig = readConfig;
        }

        public async Task<ApiKey> Apply()
        {
            if (_apiKey == null)
            {
                var vaultClient = VaultClientFactory.CreateVaultClient(_authConfig.Endpoint, _authConfig.Auth);
                var secret = await vaultClient.GenericReadSecretAsync(_readConfig.Path);
                _apiKey = new ApiKey(secret.Data[_readConfig.KeyName].ToString());
            }

            return _apiKey;
        }
    }

    public class VaultReadConfig
    {
        public string Path { get; }
        public string KeyName { get; }

        public VaultReadConfig(string path, string keyName)
        {
            Path = path;
            KeyName = keyName;
        }
    }
}