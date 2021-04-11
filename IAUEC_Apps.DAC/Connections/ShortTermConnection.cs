using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAC.Connections
{
  public  class ShortTermConnection
    {
        MyConnections.AdobeConnection sc = new MyConnections.AdobeConnection();
       
        public string sc_con
        {
            get
            { 
                return "server=192.168.1.21; database=Short-TermCourses; user=sc_user ; password=sc_user ; connection timeout=30";
                //return sc.con_sc;

            }
        }
    }
}
