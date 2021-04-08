using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileProcessor.ApiCaller.Encryption.Config
{
    public class HighOrderAesKeyProvider : IAesKeyProvider
    {
        private readonly Func<string,byte[]> _reader;
        private readonly IDictionary<AesKeyBitSize,HighOrderConfigKeys> _configKeys;
        private readonly IDictionary<AesKeyBitSize,AesKey> _cipherKeys = new ConcurrentDictionary<AesKeyBitSize, AesKey>();

        public HighOrderAesKeyProvider(Func<string,byte[]> reader) : this(reader, HighOrderConfigKeys.Default)
        {
        }

        public HighOrderAesKeyProvider(Func<string,byte[]> reader, IDictionary<AesKeyBitSize,HighOrderConfigKeys> configKeys)
        {
            _reader = reader;
            _configKeys = configKeys;
        }

        public Task<AesKey> Get(AesKeyBitSize aesKeyBitSize)
        {
            return Task.Run(() =>
            {
                if (!_cipherKeys.ContainsKey(aesKeyBitSize))
                {
                    var configKeySet = _configKeys[aesKeyBitSize];
                    _cipherKeys[aesKeyBitSize] =
                        AesKey.KeyByValidating(aesKeyBitSize, _reader(configKeySet.CipherKey), _reader(configKeySet.CipherIv));
                }
                
                return _cipherKeys[aesKeyBitSize];
            });
        }
    }

    public class HighOrderConfigKeys
    {
        public string CipherKey { get; }
        public string CipherIv { get; }

        public HighOrderConfigKeys(string cipherKey, string cipherIv)
        {
            CipherKey = cipherKey;
            CipherIv = cipherIv;
        }
        
        public static readonly HighOrderConfigKeys Default128Bit = new HighOrderConfigKeys("Encryption.Aes128.Key", "Encryption.Aes128.Iv");
        public static readonly HighOrderConfigKeys Default256Bit = new HighOrderConfigKeys("Encryption.Aes256.Key", "Encryption.Aes256.Iv");
        public static readonly IDictionary<AesKeyBitSize,HighOrderConfigKeys> Default = new Dictionary<AesKeyBitSize, HighOrderConfigKeys>
        {
            {AesKeyBitSize.Aes128, Default128Bit},
            {AesKeyBitSize.Aes256, Default256Bit}
        };
    }
}