using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.CommonDAO
{
   public class StudentDAO
    {
       
#region read  

        public string GetLastTermBystcode(string stcode)
        {
            SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[dbo].[SP_GetLastTermByStcode]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            string term = "";
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    term = rdr.GetString(0);
                }
                conn.Close();

            }
            catch
            {
                throw;
            }
            return term;
        }
#endregion
    }
}
