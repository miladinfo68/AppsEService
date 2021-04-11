using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DTO.University.Request;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.University.Request
{
    public class MadarekReportDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        public DataTable GetInfo(MadarekReportDTO dto)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Request].[SP_GetMadarekBysearch]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@riz", dto.rizNomarat);
            cmd.Parameters.AddWithValue("@govahi", dto.GovahiMovaghat);
            cmd.Parameters.AddWithValue("@danesh", dto.DaneshName);
            cmd.Parameters.AddWithValue("@sDate", dto.sDate);
            cmd.Parameters.AddWithValue("@eDate", dto.eDate);

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
