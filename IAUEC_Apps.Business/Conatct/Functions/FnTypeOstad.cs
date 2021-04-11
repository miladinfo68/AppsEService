using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.Conatct.Functions
{
   public class FnTypeOstad
    {

        public static string GetTypeOstad(int id)
        {
            switch (id)
            {
                case 1:
                    {
                        return "OS_Mosh1";
                    }
                case 2:
                    {
                        return "OS_Mosh2";
                    }
                case 3:
                    {
                        return "OS_Rah1";
                    }
                case 4:
                    {
                        return "OS_Rah2";
                    }
                case 5:
                    {
                        return "OS_DI";
                    }
                case 6:
                    {
                        return "OS_DO";
                    }
                default:
                    return "";

            }
        }

    }
}
