using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.WelfareAffairs;
using IAUEC_Apps.DAO.University.WelfareAffairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.WelfareAffairs.CMS
{
    public partial class ManageStudentLoanRequests : System.Web.UI.Page
    {
        WelfareAffairsBusiness wb = new WelfareAffairsBusiness();
        CommonBusiness CB = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            string mId = generaterandomstr() + "@A4187-" + generaterandomstr();//Request.QueryString["id"].ToString();
            string[] id = mId.ToString().Split(new char[] { '@' });
            string menuId = "";
            for (int i = 0; i < id[1].Length; i++)
            {
                string s = id[1].Substring(i + 1, 1);
                if (s != "-")
                    menuId += s;
                else
                    break;
            }
            Session[sessionNames.menuID] = menuId;
            AccessControl.MenuId = menuId;
            AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
        }
        public string generaterandomstr()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        protected void rgvLoanRequests_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgvLoanRequests.DataSource = wb.GetStudentLoans().Where(w => (w.Status == 1 || w.Status == 2));
        }

        protected void rgvLoanRequests_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "ViewDetails":
                    RadWindowManager rwmViewDetails = new RadWindowManager();
                    RadWindow viewDetailsWindow = new RadWindow();
                    viewDetailsWindow.NavigateUrl = "../CMS/ShowLoanDetails.aspx?lid=" + e.CommandArgument.ToString();
                    viewDetailsWindow.ReloadOnShow = true;
                    viewDetailsWindow.ID = "RadWindow2";
                    viewDetailsWindow.DestroyOnClose = true;
                    viewDetailsWindow.ShowContentDuringLoad = false;
                    viewDetailsWindow.ReloadOnShow = true;
                    viewDetailsWindow.OnClientClose = "Rebind";
                    rwmViewDetails.Width = Unit.Pixel(1100);
                    rwmViewDetails.Height = Unit.Pixel(500);
                    viewDetailsWindow.VisibleOnPageLoad = true;
                    rwmViewDetails.Windows.Add(viewDetailsWindow);
                    ContentPlaceHolder ViewDetailsContentPlaceHolder;
                    ViewDetailsContentPlaceHolder =
                     (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                    ViewDetailsContentPlaceHolder.Controls.Add(viewDetailsWindow);
                    rwmViewDetails.Windows.Clear();
                    break;
                case "Accept":
                    var loanInfo = wb.GetStudentLoans(loanId: Convert.ToInt32(e.CommandArgument)).FirstOrDefault();
                    if (!loanInfo.LoanDocs.Any(a => a.DocStatus != 2))
                    {
                        loanInfo.Status = 2;
                        wb.AddOrUpdateLoan(loanInfo);
                        CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 14, 190, string.Empty, loanInfo.LoanId);
                        rgvLoanRequests.Rebind();
                    }
                    else
                        rwmMessages.RadAlert("لطفا ابتدا مدارک درخواست را تائید نمائید.", 300, 100, "", null);
                    break;
                case "Reject":
                    var btnReject = (Button)e.CommandSource;
                    var pnlRequestButtons = (Panel)btnReject.Parent;
                    var pnlConfirmAction = pnlRequestButtons.Parent.FindControl("pnlConfirmAction");
                    if (pnlConfirmAction != null)
                    {
                        pnlRequestButtons.Visible = false;
                        pnlConfirmAction.Visible = true;
                    }
                    break;
                case "CancelAction":
                    var btnCancelAction = (Button)e.CommandSource;
                    var pnlConfirmActionCancel = (Panel)btnCancelAction.Parent;
                    var pnlRequestButtonsCancel = pnlConfirmActionCancel.Parent.FindControl("pnlRequestButtons");
                    if (pnlRequestButtonsCancel != null)
                    {
                        ((TextBox)pnlConfirmActionCancel.FindControl("txtMessage")).Text = string.Empty;
                        pnlConfirmActionCancel.Visible = false;
                        pnlRequestButtonsCancel.Visible = true;
                    }
                    break;
                case "RejectRequest":
                    var btnRejectRequest = (Button)e.CommandSource;
                    var pnlConfirmActionRR = (Panel)btnRejectRequest.Parent;
                    var loanInfoReject = wb.GetStudentLoans(loanId: Convert.ToInt32(e.CommandArgument)).FirstOrDefault();
                    loanInfoReject.Status = 3;
                    loanInfoReject.Message = ((TextBox)pnlConfirmActionRR.FindControl("txtMessage")).Text;
                    wb.AddOrUpdateLoan(loanInfoReject);
                    CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 14, 191, ((TextBox)pnlConfirmActionRR.FindControl("txtMessage")).Text, loanInfoReject.LoanId);
                    rgvLoanRequests.Rebind();

                    //############# Send  SMS //#############
                    bool sentSMS = false;
                    string smsStatusText;                    
                    var msg1 = "دانشجوی گرامی درخواست وام شما رد گردید";
                    var result = CB.sendSMS(1, Session[sessionNames.userID_Karbar].ToString(), msg1, out sentSMS, out smsStatusText);


                    break;
                case "FinalAccept":
                    var loan = wb.GetStudentLoans(loanId: Convert.ToInt32(e.CommandArgument)).FirstOrDefault();
                    loan.Status = 4;
                    wb.AddOrUpdateLoan(loan);
                    CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 14, 208, string.Empty, loan.LoanId);
                    rgvLoanRequests.Rebind();
                    break;
            }
        }
        protected void RebindeGrid()
        {
            rgvLoanRequests.Rebind();
        }


        protected void rgvLoanRequests_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var btnAccept = (Button)item.FindControl("btnAccept");
                var btnFinalAccept = (Button)item.FindControl("btnFinalAccept");
                if(btnFinalAccept != null && btnAccept != null)
                {
                    if(((IAUEC_Apps.DTO.University.WelfareAffairs.LoanInfo)item.DataItem).Status == 1)
                    {
                        btnAccept.Visible = true;
                        btnFinalAccept.Visible = false;
                    }
                    else if (((IAUEC_Apps.DTO.University.WelfareAffairs.LoanInfo)item.DataItem).Status == 2)
                    {
                        btnAccept.Visible = false;
                        btnFinalAccept.Visible = true;
                    }
                }
            }
        }
    }
}