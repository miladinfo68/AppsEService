using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.University.Students
{
    public class CardQuizTemporaryStudentsDAO
    {
        SqlConnection conn = new SqlConnection(new AmozeshConnection().con_sida);
        public DataTable CardQuizStudents(int Daneshkade , int Field , int Degree , int Dorpar , int Sex , string SalVorod , int MablaghAz , int MablaghTa, string TarikhSodor , string ModatEtebar , string Semat , string FamilySemat, int Mojaz , string Term , int CtrlCheck , int TypeAction , string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Students.SP_CardQuiz";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@idresh", Field); 
            cmd.Parameters.Add("@kardan", Degree);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@sex", Sex);
            cmd.Parameters.Add("@sal_v", SalVorod);
            cmd.Parameters.Add("@bed", MablaghAz);
            cmd.Parameters.Add("@bed2", MablaghTa);
            cmd.Parameters.Add("@TarikhSodor", TarikhSodor);
            cmd.Parameters.Add("@ModatEtabar", ModatEtebar);
            cmd.Parameters.Add("@Semat", Semat);
            cmd.Parameters.Add("@FamilySemat", FamilySemat);
            cmd.Parameters.Add("@mojaz", Mojaz);
            cmd.Parameters.Add("@term", Term);
            cmd.Parameters.Add("@ctrl_check", CtrlCheck);
            cmd.Parameters.Add("@typeaction",TypeAction);
            cmd.Parameters.Add("@stu1", stCode);
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
        public DataTable GetListStudentsNotImage(int Daneshkade, int Field, int Degree, int Dorpar, int Sex, string SalVorod, int MablaghAz, int MablaghTa, int Mojaz, string Term, int CtrlCheck, string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Students.SP_NotImage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@iddanesh", Daneshkade);
            cmd.Parameters.Add("@idresh", Field);
            cmd.Parameters.Add("@kardan", Degree);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@sex", Sex);
            cmd.Parameters.Add("@sal_v", SalVorod);
            cmd.Parameters.Add("@bed", MablaghAz);
            cmd.Parameters.Add("@bed2", MablaghTa);
            cmd.Parameters.Add("@mojaz", Mojaz);
            cmd.Parameters.Add("@term", Term);
            cmd.Parameters.Add("@ctrl_check", CtrlCheck);
            cmd.Parameters.Add("@stu1", stCode);
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
    }
}
