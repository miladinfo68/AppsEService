using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class AddOrUpdatePoll : System.Web.UI.Page
    {
        ExamBusiness eb = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        PersianCalendar pc = new PersianCalendar();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pid = Request.QueryString["pid"];
                FillForm();
                if (!string.IsNullOrEmpty(pid))
                    LoadPollData(Convert.ToInt32(pid));
            }
        }
        private void FillForm()
        {
            pnlError.Visible = false;
            //----- Fill Terms DropDownList
            var terms = cmnb.SelectAllTerm();
            ddlTerm.DataTextField = "tterm";
            ddlTerm.DataSource = terms;
            ddlTerm.DataBind();
            //ddlTerm.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
            //-----------------------------
        }
        private void LoadPollData(int pollId)
        {
            var poll = eb.GetPollById(pollId);
            if (poll != null)
            {
                ddlTitleType.SelectedValue = poll.PollType.ToString();
                ddlNeedComment.SelectedValue = poll.NeedComment ? "1" : "0";
                ddlTerm.SelectedValue = poll.Term;
                txtDescription.Text = poll.Description;
                if (poll.FromDate != DateTime.MinValue)
                    txtFromDate.Text = pc.GetYear(poll.FromDate) + "/" + pc.GetMonth(poll.FromDate) + "/" + pc.GetDayOfMonth(poll.FromDate);
                if (poll.ToDate != DateTime.MinValue)
                    txtToDate.Text = pc.GetYear(poll.ToDate) + "/" + pc.GetMonth(poll.ToDate) + "/" + pc.GetDayOfMonth(poll.ToDate);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseAndRebind", "CloseAndRebind(true);", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var dateIsValid = true;
            DateTime? toDate = null;
            DateTime? fromDate = null;
            if (!string.IsNullOrEmpty(txtToDate.Text))
            {
                var toDateParts = txtToDate.Text.Split('/');
                try
                {
                    toDate = pc.ToDateTime(Convert.ToInt32(toDateParts[0]), Convert.ToInt32(toDateParts[1]), Convert.ToInt32(toDateParts[2]), 0, 0, 0, 0, 0);
                }
                catch (Exception ex)
                {
                    dateIsValid = false;
                }
            }
            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                var fromDateParts = txtFromDate.Text.Split('/');
                try
                {
                    fromDate = pc.ToDateTime(Convert.ToInt32(fromDateParts[0]), Convert.ToInt32(fromDateParts[1]), Convert.ToInt32(fromDateParts[2]), 0, 0, 0, 0, 0);
                }
                catch (Exception ex)
                {
                    dateIsValid = false;
                }
            }
            if (dateIsValid && ((toDate == null && fromDate == null) || (toDate != null && fromDate != null && toDate >= fromDate)))
            {
                int pid = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                    pid = Convert.ToInt32(Request.QueryString["pid"]);
                var duplicate = eb.CheckPollExistForTerm(Convert.ToInt32(pid), ddlTerm.SelectedItem.Value, Convert.ToInt32(ddlTitleType.SelectedItem.Value));
                if (duplicate == 0 || duplicate == pid)
                {
                    if (eb.AddOrUpdatePoll(title: ddlTitleType.SelectedItem.Text, term: ddlTerm.SelectedItem.Value
                        , description: txtDescription.Text.Replace("&lt;", "<").Replace("&gt;", ">")
                        , needComment: Convert.ToBoolean(Convert.ToInt32(ddlNeedComment.SelectedItem.Value)), pollId: pid, fromDate: fromDate, toDate: toDate
                        , pollType: Convert.ToInt32(ddlTitleType.SelectedItem.Value)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseAndRebind", "CloseAndRebind(true);", true);
                    }
                    else
                    {
                        txtError.Text = "خطا در ذخیره اطلاعات، لطفاً مجدداً تلاش نمائید.";
                        pnlError.Visible = true;
                    }
                }
                else
                {
                    txtError.Text = "این پرسشنامه قبلاً برای ترم انتخابی ایجاد شده است.";
                    pnlError.Visible = true;
                }
            }
            else
            {
                txtError.Text = "بازه زمانی صحیح نیست.";
                pnlError.Visible = true;
            }
        }
    }
}