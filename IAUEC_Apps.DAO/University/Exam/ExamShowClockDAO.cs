using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

namespace IAUEC_Apps.DAO.University.Exam
{
    public class ExamShowClockDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        #region read

        public DataTable Get_ShowNimsal()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Exam.SP_GetShowTerm";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable   dtTerm = new DataTable  ();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dtTerm.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dtTerm;
        }

        public DataTable ShowClockDateExam(string tterm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Exam.SP_GetSaatDateExam";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tterm", tterm);
            DataTable ClockExam = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                ClockExam.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return ClockExam;

        }

        #endregion

    }
}
