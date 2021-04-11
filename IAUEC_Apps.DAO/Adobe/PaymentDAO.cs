using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO;
using IAUEC_Apps.DTO.CommonClasses;
using System.Globalization;

namespace IAUEC_Apps.DAO.CommonDAO
{
   public class PaymentDAO
    {
       SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
     

           #region Create
       public void CreateStudentPayment(PaymentDTO payInfo)
       {
           PaymentDTO PaymentDTO = new PaymentDTO();
           SqlCommand cmdpay = new SqlCommand();
           cmdpay.Connection = conn;
           cmdpay.CommandType = CommandType.StoredProcedure;
           cmdpay.CommandText = "Adobe.SP_InsertPaymentRequest";


           cmdpay.Parameters.AddWithValue("@orderId", payInfo.OrderId);
           cmdpay.Parameters.AddWithValue("@amount", payInfo.Amount);
           cmdpay.Parameters.AddWithValue("@stcode", payInfo.stcode);
           cmdpay.Parameters.AddWithValue("@tterm", payInfo.tterm);
           cmdpay.Parameters.AddWithValue("@RequestKey", payInfo.ReqKey);
           cmdpay.Parameters.AddWithValue("@bankId", payInfo.bankId);
           cmdpay.Parameters.AddWithValue("@appStatus", payInfo.AppStatus);
           cmdpay.Parameters.AddWithValue("@TraceNumber", payInfo.TraceNumber);

           try
           {
               conn.Open();
               cmdpay.ExecuteNonQuery();
               conn.Close();

           }
           catch (Exception)
           {

               throw;
           }

       }
       public void CreateRequestPayment(PaymentRequest payInfo)
       {
           PaymentRequest PaymentDTO = new PaymentRequest();
           SqlCommand cmdpay = new SqlCommand();
           cmdpay.Connection = conn;
           cmdpay.CommandType = CommandType.StoredProcedure;
           cmdpay.CommandText = "Adobe.SP_Insert_RequestPayment";


          
           cmdpay.Parameters.AddWithValue("@RequestID", payInfo.RequestID);
           cmdpay.Parameters.AddWithValue("@OrderID", payInfo.OrderID);

           try
           {
               conn.Open();
               cmdpay.ExecuteNonQuery();
               conn.Close();

           }
           catch (Exception)
           {

               throw;
           }

       }

        public long CreateTuitionPayment(PaymentDTO payInfo)
        {
            PaymentRequest PaymentDTO = new PaymentRequest();
            SqlCommand cmdpay = new SqlCommand();
            cmdpay.Connection = conn;
            cmdpay.CommandType = CommandType.StoredProcedure;
            cmdpay.CommandText = "dbo.webedusys_sabt_meli";



            cmdpay.Parameters.AddWithValue("@stcode", payInfo.stcode);
            cmdpay.Parameters.AddWithValue("@amount", payInfo.Amount);
            cmdpay.Parameters.AddWithValue("@datesabt", payInfo.PayDate);
            cmdpay.Parameters.AddWithValue("@tterm", payInfo.tterm);
            cmdpay.Parameters.AddWithValue("@timesabt", DateTime.Now.ToString("HH:mm"));
            cmdpay.Parameters.AddWithValue("@jary", "");
            cmdpay.Parameters.AddWithValue("@typepay", "0");
            cmdpay.Parameters.AddWithValue("@san", 0);
            cmdpay.Parameters.AddWithValue("@oksabt", -2);
            cmdpay.Parameters.AddWithValue("@type_gate", 1);
            cmdpay.Parameters.AddWithValue("@payment_iau_senaseh", payInfo.OrderId);
            cmdpay.Parameters.AddWithValue("@idsandogh", "0");
            cmdpay.Parameters.AddWithValue("@xml_id_mabpar", "");

            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@id";
            outPutParameter.SqlDbType = SqlDbType.Decimal;
            outPutParameter.Direction = ParameterDirection.Output;
            cmdpay.Parameters.Add(outPutParameter);

            try
            {
                conn.Open();
                cmdpay.ExecuteNonQuery();
                var idOrDefaultValue = outPutParameter.Value as int? ?? default(int);
                conn.Close();
                return idOrDefaultValue;

            }
            catch (Exception)
            {

                throw;
            }

            //
        }
        #endregion


