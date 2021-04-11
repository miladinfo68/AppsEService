using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.University.Students
{
    public class PaymentStatusAndEnrollment
    {
        SqlConnection conn = new SqlConnection(new AmozeshConnection().con_sida);
        #region Read
        public DataTable GetStatusPayStu(string Term, int StatusStu, int Daneshkade, int Field, int Degree)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Students.SP_InfoStatusPaymentStu";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.Add("@Term", Term);
            cmd.Parameters.Add("@StatusStu", StatusStu);
            cmd.Parameters.Add("@Danshkade", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@Degree", Degree);
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
        public DataTable GetStatusRegistrationStu(string Term, int StatusStu, int Daneshkade, int Field, int Degree)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Students.SP_InfoStatusRegistartionStu";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.Add("@Term", Term);
            cmd.Parameters.Add("@StatusStu", StatusStu);
            cmd.Parameters.Add("@Danshkade", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@Degree", Degree);
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


        public DataTable GetStudentRegistrationReport(int daneshid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Students.SP_GetStudentRegToSida";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.Add("@daneshid", daneshid);
           
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
        #endregion
    }
}
