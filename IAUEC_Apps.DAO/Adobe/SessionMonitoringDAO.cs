using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.Adobe
{
    public class SessionMonitoringDAO
    {
        
               
         
        /// <summary>
        /// لیست کلاس هایی آنلاین 
        /// </summary>
        /// <returns></returns>
        public DataTable GetMeetingsOnline()
        {
            SqlConnection conn = new SqlConnection() ;
            SettingDAO setting = new Adobe.SettingDAO();
            string termJary= System.Configuration.ConfigurationManager.AppSettings["Term"];
            conn.ConnectionString = setting.getAdobeConnectionString(termJary);
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            //SqlCommand cmd = new SqlCommand("R_SessionMonitoring_Meetings", conn);
            SqlCommand cmd = new SqlCommand("[Session_OnlineMeetings]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }


        public DataTable GetMeetingOnlineUser(string ScoId)
        {
            SqlConnection conn = new SqlConnection();
            SettingDAO setting = new Adobe.SettingDAO();
            string termJary = System.Configuration.ConfigurationManager.AppSettings["Term"];
            conn.ConnectionString = setting.getAdobeConnectionString(termJary);

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("Session_OnlineUserInMeetingsByScoId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SCO_ID", SqlDbType.NVarChar);
            cmd.Parameters["@SCO_ID"].Value = ScoId;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }



















    }
}
