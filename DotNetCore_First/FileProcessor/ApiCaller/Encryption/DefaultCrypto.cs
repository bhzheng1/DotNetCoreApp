using System.IO;
using System.Text;

namespace FileProcessor.ApiCaller.Encryption
{
    public class DefaultCrypto : ICrypto
    {
        private readonly IStreamingCryptoProvider _cryptoProvider;

        public DefaultCrypto(IStreamingCryptoProvider cryptoProvider)
        {
            _cryptoProvider = cryptoProvider;
        }
        
        public byte[] EncryptString(string plainText, Encoding encoding)
        {
            return Encrypt(encoding.GetBytes(plainText));
        }

        public byte[] Encrypt(byte[] plainText)
        {
            using (var crypto = Encrypt(new MemoryStream(plainText)))
            using (var dest = new MemoryStream())
            {
                crypto.CopyTo(dest);
                return dest.ToArray();
            }
        }

        public Stream Encrypt(Stream plainText)
        {
            return _cryptoProvider.Apply(plainText);
        }

        public string DecryptString(byte[] cipherText, Encoding encoding)
        {
            return encoding.GetString(Decrypt(cipherText));
        }

        public byte[] Decrypt(byte[] cipherText)
        {
            using (var crypto = Decrypt(new MemoryStream(cipherText)))
            using (var dest = new MemoryStream())
            {
                crypto.CopyTo(dest);
                return dest.ToArray();
            }
        }

        public Stream Decrypt(Stream cipherText)
        {
            return _cryptoProvider.Apply(cipherText);
        }
    }
}