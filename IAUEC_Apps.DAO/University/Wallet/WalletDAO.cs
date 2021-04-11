using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.University.Wallet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.DAO.University.Wallet
{
    public class WalletDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        public List<PaymentDTO> SelectPayment(PaymentDTO model)
        {
            var res = new List<PaymentDTO>();
            var condition = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            if (!string.IsNullOrEmpty(model.stcode))
            {
                condition += " AND stcode = @stcode ";
                cmd.Parameters.AddWithValue("@stcode", CleanString(model.stcode));
            }
            if (model.Id > 0)
            {
                condition += " AND Id = @Id ";
                cmd.Parameters.AddWithValue("@Id", model.Id);
            }
            if (model.OrderId > 0)
            {
                condition += " AND OrderId = @OrderId ";
                cmd.Parameters.AddWithValue("@OrderId", model.OrderId);
            }
            if (model.BankId > 0)
            {
                condition += " AND BankId = @BankId ";
                cmd.Parameters.AddWithValue("@BankId", model.BankId);
            }
            if (!string.IsNullOrEmpty(model.RequestKey))
            {
                condition += " AND RequestKey = @RequestKey ";
                cmd.Parameters.AddWithValue("@RequestKey", model.RequestKey);
            }
            if (model.TraceNumber != null && model.TraceNumber > 0)
            {
                condition += " AND TraceNumber = @TraceNumber ";
                cmd.Parameters.AddWithValue("@TraceNumber", model.TraceNumber);
            }

            cmd.CommandText = "SELECT * FROM [Wallet].[Payment] WHERE 1=1 " + condition;
            var dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception x)
            {
            }

            foreach (DataRow row in dt.Rows)
                res.Add(new PaymentDTO
                {
                    Amount = Convert.ToDecimal(row["Amount"]),
                    BankId = Convert.ToDecimal(row["BankId"]),
                    CreateDate = Convert.ToDateTime(row["CreateDate"]),
                    Id = Convert.ToDecimal(row["Id"]),
                    OrderId = Convert.ToDecimal(row["OrderId"]),
                    PayType = Convert.ToInt32(row["PayType"]),
                    RequestKey = row["RequestKey"].ToString(),
                    RetrivalRefNo = row["RetrivalRefNo"].ToString(),
                    Result = row["Result"] != null ? Convert.ToDecimal(row["Result"]) : (decimal?)null,
                    Status = Convert.ToInt32(row["Status"]),
                    stcode = row["stcode"].ToString(),
                    TraceNumber = row["TraceNumber"] != DBNull.Value ? Convert.ToDecimal(row["TraceNumber"]) : (decimal?)null,
                });

            return res;
        }
        public List<TransactionDTO> SelectTransaction(TransactionDTO model)
        {
            var res = new List<TransactionDTO>();
            var condition = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            if (!string.IsNullOrEmpty(model.stcode))
            {
                condition += " AND stcode = @stcode ";
                cmd.Parameters.AddWithValue("@stcode", CleanString(model.stcode));
            }
            if (model.TransactionTypeId > 0)
            {
                condition += " AND TransactionTypeId = @TransactionTypeId ";
                cmd.Parameters.AddWithValue("@TransactionTypeId", model.TransactionTypeId);
            }
            if (model.Id > 0)
            {
                condition += " AND Id = @Id ";
                cmd.Parameters.AddWithValue("@Id", model.Id);
            }

            cmd.CommandText = "SELECT * FROM [Wallet].[Transactions] WHERE 1=1 " + condition;
            var dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception x)
            {
            }

            foreach (DataRow row in dt.Rows)
                res.Add(new TransactionDTO
                {
                    Amount = Convert.ToDecimal(row["Amount"]),
                    CreateDate = Convert.ToDateTime(row["CreateDate"]),
                    Id = Convert.ToDecimal(row["Id"]),
                    stcode = row["stcode"].ToString(),
                    CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]),
                    TransactionTypeId = Convert.ToDecimal(row["TransactionTypeId"]),
                    Description = row["Description"].ToString(),
                });

            return res;
        }

        public decimal InsertPayment(PaymentDTO model)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wallet.sp_insertPayment";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Amount", model.Amount);
            cmd.Parameters.AddWithValue("@BankId", model.BankId);
            cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@OrderId", model.OrderId);
            cmd.Parameters.AddWithValue("@PayType", model.PayType);
            cmd.Parameters.AddWithValue("@RequestKey", CleanString(model.RequestKey));
            cmd.Parameters.AddWithValue("@Result", model.Result);
            cmd.Parameters.AddWithValue("@RetrivalRefNo", CleanString(model.RetrivalRefNo));
            cmd.Parameters.AddWithValue("@Status", model.Status);
            cmd.Parameters.AddWithValue("@stcode", CleanString(model.stcode));
            cmd.Parameters.AddWithValue("@TraceNumber", model.TraceNumber ?? (object)DBNull.Value);
            var dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception x)
            {
                return 0;
            }
            if (dt.Rows.Count > 0)
                return Convert.ToDecimal(dt.Rows[0][0]);
            else
                return 0;
        }
        public decimal InsertTransaction(TransactionDTO model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "wallet.sp_insertTransaction";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Amount", model.Amount);
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@stcode", CleanString(model.stcode));
                cmd.Parameters.AddWithValue("@CurrentBalance", model.CurrentBalance);
                cmd.Parameters.AddWithValue("@Description", (object)CleanString(model.Description)?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TransactionTypeId", model.TransactionTypeId);
                var dt = new DataTable();
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
                if (dt.Rows.Count > 0)
                    return Convert.ToDecimal(dt.Rows[0][0]);
                else
                    return 0;
            }
            catch (Exception x)
            {
                return 0;
            }
        }

        public bool UpdatePayment(PaymentDTO model)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wallet.sp_updatePayment";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", model.Id);
            cmd.Parameters.AddWithValue("@Amount", model.Amount);
            cmd.Parameters.AddWithValue("@BankId", model.BankId);
            cmd.Parameters.AddWithValue("@OrderId", model.OrderId);
            cmd.Parameters.AddWithValue("@PayType", model.PayType);
            cmd.Parameters.AddWithValue("@RequestKey", CleanString(model.RequestKey));
            cmd.Parameters.AddWithValue("@Result", model.Result);
            cmd.Parameters.AddWithValue("@RetrivalRefNo", CleanString(model.RetrivalRefNo));
            cmd.Parameters.AddWithValue("@Status", model.Status);
            cmd.Parameters.AddWithValue("@stcode", CleanString(model.stcode));
            cmd.Parameters.AddWithValue("@TraceNumber", model.TraceNumber);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateTransaction(TransactionDTO model)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wallet.sp_updateTransaction";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", model.Id);
            cmd.Parameters.AddWithValue("@Amount", model.Amount);
            cmd.Parameters.AddWithValue("@stcode", CleanString(model.stcode));
            cmd.Parameters.AddWithValue("@TransactionTypeId", model.TransactionTypeId);
            cmd.Parameters.AddWithValue("@CurrentBalance", model.CurrentBalance);
            cmd.Parameters.AddWithValue("@Description", CleanString(model.Description));
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePayment(decimal id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM [Wallet].[Payment] WHERE Id = @Id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteTransaction(decimal id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM [Wallet].[Transactions] WHERE Id = @Id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string CleanString(string input = null)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            return Regex.Replace(input, @"[^0-9a-zA-Z]+", "");
        }
    
        public DataTable getDebitTypes()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wallet.sp_getDebitTypes";
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
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
        public DataTable getStudentWalletInformation(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "wallet.sp_getStudentWalletInformation";
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
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }


    }
}
