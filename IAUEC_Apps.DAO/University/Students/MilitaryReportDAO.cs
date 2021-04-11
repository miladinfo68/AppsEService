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
    public class MilitaryReportDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        public DataTable GetInfoSTU(MilitaryReportDTO msd)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[Request].[SP_Get_StudentInfoBySearch]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@reshte", msd.reshte);
            cmd.Parameters.AddWithValue("@salVorood", msd.salVorood);
            cmd.Parameters.AddWithValue("@vaziat", msd.vaziat);
            cmd.Parameters.AddWithValue("@maghta", msd.maghta);
            cmd.Parameters.AddWithValue("@mojavez", msd.mojavez);
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
        
    }
}
