using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.University.Support
{
    public class ResetPasswordDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        #region Professor
        public bool IsProfessorCodeExist(decimal professorCode)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "[Support].[SP_Get_ProfessorByProfessorCode]";
            cmds.Parameters.AddWithValue("@professorCode", professorCode);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int resultCount = int.Parse(cmds.ExecuteScalar().ToString());
                conn.Close();
                cmds.Dispose();
                return resultCount > 0;


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw ex;

            }
        }
        public bool ChangeProfessorPassword(decimal professorCode, string newPassword)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "[Support].[SP_Change_ProfessorPassword]";
            cmds.Parameters.AddWithValue("@professorCode", professorCode);
            cmds.Parameters.AddWithValue("@newPassword", newPassword);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmds.ExecuteScalar();
                conn.Close();
                cmds.Dispose();
                return true;


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw ex;

            }
        }
        #endregion

        #region Student
        public bool IsStudentCodeExist(decimal studentCode)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "[Support].[SP_Get_StudentByStudentCode]";
            cmds.Parameters.AddWithValue("@stcode", studentCode);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int resultCount = int.Parse(cmds.ExecuteScalar().ToString());
                conn.Close();
                cmds.Dispose();
                return resultCount > 0;


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw ex;

            }
        }
        public bool ChangeStudentPassword(decimal studentCode, string newPassword)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "[Support].[SP_Change_StudentPassword]";
            cmds.Parameters.AddWithValue("@stcode", studentCode);
            cmds.Parameters.AddWithValue("@newPassword", newPassword);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmds.ExecuteScalar();
                conn.Close();
                cmds.Dispose();
                return true;


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw ex;

            }
        }
        #endregion
    }
}
