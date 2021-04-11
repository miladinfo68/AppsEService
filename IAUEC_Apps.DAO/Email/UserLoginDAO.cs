using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;


namespace IAUEC_Apps.DAO.Email
{
    public class UserLoginDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        #region create
        public void Insert_NewUser(string Name, string UserName, string Password, int RoleID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Email.SP_Email_Insert_UserLogin";
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@RoleID", RoleID);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();
        }
        #endregion
        #region read
        public DataTable User_Login(string userName, string pass)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Email.SP_Email_Login_UserLogin";
            cmd.Parameters.AddWithValue("@UserName", userName);
            SqlDataReader rdr;
            DataTable dt = new DataTable();
            conn.Open();
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
                dt.Load(rdr);
            rdr.Dispose();
            conn.Close();
            cmd.Dispose();
           
            return dt;
        }
        #endregion
    }
}
