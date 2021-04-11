using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.University.Request
{
    public class CheckOutMaliDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        public bool InsertBankAcount(string stCode, string BankMeliID,string AcountSheba, string AcountOwner, string PhoneNumber, string BankName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_InsertBankAcountInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode",stCode);
            cmd.Parameters.AddWithValue("@BankMeliID", BankMeliID);
            cmd.Parameters.AddWithValue("@AcountSheba", AcountSheba);
            cmd.Parameters.AddWithValue("@AcountOwner", AcountOwner);
            cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
            cmd.Parameters.AddWithValue("@BankName", BankName);
            try
            {
                int a = 0;
                conn.Open();
                a =Convert.ToInt32(cmd.ExecuteScalar());
                if (a >0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public DataTable HasAcountNo(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Request].[SP_CheckStcodeInBankinfo]";
            cmd.CommandType = CommandType.StoredProcedure;
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


        public DataTable HasDebitRefah(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Request].[SP_CheckdebitStcodeInBankinfoRefah]";
            cmd.CommandType = CommandType.StoredProcedure;
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
        public DataTable HasDebitRefahVezarat(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Request].[SP_CheckdebitRefahVam]";
            cmd.CommandType = CommandType.StoredProcedure;
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
            catch (Exception ex)
            {

                throw;

            }
            return dt;

        }

        public DataTable getAllDebit(string stcode,int debitType=0)
        {
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = "request.SP_CheckoutDebit";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@debitType", debitType);
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch(Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

            }
            return dt;
        }

        public DataTable HasDebitAcountNo(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Request].[SP_CheckdebitStcodeInBankinfo]";
            cmd.CommandType = CommandType.StoredProcedure;
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



        public DataTable HasAcountInfo(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_GetBankAcountInfoCheckOut";
            cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetAllMaliDebitByStcode(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_GetAllMaliDebits";
            cmd.CommandType = CommandType.StoredProcedure;
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

        public void InsertUpdateDebit(string stcode, string strNumber1, string strAmount, string strNote, int debitTypeId, string strFishNumber, string strFishDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateStudentMaliDebit]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@debitTypeId", debitTypeId);
            cmd.Parameters.AddWithValue("@debitAmount", strAmount);
            cmd.Parameters.AddWithValue("@debitNumber", strNumber1);
            cmd.Parameters.AddWithValue("@fishNumber", strFishNumber);
            cmd.Parameters.AddWithValue("@fishDate", strFishDate);
            cmd.Parameters.AddWithValue("@note", strNote);


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

        public void AddFishInfo(int debitID, string fishNumber, string fishDate, string Note)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_AddMaliFishInfo]";
            cmd.Parameters.AddWithValue("@debitID", debitID);
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

        public int CheckMaliCheckOut(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_GetCountMaliUnCheckOutDebits";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);

            int result=0;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
                result = Convert.ToInt32(dt.Rows[0]["sidabedehi"]);
            }
            catch (Exception)
            {

                throw;

            }
            return result;
            //try
            //{
            //    conn.Open();
            //    result =Convert.ToInt32(cmd.ExecuteScalar());
            //    conn.Close();

            //}
            //catch (Exception)
            //{

            //    throw;

            //}
            //return result;
        }
        public DataTable CheckMaliCheckOutVezarat(string stcode)
        {
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Request.SP_GetCheckOutVezaratDebits";
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
        }
        public int RemoveVahedInTerm(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_RemoveVahedinTermStudent]";
            cmd.Parameters.AddWithValue("@stcode", stCode);
            int count=0;
            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {

                throw;

            }

            return count;
        }

        public DataTable GetStudentNewCode(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetNewStCodeFromInitial]";
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

        public int UpdateMaliDebit(DTO.University.Request.StudentCheckOutDebit debit)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_UpdateStudentMaliDebit]";

            cmd.Parameters.AddWithValue("@RefID", debit.RefID);
            cmd.Parameters.AddWithValue("@stcode", debit.StCode);
            cmd.Parameters.AddWithValue("@debitTypeId", debit.DebitTypeID);
            cmd.Parameters.AddWithValue("@debitAmount", debit.DebitAmount);
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
            cmd.CommandText = "[Request].[SP_DeleteStudentMaliDebit]";

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
