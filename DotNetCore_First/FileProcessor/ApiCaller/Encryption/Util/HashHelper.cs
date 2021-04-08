using System;
using System.Security.Cryptography;
using System.Text;

namespace FileProcessor.ApiCaller.Encryption.Util
{
    public static class HashHelper
    {
        /// <summary>
        /// Returns a Sha 256 compatable Hex string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="salt"></param>
        /// <returns>Hex string</returns>
        public static string Sha256Hash(string value, string salt)
        {
            var hasher = SHA256.Create();
            var saltedId = string.Format("{0}{1}", value, salt);
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(saltedId));
            var result = ByteArrayToString(hash);
            return result;
        }
        
        private static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}