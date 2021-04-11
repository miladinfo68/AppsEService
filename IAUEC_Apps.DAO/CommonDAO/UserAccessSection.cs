using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.CommonClasses;

namespace IAUEC_Apps.DAO.CommonDAO
{
    public class UserAccessSection
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().UserAccess_con);
        
        public DataTable GetSection()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_GetAllSection]";

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

        public DataTable GetRequest(UserAccessSectionDTO uasDTO)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_Get_UserLogTasvie]";
            cmd.Parameters.AddWithValue("@userId", uasDTO.userId);
            cmd.Parameters.AddWithValue("@fdaet", uasDTO.StartDate);
            cmd.Parameters.AddWithValue("@tdate", uasDTO.EndDate);
                    
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
        public int GetDaneshIDByRoleID(int RoleId)
        {
            SqlCommand cmdapp = new SqlCommand();
            cmdapp.Connection = conn;
            cmdapp.CommandText = "SP_GetDaneshIDByRoleID";
            cmdapp.CommandType = CommandType.StoredProcedure;
            cmdapp.Parameters.AddWithValue("@RoleId", RoleId);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdapp.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return int.Parse(dt.Rows[0][0].ToString());

        }
    }
}
