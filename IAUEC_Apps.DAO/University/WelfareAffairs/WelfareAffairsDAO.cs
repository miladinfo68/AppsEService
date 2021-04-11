
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.University.WelfareAffairs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using IAUEC_Apps.DTO.University.WelfareAffairs;

namespace IAUEC_Apps.DAO.University.WelfareAffairs
{
    public class WelfareAffairsDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        public DataTable GetAllLoanByStCode(string stCode)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[dbo].[sp_GetAllLoanByStCode]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@stcode", stCode);

            var dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception x) { throw x; }
            return dt;
        }

        public List<LoanInfo> GetStudentLoans(string stcode = null, string term = null, int loanId = 0)
        {
            List<LoanInfo> res;
            var dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.SP_GetStudentLoans";
            cmd.CommandType = CommandType.StoredProcedure;
            if ((!string.IsNullOrEmpty(stcode) && !string.IsNullOrEmpty(term)) || loanId > 0 )
            {
                cmd.Parameters.AddWithValue("@stcode", stcode);
                cmd.Parameters.AddWithValue("@term", term);
                cmd.Parameters.AddWithValue("@loanId", loanId);
            }
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception x) { throw x; }

            var docs = dt.AsEnumerable().Select(s => new LoanDocInfoDTO
            {
                DocId = s.Field<int?>("DocId"),
                LoanId = s.Field<int?>("LoanId"),
                DocName = s.Field<string>("DocName"),
                DocTitle = s.Field<string>("DocTitle"),
                DocPath = s.Field<string>("DocPath"),
                DocType = s.Field<byte?>("DocType"),
                DocStatus = s.Field<byte?>("DocStatus"),
                Description = s.Field<string>("Description")
            }).ToList();


            var loans = dt.AsEnumerable().Select(s => new LoanInfo
            {
                LoanId = s.Field<int>("LoanId"),
                LoanType = s.Field<byte>("LoanType"),
                Status = s.Field<byte>("Status"),
                StudentCode = s.Field<string>("StudentCode"),
                Term = s.Field<string>("Term"),
                ReqRegisterDate = s.Field<DateTime>("ReqRegisterDate"),
                StudentFirstname = s.Field<string>("name"),
                StudentLastname = s.Field<string>("family"),
                LoanTypeTitle = GetLoanTypeTitle(s.Field<byte>("LoanType")),
                Message = s.Field<string>("Message"),
            }).GroupBy(g => new { g.LoanId, g.StudentCode, g.LoanType, g.Term, g.Status, g.ReqRegisterDate, g.StudentFirstname, g.StudentLastname, g.LoanTypeTitle, g.Message }).ToList();

            res = loans.Select(s => new LoanInfo()
            {
                LoanId = s.Key.LoanId,
                LoanType = s.Key.LoanType,
                StudentCode = s.Key.StudentCode,
                Term = s.Key.Term,
                Message = s.Key.Message,
                ReqRegisterDate = s.Key.ReqRegisterDate,
                Status = s.Key.Status,
                StudentFirstname = s.Key.StudentFirstname,
                StudentLastname = s.Key.StudentLastname,
                LoanTypeTitle = s.Key.LoanTypeTitle,
                LoanDocs = docs == null ? null : docs.Where(w => w.LoanId == s.Key.LoanId).ToList()
            }).ToList();

            return res;
        }

        public List<PaymentRecord> ListPaymentHistoryByStcode(string stcode = null)
        {
            var res = new List<PaymentRecord>();
            if (!string.IsNullOrEmpty(stcode))
            {
                var dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "dbo.SP_ListPaymentHistoryByStcode";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stcode", stcode);
                try
                {
                    conn.Open();
                    SqlDataReader rdr;
                    rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                    conn.Close();
                }
                catch (Exception x) { throw x; }
                res = dt.AsEnumerable().Select(s => new PaymentRecord
                {
                    Debt = s.Field<int>("Debt"),
                    DebtAmount = s.Field<int>("DebtAmount"),
                    Deposit = s.Field<int>("Deposit"),
                    Status = s.Field<string>("Status"),
                    Term = s.Field<string>("tterm"),
                }).ToList();
            }
            return res;
        }

        public int AddOrUpdateLoan(LoanInfo loan, string ipAddress)
        {
            int newInsertedLoanID = 0;
            var dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.sp_AddOrUpdateStudentLoan";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@loanId", loan.LoanId);
            cmd.Parameters.AddWithValue("@stcode", loan.StudentCode);
            cmd.Parameters.AddWithValue("@loanType", loan.LoanType);
            cmd.Parameters.AddWithValue("@term", loan.Term);
            cmd.Parameters.AddWithValue("@status", loan.Status);
            cmd.Parameters.AddWithValue("@reqRegisterDate", loan.ReqRegisterDate);
            cmd.Parameters.AddWithValue("@message", loan.Message);
            cmd.Parameters.AddWithValue("@ipAddress", ipAddress);
            //cmd.Parameters.AddWithValue("@newID", SqlDbType.Int).Direction=ParameterDirection.Output;
            try
            {
                conn.Open();
                //SqlDataReader rd= cmd.ExecuteReader();
                //dt.Load(rd);
                //newInsertedLoanID =(int)cmd.Parameters["@newID"].Value;
                newInsertedLoanID = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            catch (Exception x) { throw x; }
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    newInsertedLoanID = Convert.ToInt32(dt.Rows[0][0]);
            //}
            return newInsertedLoanID;
        }

        public void DeleteLoan(string stcode, string term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.sp_DeleteStudentLoan";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@term", term);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception x) { throw x; }
        }

        public void AddOrUpdateLoansDocs(List<LoanDocInfoDTO> dataList, string stcode, string ipAddress)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.sp_AddOrUpdateLoansDocs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            var prms = new DataTable();

            try
            {
                //columns header names
                prms.Columns.Add("DocId", typeof(int));
                prms.Columns.Add("LoanId", typeof(int));
                prms.Columns.Add("DocPath", typeof(string));
                prms.Columns.Add("DocName", typeof(string));
                prms.Columns.Add("DocTitle", typeof(string));
                prms.Columns.Add("DocType", typeof(byte));
                prms.Columns.Add("DocStatus", typeof(byte));
                prms.Columns.Add("Description", typeof(string));

                foreach (var item in dataList)
                {
                    var dr = prms.NewRow();
                    dr["DocId"] = !string.IsNullOrWhiteSpace(item.DocId?.ToString()) ? (object)item.DocId : (object)DBNull.Value;
                    dr["LoanId"] = item.LoanId;
                    dr["DocPath"] = item.DocPath;
                    dr["DocName"] = item.DocName;
                    dr["DocTitle"] = item.DocTitle;
                    dr["DocType"] = item.DocType;
                    dr["DocStatus"] = item.DocStatus;
                    dr["Description"] = item.Description;
                    prms.Rows.Add(dr);
                }
                cmd.Parameters.Add("LoanInfo", SqlDbType.Structured).Value = prms;

                cmd.Parameters.AddWithValue("@stcode", stcode);
                cmd.Parameters.AddWithValue("@ipAddress", ipAddress);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public List<DigitParvande> GetNationalCardAndID(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[dbo].[sp_GetNatinalCardAndID]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@stcode", stCode);

            var dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception x) { throw x; }

            var res = dt.AsEnumerable().Select(s => new DigitParvande
            {
                StCode = s.Field<string>("stcode"),
                Ext = s.Field<string>("ext"),
                Pic = s.Field<byte[]>("pic"),
                Cat = s.Field<decimal>("cat"),
                DateSend = s.Field<string>("date_send"),
                TimeSend = s.Field<string>("time_send"),
                Base64Image = s.Field<byte[]>("pic") == null ? null : Convert.ToBase64String(s.Field<byte[]>("pic"))
            }).ToList();
            res.ForEach((item) =>
            {
                switch ((int)item.Cat)
                {
                    case 100:
                        item.CategoryName = "تصویر شناسنامه";
                        break;
                    case 101:
                        item.CategoryName = "تصویر کارت ملی";
                        break;
                }
            });
            return res;
        }

        public void SetDocumentStatus(int docId, int status, string description = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "dbo.SP_SetLoanDocumentStatus";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DocId", docId);
                cmd.Parameters.AddWithValue("@DocStatus", status);
                cmd.Parameters.AddWithValue("@Descripttion", description);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int HasConditional_Loan(string stCode, string term)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.sp_CheckConditions_Loan";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@stcode", stCode);
            cmd.Parameters.AddWithValue("@term", term);
            cmd.Parameters.AddWithValue("@result",SqlDbType.SmallInt).Direction=ParameterDirection.ReturnValue;
            var dt = new DataTable();
            int res = 0;
            try
            {
                conn.Open();
                //SqlDataReader rdr;
                //rdr = cmd.ExecuteReader();
                //dt.Load(rdr);

                 cmd.ExecuteNonQuery();
                res=(int)cmd.Parameters["@result"].Value;
                conn.Close();
            }
            catch (Exception x) { throw x; }
            //var res = (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().ToLower() == "true") ? true : false;
            return res;
        }


        #region Helper
        private string GetLoanTypeTitle(byte loanType)
        {
            switch (loanType)
            {
                case 1:
                    return "کوتاه مدت";
                case 2:
                    return "میان مدت";
                case 3:
                    return "بلند مدت";
                case 4:
                    return "مهر اقتصاد";
            }
            return null;
        }
        #endregion
    }
}
