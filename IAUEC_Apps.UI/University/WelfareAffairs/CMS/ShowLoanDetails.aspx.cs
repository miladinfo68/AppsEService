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

namespace IAUEC_Apps.UI.University.WelfareAffairs.CMS
{
    public partial class ShowLoanDetails : System.Web.UI.Page
    {
        WelfareAffairsBusiness wb = new WelfareAffairsBusiness();
        CommonBusiness CB = new CommonBusiness();
        PersianCalendar PC = new PersianCalendar();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["lid"]))
                {
                    var loanId = Convert.ToInt32(Request.QueryString["lid"]);
                    var req = wb.GetStudentLoans(loanId: loanId).FirstOrDefault();
                    lblFullName.Text = req.StudentFirstname + ' ' + req.StudentLastname;
                    lblLoanType.Text = req.LoanTypeTitle;
                    lblRequestDate.Text = PC.GetYear(req.ReqRegisterDate) + "/" + PC.GetMonth(req.ReqRegisterDate) + "/" + PC.GetDayOfMonth(req.ReqRegisterDate);
                    lblStCode.Text = req.StudentCode;
                    BindDocs();
                    BindPaymentGrid(req.StudentCode);
                }
            }
        }

        private void BindPaymentGrid(string stcode = null)
        {
            var payments = wb.ListPaymentHistoryByStcode(stcode);
            grvPayments.DataSource = payments;
            grvPayments.DataBind();
            lblTotalDebt.Text = string.Format("{0:n0}", Convert.ToInt32(payments.Sum(s => s.DebtAmount))) + "ريال";
            if (payments.Sum(s => s.DebtAmount) > 0)
            {
                pnlTotal.CssClass = "bg-danger";
                lblTotalStatus.Text = "بدهکار";
            }
            else
            {
                pnlTotal.CssClass = "bg-success";
                lblTotalStatus.Text = "تسویه حساب";
            }
        }

        protected void rptDocs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "Accept":
                    var docId = Convert.ToInt32(e.CommandArgument);
                    wb.SetDocumentStatus(docId, 2);
                    CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 14, 192, string.Empty, docId);
                    BindDocs();
                    break;

                case "Reject":
                    var btnReject = (Button)e.CommandSource;
                    var pnlDocButtons = (Panel)btnReject.Parent;
                    var pnlRejectDoc = pnlDocButtons.Parent.FindControl("pnlRejectDoc");
                    if (pnlRejectDoc != null)
                    {
                        pnlDocButtons.Visible = false;
                        pnlRejectDoc.Visible = true;
                    }
                    break;

                case "CancelReject":
                    var btnCancelReject = (Button)e.CommandSource;
                    var pnlRejectDocCancel = (Panel)btnCancelReject.Parent;
                    var pnlDocButtonsCancel = pnlRejectDocCancel.Parent.FindControl("pnlDocButtons");
                    if (pnlDocButtonsCancel != null)
                    {
                        ((TextBox)pnlRejectDocCancel.FindControl("txtDec")).Text = string.Empty;
                        pnlRejectDocCancel.Visible = false;
                        pnlDocButtonsCancel.Visible = true;
                    }
                    break;

                case "RejectDoc":
                    var docIdReject = Convert.ToInt32(e.CommandArgument);
                    var btnRejectDoc = (Button)e.CommandSource;
                    var pnlRejectDocRD = (Panel)btnRejectDoc.Parent;
                    var pnlDocButtonsRD = pnlRejectDocRD.Parent.FindControl("pnlDocButtons");
                    if (pnlDocButtonsRD != null)
                    {
                        pnlRejectDocRD.Visible = false;
                        pnlDocButtonsRD.Visible = true;
                        wb.SetDocumentStatus(docIdReject, 3, ((TextBox)pnlRejectDocRD.FindControl("txtDec")).Text);
                        CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 14, 193, ((TextBox)pnlRejectDocRD.FindControl("txtDec")).Text, docIdReject);
                        BindDocs();
                    }                    
                    //############# Send  SMS //#############
                    bool sentSMS = false;
                    string smsStatusText;
                    var lblDoc_Title2 = (pnlRejectDocRD.Parent.FindControl("lblDoc_Title") as Label).Text;                 
                    var msg = $"دانشجوی گرامی مدرک [{lblDoc_Title2 }] مورد تایید واقع نشد لطفا در اسرع وقت با مراجعه به سامانه خدمات نسبت به اصلاح مدرک نامبرده اقدام نمایید ";
                    var result2 = CB.sendSMS(1, Session[sessionNames.userID_Karbar].ToString(), msg, out sentSMS, out smsStatusText);

                    break;
            }
        }

        private void BindDocs()
        {
            var loanId = Convert.ToInt32(Request.QueryString["lid"]);
            var loan = wb.GetStudentLoans(loanId: loanId).FirstOrDefault();
            rptDocs.DataSource = loan.LoanDocs;//.Where(w=> w.DocStatus == 1)
            rptDocs.DataBind();
            rptArchiveDocs.DataSource = wb.GetNationalCardAndID(loan.StudentCode);
            rptArchiveDocs.DataBind();
        }

        protected void rptDocs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var dataItem = (LoanDocInfoDTO)e.Item.DataItem;
            ((Label)e.Item.FindControl("pnlDocInfo").FindControl("lblDocStatus")).Text = GetDocStatusTitle(dataItem.DocStatus.Value);
            if (dataItem.DocStatus == 1 && string.IsNullOrEmpty(Request.QueryString["ro"]))
            {
                e.Item.FindControl("pnlDocButtons").Visible = true;
                e.Item.FindControl("pnlRejectDoc").Visible = false;
                e.Item.FindControl("pnlDocInfo").Visible = false;
            }
            else
            {
                e.Item.FindControl("pnlDocButtons").Visible = false;
                e.Item.FindControl("pnlRejectDoc").Visible = false;
                e.Item.FindControl("pnlDocInfo").Visible = true;
                if (!string.IsNullOrEmpty(dataItem.Description))
                    ((Label)e.Item.FindControl("pnlDocInfo").FindControl("lblDesc")).Text = "توضیحات: " + dataItem.Description;
            }
        }
        private string GetDocStatusTitle(byte? docStatus)
        {
            switch (docStatus)
            {
                case 1:
                    return "درحال بررسی";
                case 2:
                    return "تائید شده";
                case 3:
                    return "رد شده";
            }
            return string.Empty;
        }

        protected void grvPayments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.TableSection = TableRowSection.TableHeader;

            else if (e.Row.RowType == DataControlRowType.DataRow)
                if (((PaymentRecord)e.Row.DataItem).DebtAmount > 0)
                    e.Row.CssClass = "bg-danger";
        }


        string GetDocTitle()
        {
            var title = "";
            foreach (RepeaterItem item in rptDocs.Items)
            {
                //if (item.ID == "lblDoc_Title")
                //{
                title = (item.FindControl("lblDoc_Title") as Label).Text;
                //break;
                //}
            }
            return title;
        }

    }
}