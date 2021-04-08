using System.Threading.Tasks;

namespace FileProcessor.ApiCaller.Encryption
{
    public interface ICryptoProvider
    {
        Task<ICrypto> GetCrypto(AesKeyBitSize aesKeyBitSize);
    }
}