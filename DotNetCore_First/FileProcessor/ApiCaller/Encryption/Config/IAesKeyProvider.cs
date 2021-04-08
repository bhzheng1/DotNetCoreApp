using System.Threading.Tasks;

namespace FileProcessor.ApiCaller.Encryption.Config
{
    public interface IAesKeyProvider
    {
        Task<AesKey> Get(AesKeyBitSize aesKeyBitSize);
    }
}
