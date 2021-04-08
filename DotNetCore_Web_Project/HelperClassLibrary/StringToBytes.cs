using System;
using System.Text;

namespace HelperClassLibrary
{
    public static class Base64Encode
    {
        //better than stream
        public static byte[] StringToBytesWithEncoding(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            var bytes = Encoding.UTF8.GetBytes(value);

            var result = new byte[bytes.Length + 3];

            Array.Copy(Encoding.UTF8.GetPreamble(), result, 3);
            Array.Copy(bytes, 0, result, 3, bytes.Length);

            return result;
        }

        public static byte[] StringToBytesWithStream(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            using (var ms = new System.IO.MemoryStream())
            using (var streamWriter = new System.IO.StreamWriter(ms, Encoding.UTF8))
            {
                streamWriter.Write(value);
                streamWriter.Flush();

                return ms.ToArray();
            }
        }


        public static bool BytesEquals(byte[] array1, byte[] array2)
        {
            if (array1 == null && array2 == null) return true;

            if (ReferenceEquals(array1, array2)) return true;

            if (array1?.Length != array2?.Length) return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i]) return false;
            }
            return true;
        }
    }
}
