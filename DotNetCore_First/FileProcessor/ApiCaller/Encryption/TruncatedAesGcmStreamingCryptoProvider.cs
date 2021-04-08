using System.IO;

namespace FileProcessor.ApiCaller.Encryption
{
    public class TruncatedAesGcmStreamingCryptoProvider : IStreamingCryptoProvider
    {
        private readonly AeadBlockCipherFactory _cipherFactory;
        public TruncatedAesGcmStreamingCryptoProvider(AeadBlockCipherFactory cipherFactory)
        {
            _cipherFactory = cipherFactory;
        }

        public Stream Apply(Stream source)
        {
            return new TruncatedAesGcmReaderStream(source, _cipherFactory.Encryptor());
        }
    }
}