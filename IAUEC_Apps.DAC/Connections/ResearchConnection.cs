using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAC.Connections
{
   public class ResearchConnection
    {
       MyConnections.ResearchConnection res = new MyConnections.ResearchConnection();
        public string con
        {
            get
            {

                return "server=192.168.1.21; database=theses; user=teamuser ; password=t123*t456  ; connection timeout=30";
            }

        }
    }
}
