using System;

namespace FileProcessor.ApiCaller.Encryption.Util
{
    public static class VaultUserId
    {
        private const string Salt = "xfwavJ66uqWkVD";

        public static string Get()
        {
            return HashHelper.Sha256Hash(Environment.MachineName, Salt);
        }
    }
}