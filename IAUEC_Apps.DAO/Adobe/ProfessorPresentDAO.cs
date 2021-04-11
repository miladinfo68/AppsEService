using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.CommonDAO
{
    public class ProfessorPresentDAO
    {
        public DateTime TermTimeStart = new DateTime(2014, 09, 15);
        public DateTime TermTimeEnd = new DateTime(2015, 01, 06);

        public DataTable TimeByUserName_ClassCode(string Min, string Max)
        {
            SqlConnection conn = new SqlConnection(new AdobeConnection().AdobeconnectionString);

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            //SqlCommand cmd = new SqlCommand("R_TeacherTotalTimeinMeetingsByUserName", conn);
            SqlCommand cmd = new SqlCommand("R_TeacherTotalTimeinMeetingsByUserName", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Min", SqlDbType.NVarChar);
            cmd.Parameters["@Min"].Value = Min;
            cmd.Parameters.Add("@Max", SqlDbType.NVarChar);
            cmd.Parameters["@Max"].Value = Max;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }

        public DataTable TimeByUserName_ClassCode_OLD(string Min, string Max)
        {
            SqlConnection conn = new SqlConnection(new AdobeConnection().VCconnectionString);

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            //SqlCommand cmd = new SqlCommand("R_TeacherTotalTimeinMeetingsByUserName", conn);
            SqlCommand cmd = new SqlCommand("R_TeacherTotalTimeinMeetingsByUserName", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Min", SqlDbType.NVarChar);
            cmd.Parameters["@Min"].Value = Min;
            cmd.Parameters.Add("@Max", SqlDbType.NVarChar);
            cmd.Parameters["@Max"].Value = Max;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }

        public DataTable GiveListPrincipal()
        {
            SqlConnection conn = new SqlConnection(new AdobeConnection().AdobeconnectionString);
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_Teacher_PRINCIPAL_ID", conn);
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }

        public DataTable GiveListPrincipal_OLD()
        {
            SqlConnection conn = new SqlConnection(new AdobeConnection().VCconnectionString);
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_Teacher_PRINCIPAL_ID", conn);
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }


        //======================================================================================
        //public DataTable GiveListPrincipal93942()
        //{
        //    SqlConnection conn = new SqlConnection(new AdobeConnection().liveconnectionString);
        //    SqlDataReader rdr = null;
        //    DataTable dt = new DataTable();
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand("R_Teacher_PRINCIPAL_ID", conn);
        //    rdr = cmd.ExecuteReader();
        //    dt.Load(rdr);
        //    rdr.Dispose();
        //    cmd.Connection.Close();
        //    cmd.Dispose();
        //    conn.Close();
        //    return dt;
        //}
        public DataTable GiveListPrincipalbyTerm(string term)
        {
            SqlConnection conn = new SqlConnection();
            DAO.Adobe.SettingDAO setting = new Adobe.SettingDAO();
            setting.getAdobeConnectionString(term);
            conn.ConnectionString = setting.getAdobeConnectionString(term);
         
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_Teacher_PRINCIPAL_ID", conn);
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }
        public DataTable TimeByUserName_ClassCodeByTerm(string Min, string Max, string term)
        {

            SqlConnection conn = new SqlConnection();
            DAO.Adobe.SettingDAO setting = new Adobe.SettingDAO();
            setting.getAdobeConnectionString(term);
            conn.ConnectionString = setting.getAdobeConnectionString(term);
            
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_TeacherTotalTimeinMeetingsByUserName", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Min", SqlDbType.NVarChar);
            cmd.Parameters["@Min"].Value = Min;
            cmd.Parameters.Add("@Max", SqlDbType.NVarChar);
            cmd.Parameters["@Max"].Value = Max;


            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }
        public DataTable TimeByUserName_ClassCodeByTerm(string term)
        {

            SqlConnection conn = new SqlConnection();
            DAO.Adobe.SettingDAO setting = new Adobe.SettingDAO();
            setting.getAdobeConnectionString(term);
            conn.ConnectionString = setting.getAdobeConnectionString(term);
           
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_TeacherTotalTimeinMeetings", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@Min", SqlDbType.NVarChar);
            //cmd.Parameters["@Min"].Value = Min;
            //cmd.Parameters.Add("@Max", SqlDbType.NVarChar);
            //cmd.Parameters["@Max"].Value = Max;


            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }
        public DataTable GetMGTimeByUserName_Term(string username, string term)
        {

            SqlConnection conn = new SqlConnection();
            DAO.Adobe.SettingDAO setting = new Adobe.SettingDAO();
            conn.ConnectionString = setting.getAdobeConnectionString(term);
           
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_GroupManagerTotalTimeinMeetingsByUserName", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Userlogin", SqlDbType.VarChar);
            cmd.Parameters["@Userlogin"].Value = username;
            //cmd.Parameters.Add("@GCode", SqlDbType.Variant);
            //cmd.Parameters["@GCode"].Value = GetMGClassCode(term);
            //cmd.Parameters.AddWithValue("@GCode", GetMGClassCode(term));
            try
            {
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                cmd.Connection.Close();
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                dt.Columns.Add("error");
                DataRow dr = dt.NewRow();
                dr[0] = "خطا. کانکشن ترم  " + term + " بسته است";
            }
            return dt;
        }

        public DataTable GetMGClassCode(string term)
        {

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = new SuppConnection().Supp_con;

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("Adobe.GetGMClassCode", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@term", SqlDbType.VarChar);
            cmd.Parameters["@term"].Value = term;


            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }

        public DataTable getActiveGroupManager(int year)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = new SuppConnection().Supp_con;

            SqlCommand cmd = new SqlCommand("adobe.sp_getActiveGroupManagers", conn);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt=new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();

            }
            catch(Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return dt;
        }
        public DataTable getActiveGroupOfGroupManagers(int year,Int64 codeOstad)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = new SuppConnection().Supp_con;

            SqlCommand cmd = new SqlCommand("adobe.sp_getGroupManagerGroups", conn);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@professorCode", codeOstad);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return dt;
        }
        public DataTable getProfessorSalary(int year, Int64 codeOstad)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = new SuppConnection().Supp_con;

            SqlCommand cmd = new SqlCommand("adobe.getProfessorSalary", conn);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@professorCode", codeOstad);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return dt;
        }

        public DataTable GetMGUsers(string term)
        {

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = new SuppConnection().Supp_con;

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("Adobe.GetGMUsers", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@term", SqlDbType.VarChar);
            cmd.Parameters["@term"].Value = term;


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
