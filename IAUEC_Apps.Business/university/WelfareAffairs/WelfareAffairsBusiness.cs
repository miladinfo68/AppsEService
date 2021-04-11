using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DAO.University.WelfareAffairs;
using IAUEC_Apps.DTO.University.WelfareAffairs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.university.WelfareAffairs
{
    public class WelfareAffairsBusiness
    {
        WelfareAffairsDAO DAO = new WelfareAffairsDAO();
        CommonBusiness commonBuiss = new CommonBusiness();

        public DataTable GetAllLoanByStCode(string stCode)
        {
            return DAO.GetAllLoanByStCode(stCode);
        }

        public List<LoanInfo> GetStudentLoans(string stcode = null, string term = null, int loanId = 0)
        {
            return DAO.GetStudentLoans(stcode, term, loanId);
        }
        public List<PaymentRecord> ListPaymentHistoryByStcode(string stcode = null)
        {
            return DAO.ListPaymentHistoryByStcode(stcode);
        }

        public void AddOrUpdateLoansDocs(List<LoanDocInfoDTO> dataList, string stcode)
        {
            DAO.AddOrUpdateLoansDocs(dataList, stcode, commonBuiss.GetIPAddress());
        }


        public int AddOrUpdateLoan(LoanInfo loan)
        {
            return DAO.AddOrUpdateLoan(loan, commonBuiss.GetIPAddress());
        }

        public void DeleteLoan(string stcode, string term)
        {
            DAO.DeleteLoan(stcode, term);
        }
        public List<DigitParvande> GetNationalCardAndID(string stCode)
        {
            return DAO.GetNationalCardAndID(stCode);
        }
        public void SetDocumentStatus(int docId, int status, string description = null)
        {
            DAO.SetDocumentStatus(docId, status, description);
        }

        public int HasConditional_Loan(string stCode, string term)
        {
            return DAO.HasConditional_Loan(stCode, term);
        }
 


    }
}
