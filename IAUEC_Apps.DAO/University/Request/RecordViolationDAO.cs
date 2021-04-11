using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.University.Request
{
    public class RecordViolationDAO
    {
        #region Connection
        private SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        #endregion

        #region Func
        public DataTable GetRecordViolationList()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[CheckOut].[SP_GetRecordViolation]";

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
        public bool InsertViolation(RecordViolationDTO recordViolationDTO)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[CheckOut].[InsertViolation]";
            cmd.Parameters.AddWithValue("@StudentCode", recordViolationDTO.StudentCode);
            cmd.Parameters.AddWithValue("@FirstName", recordViolationDTO.FirstName);
            cmd.Parameters.AddWithValue("@LastName", recordViolationDTO.LastName);
            cmd.Parameters.AddWithValue("@NationalCode", recordViolationDTO.NationalCode);
            cmd.Parameters.AddWithValue("@Description", recordViolationDTO.Description);
            cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
        }
        public RecordViolationStudentInfo GetStudentInfo(RecordViolationDTO recordViolationDTO)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[CheckOut].[GetStudentInfo]";
            cmd.Parameters.AddWithValue("@StudentCode", recordViolationDTO.StudentCode);

            DataTable dt = new DataTable();
            var res = new RecordViolationStudentInfo();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
                //var row = dt.Rows[0];
                foreach (DataRow row in dt.Rows)
                {
                    res.Status = true;
                    res.FirstName = row["firstName"].ToString();
                    res.LastName = row["lastName"].ToString();
                    res.FatherName = row["fatherName"].ToString();
                    res.NationalCode= row["nationalCode"].ToString();
                    res.FieldName = row["fieldName"].ToString();
                    res.DepartmentName = row["departmentName"].ToString();

                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool SubmitDeleteDate(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[CheckOut].[SP_SubmitDeleteDate]";
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@DeleteDate", DateTime.Now);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
        }
        public bool UnSubmitDeleteDate(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[CheckOut].[SP_UnSubmitDeleteDate]";
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
        }
        #endregion

    }
}
