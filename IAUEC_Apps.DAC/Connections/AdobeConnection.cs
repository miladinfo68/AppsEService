using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace IAUEC_Apps.DAC.Connections
{
    public class AdobeConnection
    {
        MyConnections.AdobeConnection adobe = new MyConnections.AdobeConnection();
        public string VCconnectionString
        {
            //
            get
            {
               // return "User=rohani;Password=6543217;server=192.168.30.190; database=adobe930919; connection timeout=30;";
                return adobe.VCconnectionString;
            }
        }

        public string AdobeconnectionString
        {
            get
            {
              //  return "User=rohani;Password=6543217;server=192.168.30.190; database=adobe930919; connection timeout=30;";
               return adobe.AdobeconnectionString;
            }
        }
        public string liveconnectionString
        {
            get
            {
                // return "User=rohani;Password=6543217;server=192.168.30.190; database=adobe930919; connection timeout=30;";
              return adobe.liveconnectionString;
            }
        }
        public string classconnectionString
        {
            get
            {
                
                // return "User=rohani;Password=6543217;server=192.168.30.190; database=adobe930919; connection timeout=30;";
                return adobe.classconnectionString;
            }
        }
        public string vc_new_connectionString
        {
            get
            {
               
                // return "User=rohani;Password=6543217;server=192.168.30.190; database=adobe930919; connection timeout=30;";
               return adobe.Vc_New_connectionString;
            }
        }
        public string vc_951
        {
            get
            {
                
               // return "User=rohani;Password=6543217;server=192.168.30.190; database=adobe930919; connection timeout=30;";
                return adobe.vc951;
               
            }
        }
        public string vc_952
        {
            get
            {

                // return "User=rohani;Password=6543217;server=192.168.30.190; database=adobe930919; connection timeout=30;";
                return adobe.vc952;

            }
        }
        public string vc_961
        {
            get
            {

                // return "User=rohani;Password=6543217;server=192.168.30.190; database=adobe930919; connection timeout=30;";
                return adobe.vc961;

            }
        }
        public string vc_962
        {
            get
            {

                return adobe.vc961;
                //return adobe.vc962;

            }
        }
        public string vc_971
        {
            get
            {

                return adobe.vc971;
                //return adobe.vc962;

            }
        }
        // public SqlConnection sql_vc951 { get { return adobe.SQL_vc951; } }

    }


   
      
    }
 