        #region Read
        public bool CheckOrderId(long orderID, int bankID)
       {
           SqlCommand cmdchk = new SqlCommand();
           cmdchk.CommandType = CommandType.StoredProcedure;
           cmdchk.CommandText = "request.SP_CheckOrderIDByBankID";

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
                   return false;
               }
               else
               {
                   conn.Close();
                   return true;
               }

           }
           catch (Exception)
           {

               throw;
           }


       }
        

       //public int SumPrice()
       //{
       //    PaymentDTO payInfo = new PaymentDTO();
       //    SqlCommand cmd = new SqlCommand();
       //    cmd.Connection = conn;
       //    cmd.CommandType = CommandType.StoredProcedure;
       //    cmd.CommandText = "Adobe.SP_GetPaymentInfoByRefID";

        //    conn.Open();
        //    SqlDataReader rdr;
        //    rdr = cmd.ExecuteReader();
        //    DataTable dt = new DataTable();
        //}

        public PaymentDTO GetPaymentByRefID(string RefId)
       {
           try
           {
               PaymentDTO payInfo = new PaymentDTO();
               SqlCommand cmd = new SqlCommand();
               cmd.Connection = conn;
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandText = "Adobe.SP_GetPaymentInfoByRefID";
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
       public DataTable Get_Student_Payment(string stcode)
       {
           SqlCommand cmd = new SqlCommand();
               cmd.Connection = conn;
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandText = "Adobe.SP_Get_StudentPayment";
               cmd.Parameters.AddWithValue("@studentCode", stcode);
               conn.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               DataTable dt = new DataTable();

               if (rdr.HasRows)
               {

                   dt.Load(rdr);
               }
               conn.Close();
               return dt;
       }
       public DataTable GetAllPayment(string FrmPersianDate, string toPersianDate)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = conn;
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.CommandText = "Adobe.SP_GetAllPayment";
           cmd.Parameters.AddWithValue("@FrmPersianDate", FrmPersianDate);
           cmd.Parameters.AddWithValue("@toPersianDate", toPersianDate);
           conn.Open();
           SqlDataReader rdr;
           rdr = cmd.ExecuteReader();
           DataTable dt = new DataTable();

           if (rdr.HasRows)
           {

               dt.Load(rdr);
           }
           conn.Close();
           return dt;
       }
       public int GetSumPayment()
       {
           SqlCommand cmds = new SqlCommand();
           cmds.Connection = conn;
           cmds.CommandType = CommandType.StoredProcedure;
           cmds.CommandText = "Adobe.SP_GetSumPayment";

           conn.Open();
           SqlDataReader rdr;
           rdr = cmds.ExecuteReader();
           DataTable dt = new DataTable();

           if (rdr.HasRows)
           {
               dt.Load(rdr);
           }
           conn.Close();
           return int.Parse(dt.Rows[0][0].ToString());
       }

        public PaymentDTO GetTuitionPaymentByRefId(decimal refId)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "dbo.SP_GetTuitionPaymentByRefId";

            conn.Open();
            SqlDataReader rdr;
            rdr = cmds.ExecuteReader();
            DataTable dt = new DataTable();

            if (rdr.HasRows)
            {
                dt.Load(rdr);
            }
            conn.Close();

            if (dt.Rows.Count == 0)
                return null;
            else
                return new PaymentDTO
                {
                    Amount = Convert.ToInt64(dt.Rows[0]["amount"]),
                    OrderId = Convert.ToInt64(refId),
                    tterm = dt.Rows[0]["tterm"].ToString(),
                    stcode = dt.Rows[0]["stcode"].ToString(),
                    ReqKey = dt.Rows[0]["requestKey"].ToString(),
                    PayDate = dt.Rows[0]["datesabt"].ToString(),
                };
        }
           #endregion


           #region Update

       public void UpdateMellatPayment(PaymentDTO payment)
       {
           try
           {

               SqlCommand cmd1 = new SqlCommand();
               cmd1.Connection = conn;
               cmd1.CommandType = CommandType.StoredProcedure;
               cmd1.CommandText = "Adobe.SP_UpdateMellatPayment";

               cmd1.Parameters.AddWithValue("@OrderID", payment.OrderId);
               cmd1.Parameters.AddWithValue("@RefID", payment.ReqKey);
               cmd1.Parameters.AddWithValue("@Status", payment.AppStatus);
               cmd1.Parameters.AddWithValue("@SaleRef", payment.TraceNumber);
               cmd1.Parameters.AddWithValue("@ResCode", payment.Result);

               conn.Open();
               cmd1.ExecuteNonQuery();
               conn.Close();


           }
           catch (Exception)
           {

               throw;
           }

       }

       public void UpdateMellatPaymentStatus(PaymentDTO payment)
       {
           try
           {

               SqlCommand cmd1 = new SqlCommand();
               cmd1.Connection = conn;
               cmd1.CommandType = CommandType.StoredProcedure;
               cmd1.CommandText = "Adobe.SP_UpdateMellatPaymentStatus";

               cmd1.Parameters.AddWithValue("@OrderID", payment.OrderId);
               //cmd1.Parameters.AddWithValue("@RefID", payment.ReqKey);
               //cmd1.Parameters.AddWithValue("@Status", payment.AppStatus);
               //cmd1.Parameters.AddWithValue("@SaleRef", payment.TraceNumber);
               //cmd1.Parameters.AddWithValue("@ResCode", payment.Result);

               conn.Open();
               cmd1.ExecuteNonQuery();
               conn.Close();


           }
           catch (Exception)
           {

               throw;
           }

       }

        public void UpdateTuitionPayment(PaymentDTO pay)
        {
            PersianCalendar pc = new PersianCalendar();
            var persianDate = pc.GetYear(DateTime.Now).ToString() + '/' + pc.GetMonth(DateTime.Now).ToString() + '/' + pc.GetDayOfMonth(DateTime.Now).ToString();
            try
            {

                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = conn;
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "dbo.webmeli_insert_mali_meli";

                cmd1.Parameters.AddWithValue("@idorder", pay.OrderId);
                cmd1.Parameters.AddWithValue("@stcode", pay.stcode);
                cmd1.Parameters.AddWithValue("@nopaygeri", pay.TraceNumber.ToString() + "IAUEC");
                cmd1.Parameters.AddWithValue("@oksabt", 0);
                cmd1.Parameters.AddWithValue("@datesabt", pay.PayDate);
                cmd1.Parameters.AddWithValue("@resultdate", persianDate);
                cmd1.Parameters.AddWithValue("@resulttime", DateTime.Now.ToString("HH:mm"));
                cmd1.Parameters.AddWithValue("@amount", pay.Amount);
                cmd1.Parameters.AddWithValue("@termjary", pay.tterm);

                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception)
            {

                throw;
            }
        }

           #endregion


           #region Delete
           #endregion
    }
}
