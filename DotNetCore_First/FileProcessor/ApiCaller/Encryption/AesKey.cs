using System;

namespace FileProcessor.ApiCaller.Encryption
{
    public class AesKey
    {
        public byte[] Key { get; }
        public byte[] Iv { get; }
        public AesKey(byte[] key, byte[] iv)
        {
            Key = key;
            Iv = iv;
        }

        public static AesKey KeyByValidating(AesKeyBitSize aesKeyBitSize, byte[] key, byte[] iv)
        {
            if (key.Length != aesKeyBitSize.ByteSize)
                throw new ArgumentOutOfRangeException(nameof(key), "key does not match requested bit size");
            
            if (iv.Length != 12)
                throw new ArgumentOutOfRangeException(nameof(iv), "iv must be 12 bytes");
            
            return new AesKey(key, iv);
        }
    }
}
