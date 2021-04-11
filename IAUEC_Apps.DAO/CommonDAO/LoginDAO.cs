
using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DTO.AdobeClasses;

namespace IAUEC_Apps.DAO.CommonDAO
{
  public class LoginDAO
    {

      SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
      SqlConnection conn1 = new SqlConnection(new SuppConnection().UserAccess_con);

        #region Create
       
        #endregion


        #region Read
    //create by bahrami
      public DataTable StHasNaghs(string stcode)
      {
          SqlCommand cmd=new SqlCommand();
          cmd.Connection=conn;
          cmd.CommandType=CommandType.StoredProcedure;
          cmd.CommandText="dbo.SP_StHasNaghs";
          cmd.Parameters.AddWithValue("@stcode",stcode);
          DataTable dt=new DataTable();
          try 
          {
              conn.Open();
              SqlDataReader rdr;
              rdr=cmd.ExecuteReader();
              dt.Load(rdr);
              conn.Close();

          }
          catch(Exception)
          { throw; }
          return dt;
      }

      public DataTable Get_UserLogin(string username)
      {
          SqlCommand cmd = new SqlCommand();
          cmd.Connection = conn1;
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.CommandText = "dbo.SP_UserLogin";
          cmd.Parameters.AddWithValue("@userName", username);

          conn1.Open();
          DataTable dt = new DataTable();
          SqlDataReader rdr;
          rdr = cmd.ExecuteReader();
          dt.Load(rdr);

          conn1.Close();
          return dt;
      }

        public LoginDTO User_Login(string userName)//, string pass
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            /// Login via Amozesh
            /// cmd.CommandText = "Adobe.SP_ctrlUserAccess";
            ///

            /// Login via Supplementary
            cmd.CommandText = "dbo.SP_GetStudentLogin";
            /// 

            cmd.Parameters.AddWithValue("@UserName", userName);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            LoginDTO login = new LoginDTO();
            if (dt.Rows.Count > 0)
            {
                login.UserName = dt.Rows[0]["stcode"].ToString();
                login.Password = dt.Rows[0]["Password"].ToString();
            }

            conn.Close();
            return login;

        }
        public DataTable GetStIdVaz(string stcode)
        {
            SqlCommand cmdgetid=new SqlCommand();
            cmdgetid.CommandText="dbo.SP_GetStIdVaz";
            cmdgetid.Connection=conn;
            cmdgetid.CommandType=CommandType.StoredProcedure;
            cmdgetid.Parameters.AddWithValue("@stcode",stcode);
            DataTable dtid=new DataTable();

            try
            { 
                conn.Open();
                SqlDataReader rdr;
                rdr=cmdgetid.ExecuteReader();
                dtid.Load(rdr);
                conn.Close();
            }
            catch(Exception)
            {
                throw;
            }
            return dtid;
        }
        public DataTable User_Img(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_Get_StImage_ByStcode";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }
        public DataTable Get_StInfo(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_Get_StudentInfo";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }

        public DataTable IsUserForbidenToLogin(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetForbidenUsers";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }
        public DataTable Get_Menu_ByUserIdAndAppId(int AppId, int UserId,int sectionId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn1;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetMenu";
            cmd.Parameters.AddWithValue("@AppId", AppId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@sectionId", sectionId);
            conn1.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn1.Close();
            return dt;
        }
        public DataTable GetUserInfo(int UserLoginRoleId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn1;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetUserInfo";
            cmd.Parameters.AddWithValue("@UserLoginRoleId", UserLoginRoleId);

            conn1.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn1.Close();
            return dt;
        }
        public DataTable Get_Menu_ByUserIdAndAppId(int MenuId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn1;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Get_MenuPermissionByMenuId";
            cmd.Parameters.AddWithValue("@MenuId", MenuId);
         
            conn1.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn1.Close();
            return dt;
        }
        public DataTable Get_MenuPermission(int MenuId, int UserId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn1;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Get_MenuPermission";
            cmd.Parameters.AddWithValue("@MenuId", MenuId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            conn1.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn1.Close();
            return dt;
        }

        public DataTable GetUserRoles(string userID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn1;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetUserRoles";
            cmd.Parameters.AddWithValue("@userId", userID);
            conn1.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn1.Close();
            return dt;
        }

        public DataTable SendUserPassword(string username,string idd_meli ,string idd ,bool isstudent)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_SendUserPassword";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@idd_meli", idd_meli);
            cmd.Parameters.AddWithValue("@isstudent", isstudent);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }

        public DataTable GetUserLoginByUserCode(string userCode, bool isStudent)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetUserLoginByUserCode";
            cmd.Parameters.AddWithValue("@UserCode", userCode);
            cmd.Parameters.AddWithValue("@IsStudent", isStudent);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }

        public DataTable GetUserIdsByRoleId(int roleId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn1;
            cmd.CommandText = "[dbo].[SP_GetUserIdsByRoleId]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoleId", roleId);
            DataTable dt = new DataTable();
            try
            {
                conn1.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn1.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        #endregion


        #region Update
        public void DisableUsers(string userName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn1;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_DisableUsers";
            cmd.Parameters.AddWithValue("@UserName", userName);
            
            conn1.Open();
          
            cmd.ExecuteNonQuery();
            conn1.Close();
            
        }

        public int SetChangePasswordToken(string userCode, bool isStudent, string token, DateTime expDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.SP_SetChangePasswordToken";
            cmd.Parameters.AddWithValue("@UserCode", userCode);
            cmd.Parameters.AddWithValue("@IsStudent", isStudent);
            cmd.Parameters.AddWithValue("@Token", token);
            cmd.Parameters.AddWithValue("@ExpDate", expDate);

            conn.Open();

            int affectRows=cmd.ExecuteNonQuery();
            conn.Close();
            return affectRows;
        }

        public DataTable ResendChangePasswordToken(string userCode, DateTime expDate, bool isStudent)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.ResendChangePasswordToken";
            cmd.Parameters.AddWithValue("@UserCode", userCode);
            cmd.Parameters.AddWithValue("@IsStudent", isStudent);
            cmd.Parameters.AddWithValue("@ExpDate", expDate);

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
        
        public bool updatePortalStudentInfo(string stcode)
        {
            SqlCommand cmd = new SqlCommand("dbo.sp_updatePortalStudentInfo", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int res = cmd.ExecuteNonQuery();
                conn.Close();
                return res > 0;
            }
            catch(Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
        }
        #endregion


        #region Delete
        #endregion
    }
}
