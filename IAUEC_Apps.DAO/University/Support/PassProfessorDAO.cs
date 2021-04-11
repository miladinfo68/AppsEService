using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.University.Support
{
    public class PassProfessorDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        #region read
        //ramezanian
        /// <summary>
        /// اطلاعات اساتید را بر می گرداند
        /// </summary>
        /// <param name="CodeOstad">The Code Ostad.</param>
        /// <param name="NameOstad">The Name Ostad.</param>
        /// <param name="CodeMelli">The Code Meli.</param>
        /// <returns></returns>
        public DataTable GetSelectRowOstad(int CodeOstad, string NameOstad, string CodeMelli)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Support].[SP_Get_ProfessorInformation]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@NameOstad", NameOstad);
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

        ///تیموری
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CodeOstad">شناسه استاد</param>
        /// <param name="NameOstad">نام خانوادگی استاد</param>
        /// <param name="CodeMelli">کد ملی استاد</param>
        /// <returns></returns>
        public DataTable GetOstadInformation(string CodeOstad, string NameOstad, string CodeMelli)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Support].[SP_ProfessorInformation_Ostad]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ostadCode", CodeOstad);
            cmd.Parameters.AddWithValue("@ostadName", NameOstad);
            cmd.Parameters.AddWithValue("@codeMelli", CodeMelli);
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

        //تیموری
        /// <summary>
        /// اطلاعات استادانی که پژوهشی هستند را برمیگرداند
        /// </summary>
        /// <param name="CodeOstad">شناسه استاد</param>
        /// <param name="NameOstad">نام خانوادگی استاد</param>
        /// <param name="CodeMelli">کد ملی استاد</param>
        /// <returns></returns>
        public DataTable GetOstadPazhuheshInformation(string CodeOstad, string NameOstad, string CodeMelli)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Support].[SP_ProfessorInformatin_Pazhuhesh]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ostadCode", CodeOstad);
            cmd.Parameters.AddWithValue("@ostadName", NameOstad);
            cmd.Parameters.AddWithValue("@codeMelli", CodeMelli);
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
        /// متن ارسال پیامک یا ایمیل را بر اساس 4 پارامتر وروری برای برنامه های مختلف بر می گرداند
        /// </summary>
        /// <param name="type">The Term.</param>
        /// <param name="App_ID">The App ID.</param>
        /// <param name="Category">The Category.</param>
        /// <param name="Status">The Status</param>
        /// <returns></returns>
        public DataTable GetSendMessage(int type, int App_ID, int Category, int Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.SP_GetConnectTextByStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@AppId", App_ID);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@Status", Status);
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
        /// پیغام وضعیت ارسال پیامک را برمیگرداند
        /// </summary>
        /// <param name="resault">The Resault.</param>
        /// <returns></returns>
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
        #endregion
        //ramezanian
        /// <summary>
        /// شش پارامتر ورودی را گرفته و در جدول ذخیره میکند تا مشخص کند به چه افرادی پیامک ارسال کردند 
        /// </summary>
        /// <param name="code">The Code Student or Code Ostad</param>
        /// <param name="codeAsanak">The Code Asanak.</param>
        /// <param name="mobile">The Mobile.</param>
        /// <param name="status">The Status.</param>
        /// <param name="msgStatus">The Text Message Status</param>
        /// <param name="IDAppMessage">The Number Row in dbo.Tbl_App_Message</param>
        #region write
        public void InsertIntoLogMsgStatus(string code, string codeAsanak, string mobile, int status, string msgStatus, int IDAppMessage)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Support.Sp_RequestLogStatusMessage";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@codeAsanak", codeAsanak);
            cmd.Parameters.AddWithValue("@mobile", mobile);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@msgStatus", msgStatus);
            cmd.Parameters.AddWithValue("@IDAppMessage", IDAppMessage);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region Update
        //ramezanian
        /// <summary>
        /// به منظور بروز کردن وضعیت ارسال پیامک از این متد استفاده می کنیم
        /// </summary>
        /// <param name="Text">The Text</param>
        /// <param name="msgStatus">The Message Status</param>
        /// <param name="codeAsanak">The Code Asanak</param>
        public void UpdateLogMessage(string Text, string msgStatus, string codeAsanak)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Support.SP_UpdateLogMessageStatus";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@Text", Text);
            cmd.Parameters.AddWithValue("@msgStatus", msgStatus);
            cmd.Parameters.AddWithValue("@codeAsanak", codeAsanak);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetInfoGroupProf_Amuzesh(int Daneshkade, int Departman)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Support.SP_GetProfessorInfoGroup_Amuzesh";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
            cmd.Parameters.AddWithValue("@Departman", Departman);
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

        public DataTable GetInfoGroupProf_Pazhuhesh(int Daneshkade, int Departman)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Support.SP_GetProfessorInfoGroup_Pazhuhesh";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
            cmd.Parameters.AddWithValue("@Departman", Departman);
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

        public DataTable GetTypeRequest()
        {
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Support].[SP_GetTypeRequestSMS]";
            cmd.Connection = conn;
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
        #endregion
    }
}

