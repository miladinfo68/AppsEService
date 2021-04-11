using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.WelfareAffairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.WelfareAffairs.CMS
{
    public partial class StudentLoanRequestsReport : System.Web.UI.Page
    {
        WelfareAffairsBusiness wb = new WelfareAffairsBusiness();
        CommonBusiness cb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //-- Fill Term DropDownList
                ddlTerm.DataSource = cb.SelectAllTerm();
                ddlTerm.DataTextField = "tterm";
                ddlTerm.DataValueField = "tterm";
                ddlTerm.DataBind();
                ddlTerm.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
                //-------------------------
            }
        }

        protected void rgvLoanRequests_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgvLoanRequests.DataSource = wb.GetStudentLoans();
        }

        protected void rgvLoanRequests_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "ViewDetails":
                    RadWindowManager rwmViewDetails = new RadWindowManager();
                    RadWindow viewDetailsWindow = new RadWindow();
                    viewDetailsWindow.NavigateUrl = "../CMS/ShowLoanDetails.aspx?ro=1&lid=" + e.CommandArgument.ToString();
                    viewDetailsWindow.ReloadOnShow = true;
                    viewDetailsWindow.ID = "RadWindow2";
                    viewDetailsWindow.DestroyOnClose = true;
                    viewDetailsWindow.ShowContentDuringLoad = false;
                    viewDetailsWindow.ReloadOnShow = true;
                    viewDetailsWindow.OnClientClose = "Rebind";
                    rwmViewDetails.Width = Unit.Pixel(900);
                    rwmViewDetails.Height = Unit.Pixel(500);
                    viewDetailsWindow.VisibleOnPageLoad = true;
                    rwmViewDetails.Windows.Add(viewDetailsWindow);
                    ContentPlaceHolder ViewDetailsContentPlaceHolder;
                    ViewDetailsContentPlaceHolder =
                     (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                    ViewDetailsContentPlaceHolder.Controls.Add(viewDetailsWindow);
                    rwmViewDetails.Windows.Clear();
                    break;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var ds = wb.GetStudentLoans();
            if (!string.IsNullOrEmpty(txtStudentCode.Text))
                ds = ds.Where(w => w.StudentCode == txtStudentCode.Text).ToList();
            if (ddlStatus.SelectedItem.Value != "-1")
            {
                var status = Convert.ToInt32(ddlStatus.SelectedItem.Value);
                ds = ds.Where(w => w.Status == status).ToList();
            }
            if (ddlTerm.SelectedItem.Value != "-1")
                ds = ds.Where(w => w.Term == ddlTerm.SelectedItem.Value).ToList();
            rgvLoanRequests.DataSource = ds;
            rgvLoanRequests.DataBind();
        }
    }
}