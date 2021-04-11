using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyConnections;

namespace IAUEC_Apps.DAC.Connections
{
    public class AmozeshConnection
    {
        MyConnections.AmozeshConnection am = new MyConnections.AmozeshConnection();
        public string con
        {
            get
            {
                return "server=192.168.1.21; database=amozesh; user=teamuser ; password=t123*t456 ; connection timeout=30";
                //    return "server=192.168.12.119; database=amozesh; user=teamuser ; password=t123@456 ; connection timeout=30";

            }
        }
        public string con_sida
        {
            get
            {
                //return "server=192.168.1.21; database=Supplementary_test; user=teamuser ; password=t123*t456 ; connection timeout=30";
                return "server=192.168.1.21; database=Supplementary; user=teamuser ; password=t123*t456 ; connection timeout=30";


            }
        }
    }
}
