using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.University.Request
{
    public class CheckOutMashmoolanDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        public DataTable GetMashmoolFareghOk()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetMashmoolFareghOk]";
            
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

        public int UpdateMashmoolStatusByReqId(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_UpdateMashmoolStatusByReqId]";
            cmd.Parameters.AddWithValue("reqId", reqId);
            
            int counter = 0;

            try
            {
                conn.Open();
                counter = cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
            return counter;
        }
    }
}
