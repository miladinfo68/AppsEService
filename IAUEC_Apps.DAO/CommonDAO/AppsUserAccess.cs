using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.CommonDAO
{
   public class AppsUserAccess
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().UserAccess_con);


        #region Read
       
        public DataTable Get_AllAppIdByUserId(int UserId)
        { 
            SqlCommand cmdapp=new SqlCommand();
            cmdapp.Connection=conn;
            cmdapp.CommandText="SP_GetAppByUserId";
            cmdapp.CommandType=CommandType.StoredProcedure;
            cmdapp.Parameters.AddWithValue("@UserId",UserId);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr=cmdapp.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch(Exception)
            {
                throw;
            }

            return dt;
        }
        public DataTable Get_MenuPermissionByMenuId(int MenuId)
        { 
            SqlCommand cmdapp=new SqlCommand();
            cmdapp.Connection=conn;
            cmdapp.CommandText = "SP_Get_MenuPermissionByMenuId";
            cmdapp.CommandType=CommandType.StoredProcedure;
            cmdapp.Parameters.AddWithValue("@MenuId", MenuId);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr=cmdapp.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch(Exception)
            {
                throw;
            }

            return dt;
        }
         #endregion

    }
}
