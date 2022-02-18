using System;

namespace Second.Utils
{
    public class CommonHelperClass
    {
        public static int GetDefaultFy()
        {
            int curfy = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            if (currentMonth >= 10)
            {
                curfy = curfy + 1;
            }
            return curfy;
        }
    }
}

