using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.CommonClasses
{
    public static class persianCharacters
    {
        public static char yeFarsi = (char)1740;
        public static char yeArabic = (char)1610;
        public static char keFarsi = (char)1705;
        public static char keArabic = (char)1603;


        public static Dictionary<string, int> persianDigits = new Dictionary<string, int>()
        {
            ["۰"] = 0,
            ["۱"] = 1,
            ["۲"] = 2,
            ["۳"] = 3,
            ["۴"] = 4,
            ["۵"] = 5,
            ["۶"] = 6,
            ["۷"] = 7,
            ["۸"] = 8,
            ["۹"] = 9
        };
    }
}
