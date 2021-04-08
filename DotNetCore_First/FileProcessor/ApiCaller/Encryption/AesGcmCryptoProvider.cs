using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileProcessor.ApiCaller.Encryption.Config;

namespace FileProcessor.ApiCaller.Encryption
{
    public class AesGcmCryptoProvider : ICryptoProvider
    {
        private readonly IAesKeyProvider _keyProvider;
        private readonly Func<AeadBlockCipherFactory, IStreamingCryptoProvider> _streamingCryptoProviderFactory;
        private readonly IDictionary<AesKeyBitSize,ICrypto> _cryptoCache = new ConcurrentDictionary<AesKeyBitSize,ICrypto>();

        public AesGcmCryptoProvider(IAesKeyProvider keyProvider, Func<AeadBlockCipherFactory,IStreamingCryptoProvider> streamingCryptoProviderFactory)
        {
            _keyProvider = keyProvider;
            _streamingCryptoProviderFactory = streamingCryptoProviderFactory;
        }

        public async Task<ICrypto> GetCrypto(AesKeyBitSize aesKeyBitSize)
        {
            if (!_cryptoCache.ContainsKey(aesKeyBitSize))
                _cryptoCache[aesKeyBitSize] = new DefaultCrypto(await GetCipherFactory(aesKeyBitSize));
            
            return _cryptoCache[aesKeyBitSize];
        }

        private async Task<IStreamingCryptoProvider> GetCipherFactory(AesKeyBitSize aesKeyBitSize)
        {
            var key = await _keyProvider.Get(aesKeyBitSize);
            return _streamingCryptoProviderFactory(new AeadBlockCipherFactory(key));
        }
    }
}