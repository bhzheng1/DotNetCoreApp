using System.IO;
using System.Text;

namespace FileProcessor.ApiCaller.Encryption
{
    public interface ICrypto
    {
        byte[] EncryptString(string plainText, Encoding encoding);
        byte[] Encrypt(byte[] plainText);
        Stream Encrypt(Stream plainText);

        string DecryptString(byte[] cipherText, Encoding encoding);
        byte[] Decrypt(byte[] cipherText);
        Stream Decrypt(Stream cipherText);
    }
}
