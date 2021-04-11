using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace IAUEC_Apps.DAO.CommonDAO
{
   public class ProfessorPresentDetailsDAO
    { 
        public DateTime TermTimeStart = new DateTime(2014, 09, 15);
        public DateTime TermTimeEnd = new DateTime(2015, 01, 06);    

        public DataTable TimeByUserName_ClassCode(string ProfCode, float ClassCode, string BeginDate, string EndDate)
        {
            SqlConnection conn = new SqlConnection(new AdobeConnection().AdobeconnectionString);

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            //SqlCommand cmd = new SqlCommand("R_TeacherTimeinMeetingsByUserName_ClassCode", conn);
            SqlCommand cmd = new SqlCommand("R_TeacherTimeinMeetingsByUserName_ClassCode_LMS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar);
            cmd.Parameters["@LoginName"].Value = ProfCode;
            cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            if (BeginDate != "")
            {
                cmd.Parameters.Add("@Shamsi_Date_Begin", SqlDbType.VarChar);
                cmd.Parameters["@Shamsi_Date_Begin"].Value = BeginDate;
            }
            if (EndDate != "")
            {
                cmd.Parameters.Add("@Shamsi_Date_End", SqlDbType.VarChar);
                cmd.Parameters["@Shamsi_Date_End"].Value = EndDate;
            }



            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();

            DataView dv = dt.DefaultView;
            dv.Sort = "PersianDate";
            DataTable sortedDT = dv.ToTable();
            return sortedDT;

            //return dt;
        }

        public DataTable Old_TimeByUserName_ClassCode(string ProfCode, float ClassCode, string BeginDate, string EndDate)
        {
            SqlConnection conn = new SqlConnection(new AdobeConnection().VCconnectionString);

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            //SqlCommand cmd = new SqlCommand("R_TeacherTimeinMeetingsByUserName_ClassCode", conn);
            SqlCommand cmd = new SqlCommand("R_TeacherTimeinMeetingsByUserName_ClassCode_LMS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar);
            cmd.Parameters["@LoginName"].Value = ProfCode;
            cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            if (BeginDate != "")
            {
                cmd.Parameters.Add("@Shamsi_Date_Begin", SqlDbType.VarChar);
                cmd.Parameters["@Shamsi_Date_Begin"].Value = BeginDate;
            }
            if (EndDate != "")
            {
                cmd.Parameters.Add("@Shamsi_Date_End", SqlDbType.VarChar);
                cmd.Parameters["@Shamsi_Date_End"].Value = EndDate;
            }



            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();


            DataView dv = dt.DefaultView;
            dv.Sort = "PersianDate";
            DataTable sortedDT = dv.ToTable();
            return sortedDT;
            //return dt;
        }

        public DataTable TimeByUserName_ClassCode(string ProfCode, int ClassCode, string BeginDate, string EndDate,string term)
        {
            SqlConnection conn = new SqlConnection();
            DAO.Adobe.SettingDAO setting = new Adobe.SettingDAO();
            setting.getAdobeConnectionString(term);
            conn.ConnectionString = setting.getAdobeConnectionString(term);
            //if (term=="93-94-2")
            // {
            //     conn.ConnectionString = new AdobeConnection().liveconnectionString;
            // }
            // else if (term == "93-94-3")
            // {
            //     conn.ConnectionString = new AdobeConnection().liveconnectionString;
            // }
            // else if (term == "94-95-1")
            // {
            //     conn.ConnectionString = new AdobeConnection().classconnectionString;
            // }
            //else if (term == "94-95-2")
            //{
            //    conn.ConnectionString = new AdobeConnection().vc_new_connectionString;
            //}
            //else if (term == "95-96-1")
            //{
            //    conn.ConnectionString = new AdobeConnection().vc_951;
            //}
            // else if (term == "95-96-2")
            // {
            //     conn.ConnectionString = new AdobeConnection().vc_952;
            // }
            // else if (term == "96-97-1")
            // {
            //     conn.ConnectionString = new AdobeConnection().vc_961;
            // }
            // else if (term == "96-97-2")
            // {
            //     conn.ConnectionString = new AdobeConnection().vc_962;
            // }
            // else if (term == "97-98-1")
            // {
            //     conn.ConnectionString = new AdobeConnection().vc_971;
            // }
            // SqlConnection conn = new SqlConnection(new AdobeConnection().liveconnectionString); 
            //SqlConnection conn =
            //    new SqlConnection("User=karimi;Password=123456;server=192.168.30.190; database=faghatkarimi; connection timeout=30;");


            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_TeacherTimeinMeetingsByUserName_ClassCode_LMS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar);
            cmd.Parameters["@LoginName"].Value = ProfCode;
            cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;      

            if (BeginDate != "")
            {
                cmd.Parameters.Add("@Shamsi_Date_Begin", SqlDbType.VarChar);
                cmd.Parameters["@Shamsi_Date_Begin"].Value = BeginDate;
            }
            if (EndDate != "")
            {
                cmd.Parameters.Add("@Shamsi_Date_End", SqlDbType.VarChar);
                cmd.Parameters["@Shamsi_Date_End"].Value = EndDate;
            }
            
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();

            DataView dv = dt.DefaultView;
            dv.Sort = "PersianDate";
            DataTable sortedDT = dv.ToTable();
            return sortedDT;
        }

    }
}
