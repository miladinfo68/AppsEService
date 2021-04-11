using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.AdobeClasses;
using System.Data.SqlClient;
using System.Data;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.CommonDAO
{
    public class RecordsDAO
    {
        //SqlConnection conn_VC = new SqlConnection(new AdobeConnection().VCconnectionString);
        //SqlConnection conn_Adobe = new SqlConnection(new AdobeConnection().AdobeconnectionString);
        //SqlConnection connNew = new SqlConnection(new AdobeConnection().liveconnectionString);

        public DataTable LinkOfClassWithLessonCodeByCodeClassAndTime(string code,string date,string term)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            Adobe.SettingDAO setting = new Adobe.SettingDAO();
            conn.ConnectionString = setting.getAdobeConnectionString(term);
            
            conn.Open();
            cmd.CommandText = "View_LinkOfClassWithLessonCodeByCodeClass";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@code", SqlDbType.NVarChar);
            cmd.Parameters["@code"].Value = code;
            cmd.Parameters.AddWithValue("@date", date);
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
          
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
           

            return dt;
        }

        public DataTable EditLinkOfClassByClassCodeAndDateAndTime(string code, string Date, int time, string term)//sargolChangeTypeClassCode
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            Adobe.SettingDAO setting = new Adobe.SettingDAO();
            conn.ConnectionString = setting.getAdobeConnectionString(term);
            
            conn.Open();
            cmd.CommandText = "Edit_RecordsOfMeetings";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.Add("@code", SqlDbType.NVarChar);
            cmd.Parameters["@code"].Value = code;
            cmd.Parameters.Add("@date", SqlDbType.VarChar);
            cmd.Parameters["@date"].Value = Date;
            cmd.Parameters.AddWithValue("@time", time);
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();

            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();


            return dt;

        }

        public DataTable LinkOfMeetingsAndContentsByCodeClassAndTime(string code, string date, string term)
        {
            SqlDataReader rdr;
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            Adobe.SettingDAO setting = new Adobe.SettingDAO();
            conn.ConnectionString = setting.getAdobeConnectionString(term);

            conn.Open();
            cmd.CommandText = "R_LinkOfMeetingWithContents";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@code", SqlDbType.NVarChar);
            cmd.Parameters["@code"].Value = code;
            cmd.Parameters.AddWithValue("@date", date);
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
