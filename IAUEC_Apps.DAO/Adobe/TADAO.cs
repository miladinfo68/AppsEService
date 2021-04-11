using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.Adobe
{
    public class TADAO
    {
        SqlConnection conn_Sida = new SqlConnection(new SuppConnection().Supp_con);
        SqlConnection conn_Adobe = new SqlConnection(new AdobeConnection().AdobeconnectionString);
        SqlConnection conn_AdobeOld = new SqlConnection(new AdobeConnection().VCconnectionString);
        SqlConnection conn_AdobeLive = new SqlConnection(new AdobeConnection().liveconnectionString);

        public DateTime TermTimeStart = new DateTime(2014, 09, 15);
        public DateTime TermTimeEnd = new DateTime(2015, 01, 06);

        #region Read

        //Read Data From TAusers Table
        public DataTable GetTAUsers()
        {
            //TEST
            //SqlConnection conn = new SqlConnection("User=karimi;Password=123456;server=192.168.30.190; database=adobe930919; connection timeout=30;");

            SqlConnection conn = conn_AdobeLive;
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("TA_GetUsers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }
         
        /// <summary>
        /// Get Professor Data From Sida Tables
        /// </summary>
        /// <param name="Department"></param>
        /// <param name="Term"></param>
        /// <returns></returns>
        public DataTable GetTAAssistantListByDepartment_Term(string Department, string Term)
        {
            
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_GetProfessorListByDepartment_Term]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Department", SqlDbType.NVarChar);
            cmd.Parameters["@Department"].Value = Department;
            cmd.Parameters.Add("@Term", SqlDbType.NVarChar);
            cmd.Parameters["@Term"].Value = Term;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();

            return dt;
        }
        
        /// <summary>
        /// this give Data by Array list include :ProfUserName & ClassCode
        /// </summary>
        /// <param name="TVP"></param>
        /// <param name="Term"></param>
        /// <returns></returns>
        public DataTable GetTAAssistantTimeByArrayList(DataTable TVP)
        {
           

            SqlConnection conn = conn_AdobeLive;

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("dbo.TA_TotalTimeinMeetingsByUserName", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter tvparam = cmd.Parameters.AddWithValue("@List", TVP);
                tvparam.SqlDbType = SqlDbType.Structured;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                cmd.Connection.Close();
                cmd.Dispose();
                conn.Close();
            }


            return dt;
        }
           
        public DataTable TimeByUserName_ClassCode(string ProfCode, float ClassCode, string BeginDate, string EndDate)
        {
           

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
           
            SqlCommand cmd = new SqlCommand("R_TeacherTimeinMeetingsByUserName_ClassCode", conn_Adobe);
         
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


            conn_Adobe.Open();
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Adobe.Close();

            DataView dv = dt.DefaultView;
            dv.Sort = "PersianDate";
            DataTable sortedDT = dv.ToTable();
            return sortedDT;

            //return dt;
        }
                
        public DataTable Old_TimeByUserName_ClassCode(string ProfCode, float ClassCode, string BeginDate, string EndDate)
        {
           
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
           
            SqlCommand cmd = new SqlCommand("R_TeacherTimeinMeetingsByUserName_ClassCode", conn_AdobeOld);
           
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


            conn_AdobeOld.Open();
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_AdobeOld.Close();


            DataView dv = dt.DefaultView;
            dv.Sort = "PersianDate";
            DataTable sortedDT = dv.ToTable();
            return sortedDT;
            //return dt;
        }

        public DataTable TimeByUserName_ClassCode93942(string ProfCode, float ClassCode, string BeginDate, string EndDate)
        {
           
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
          
            SqlCommand cmd = new SqlCommand("R_TeacherTimeinMeetingsByUserName_ClassCode_LMS", conn_AdobeLive);
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
            conn_AdobeLive.Open();
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_AdobeLive.Close();

            DataView dv = dt.DefaultView;
            dv.Sort = "PersianDate";
            DataTable sortedDT = dv.ToTable();
            return sortedDT;
        }










        #endregion


        #region Insert

        public void AddToTAUser(string Login, string Name, string Family, string Email, string ClassCode)
        {
            SqlConnection conn = conn_AdobeLive;
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[TA_AddUsers]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Login", SqlDbType.NVarChar);
            cmd.Parameters["@Login"].Value = Login;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters["@Name"].Value = Name;
            cmd.Parameters.Add("@Family", SqlDbType.NVarChar);
            cmd.Parameters["@Family"].Value = Family;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters["@Email"].Value = Email;
            cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
        }

        #endregion


    }
}
