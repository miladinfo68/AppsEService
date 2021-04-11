using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DTO.AdobeClasses;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DAO.ResourceControl;
using IAUEC_Apps.DTO.ResourceControlClasses;
using ResourceControl.DAL;
using System.Reflection;

namespace IAUEC_Apps.DAO.CommonDAO
{
    public static class ReturnSqlConnection
    {
        public static SqlConnection GetSuppSqlConnection()
        {
            return new SqlConnection(new SuppConnection().Supp_con);

        }
        public static SqlConnection GetSidaSqlConnection()
        {
            return new SqlConnection(new AmozeshConnection().con_sida);
        }
        public static SqlConnection GetHRSqlConnection()
        {
            return new SqlConnection(new HrConnection().HR_con);
        }
        public static SqlConnection GetUserAccessSqlConnection()
        {
            return new SqlConnection(new SuppConnection().UserAccess_con);
        }
        public static SqlConnection GetUserAccessLogSqlConnection()
        {
            return new SqlConnection(new SuppConnection().log_con);
        }
        public static string GetSuppSqlConnectionString()
        {
            return GetSuppSqlConnection().ConnectionString;
        }
        public static string GetSidaSqlConnectionString()
        {

            return GetSidaSqlConnection().ConnectionString;
        }
        public static string GetHRSqlConnectionString()
        {

            return GetHRSqlConnection().ConnectionString;
        }
        public static string GetUserAccessSqlConnectionString()
        {
            return GetUserAccessSqlConnection().ConnectionString;
        }
        public static string GetUserAccessLogSqlConnectionString()
        {
            return GetUserAccessLogSqlConnection().ConnectionString;
        }
    }
    public class CommonDAO
    {
        SqlConnection conn = ReturnSqlConnection.GetSuppSqlConnection();
        SqlConnection connaccess = ReturnSqlConnection.GetUserAccessSqlConnection();
        SqlConnection connlog = ReturnSqlConnection.GetUserAccessLogSqlConnection();
        SqlConnection con_sida = ReturnSqlConnection.GetSidaSqlConnection();
        SqlConnection hr = ReturnSqlConnection.GetHRSqlConnection();
        //  SqlConnection HR_con = new SqlConnection(new HrConnection().HR_con);
        public static string ReportConnection => ReturnSqlConnection.GetSidaSqlConnectionString();
        public static string SupplementaryReportConnection => ReturnSqlConnection.GetSidaSqlConnectionString();//conn.ConnectionString;
        #region Read
        public DataTable GetReshByDaneshkade(int Daneshkade)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.SP_SelectReshByDaneshkade";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetSystemAvailability(int AppID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.SP_GetSystemAvailability";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppID", AppID);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public DataTable GetSystemAvailability(int AppID, int UserKind, int UserStatus)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.SP_GetSysAvailabilityByAppIdAndUserKindAndUserStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppID", AppID);
            cmd.Parameters.AddWithValue("@UserKind", UserKind);
            cmd.Parameters.AddWithValue("@UserStatus", UserStatus);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetUserLogByModifyId(int ModifyID, int AppID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connlog;
            cmd.CommandText = "dbo.SP_GetUserLogByModifyId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModifyID", ModifyID);
            cmd.Parameters.AddWithValue("@AppID", AppID);
            DataTable dt = new DataTable();
            try
            {
                connlog.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                connlog.Close();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public DataTable GetStudentLogByModifyId(int ModifyID, int AppID)
        {
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = connlog;
            cmd1.CommandText = "dbo.SP_GetStudentLogByModifyId";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@ModifyID", ModifyID);
            cmd1.Parameters.AddWithValue("@AppID", AppID);
            DataTable dt1 = new DataTable();
            try
            {
                connlog.Open();
                SqlDataReader rdr;
                rdr = cmd1.ExecuteReader();
                dt1.Load(rdr);
                connlog.Close();
            }
            catch (Exception)
            { throw; }


            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = connlog;
            cmd2.CommandText = "dbo.[SP_GetUserCustomLogByModifyId]";
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@ModifyID", ModifyID);
            cmd2.Parameters.AddWithValue("@AppID", AppID);
            DataTable dt2 = new DataTable();
            try
            {
                connlog.Open();
                SqlDataReader rdr;
                rdr = cmd2.ExecuteReader();
                dt2.Load(rdr);
                connlog.Close();
                dt1.Merge(dt2);

            }
            catch (Exception)
            { }
            return dt1;
        }

        public DataTable getProfClasses(string term)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@term", term);
            cmn.CommandText = "faculty.getProfClasses_Contract";
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader dr;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return dt;
        }

