using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.StudentInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.StudentInfo
{
    public class StudentInformationDAO
    {
        #region ConnectionString
        SqlConnection con = new SqlConnection(new SuppConnection().Supp_con);
        #endregion

        #region Methods
        public List<StateDTO> GetStates()
        {
            var res = new List<StateDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[StudentInfo].[GetAmozeshStates]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            foreach (DataRow row in dt.Rows)
            {
                res.Add(new StateDTO
                {
                    STATE_CODE = Convert.ToInt32(row["STATE_CODE"]),
                    STATE_NAME = row["STATE_NAME"].ToString()
                });
            }
            return res;
        }

        public string GetStudentByStudentCode(string stCode)
        {
            var mobile = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[StudentInfo].[GetStudentByStudentCode]";
            cmd.Parameters.AddWithValue("@studentCode", stCode);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            foreach (DataRow row in dt.Rows)
            {
                mobile = row["mobile"].ToString();
            }
            return mobile;
        }

        public List<CityDTO> GetCityDTO(int stateCode)
        {
            var res = new List<CityDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[StudentInfo].[GetAmozeshCity]";
            cmd.Parameters.AddWithValue("@stateCode", stateCode);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            foreach (DataRow row in dt.Rows)
            {
                res.Add(new CityDTO
                {
                    CITY_CODE = Convert.ToInt32(row["CITY_CODE"]),
                    CITY_NAME = row["CITY_NAME"].ToString()
                });
            }
            return res;
        }
        public bool UpdateStudent(StudentDTO model)
        {
            var res = new List<StateDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[StudentInfo].[UpdateStudent]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Address", model.Address);
            cmd.Parameters.AddWithValue("@BirthDay", model.BirthDay);
            cmd.Parameters.AddWithValue("@BirthPlaceState", model.BirthPlaceState);
            cmd.Parameters.AddWithValue("@BirthPlaceCity", model.BirthPlaceCity);
            cmd.Parameters.AddWithValue("@IssuePlaceState", model.IssuePlaceState);
            cmd.Parameters.AddWithValue("@IssuePlaceCity", model.IssuePlaceCity);
            cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
            cmd.Parameters.AddWithValue("@Phone", model.Phone);
            cmd.Parameters.AddWithValue("@Militrystatus", model.Militrystatus);
            cmd.Parameters.AddWithValue("@gender", model.Gender);
            cmd.Parameters.AddWithValue("@fatherName", model.FatherName);
            cmd.Parameters.AddWithValue("@idNumber", model.IdNumber);
            cmd.Parameters.AddWithValue("@home_postalCode", model.PostalCode);
            cmd.Parameters.AddWithValue("@email", model.Email);
            cmd.Parameters.AddWithValue("@home_state", model.State);
            cmd.Parameters.AddWithValue("@home_city", model.City);
            cmd.Parameters.AddWithValue("@StudentCode", model.StudentCode);
   
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                return false;
            }
        }
        public bool IsStudentUpdate(string stCode)
        {
            var res = new List<StateDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[StudentInfo].[IsStudentUpdated]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentCode", stCode);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            return dt.Rows.Count != 0;
        }
        #endregion
    }
}
