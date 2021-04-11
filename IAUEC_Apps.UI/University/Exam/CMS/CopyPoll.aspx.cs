using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class CopyPoll : System.Web.UI.Page
    {
        CommonBusiness cmnb = new CommonBusiness();
        ExamBusiness eb = new ExamBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pid = Request.QueryString["pid"];
                if (!string.IsNullOrEmpty(pid))
                    LoadForm();
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Close", "Close();", true);
            }
        }

        private void LoadForm()
        {
            //----- Fill Terms DropDownList
            var terms = cmnb.SelectAllTerm();
            ddlTerms.DataTextField = "tterm";
            ddlTerms.DataSource = terms;
            ddlTerms.DataBind();
            //ddlTerm.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
            //-----------------------------
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            var pid = Request.QueryString["pid"];
            if(eb.CheckPollExistForTerm(Convert.ToInt32(pid), ddlTerms.SelectedItem.Value) == 0)
                if (eb.CopyPoll(Convert.ToInt32(pid), ddlTerms.SelectedItem.Value))
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseAndRebind", "CloseAndRebind(true);", true);
                else ShowError("خطا در ثبت اطلاعات! لطفاً مجدداً تلاش نمائید.");
            else
                 ShowError("این پرسشنامه قبلاً برای ترم انتخابی ایجاد شده است.");
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            pnlError.Visible = true;
        }
    }
}