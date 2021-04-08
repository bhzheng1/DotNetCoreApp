using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace FileProcessor.ApiCaller.Encryption
{
    /// <summary>
    /// Produces IAeadBlockCipher for encryption/decryption.
    /// It should be noted that IAeadBlockCipher is *NOT* thread safe. 
    /// </summary>
    public class AeadBlockCipherFactory
    {
        private readonly AesKey _cipherKey;
        public AeadBlockCipherFactory(AesKey cipherKey)
        {
            _cipherKey = cipherKey;
        }

        public IAeadBlockCipher Encryptor()
        {
            return WithKey(_cipherKey, true);
        }

        public IAeadBlockCipher Decryptor()
        {
            return WithKey(_cipherKey, false);
        }

        private static IAeadBlockCipher WithKey(AesKey cipherKey, bool forEncryption)
        {
            var cipher = new GcmBlockCipher(new AesFastEngine());
            var parameters = new ParametersWithIV(new KeyParameter(cipherKey.Key), cipherKey.Iv);
            cipher.Init(forEncryption, parameters);
            return cipher;
        }
    }
}