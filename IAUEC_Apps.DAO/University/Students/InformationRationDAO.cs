using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

namespace IAUEC_Apps.DAO.University.Students
{
    public class InformationRationDAO
    {
        SqlConnection conn = new SqlConnection(new AmozeshConnection().con_sida);
        public DataTable GetInfoSahmie(string Term, int Sex, int StatusStu, int Degree )
        {
           SqlCommand cmd = new SqlCommand();
            cmd.Connection=conn;
            cmd.CommandText="Students.SP_InfoSahmie";
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term" , Term);
            cmd.Parameters.AddWithValue("@Sex" ,Sex );
            cmd.Parameters.AddWithValue("@Degree", Degree);
            cmd.Parameters.AddWithValue("@StatusStu", StatusStu);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr=cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetInfoSahmie2(string Term, int Sex, int Degree, int StatusStu)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Students].[Sp_InfoSahmie2]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@Sex", Sex);
            cmd.Parameters.AddWithValue("@Degree", Degree);
            cmd.Parameters.AddWithValue("@StatusStu", StatusStu);
            DataTable dt = new DataTable();
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
        public DataTable GetInfoSahmiePermitted(string Term, int Sex, int Degree, int StatusStu)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Students].[SP_InfoSahmiePermitted]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@Sex", Sex);
            cmd.Parameters.AddWithValue("@Degree", Degree);
            cmd.Parameters.AddWithValue("@StatusStu", StatusStu);
            DataTable dt = new DataTable();
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
