using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAC.Connections
{
    public class SuppConnection
    {
        MyConnections.SuppConnection supp = new MyConnections.SuppConnection();
        MyConnections.local_SuppConnection local_supp = new MyConnections.local_SuppConnection();
        public string Supp_con
        {
            get
            {
                //return "server=192.168.1.21; database=Supplementary_test; user=teamuser ; password=t123*t456  ; connection timeout=30";
                //return "server=192.168.12.119; database=Supplementary; user=teamuser ; password=t123@456  ; connection timeout=30";
                return "Data Source = 192.168.1.21; database = Supplementary;  User ID = teamuser; Password = t123*t456; connection timeout=30";
                
            }
        }
        public string UserAccess_con
        {
            get
            {
                return "server=192.168.1.21; database=appsuseraccess_s; user=teamuser ; password=t123*t456  ; connection timeout=30";
                // return local_supp.UserAccess_con;

            }
        }
        public string log_con
        {
            get
            {
                return "server=192.168.1.21; database=Apps_Log; user=teamuser ; password=t123*t456  ; connection timeout=30";
                // return local_supp.log_con;


            }

        }



        }


 }
    
