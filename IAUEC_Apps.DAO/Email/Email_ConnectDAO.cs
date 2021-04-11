using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.Email
{
    public class Email_ConnectDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        #region create
        public void CreateEmailConnect(string SmsText, string EmailText, int status)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Email.SP_Email_Create_EmailConnect";
            cmd.Parameters.AddWithValue("@SmsText", SmsText);
            cmd.Parameters.AddWithValue("@EmailText", EmailText);
            cmd.Parameters.AddWithValue("@StatusId", status);
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        #endregion
        #region read
        public DataTable GetConnectTextByStatus(int status)
         {
             SqlDataReader rdr = null;
             DataTable dt = new DataTable();
             conn.Open();
             SqlCommand cmdConnect = new SqlCommand("Email.SP_Email_GetConnectTextByStatus", conn);
             cmdConnect.CommandType = CommandType.StoredProcedure;
             cmdConnect.Parameters.Add("@status", SqlDbType.Int);
             cmdConnect.Parameters["@status"].Value = status;
             rdr = cmdConnect.ExecuteReader();
             dt.Load(rdr);
             rdr.Dispose();
             cmdConnect.Connection.Close();
             cmdConnect.Dispose();
             conn.Close();
             return dt;
         }
        public int GetConnectTypeByStcode(string stcode)
        {
            int ConnectType = 0;
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmdConnect = new SqlCommand("Email.SP_Email_GetConnectTextByStcode", conn);
            cmdConnect.CommandType = CommandType.StoredProcedure;
            cmdConnect.Parameters.Add("@stcode", SqlDbType.VarChar);
            cmdConnect.Parameters["@stcode"].Value = stcode;
            rdr = cmdConnect.ExecuteReader();
            dt.Load(rdr);
            ConnectType = int.Parse(dt.Rows[0]["ConnectType"].ToString());
            rdr.Dispose();
            cmdConnect.Connection.Close();
            cmdConnect.Dispose();
            conn.Close();
            return ConnectType;
        }
        #endregion
       
    }
}
