using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAC.Connections
{
     public class HrConnection
    {
         MyConnections.HRConnection hr = new MyConnections.HRConnection();
         public string HR_con
         {
             get
             {
                return "server=192.168.1.21; database=HR; user=teamuser ; password=t123*t456  ; connection timeout=30";
                //return hr.HR_con;
            }
         }
    }
}
