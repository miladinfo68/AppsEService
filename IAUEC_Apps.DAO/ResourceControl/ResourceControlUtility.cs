using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.ResourceControl
{
   public static class ResourceControlUtility
    {
        /// <summary>
        /// convert ticks to String Time
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static string ToTime(this long entry)
        {
            var hour = entry / 36000000000;
            var min = (entry % 36000000000) / 600000000;
            var minString = min.ToString();
            if (min == 0)
            {
                minString = "00";
            }
            return string.Concat(hour, ":", minString);
        }
        public static string ToTime(this long? entry)
        {
            var hour = entry / 36000000000;
            var min = (entry % 36000000000) / 600000000;
            var minString = min.ToString();
            if (min == 0)
            {
                minString = "00";
            }
            return string.Concat(hour, ":", minString);
        }
    }
}
