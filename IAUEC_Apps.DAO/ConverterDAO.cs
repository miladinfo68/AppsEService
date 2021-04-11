using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IAUEC_Apps.DAO
{
    public class ConverterDAO
    {
        readonly SqlConnection connSupp = new SqlConnection(new SuppConnection().Supp_con);
        readonly SqlConnection connAmoz = new SqlConnection(new AmozeshConnection().con);

        public DataTable GetAllStudentsLogins()
        {
            SqlCommand cmdGet = new SqlCommand
            {
                Connection = connAmoz,
                CommandText = "SELECT stcode, password_stu FROM amozesh..web_user",
                CommandType = CommandType.Text
            };
            DataTable dt = new DataTable();
            try
            {
                connAmoz.Open();
                SqlDataReader rdr;
                rdr = cmdGet.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                connAmoz.Close();
                cmdGet.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public DataTable GetAllProfessorsLogins()
        {
            SqlCommand cmdGet = new SqlCommand
            {
                Connection = connAmoz,
                CommandText = "SELECT ocode, password_ost FROM amozesh..web_user_ost",
                CommandType = CommandType.Text
            };
            DataTable dt = new DataTable();
            try
            {
                connAmoz.Open();
                SqlDataReader rdr;
                rdr = cmdGet.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                connAmoz.Close();
                cmdGet.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }



        public void InsertStudentsLogin(string userCode, string pass)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = connSupp,
                CommandText = "INSERT INTO StudentLogin (stcode, [Password]) VALUES ('" + userCode + "', '" + pass + "')",
                CommandType = CommandType.Text
            };
            try
            {
                connSupp.Open();
                cmd.ExecuteNonQuery();
                connSupp.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }

        public void InsertProfessorsLogin(string userCode, string pass)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = connSupp,
                CommandText = "INSERT INTO ProfessorLogin (ProfessorCode, [Password]) VALUES ('" + userCode + "', '" + pass + "')",
                CommandType = CommandType.Text
            };
            try
            {
                connSupp.Open();
                cmd.ExecuteNonQuery();
                connSupp.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }
    }
}
