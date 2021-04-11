using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.WelfareAffairs;
using IAUEC_Apps.DTO.University.WelfareAffairs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.WelfareAffairs.Pages
{
    public partial class ShowDocs_StudentLoan : System.Web.UI.Page
    {
        WelfareAffairsBusiness wb = new WelfareAffairsBusiness();
        CommonBusiness CB = new CommonBusiness();
        PersianCalendar PC = new PersianCalendar();
        protected void Page_Load(object sender, EventArgs e)
        {
            var loanID = Request.QueryString["loanID"];
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(loanID))
                {
                    var req = wb.GetStudentLoans(loanId: int.Parse(loanID)).FirstOrDefault();
                    lblFullName.Text = req.StudentFirstname + ' ' + req.StudentLastname;
                    lblLoanType.Text = req.LoanTypeTitle;
                    lblRequestDate.Text = PC.GetYear(req.ReqRegisterDate) + "/" + PC.GetMonth(req.ReqRegisterDate) + "/" + PC.GetDayOfMonth(req.ReqRegisterDate);
                    lblStCode.Text = req.StudentCode;
                    BindDocs(loanID);
                }
            }
        }

        private void BindDocs(string loanID)
        {
            var loan = wb.GetStudentLoans(loanId: int.Parse(loanID)).FirstOrDefault();
            rptDocs.DataSource = loan.LoanDocs;//.Where(w=> w.DocStatus == 1)
            rptDocs.DataBind();

            rptArchiveDocs.DataSource = wb.GetNationalCardAndID(loan.StudentCode);
            rptArchiveDocs.DataBind();
        }

        protected void rptDocs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var dataItem = (LoanDocInfoDTO)e.Item.DataItem;
            ((Label)e.Item.FindControl("pnlDocInfo").FindControl("lblDocStatus")).Text = GetDocStatusTitle(dataItem.DocStatus.Value);            
        }
        private string GetDocStatusTitle(byte? docStatus)
        {
            switch (docStatus)
            {
                case 1:
                    return "درحال بررسی";
                case 2:
                    return "تائید اولیه شده";
                case 3:
                    return "رد شده";
                case 4:
                    return "تائید  نهایی شده";
            }
            return string.Empty;
        }


    }
}