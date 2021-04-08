using System.IO;

namespace FileProcessor.ApiCaller.Encryption
{
    public interface IStreamingCryptoProvider
    {
        Stream Apply(Stream source);
    }
}