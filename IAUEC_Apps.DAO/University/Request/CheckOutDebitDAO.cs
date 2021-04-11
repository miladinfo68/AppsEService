using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.University.Request
{
    public class CheckOutDebitDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        public DataTable GetAllDebitTypes()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetAllDebitType]";
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

        public void InsertUpdateDebit(string stcode, string debitNumber, string debitAmount, string TasvieAmount, string description, int debitType, string fishNumber, string fishDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateStudentDebit]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@debitTypeId", debitType);
            cmd.Parameters.AddWithValue("@debitAmount", debitAmount);
            cmd.Parameters.AddWithValue("@TasvieAmount", TasvieAmount);
            cmd.Parameters.AddWithValue("@debitNumber", debitNumber);
            cmd.Parameters.AddWithValue("@fishNumber", fishNumber);
            cmd.Parameters.AddWithValue("@fishDate", fishDate);
            cmd.Parameters.AddWithValue("@note", description);


            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
        }

        public DataTable GetAllDebitByStcode(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetAllDebitByStcode]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
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

        public void AddFishInfo(int DebitID, string fishNumber, string fishDate, string Note)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_AddFishInfo]";
            cmd.Parameters.AddWithValue("@debitID", DebitID);
            cmd.Parameters.AddWithValue("@fishNumber", fishNumber);
            cmd.Parameters.AddWithValue("@fishDate", fishDate);
            cmd.Parameters.AddWithValue("@note", Note);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
        }

        public int CheckRefahHasBedehi(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetAllUnCheckedOutDebit]";
            cmd.Parameters.AddWithValue("@stcode", stcode);

            try
            {
                int count;
                conn.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return count;
            }
            catch (Exception)
            {

                throw;

            }
        }

        public DataTable GetLastUniInfo(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetStLastUniInfo]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
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

        public void InsertStudentLastUniInfo(DTO.University.Request.CheckOutStLastUniInfo lastInfo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertStudentLastUniInfo]";
            cmd.Parameters.AddWithValue("@StCode", lastInfo.StCode);
            cmd.Parameters.AddWithValue("@TypeOfUni", lastInfo.TypeOfUni);
            cmd.Parameters.AddWithValue("@UniName", lastInfo.UniName);
            cmd.Parameters.AddWithValue("@FieldName", lastInfo.FieldName);
            cmd.Parameters.AddWithValue("@Maghta", lastInfo.Maghta);
            cmd.Parameters.AddWithValue("@FeraghatYear", lastInfo.FeraghatYear);
            cmd.Parameters.AddWithValue("@FeraghatHalfyear", lastInfo.FeraghatHalfYear);
            cmd.Parameters.AddWithValue("@FeraghatDate", lastInfo.FeraghatDate);
            cmd.Parameters.AddWithValue("@FeraghatType", lastInfo.FeraghatType);
            cmd.Parameters.AddWithValue("@CheckOutStatus", lastInfo.CheckOutStatus);
            cmd.Parameters.AddWithValue("@LoanAmount", lastInfo.LoanAmount);
            cmd.Parameters.AddWithValue("@IsMashmool", lastInfo.IsMashmool);
            cmd.Parameters.AddWithValue("@MadrakURL", lastInfo.ScanMadarekURL);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
        }

        public void InsertStudentAddress(DTO.University.Request.CheckOutStLastUniInfo AddressInfo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertStudentAdress]";
            cmd.Parameters.AddWithValue("@StCode", AddressInfo.StCode);
            cmd.Parameters.AddWithValue("@ProvinceID", AddressInfo.ProvinceID);
            cmd.Parameters.AddWithValue("@CityID", AddressInfo.CityID);
            cmd.Parameters.AddWithValue("@Street", AddressInfo.Street);
            cmd.Parameters.AddWithValue("@Alley", AddressInfo.Alley);
            cmd.Parameters.AddWithValue("@Pelak", AddressInfo.Pelak);
            cmd.Parameters.AddWithValue("@ZipCode", AddressInfo.ZipCode);
            cmd.Parameters.AddWithValue("@Phone", AddressInfo.Phone);
            cmd.Parameters.AddWithValue("@Mobile", AddressInfo.Mobile);
            cmd.Parameters.AddWithValue("@Email", AddressInfo.Email);
            cmd.Parameters.AddWithValue("@RabetPhone", AddressInfo.RabetPhone);
            cmd.Parameters.AddWithValue("@RabetMobile", AddressInfo.RabetMobile);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
        }

        public int GetStudentPastMaghta(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetStudentPastMaghta]";
            cmd.Parameters.AddWithValue("@StCode", stCode);
            int pastMaghta = 0;
            try
            {
                conn.Open();
                object x = cmd.ExecuteScalar();
                if (x != null)
                {
                    pastMaghta = Convert.ToInt32(x);
                }
                conn.Close();
                return pastMaghta;
            }
            catch (Exception)
            {

                throw;

            }
        }

        public DataTable GetStudentAddress(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetStudentAddress]";
            cmd.Parameters.AddWithValue("@stcode", stCode);
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

        public DataTable GetMaghtaInfo(string stcode, int maghta)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetStudentMaghtaInfo]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@maghta", maghta);
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

        public int UpdateDebit(DTO.University.Request.StudentCheckOutDebit debit)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_UpdateStudentDebit]";

            cmd.Parameters.AddWithValue("@RefID", debit.RefID);
            cmd.Parameters.AddWithValue("@stcode", debit.StCode);
            cmd.Parameters.AddWithValue("@debitTypeId", debit.DebitTypeID);
            cmd.Parameters.AddWithValue("@debitAmount", debit.DebitAmount);
            cmd.Parameters.AddWithValue("@Tasvie", debit.TotalLoanAmount);
            cmd.Parameters.AddWithValue("@debitNumber", debit.DebitNumber);
            cmd.Parameters.AddWithValue("@fishNumber", debit.FishNumber);
            cmd.Parameters.AddWithValue("@fishDate", debit.FishDate);
            cmd.Parameters.AddWithValue("@note", debit.Note);

            int result = 0;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {

                throw;

            }

            return result;
        }

        public bool DeleteDebit(long RefID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_DeleteStudentDebit]";

            cmd.Parameters.AddWithValue("@RefID", RefID);

            int result = 0;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
