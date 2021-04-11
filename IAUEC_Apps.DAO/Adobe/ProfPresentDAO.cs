using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.Adobe
{
    public class ProfPresentDAO
    {

        SqlConnection conn_Sida = new SqlConnection(new SuppConnection().Supp_con);
        //SqlConnection conn_Adobe = new SqlConnection(new AdobeConnection().AdobeconnectionString);
        //SqlConnection conn_AdobeOld = new SqlConnection(new AdobeConnection().VCconnectionString);
        SqlConnection conn_AdobeLive = new SqlConnection(new AdobeConnection().liveconnectionString);

        public DateTime TermTimeStart = new DateTime(2014, 09, 15);
        public DateTime TermTimeEnd = new DateTime(2015, 01, 06);

        #region Read


        /// <summary>
        /// همه دپارتمان ها به جز (دروه های کوتاه مدت) را از جدول 
        /// fresh 
        /// میخواند + یک فیلد به نام 
        /// همه 
        /// اضافه میکند
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDepartment()
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_GetAllDepartment]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();

            DataTable DT = new DataTable();
            DT.Columns.Add("Department", typeof(string));

            //First Row
            DataRow row0 = DT.NewRow();
            row0["Department"] = "همه";
            DT.Rows.Add(row0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["namedanesh"].ToString()!= "دوره هاي کوتاه مدت")
                {
                    DataRow row = DT.NewRow();
                    row["Department"] = dt.Rows[i]["namedanesh"].ToString();
                    DT.Rows.Add(row);
                }
            }
            return DT;
        }

        /// <summary>
        /// Get Professor Data From Sida Tables
        /// </summary>
        /// <param name="Department"></param>
        /// <param name="Term"></param>
        /// <returns></returns>
        public DataTable GetProfessorListByDepartment_Term(string Department, string Term)
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
        /// Get Professor Data From Sida Tables
        /// </summary>
        /// <param name="Department"></param>
        /// <param name="Term"></param>
        /// <returns></returns>
        public DataTable GetProfessorListByDepartment_Term(int DepartmentCode, string Term)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_GetProfessorListByDepartmentCodeAndTerm]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DepartmentCode", SqlDbType.Int);
            cmd.Parameters["@DepartmentCode"].Value = DepartmentCode;
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
        /// Get Professor Data From Sida Tables
        /// </summary>
        /// <param DataTable="GetProfessorListByDepartment_Term"></param>
        /// <param name="Term"></param>
        /// <returns></returns>
        public DataTable R_TeacherTotalTimeinMeetingsListByClassListAndTerm(DataTable ClassList,string term)
           
        {

            SqlConnection conn = new SqlConnection();
            DAO.Adobe.SettingDAO setting = new Adobe.SettingDAO();
            setting.getAdobeConnectionString(term);
            conn.ConnectionString = setting.getAdobeConnectionString(term);


            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            
            SqlCommand cmd = new SqlCommand("R_TeacherTotalTimeinMeetingsListByClassListAndTerm", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter cmdparam = cmd.Parameters.AddWithValue("@ClassList", ClassList);
            cmdparam.SqlDbType = SqlDbType.Structured;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();

            return dt;
        }
        /// <summary>
        /// this give Data by Array list include :ProfUserName & ClassCode
        /// </summary>
        /// <param name="TVP"></param>
        /// <param name="Term"></param>
        /// <returns></returns>
        public DataTable GetProfessorTimeByArrayList(DataTable TVP)
        {     
            //TEST
            //SqlConnection conn =new SqlConnection("User=karimi;Password=123456;server=192.168.30.190; database=adobe930919; connection timeout=30;");

            SqlConnection conn = conn_AdobeLive;
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            using (conn)
            {
               
                //SqlCommand cmd = new SqlCommand("dbo.R_TeacherTotalTimeinMeetingsByUserName", conn);
                SqlCommand cmd = new SqlCommand("dbo.R_TeacherTotalTimeinMeetingsByUserName_Department", conn);
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


















        public DataTable TimeByUserName_ClassCode(string Min, string Max)
        {
            SqlConnection conn = new SqlConnection(new AdobeConnection().AdobeconnectionString);

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();           
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

        public DataTable TimeByUserName_ClassCode93942(string Min, string Max)
        {
            SqlConnection conn = new SqlConnection(new AdobeConnection().liveconnectionString);
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


        #endregion


    }
}
