using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.University.Students;
using System.Data;
using System.Data.SqlClient;

namespace IAUEC_Apps.DAO.University.Students
{
    public class MilitaryStatusDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        public DataTable GetInfoSTU(MilitaryStatusDTO msd)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[Request].[SP_Get_StudentInfoByFamily]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@stcode", msd.stCode);
            cmd.Parameters.AddWithValue("@family", msd.family);
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

        public void updateMashmulNumber(MilitaryStatusDTO msd)
        {
            SqlCommand cmdInsMash = new SqlCommand();
            cmdInsMash.Connection = conn;
            cmdInsMash.CommandText = "[Request].[SP_updateMilitaryStatus]";
            cmdInsMash.CommandType = CommandType.StoredProcedure;
            cmdInsMash.Parameters.AddWithValue("@stCode", msd.stCode);
            cmdInsMash.Parameters.AddWithValue("@mashmulNumber", msd.mashmulNumber);
            cmdInsMash.Parameters.AddWithValue("@MashmulTarikh", msd.mashmulTarikh);
            cmdInsMash.Parameters.AddWithValue("@MashmulStatus", msd.mashmulStatus);
            try
            {
                conn.Open();
                cmdInsMash.ExecuteNonQuery();
                conn.Close();
                cmdInsMash.Dispose();
            }
            catch (Exception)
            { 
                throw;
            }            
        }

        public void insertMashmulNumber(MilitaryStatusDTO msd)
        {
            SqlCommand cmdInsMash = new SqlCommand();
            cmdInsMash.Connection = conn;
            cmdInsMash.CommandText = "Request.SP_insertMilitaryStatus";
            cmdInsMash.CommandType = CommandType.StoredProcedure;
            cmdInsMash.Parameters.AddWithValue("@stCode", msd.stCode);
            cmdInsMash.Parameters.AddWithValue("@mashmulNumber", msd.mashmulNumber);
            cmdInsMash.Parameters.AddWithValue("@MashmulTarikh", msd.mashmulTarikh);
            cmdInsMash.Parameters.AddWithValue("@MashmulStatus", msd.mashmulStatus);
            try
            {
                conn.Open();
                cmdInsMash.ExecuteNonQuery();
                conn.Close();
                cmdInsMash.Dispose();
            }
            catch (Exception)
            { 
                throw;
            }
        }
    }
}
