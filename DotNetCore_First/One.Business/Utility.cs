using System;
using System.IO;

namespace One.Business
{
    public interface IUtility
    {
        bool DeleteFolder(string path);
    }
    public class Utility
    {
        public bool DeleteFolder(string path)
        {
            try
            {
                if (!Directory.Exists(path)) return true;
                string name = null;
                if (File.Exists(path))
                {
                    name = Path.GetFileName(path);
                    File.Move(name, "temp.temp");
                }
                Directory.Delete(path, true);
                if (!string.IsNullOrEmpty(name))
                {
                    File.Move(path.Replace(name,"temp.temp"),path);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
