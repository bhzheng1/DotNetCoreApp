using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FileProcessor.ApiCaller.Encryption.Util;
using Optional;
using VaultSharp;
using VaultSharp.Backends.Authentication.Models;
using VaultSharp.Backends.Authentication.Models.AppId;

namespace FileProcessor.ApiCaller.Encryption.Config
{
    public class VaultAesKeyProvider : IAesKeyProvider
    {
        private readonly VaultAuthConfig _authConfig;
        private readonly IDictionary<AesKeyBitSize, AesKey> _cipherKeys = new ConcurrentDictionary<AesKeyBitSize, AesKey>();
        private readonly IDictionary<AesKeyBitSize, VaultPathConfig> _cipherKeyPaths;

        public VaultAesKeyProvider(VaultAuthConfig authConfig, IDictionary<AesKeyBitSize, VaultPathConfig> cipherKeyPaths)
        {
            _authConfig = authConfig;
            _cipherKeyPaths = cipherKeyPaths;
        }

        public async Task<AesKey> Get(AesKeyBitSize aesKeyBitSize)
        {
            if (!_cipherKeys.ContainsKey(aesKeyBitSize))
            {
                var pathConfig = _cipherKeyPaths[aesKeyBitSize];
                var vaultClient = VaultClientFactory.CreateVaultClient(_authConfig.Endpoint, _authConfig.Auth);
                var secret = await vaultClient.GenericReadSecretAsync(pathConfig.Path);
                var cipherKey = AesKey.KeyByValidating(
                    aesKeyBitSize,
                    Encoding.ASCII.GetBytes(secret.Data[pathConfig.CipherKeyName].ToString()),
                    Encoding.ASCII.GetBytes(secret.Data[pathConfig.IvKeyName].ToString()));
                _cipherKeys[aesKeyBitSize] = cipherKey;
            }

            return _cipherKeys[aesKeyBitSize];
        }
    }

    public class VaultAuthConfig
    {
        public Uri Endpoint { get; }
        public IAuthenticationInfo Auth { get; }

        public VaultAuthConfig(Uri endpoint, IAuthenticationInfo auth)
        {
            Endpoint = endpoint;
            Auth = auth;
        }

        public static VaultAuthConfig MakeForAppIdAuth(Uri endpoint, string mountPoint, string appId, Option<string> maybeUserId)
        {
            var userId = maybeUserId.ValueOr(VaultUserId.Get());
#pragma warning disable 618
            return new VaultAuthConfig(endpoint, new AppIdAuthenticationInfo(mountPoint, appId, userId));
#pragma warning restore 618
        }
    }

    public class VaultPathConfig
    {
        public string Path { get; }
        public string CipherKeyName { get; }
        public string IvKeyName { get; }

        public VaultPathConfig(string path, string cipherKeyName, string ivKeyName)
        {
            Path = path;
            CipherKeyName = cipherKeyName;
            IvKeyName = ivKeyName;
        }
    }
}