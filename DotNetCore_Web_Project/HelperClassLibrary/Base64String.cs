using System;
using System.IO;

namespace HelperClassLibrary
{
    public class Base64String
    {
        public static string ImageToBase64String(string imagePath)
        {
            var ms = new MemoryStream();

            using (var s = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                s.CopyTo(ms);
                ms.Position = 0;
            }

            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
