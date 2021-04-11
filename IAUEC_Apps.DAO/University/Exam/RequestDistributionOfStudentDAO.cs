using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.University.Exam
{
    public class RequestDistributionOfStudentDAO
    {
        SqlConnection conn_Sida = new SqlConnection(new SuppConnection().Supp_con);
        //SqlConnection conn_Adobe = new SqlConnection(new AdobeConnection().AdobeconnectionString);
        //SqlConnection conn_AdobeOld = new SqlConnection(new AdobeConnection().VCconnectionString);
        //SqlConnection conn_AdobeLive = new SqlConnection(new AdobeConnection().liveconnectionString);


      
        public DataTable GetAllProvince()
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("[Exam].[SP_Province]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();

            DataTable DT = new DataTable();
            DT.Columns.Add("Province", typeof(string));

            //First Row
            DataRow row0 = DT.NewRow();
            row0["Province"] = "همه";
            DT.Rows.Add(row0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {                
                DataRow row = DT.NewRow();
                row["Province"] = dt.Rows[i]["Province"].ToString();
                DT.Rows.Add(row);                
            }
            return DT;
        }

        public DataTable GetAllTerm()
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_GetAllTerm]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();

            DataTable DT = new DataTable();
            DT.Columns.Add("tterm", typeof(string));

            //First Row
            DataRow row0 = DT.NewRow();
            row0["tterm"] = "همه";
            DT.Rows.Add(row0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = DT.NewRow();
                row["tterm"] = dt.Rows[i]["Term"].ToString();
                DT.Rows.Add(row);                
            }
            return DT;
        }

        public DataTable GetProvinceStudent(string Province)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("[Exam].[SP_ProvinceStudent]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Province", SqlDbType.NVarChar);
            cmd.Parameters["@Province"].Value = Province;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();
            return dt;
        }
    }
}
