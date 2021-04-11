using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.University.Students
{
     public class ReportExcellentStuDAO
    {
         SqlConnection conn = new SqlConnection(new AmozeshConnection().con_sida);
         public DataTable GetInfoSTU(int Daneshkade, int Field, int Education, string SalVorod, int Degree, int NimsalVorod, int Vaziatkol, int Sex, string FromDate, string ToDate)
         {
             SqlCommand cmd = new SqlCommand();
             cmd.CommandText = "[Students].[SP_GraduateStudents]";
             cmd.Connection = conn;
             cmd.CommandType = CommandType.StoredProcedure;
             DataTable dt = new DataTable();
             cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
             cmd.Parameters.AddWithValue("@Field", Field);
             cmd.Parameters.AddWithValue("@dorpar", Education);
             cmd.Parameters.AddWithValue("@sal_v", SalVorod);
             cmd.Parameters.AddWithValue("@Degree", Degree);
             cmd.Parameters.AddWithValue("@NimsalVorod", NimsalVorod);
             cmd.Parameters.AddWithValue("@Vazkol", Vaziatkol);
             cmd.Parameters.AddWithValue("@Sex", Sex);
             cmd.Parameters.AddWithValue("@FromDate", FromDate);
             cmd.Parameters.AddWithValue("@ToDate", ToDate);
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
         public DataTable dtExcellentStudents(string Term, string SalVorod, int NimsalVorod, int Degree, int Field)
         {
             SqlCommand cmd = new SqlCommand();
             cmd.Connection = conn;
             cmd.CommandText = "Students.SP_ExcellentStudents";
             cmd.CommandType = CommandType.StoredProcedure;
             DataTable dt = new DataTable();
             cmd.Parameters.AddWithValue("@Term", Term);
             cmd.Parameters.AddWithValue("@SalVorod", SalVorod);
             cmd.Parameters.AddWithValue("@NimsalVorod", NimsalVorod);
             cmd.Parameters.AddWithValue("@Degree", Degree);
             cmd.Parameters.AddWithValue("@Field", Field);
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
