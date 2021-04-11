using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAC.Connections;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DTO.CommonClasses;

namespace IAUEC_Apps.DAO.University.Request
{
    public class PaymentRequestDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        #region create
        //سپیده حسینی-09-04-93-create
        public void CreateStudentPayment(PaymentDTO payInfo)
        {
            PaymentDTO PaymentDTO = new PaymentDTO();
            SqlCommand cmdpay = new SqlCommand();
            cmdpay.Connection = conn;
            cmdpay.CommandType = CommandType.StoredProcedure;
            cmdpay.CommandText = "Request.SP_InsertPaymentRequest";


            cmdpay.Parameters.AddWithValue("@orderId", payInfo.OrderId);
            cmdpay.Parameters.AddWithValue("@amount", payInfo.Amount);
            cmdpay.Parameters.AddWithValue("@stcode", payInfo.stcode);
            cmdpay.Parameters.AddWithValue("@tterm", payInfo.tterm);
            cmdpay.Parameters.AddWithValue("@bankId", payInfo.bankId);
            cmdpay.Parameters.AddWithValue("@PayType", payInfo.PayType);
            cmdpay.Parameters.AddWithValue("@Description", payInfo.Description);
            cmdpay.Parameters.AddWithValue("@RequestId", payInfo.RequestId);
            cmdpay.Parameters.AddWithValue("@RequestKey", payInfo.ReqKey);
            cmdpay.Parameters.AddWithValue("@AppStatus", payInfo.AppStatus);
            cmdpay.Parameters.AddWithValue("@result", payInfo.Result);
            try
            {
                conn.Open();
                cmdpay.ExecuteNonQuery();
                conn.Close();
                cmdpay.Dispose();

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }

        }
        #endregion
        #region Read
        //16-10-94-bahrami-create
        public PaymentDTO GetPaymentInfoByOrderID(long OrderID)
        {

            SqlCommand cmdPayment = new SqlCommand();
            cmdPayment.Connection = conn;
            cmdPayment.CommandType = CommandType.StoredProcedure;
            cmdPayment.CommandText = "Request.SP_GetPaymentInfoByOrderID";
            cmdPayment.Parameters.AddWithValue("@OrderID", OrderID);

            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader dr = cmdPayment.ExecuteReader();
                PaymentDTO PayInfo = new PaymentDTO();

                if (dr.HasRows)
                {
                    dt.Load(dr);
                    PayInfo.Amount = long.Parse(dt.Rows[0]["AmountTrans"] == DBNull.Value ? 0.ToString() : dt.Rows[0]["AmountTrans"].ToString());
                    PayInfo.Result = int.Parse(dt.Rows[0]["Result"] == DBNull.Value ? 0.ToString() : dt.Rows[0]["Result"].ToString());
                    PayInfo.ReqKey = dt.Rows[0]["RequestKey"].ToString();
                    PayInfo.AppStatus = dt.Rows[0]["AppStatus"].ToString();
                    PayInfo.stcode = dt.Rows[0]["StudentCode"].ToString();
                    PayInfo.tterm = dt.Rows[0]["tterm"].ToString();
                    PayInfo.MiladiDate = Convert.ToDateTime(dt.Rows[0]["MiladiDate"].ToString());
                    PayInfo.PersianDate = dt.Rows[0]["PersianDate"].ToString();
                    PayInfo.OrderId = long.Parse(dt.Rows[0]["OrderID"] == DBNull.Value ? 0.ToString() : dt.Rows[0]["OrderID"].ToString());
                    PayInfo.TraceNumber = long.Parse(dt.Rows[0]["TraceNumber"] == DBNull.Value ? 0.ToString() : dt.Rows[0]["TraceNumber"].ToString());

                }


                conn.Close();
                cmdPayment.Dispose();
                return PayInfo;

            }
            catch (Exception)
            {

                throw;
            }



        }
        //21-10-94-bahrami-create
        public DataTable GetStudentGovahiPaymentInquiry(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetStudentGovahiPaymentInquiry";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        //16-10-94-bahrami-create
        public DataTable GetStudentPaymentInquiry(string stcode, int payType)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetStudentPaymentInquiry";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@payType", payType);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        //سپیده حسینی-09-04-94-create
        public bool CheckOrderId(long orderID, int bankID)
        {
            SqlCommand cmdchk = new SqlCommand();
            cmdchk.CommandType = CommandType.StoredProcedure;
            cmdchk.CommandText = "Request.SP_CheckPaymentOrderID";

            cmdchk.Connection = conn;
            cmdchk.Parameters.AddWithValue("@orderId", orderID);
            cmdchk.Parameters.AddWithValue("@bankId", bankID);
            try
            {
                conn.Open();
                long oID = long.Parse(cmdchk.ExecuteScalar().ToString());
                if (oID == 0)
                {
                    conn.Close();
                    cmdchk.Dispose();
                    return false;
                }
                else
                {
                    conn.Close();
                    cmdchk.Dispose();
                    return true;
                }

            }
            catch (Exception)
            {

                throw;
            }


        }
        //سپیده حسینی-09-04-94-create
        public PaymentDTO GetPaymentByRefID(string RefId)
        {
            try
            {
                PaymentDTO payInfo = new PaymentDTO();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Request.SP_GetPaymentInfoByRefID";
                cmd.Parameters.AddWithValue("@RefID", RefId);
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();

                if (rdr.HasRows)
                {

                    dt.Load(rdr);

                    payInfo.stcode = dt.Rows[0]["StudentCode"].ToString();

                    rdr.Dispose();
                    conn.Close();

                    return payInfo;

                }
                else
                {

                    rdr.Dispose();
                    conn.Close();
                    return payInfo;
                }

            }
            catch (Exception)
            {

                throw;
            }


        }
        //سپیده حسینی-09-04-94-create
        public DataTable Get_Student_Payment(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_Get_StudentPayment";
            cmd.Parameters.AddWithValue("@studentCode", stcode);
            conn.Open();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            if (rdr.HasRows)
            {

                dt.Load(rdr);
                rdr.Dispose();
            }
            conn.Close();
            return dt;
        }
        //سپیده حسینی-09-04-94-create
        public DataTable GetAllPayment()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "request.SP_GetAllPayment";

            conn.Open();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            if (rdr.HasRows)
            {

                dt.Load(rdr);
                rdr.Dispose();
            }
            conn.Close();
            cmd.Dispose();
            return dt;
        }
        #endregion
        #region update
        //سپیده حسینی-09-04-94-create
        public void UpdateMellatPayment(PaymentDTO payment)
        {
            try
            {

                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = conn;
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "Request.SP_UpdateMellatPayment";

                cmd1.Parameters.AddWithValue("@OrderID", payment.OrderId);
                cmd1.Parameters.AddWithValue("@RefID", payment.ReqKey);
                cmd1.Parameters.AddWithValue("@Status", payment.AppStatus);
                cmd1.Parameters.AddWithValue("@SaleRef", payment.TraceNumber);
                cmd1.Parameters.AddWithValue("@ResCode", payment.Result);

                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();
                cmd1.Dispose();


            }
            catch (Exception)
            {

                throw;
            }

        }
        //سپیده حسینی-09-04-94-create
        public void UpdateMellatPaymentStatus(PaymentDTO payment)
        {
            try
            {

                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = conn;
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "Request.SP_UpdateMellatPaymentStatus";
                cmd1.Parameters.AddWithValue("@OrderID", payment.OrderId);
                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();
                cmd1.Dispose();

            }
            catch (Exception)
            {

                throw;
            }

        }


        public DataTable GetPaymentByRequestID(Int64 ReqId, int payType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Request.SP_GetPaymentInfoByRequestId";
                SqlParameter p1 = new SqlParameter();
                cmd.Parameters.AddWithValue("@ResID", ReqId);
                cmd.Parameters.AddWithValue("@payType", payType);
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();

                if (rdr.HasRows)
                {

                    dt.Load(rdr);


                    rdr.Dispose();
                    conn.Close();

                    return dt;

                }
                else
                {

                    rdr.Dispose();
                    conn.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return new DataTable();


        }

        public DataTable GetDefencePaymentByStcode(string stcode)
        {
            PaymentDTO PaymentDTO = new PaymentDTO();
            SqlCommand cmdpay = new SqlCommand();
            cmdpay.Connection = conn;
            cmdpay.CommandType = CommandType.StoredProcedure;
            cmdpay.CommandText = "request.sp_getDefencePayment";


            cmdpay.Parameters.AddWithValue("@stcode", stcode);

            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr=cmdpay.ExecuteReader();
                dt.Load(dr);
                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        #endregion
    }
}