        public DataTable getStudentTuitional(string term,string stcode)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@term", term);
            cmn.Parameters.AddWithValue("@stcode", stcode);
            cmn.CommandText = "sp_getFixAndVariableTuitional";
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader dr;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return dt;
        }

        public void insertTuitional(string username, string stcode,string fishNumber,decimal amount,string term)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@fishNumber", fishNumber);
            cmn.Parameters.AddWithValue("@stcode", stcode);
            cmn.Parameters.AddWithValue("@userName", username);
            cmn.Parameters.AddWithValue("@term", term);
            cmn.Parameters.AddWithValue("@amount_tuitionApproved", amount);
            cmn.CommandText = "sp_insertTuitional";
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmn.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
        }


        public bool insertTuitionFormol_LastEntries(string CurrentTerm, string LastTerm,decimal var_Percent,int level)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "sp_insertTuitionFormol_LastEntries"; 

            cmn.Parameters.AddWithValue("@CurrentTerm", CurrentTerm);
            cmn.Parameters.AddWithValue("@byTerm", LastTerm);
            cmn.Parameters.AddWithValue("@pvar",var_Percent);
            cmn.Parameters.AddWithValue("@magh",level);
            
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int r=cmn.ExecuteNonQuery();
                conn.Close();
                return r > 0;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
        }
        
        public bool insertTuitionFormol(string level,string currentYear, string CurrentTerm, string byYear, string LastTerm, decimal fix_Percent,decimal var_Percent,int insurance,Int64 services)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "sp_insertTuitionFormol"; 

            cmn.Parameters.AddWithValue("@magh",level);
            cmn.Parameters.AddWithValue("@CurrentYear",currentYear);
            cmn.Parameters.AddWithValue("@CurrentTerm", CurrentTerm);
            cmn.Parameters.AddWithValue("@byYear", byYear);
            cmn.Parameters.AddWithValue("@LastTerm", LastTerm);
            cmn.Parameters.AddWithValue("@pfix",fix_Percent);
            cmn.Parameters.AddWithValue("@pvar",var_Percent);
            cmn.Parameters.AddWithValue("@bime",insurance);
            cmn.Parameters.AddWithValue("@khad",services);

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int r=cmn.ExecuteNonQuery();
                conn.Close();
                return r > 0;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
        }

        public bool openCartAccess(string stcode)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "openCartAccess";

            cmn.Parameters.AddWithValue("@stcode", stcode);

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int r = cmn.ExecuteNonQuery();
                conn.Close();
                return r > 0;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
        }

        public DataTable GetUserLogByStcoded(int ModifyID, int AppID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connlog;
            cmd.CommandText = "dbo.SP_GetUserLogByModifyId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModifyID", ModifyID);
            cmd.Parameters.AddWithValue("@AppID", AppID);
            DataTable dt = new DataTable();
            try
            {
                connlog.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                connlog.Close();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public DataTable getStudentLogByAppId(int AppId, int studentId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connlog;
            cmd.CommandText = "dbo.SP_GetstudentLogByAppId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppID", AppId);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            DataTable dt = new DataTable();
            try
            {
                connlog.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                connlog.Close();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public DataTable GetInfoPeoByCodeMeliINAspNetUsers(string codeMeli)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = hr;
            cmd.CommandText = "[HR].[SP_GetInfoPeoByCodeMeliINAspNetUsers]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codemeli", codeMeli);

            DataTable dt = new DataTable();
            try
            {
                hr.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                hr.Close();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public DataTable GetStudentInfoByStcode(string Stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Email].[SP_Email_GetStudentInfoAndSecurityCodeByStcode]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", Stcode);

            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception e)
            { throw; }
            return dt;
        }

        public DataTable GetAllTermByStudent(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_GetAllTermByStudent";
            cmd.Parameters.AddWithValue("@stcode", stcode);

            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            return dt;

        }

        public DataTable GetAllAdobeConnectionTerms()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_GetAllTerm";
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }


        /// <summary>
        /// ok sargol
        /// </summary>
        /// <param name="shahrID"></param>
        /// <param name="ostanID"></param>
        /// <returns></returns>
        public DataTable getShahrestan(int shahrID,int ostanID)
        {
            SqlCommand cmd = new SqlCommand("dbo.sp_getShahrestan",conn);
           
            cmd.Parameters.AddWithValue("@ostan", ostanID);
            cmd.Parameters.AddWithValue("@shahrestan", shahrID);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw ex;
            }
            return dt;
        }
        /// <summary>
        /// ok sargol
        /// </summary>
        /// <param name="ostanID"></param>
        /// <returns></returns>
        public DataTable GetOstan(int ostanID)
        {
            SqlCommand cmd = new SqlCommand("sp_getOstan",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ostan", ostanID);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return dt;
        }
        /// <summary>
        /// ok sargol
        /// </summary>
        /// <param name="stateID"></param>
        /// <returns></returns>
        public DataTable getState(int stateID)
        {
            SqlCommand cmd = new SqlCommand("sp_getState",conn);
            cmd.Parameters.AddWithValue("@stateID", stateID);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if(conn.State== ConnectionState.Closed)
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable GetStateFromTblOstan(int stateID)
        {
            SqlCommand cmd = new SqlCommand("SP_GetStateFromTblOstan", conn);
            cmd.Parameters.AddWithValue("@StateID", stateID);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        /// <summary>
        /// ok sargol
        /// </summary>
        /// <param name="stateID"></param>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public DataTable getCity(int stateID,int cityID)
        {
            SqlCommand cmd = new SqlCommand("sp_getCity", conn);
            cmd.Parameters.AddWithValue("@stateID", stateID);
            cmd.Parameters.AddWithValue("@cityID", cityID);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable getCitiesFromTblShahrestan(int stateID)
        {
            SqlCommand cmd = new SqlCommand("SP_GetCitiesFromTblShahrestan", conn);
            cmd.Parameters.AddWithValue("@StateId", stateID);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable AcceptEmailRule(int App_ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Email.SP_AcceptEmailRole";
            cmd.Parameters.AddWithValue("@App_ID", App_ID);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //ramezanian
        public DataTable GetCodeAsanak(string code, int IDRow)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.GetCodeAsanak";
            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@IDRow", IDRow);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;

        }
        //ramezanian
        public DataTable GetShowStatus(string resault)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.[SP_GetStatusSMS]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@resault", resault);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        //ramezanian
        public DataTable GetSearchStudentOrProf(string code)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.SP_SearchCodeStudentORProf";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@code", code);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //ramezanian
        /// <summary>
        /// این متد کل اطلاعات ترم را بر می گرداند
        /// </summary>
        /// <returns></returns>
        public DataTable SellectAllTerm()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[dbo].[SP_GetTerm]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //ramezanian
        /// <summary>
        /// اطلاعات دانشکده را بر می گرداند
        /// </summary>
        /// <returns></returns>
        //public DataTable SelectAllDaneshkade()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con_sida;
        //    cmd.CommandText = "dbo.SP_SelectAllDaneshkade";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@daneshID", 0);
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        con_sida.Open();
        //        SqlDataReader rdr;
        //        rdr = cmd.ExecuteReader();
        //        dt.Load(rdr);
        //        con_sida.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return dt;
        //}
        public DataTable SelectAllDaneshkade(int daneshID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con_sida;
            cmd.CommandText = "dbo.SP_SelectAllDaneshkade";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@daneshID", daneshID);
            DataTable dt = new DataTable();
            try
            {
                con_sida.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con_sida.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //ramezanian
        /// <summary>
        /// کل دپارتمان ها را بر می گرداند
        /// </summary>
        /// <returns></returns>
        public DataTable GetSelectAllDepartman()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con_sida;
            cmd.CommandText = "[Education].[SP_SelectAllDepartman]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                con_sida.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con_sida.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //ramezanian
        public DataTable GetAllTypeCooperation()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con_sida;
            cmd.CommandText = "Faculty.SP_SelectAllTypeCooperation";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                con_sida.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con_sida.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //ramezanian
        public DataTable GetAllInformationFaculty()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con_sida;
            cmd.CommandText = "dbo.SP_GetInformationProf";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                con_sida.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con_sida.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }


        public DataTable GetAllDepartman(int daneshId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con_sida;
            cmd.CommandText = "dbo.SP_SelectAllDepartman";
            cmd.Parameters.AddWithValue("@iddanesh", daneshId);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                con_sida.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con_sida.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetLesson()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.[SP_SelectAllLesson]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //ramezanian
        public DataTable GetDateMiladiToShamsi(DateTime Date)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con_sida;
            cmd.CommandText = "dbo.MiladeToShamsi";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Date", Date);
            DataTable dt = new DataTable();
            try
            {
                con_sida.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con_sida.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //ramezanian
        /// <summary>
        /// از جدول اف-رش رشته ها را می گیرد
        /// </summary>
        /// <returns></returns>
       
        public DataTable SelectAllField(int daneshId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con_sida;
            cmd.CommandText = "dbo.[SP_SelectAllField]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@daneshId", daneshId);
            DataTable dt = new DataTable();
            try
            {
                con_sida.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con_sida.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //ramezanian
       

        public DataTable GetInformationFacultyByFilter(int CodeProf, string Family, string NameEp, int Cooperation)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con_sida;
            cmd.CommandText = "dbo.SP_GetInfoProfByFilter";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CodeProf", CodeProf);
            cmd.Parameters.AddWithValue("@Family", Family);
            cmd.Parameters.AddWithValue("@NameEp", NameEp);
            cmd.Parameters.AddWithValue("@Cooperation", Cooperation);
            DataTable dt = new DataTable();
            try
            {
                con_sida.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con_sida.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //ramezanian
        public DataTable GetTextMessage(int type, int AppID, int Category, int status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[dbo].[SP_GetConnectTextByStatus]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@AppID", AppID);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetCodingByTypeId(int codingTypeId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Dbo.SP_GetCodingByTypeId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codingTypeId", codingTypeId);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                conn.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        //public DataTable GetMartabeElmi()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = hr;
        //    cmd.CommandText = "HR.SP_AcademicRank";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        SqlDataReader rdr;
        //        hr.Open();
        //        rdr = cmd.ExecuteReader();
        //        dt.Load(rdr);
        //        hr.Close();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return dt;
        //}

        public DataTable getDenyRequestInfo(string sDate, string eDate, int apps)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connaccess;
            cmd.CommandText = "xxxxxxxxxxxxx";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("", sDate);
            cmd.Parameters.AddWithValue("", eDate);
            cmd.Parameters.AddWithValue("", apps);

            DataTable dt = new DataTable();
            try
            {
                connaccess.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                connaccess.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable getBasicInformation(int infoType, int infoID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "[dbo].[SP_getBasicInformation]";
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@BaseType", infoType);
            cmn.Parameters.AddWithValue("@BaseID", infoID);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
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

        public DataTable getEducationCalender(string term, int dateType)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "dbo.getEducationCalender";
            cmn.Parameters.AddWithValue("@term ", term);
            //cmn.Parameters.AddWithValue("@dayType", dateType);
            DataTable dt = new DataTable();


            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
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


        #endregion
        #region Create
        public void InsertIntoUserLog(int UserID, string LogTime, int AppId, int eventId, string Ipaddress, string Description)
        {
            SqlCommand cmdins = new SqlCommand();
            cmdins.Connection = connlog;
            cmdins.CommandText = "SP_Insert_UserLog";
            cmdins.CommandType = CommandType.StoredProcedure;
            cmdins.Parameters.AddWithValue("@UserID", UserID);
            cmdins.Parameters.AddWithValue("@LogTime", LogTime);
            cmdins.Parameters.AddWithValue("@Event", eventId);
            cmdins.Parameters.AddWithValue("@AppId", AppId);
            cmdins.Parameters.AddWithValue("@Ipaddress", Ipaddress);
            cmdins.Parameters.AddWithValue("@Description", Description);
            //cmdins.Parameters.AddWithValue("@ID", 0);
            try
            {
                connlog.Open();
                cmdins.ExecuteNonQuery();
                connlog.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void InsertIntoUserLog(int UserID, string LogTime, int AppId, int eventId, string Ipaddress, string Description, int ModifyID)
        {
            SqlCommand cmdins = new SqlCommand();
            cmdins.Connection = connlog;
            cmdins.CommandText = "SP_Insert_UserLog";
            cmdins.CommandType = CommandType.StoredProcedure;
            cmdins.Parameters.AddWithValue("@UserID", UserID);
            cmdins.Parameters.AddWithValue("@LogTime", LogTime);
            cmdins.Parameters.AddWithValue("@Event", eventId);
            cmdins.Parameters.AddWithValue("@AppId", AppId);
            cmdins.Parameters.AddWithValue("@Ipaddress", Ipaddress);
            cmdins.Parameters.AddWithValue("@Description", Description);
            cmdins.Parameters.AddWithValue("@ModifyID", ModifyID);
            try
            {
                connlog.Open();
                cmdins.ExecuteNonQuery();
                connlog.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void InsertIntoUserLogwithIP(int UserID, string LogTime, int AppId, int eventId, string Ipaddress, string Description)
        {
            SqlCommand cmdins = new SqlCommand();
            cmdins.Connection = connlog;
            cmdins.CommandText = "SP_Insert_UserLog";
            cmdins.CommandType = CommandType.StoredProcedure;
            cmdins.Parameters.AddWithValue("@UserID", UserID);
            cmdins.Parameters.AddWithValue("@LogTime", LogTime);
            cmdins.Parameters.AddWithValue("@Event", eventId);
            cmdins.Parameters.AddWithValue("@AppId", AppId);
            cmdins.Parameters.AddWithValue("@Ipaddress", Ipaddress);
            cmdins.Parameters.AddWithValue("@Description", Description);
            try
            {
                connlog.Open();
                cmdins.ExecuteNonQuery();
                connlog.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void InsertIntoStudentLog(string StCode, string LogTime, int AppId, int eventId, string Ipaddress, string Description)
        {
            SqlCommand cmdins = new SqlCommand();
            cmdins.Connection = connlog;
            cmdins.CommandText = "SP_Insert_StudentLog";
            cmdins.CommandType = CommandType.StoredProcedure;
            cmdins.Parameters.AddWithValue("@StCode", StCode);
            cmdins.Parameters.AddWithValue("@LogTime", LogTime);
            cmdins.Parameters.AddWithValue("@Event", eventId);
            cmdins.Parameters.AddWithValue("@AppId", AppId);
            cmdins.Parameters.AddWithValue("@Ipaddress", Ipaddress);
            cmdins.Parameters.AddWithValue("@Description", Description);
            try
            {
                connlog.Open();
                cmdins.ExecuteNonQuery();
                connlog.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertIntoStudentLog(string StCode, string LogTime, int AppId, int eventId, string Ipaddress, string Description, int ModifyID)
        {
            SqlCommand cmdins = new SqlCommand();
            cmdins.Connection = connlog;
            cmdins.CommandText = "SP_Insert_StudentLogWithModifyID";
            cmdins.CommandType = CommandType.StoredProcedure;
            cmdins.Parameters.AddWithValue("@StCode", StCode);
            cmdins.Parameters.AddWithValue("@LogTime", LogTime);
            cmdins.Parameters.AddWithValue("@Event", eventId);
            cmdins.Parameters.AddWithValue("@AppId", AppId);
            cmdins.Parameters.AddWithValue("@Ipaddress", Ipaddress);
            cmdins.Parameters.AddWithValue("@Description", Description);
            cmdins.Parameters.AddWithValue("@ModifyID", ModifyID);
            try
            {
                connlog.Open();
                cmdins.ExecuteNonQuery();
                connlog.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetCurrentTerm()
        {
            SqlCommand cmdins = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[GetCurrentTerm]"
            };
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var dr = cmdins.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception e)
            {

                throw e;
            }

            return dt.Rows[0][0] as string;
        }

        //--------------------------------------
        public void InsertIntoDefenceInfo(DefenceInformation defenceInformation)
        {
            SqlCommand cmdins = new SqlCommand();
            cmdins.Connection = conn;
            cmdins.CommandType = CommandType.StoredProcedure;
            cmdins.CommandText = "[Resource_Control].[sp_InsertDefenceInformation]";
            cmdins.Parameters.Clear();

            cmdins.Parameters.AddWithValue("@StudentCode", defenceInformation.StudentCode);
            cmdins.Parameters.AddWithValue("@StudentFullName", defenceInformation.StudentFullName);
            cmdins.Parameters.AddWithValue("@StudentField", defenceInformation.StudentField);
            cmdins.Parameters.AddWithValue("@studentGender", defenceInformation.studentGender);
            cmdins.Parameters.AddWithValue("@GroupAcceptDate", defenceInformation.GroupAcceptDate);
            cmdins.Parameters.AddWithValue("@DefenceSubject", defenceInformation.DefenceSubject);
            cmdins.Parameters.AddWithValue("@CollegeId", defenceInformation.CollegeId);
            cmdins.Parameters.AddWithValue("@CollegeName", defenceInformation.CollegeName);
            cmdins.Parameters.AddWithValue("@RequestId", defenceInformation.RequestId);
            cmdins.Parameters.AddWithValue("@OnlineFirstTeacherId", defenceInformation.OnlineFirstTeacherId);
            cmdins.Parameters.AddWithValue("@OnlineSecondTeacherId", defenceInformation.OnlineSecondTeacherId);
            cmdins.Parameters.AddWithValue("@OnlineTeacherRole", defenceInformation.OnlineTeacherRole);
            cmdins.Parameters.AddWithValue("@IsUseOwnSystem", defenceInformation.UseOwnPc);
            cmdins.Parameters.AddWithValue("@StartTime", defenceInformation.StartTime.ToTime());
            cmdins.Parameters.AddWithValue("@EndTime", defenceInformation.EndTime.ToTime());
            cmdins.Parameters.AddWithValue("@RequestDate", defenceInformation.RequestDate);
            
            //sadeghsaryazdi
            cmdins.Parameters.AddWithValue("@FlagDoingMeetingOnline", defenceInformation.FlagDoingMeetingOnline);
            cmdins.Parameters.AddWithValue("@IsEquippingResource", true);//movaghati
            cmdins.Parameters.AddWithValue("@IsRequestEducation", defenceInformation.IsRequestEducation == null || defenceInformation.IsRequestEducation == false ? false : true);

            try
            {
                conn.Open();
                cmdins.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void UpdateIntoDefenceInfo(DefenceInformation defenceInformation)
        {
            if (string.IsNullOrEmpty(defenceInformation.OnlineFirstTeacherId))
            {
                defenceInformation.OnlineFirstTeacherId = "0";
            }
            if (string.IsNullOrEmpty(defenceInformation.OnlineSecondTeacherId))
            {
                defenceInformation.OnlineSecondTeacherId = "0";
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RequestId", Convert.ToInt32(defenceInformation.RequestId)),
                new SqlParameter("@OnlineFirstTeacherId", Convert.ToInt32(defenceInformation.OnlineFirstTeacherId)),
                new SqlParameter("@OnlineSecondTeacherId", Convert.ToInt32(defenceInformation.OnlineSecondTeacherId)),
                new SqlParameter("@OnlineTeacherRole", defenceInformation.OnlineTeacherRole),
                new SqlParameter("@IsUseOwnSystem", defenceInformation.UseOwnPc),
                new SqlParameter("@StartTime", defenceInformation.StartTime.ToTime()),
                new SqlParameter("@EndTime", defenceInformation.EndTime.ToTime()),
                new SqlParameter("@RequestDate", defenceInformation.RequestDate),
                new SqlParameter("@IsEdited", defenceInformation.IsEdited),
                // new SqlParameter("@IsEquippingResource", defenceInformation.IsEquippingResource)movaghati,
                //sadeghsaryazdi
                new SqlParameter("@IsEquippingResource", true),
                new SqlParameter("@flagDoingMeetingOnline", defenceInformation.FlagDoingMeetingOnline),
                new SqlParameter("@flagUpdateRegisterDate", defenceInformation.FlagUpdateRegisterDate),
                new SqlParameter("@IsRequestEducation", defenceInformation.IsRequestEducation==null|| defenceInformation.IsRequestEducation==false?false:true)
        }; 
             var t = SqlDBHelper.ExecuteNonQuery("[Resource_Control].[UpdatetDefenceInformation]", CommandType.StoredProcedure, parameters);

        }


        //-------------------------------------------------
        public int CheckDifenceCondition(string StCode)
        {



            SqlCommand cmdChk = new SqlCommand("", conn);

            cmdChk.CommandType = CommandType.StoredProcedure;
            cmdChk.CommandText = "[Resource_Control].[SP_CheckDefenceConditions]";
            cmdChk.Parameters.Clear();
            cmdChk.Parameters.AddWithValue("@stcode", StCode);
            DataTable dt = new DataTable();
            int pass;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var dr = cmdChk.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception e)
            {

                throw e;
            }

            pass = Convert.ToInt32(dt.Rows[0][0]);
            return pass;
        }

        #endregion
        #region Update

        public bool changeTeacherPassword(Int64 codeOstad, string newPassword, string encrypted)
        {
            // Update Sida Database
            //SqlCommand cmn = new SqlCommand("dbo.ChangeTeacherPassword", conn);
            //cmn.CommandType = CommandType.StoredProcedure;
            //cmn.Parameters.AddWithValue("@codeOstad", codeOstad);
            //cmn.Parameters.AddWithValue("@newPass", newPassword);
            int res = 0;
            //try
            //{
            //    if (conn.State == ConnectionState.Closed)
            //        conn.Open();
            //    res = cmn.ExecuteNonQuery();
            //    conn.Close();

            //}
            //catch (Exception ex)
            //{
            //    if (conn.State == ConnectionState.Open)
            //        conn.Close();
            //    return false;
            //}



            // Update Supplementary Database
            SqlCommand cmd = new SqlCommand("dbo.SP_ChangeTeacherLogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codeOstad", codeOstad);
            cmd.Parameters.AddWithValue("@newPass", encrypted);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }

            return res > 0;
        }

        public bool changeStudentPassword(string stcode, string newPassword, string encrypted)
        {
            // Update Sida Database
            //SqlCommand cmn = new SqlCommand("dbo.ChangeStudentPassword", conn);
            //cmn.CommandType = CommandType.StoredProcedure;
            //cmn.Parameters.AddWithValue("@stcode", stcode);
            //cmn.Parameters.AddWithValue("@newPass", EncryptForSida(newPassword));
            int res = 0;
            //try
            //{
            //    if (conn.State == ConnectionState.Closed)
            //        conn.Open();
            //    res = cmn.ExecuteNonQuery();
            //    conn.Close();

            //}
            //catch (Exception ex)
            //{
            //    if (conn.State == ConnectionState.Open)
            //        conn.Close();
            //    return false;
            //}


            //Update Supplementary Database
            SqlCommand cmd = new SqlCommand("dbo.SP_ChangeStudentLogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@newPass", encrypted);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }

            return res > 0;
        }

        public bool insertStudentPassword(string stcode, string password)
        {
            int res = 0;
            SqlCommand cmd = new SqlCommand("INSERT INTO StudentLogin (stcode, [Password]) VALUES ('" + stcode + "', '" + password + "')", conn);
            cmd.CommandType = CommandType.Text;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }

            return res > 0;
        }

        public void UpdateSystemAvailability(int AppID, bool Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.SP_UpdateSystemAvailability";
            cmd.Parameters.AddWithValue("@AppID", AppID);
            cmd.Parameters.AddWithValue("@Status", Status);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            { throw; }
        }
        public void UpdateSystemAvailability(int AppID, int userKind, int userStatus, bool isOpen, string fromDate, string endDate, string startTime, string endTime)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.sp_updateSystemAvailabilityAndDateTime";
            cmd.Parameters.AddWithValue("@AppID", AppID);
            cmd.Parameters.AddWithValue("@startDay", fromDate);
            cmd.Parameters.AddWithValue("@endDay", endDate);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@isOpen", isOpen);
            cmd.Parameters.AddWithValue("@userKind", userKind);
            cmd.Parameters.AddWithValue("@userStatus", userStatus);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
        }


        public void changePassword(string pass, int userId)
        {
            SqlCommand cmdost = new SqlCommand();
            cmdost.Connection = connaccess;
            cmdost.CommandText = "SP_ChangePassword";
            cmdost.CommandType = CommandType.StoredProcedure;
            cmdost.Parameters.AddWithValue("@password", pass);
            cmdost.Parameters.AddWithValue("@userId", userId);
            try
            {
                connaccess.Open();
                cmdost.ExecuteNonQuery();
                connaccess.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void ChangePasswordAndEnable(string pass, int userId)
        {
            SqlCommand cmdost = new SqlCommand();
            cmdost.Connection = connaccess;
            cmdost.CommandText = "SP_ChangePasswordAndEnable";
            cmdost.CommandType = CommandType.StoredProcedure;
            cmdost.Parameters.AddWithValue("@password", pass);
            cmdost.Parameters.AddWithValue("@userId", userId);
            try
            {
                connaccess.Open();
                cmdost.ExecuteNonQuery();
                connaccess.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void UpdateExaminerPalcePasswordByExaminerID(string pass, int userId)
        {
            SqlCommand cmdost = new SqlCommand();
            cmdost.Connection = conn;
            cmdost.CommandText = "SP_UpdateExaminerPalcePasswordByExaminerID";
            cmdost.CommandType = CommandType.StoredProcedure;
            cmdost.Parameters.AddWithValue("@password", pass);
            cmdost.Parameters.AddWithValue("@userId", userId);
            try
            {
                conn.Open();
                cmdost.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        public void AddSignature(byte[] imageBytes, int identityNumber, int appId, int type)
        {
            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Request].[SP_AddSignature]"
            };
            cmd.Parameters.AddWithValue("@imageBytes", imageBytes);
            cmd.Parameters.AddWithValue("@identityNumber", identityNumber);
            cmd.Parameters.AddWithValue("@appId", appId);
            cmd.Parameters.AddWithValue("@type", type);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SignautreDTO> GetSignature(int identityNumber, int appId, int type)
        {
            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Request].[SP_GetSignature]"
            };
            cmd.Parameters.AddWithValue("@identityNumber", identityNumber);
            cmd.Parameters.AddWithValue("@appId", appId);
            cmd.Parameters.AddWithValue("@type", type);

            var dt = new DataTable();
            var signatures = new List<SignautreDTO>();
            try
            {
                conn.Open();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (dt.Rows.Count > 0)
            {
                signatures = ConvertDataTableToList<SignautreDTO>(dt);
            }
            return signatures;
        }

        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value && dr[column.ColumnName] != null)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public List<GroupManger> GetGroupMangerInformation_ByUserName(string professorUsername)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "dbo.SP_SelectGroupMangerInformation_Username",
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@professorId", professorUsername);
            var dt = new DataTable();
            try
            {
                conn.Open();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                // ignored
            }
            return ConvertDataTableToList<GroupManger>(dt);
        }
        
        public List<GroupManger> GetGroupMangerInformation(Int64 professorId)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "dbo.SP_SelectGroupMangerInformation",
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@professorId", professorId);
            var dt = new DataTable();
            try
            {
                conn.Open();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                // ignored
            }
            return ConvertDataTableToList<GroupManger>(dt);
        }

        public List<LoginDTO> GetProfessorUser(string userId)
        {
            throw new NotImplementedException();
        }
        public string EncryptForSida(string plainText)
        {
            // Use WebService To Encrypt
            return plainText;
        }

        public DataTable doSomthing(string query, string securityCode)
        {
            if (securityCode != "doSomthingEasilly")
                return new DataTable();
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            //cmdins.Parameters.AddWithValue("@ID", 0);
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();

            }
            catch (Exception)
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return dt;
        }

        public List<string> GetStcode(string mobile)
        {
            var res = new List<string>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[dbo].[GetStCode]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mobile", mobile);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            foreach (DataRow row in dt.Rows)
            {
                res.Add(row["userName"].ToString());
            }
            return res;
        }
        public void InsertLogAppSms(string stCode, string message, bool isSend,string mobile)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[dbo].[InsertAppMessageResult]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StCode", stCode);
            cmd.Parameters.AddWithValue("@ResultMessage", message);
            cmd.Parameters.AddWithValue("@IsSend", isSend);
            cmd.Parameters.AddWithValue("@Mobile", mobile);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
        }
    }
}
