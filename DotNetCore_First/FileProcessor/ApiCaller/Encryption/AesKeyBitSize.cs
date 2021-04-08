namespace FileProcessor.ApiCaller.Encryption
{
    public class AesKeyBitSize
    {
        public int BitSize { get; }
        public int ByteSize { get; }
        public AesKeyBitSize(int bitSize)
        {
            BitSize = bitSize;
            ByteSize = bitSize / 8;
        }

        protected bool Equals(AesKeyBitSize other)
        {
            return BitSize == other.BitSize && ByteSize == other.ByteSize;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AesKeyBitSize) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (BitSize * 397) ^ ByteSize;
            }
        }

        public static readonly AesKeyBitSize Aes128 = new AesKeyBitSize(128);
        public static readonly AesKeyBitSize Aes256 = new AesKeyBitSize(256);
    }
}