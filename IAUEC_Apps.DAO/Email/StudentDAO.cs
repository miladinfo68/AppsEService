using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DAO.Email;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

namespace IAUEC_Apps.DAO.Email
{
    public class StudentDAO
    { 
        SqlConnection conn_Sida = new SqlConnection(new SuppConnection().Supp_con);
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        #region Create

        public void InsertIntoStudentSupInfo(string stcode, string name, string family)
        {
            SqlCommand cmdins = new SqlCommand();
            cmdins.Connection = conn;
            cmdins.CommandText = "dbo.SP_InsertIntoStudentSupInfo";
            cmdins.CommandType = CommandType.StoredProcedure;
            cmdins.Parameters.AddWithValue("@stcode",stcode);
            cmdins.Parameters.AddWithValue("@StName",name);
            cmdins.Parameters.AddWithValue("@StFamily",family);
            try
            {
                conn.Open();
                cmdins.ExecuteNonQuery();
                conn.Close();
                cmdins.Dispose();

            }
            catch(Exception)
            {
                throw;
            }

        }
        public string GetMobileByStcode(string stcode)
        {
            SqlDataReader rdr;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("Email.SP_Email_GetMobileByStcode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            string Mobile = "";
            cmd.Parameters.Add("@STCODE", SqlDbType.VarChar);
            cmd.Parameters["@STCODE"].Value = stcode;
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                dt.Load(rdr);
                Mobile = dt.Rows[0]["mobile"].ToString();
            }
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return Mobile;
        }

        public string GetMobileByStcode1(string stcode)
        {
            SqlDataReader rdr;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("Email.SP_Email_GetMobileByStcode1", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            string Mobile = "";
            cmd.Parameters.Add("@STCODE", SqlDbType.VarChar);
            cmd.Parameters["@STCODE"].Value = stcode;
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                dt.Load(rdr);
                Mobile = dt.Rows[0]["s_mobile"].ToString();
            }
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return Mobile;
        }
      
        #endregion
        #region read
        public DataTable GetEmailRequestStatus(string stcode)
        {
            SqlCommand cmdget = new SqlCommand();
            cmdget.Connection = conn;
            cmdget.CommandType = CommandType.StoredProcedure;
            cmdget.CommandText = "Email.SP_GetEmailRequestStatus";
            cmdget.Parameters.AddWithValue("@stcode",stcode);
            DataTable dt=new DataTable();

            try 
            {
                conn.Open();
                SqlDataReader rdr;
                rdr=cmdget.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdget.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable GetstFromStudentSupInfo(string stcode)
        {
            SqlCommand cmdgsup = new SqlCommand();
            cmdgsup.Connection = conn;
            cmdgsup.CommandText = "dbo.SP_GetstFromStudentSupInfo";
            cmdgsup.CommandType = CommandType.StoredProcedure;
            cmdgsup.Parameters.AddWithValue("@stcode",stcode);
            DataTable dtsup = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdgsup.ExecuteReader();
                dtsup.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdgsup.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return dtsup;
        }

        public DataTable GetAllStHaveEmail(string stcode)
        {
            SqlCommand cmdAll = new SqlCommand();
            cmdAll.Connection = conn;
            cmdAll.CommandText = "Email.SP_GetAllStHaveEmail";
            cmdAll.CommandType = CommandType.StoredProcedure;
            cmdAll.Parameters.AddWithValue("@stcode",stcode);
            DataTable dt=new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr=cmdAll.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdAll.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }
        public bool CheckUser(string user, string pass)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("Email.webmeli_ctrl_pass", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@username", SqlDbType.VarChar);
            cmd.Parameters["@username"].Value = user;
            cmd.Parameters.Add("@password", SqlDbType.VarChar);
            cmd.Parameters["@password"].Value = pass;  
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();
                 
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;

        }
        public DataTable GetStSecurityCode(string stcode)
        {
            SqlCommand cmdgetsec = new SqlCommand();
            cmdgetsec.Connection = conn;
            cmdgetsec.CommandText = "Email.SP_GetStSecurityCode";
            cmdgetsec.CommandType = CommandType.StoredProcedure;
            cmdgetsec.Parameters.AddWithValue("@stcode",stcode);
            DataTable dtsec=new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr=cmdgetsec.ExecuteReader();
                dtsec.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdgetsec.Dispose();
            }
            catch(Exception)
            { throw; }
            return dtsec;
        }
        public DataTable Giveinfo(string stcode)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("Email.SP_Email_GetStudentInfo", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@STCODE", SqlDbType.VarChar);
            cmd.Parameters["@STCODE"].Value = stcode;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();            
            return dt;
        }


        public void Update_Mobile(string stcode, string Mobile)
        {
            SqlDataReader rdr = null;
            conn.Open();
            SqlCommand cmd = new SqlCommand("Email.SP_Email_insertMobile", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@STCODE", SqlDbType.VarChar);
            cmd.Parameters["@STCODE"].Value = stcode;
            cmd.Parameters.Add("@mobile", SqlDbType.VarChar);
            cmd.Parameters["@mobile"].Value = Mobile;
            rdr = cmd.ExecuteReader();
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
        }

        public DataTable GetMobileByRequestID(int RequestID)
        {
            SqlCommand cmdinf = new SqlCommand();
            cmdinf.Connection = conn;
            cmdinf.CommandText = "Email.SP_Email_GetStCodeByIdRequest";
            cmdinf.CommandType = CommandType.StoredProcedure;
            cmdinf.Parameters.AddWithValue("@RequestID", RequestID);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdinf.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdinf.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        #endregion

        #region Update

        public void Update_UserEmailFsf2(string emailaddress)
        {
            SqlCommand cmdup=new SqlCommand();
            cmdup.Connection=conn;
            cmdup.CommandText="Email.SP_Update_UserEmailFsf2";
            cmdup.CommandType=CommandType.StoredProcedure;
            cmdup.Parameters.AddWithValue("EmailAddress",emailaddress);

            try
            {
                conn.Open();
                cmdup.ExecuteNonQuery();
                conn.Close();
                cmdup.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
        }

#endregion
    }
}
