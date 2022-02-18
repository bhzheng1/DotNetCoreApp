using System.Collections.Generic;

namespace Second.Utils
{
    public class Standard
    {
        public static IList<Standard> standardList = new List<Standard>() {
                new Standard(){ StandardID = 1, StandardName="Standard 1"},
                new Standard(){ StandardID = 2, StandardName="Standard 2"},
                new Standard(){ StandardID = 3, StandardName="Standard 3"}
             };

        public int StandardID { get; set; }
        public string StandardName { get; set; }
    }
}
