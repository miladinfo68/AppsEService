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
    public class CheckOutPajooheshDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        public DataTable GetCheckOutInfoByStCodeForPajohesh(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetStudentInfoByStCode]";
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
        public DataTable GetCheckOutInfoByStCodeNaghs(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_HasNaghsEditThes]";
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
        public void UpdateStudentsReqIDinNaghs(string stcode, int reqID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_UpdateStudentsReqIDinNaghs]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@StudentRequestID", reqID);


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
        public bool Add_Def_Date(string defdate, string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateStudentDefDate]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@defdate", defdate);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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
            catch (Exception)
            {

                throw;

            }
        }

        public bool Add_Cancel_Date(string stcode, string canceldate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateStudentCancelDate]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@canceldate", canceldate);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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
            catch (Exception)
            {

                throw;

            }
        }

        public bool Add_Receive_Date(string stcode, string receivedate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateStudentReceiveDate]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@receivedate", receivedate);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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
            catch (Exception)
            {

                throw;

            }
        }

        public bool Add_Send_Date(string stcode, string senddate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateStudentSendDate]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@senddate", senddate);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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
            catch (Exception)
            {

                throw;

            }
        }
        public bool Add_Send_Date_paj(string stcode, string senddate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateStudentSendDate_paj]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@senddate", senddate);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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
            catch (Exception)
            {

                throw;

            }
        }

        public bool Add_DefPoint(string stcode, string defpoint)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateStudentDefPoint]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@defpoint", defpoint);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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
            catch (Exception)
            {

                throw;

            }
        }
        public bool Add_HasCancelForm(string stcode, bool HasCancelForm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateHasCancelForm]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@hasCancelForm", HasCancelForm);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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
            catch (Exception)
            {

                throw;

            }
        }
        public bool Add_DeadLineDateOrCancelDate(string stcode, string DeadLineDate, string cancelDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateDeadLineDateOrCancelDate]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@deadLineDate", DeadLineDate);
            cmd.Parameters.AddWithValue("@cancelDate", cancelDate);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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
            catch (Exception)
            {

                throw;

            }
        }
        public bool Add_EditThesDetail(string stcode, string editThes, int ReqLogId,int isDeleted)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateEditThesDetail]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@EditThes", editThes);
            cmd.Parameters.AddWithValue("@ReqLogId", ReqLogId);
            cmd.Parameters.AddWithValue("@isDel", isDeleted);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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
            catch (Exception ex)
            {

                throw;

            }
        }
        public bool Add_EditThesDetailWithEditForm(string stcode, string editThes, int ReqLogId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateEditThesDetailwithEditForm]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@EditThes", editThes);
            cmd.Parameters.AddWithValue("@ReqLogId", ReqLogId);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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
            catch (Exception)
            {

                throw;

            }
        }
        public bool FinizlizePajoohesh(CheckOutPajoheshDTO oPajohesh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_CheckOutFinilizePjoohesh]";
            cmd.Parameters.AddWithValue("@stcode", oPajohesh.StCode);
            cmd.Parameters.AddWithValue("@Def_Date", oPajohesh.Def_Date);
            cmd.Parameters.AddWithValue("@Def_Point", oPajohesh.Def_Point);
            cmd.Parameters.AddWithValue("@Date_Paper_Cancel", oPajohesh.Date_Paper_Cancel);
            cmd.Parameters.AddWithValue("@HasPaper", oPajohesh.HasPaper);
            cmd.Parameters.AddWithValue("@Date_Recieve_Doc_Accept", oPajohesh.Date_Recieve_Doc_Accept);
            cmd.Parameters.AddWithValue("@Date_Send_Doc_Edu", oPajohesh.Date_Send_Doc_Edu);
            cmd.Parameters.AddWithValue("@IsFinalize", oPajohesh.IsFinalize);
            cmd.Parameters.AddWithValue("@HasCancelForm", oPajohesh.HasCancelForm);
            cmd.Parameters.AddWithValue("@DeadLineDate", oPajohesh.DeadLineDate);
            cmd.Parameters.AddWithValue("@EditThes", oPajohesh.EditThes);


            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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
            catch (Exception)
            {

                throw;

            }
        }

        public bool IsFinilized(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_CheckOutIsFinalized";
            cmd.Parameters.AddWithValue("@stcode", stCode);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                int r_a = (int)cmd.ExecuteScalar();
                if (r_a > 0)
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
            catch (Exception)
            {

                throw;

            }
        }

        public DataTable GetListOfFinalizedStudentByDate(string startDate, string endDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOfFinalizedStudent]";
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
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


        public DataTable GetConfirmStudentAfter48H(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_CanSubmitCheckoutReqByFinalDateafter48hour]";
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
}
