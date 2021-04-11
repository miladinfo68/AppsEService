using IAUEC_Apps.Business.university.Exam;

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using IAUEC_Apps.Business.Common;
using System.Globalization;

namespace IAUEC_Apps.UI.University.Exam.CMS
{


    public partial class ExamPaperDelivery : System.Web.UI.Page
    {

        CommonBusiness cmnb = new CommonBusiness();
        ExamBusiness bussiness = new ExamBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/loginRequestCMS.aspx");

            if (!Page.IsPostBack)
                BindDrpTerms();
        }



        void BindDrpTerms()
        {
            DataTable dterm = new DataTable();
            dterm = cmnb.SelectAllTerm();

            if (dterm != null)
            {
                ddlTerm.DataSource = dterm;
                ddlTerm.DataValueField = "tterm";
                ddlTerm.DataTextField = "tterm";
                ddlTerm.DataBind();
                ddlTerm.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));
            }

        }
        DataTable FillGridView(string ostID, string family, string term)
        {
            DataTable dt = bussiness.GetClassesForOstadByCodeOstad(decimal.Parse(ostID), family, term);
            return dt;
        }
        //#########################################

        protected void btnSearching_ServerClick(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var txtOst = string.IsNullOrEmpty(this.txtCodeOst.Value.Trim()) ? "-1" : this.txtCodeOst.Value.Trim();
            var txtOstFamily = string.IsNullOrEmpty(this.txtOstFamily.Value.Trim()) ? "" : this.txtOstFamily.Value.Trim();
            //var term = ddlTerm.SelectedValue;
            var term = ddlTerm.SelectedValue ?? "-1";
            DataTable dt = FillGridView(txtOst, txtOstFamily, term);
            if (dt.Rows.Count > 0)
                msgWarning.Visible = false;
            else
                msgWarning.Visible = true;

            grdv.DataSource = FillGridView(txtOst, txtOstFamily, term);
            grdv.DataBind();

            txtCodeOst.Focus();

        }


        protected void btnUpdateStatus_Command(object sender, CommandEventArgs e)
        {
            var cmdArgs = e.CommandArgument.ToString().Split(';');
            var did = cmdArgs[0];
            var ostCode = cmdArgs[1];
            var term = ddlTerm.SelectedValue??"-1";

            var item = bussiness.GetExamQuestionsbyDid(did ,term);
            var status = item.AsEnumerable().Select(s => s.Field<int>("Status")).ToArray()[0];

            var res = bussiness.UpdateExamQuestion_Status(int.Parse(did), 7, term);
            var questionID = item.AsEnumerable().Select(s => s.Field<int>("examQuestionID")).FirstOrDefault();

            var nimsal = "";
            if (term.Substring(term.Length - 1, 1) == "1")
                nimsal = "اول";
            else if (term.Substring(term.Length - 1, 1) == "2")
                nimsal = "دوم";
            else if (term.Substring(term.Length - 1, 1) == "3")
                nimsal = "سوم";

            var details = bussiness.Get_ExamdetailbyDid(did, term);

            string Text = "استاد محترم واحد الکترونیکی؛\r\nبه استحضار می­رساند پاکت اوراق امتحانات پایان ترم نیمسال " + nimsal + " سال تحصیلی " + term.Substring(0, 5) + " درس " 
                + details.Rows[0]["namedars"].ToString() + " کد مشخصه " + did + " در تاریخ " + MiladiToShamsi(DateTime.Now) + " به اداره امتحانات واحد الکترونیکی واصل گردید.\r\nاداره امتحانات واحد الکترونیکی";
            string asanak = "";
            DataTable dtResault = bussiness.GetMobileProfByDid(did);
            if (dtResault.Rows[0]["mobile"].ToString() != null || dtResault.Rows[0]["mobile"].ToString() != "")
            {
                bool sentSMS; string smsStatusText;
                asanak = cmnb.sendSMS(dtResault.Rows[0]["mobile"].ToString(), Text, out sentSMS, out smsStatusText);
                int asanakStatus = cmnb.getAsanakStatusID(asanak);
                cmnb.LogStatusMessage(dtResault.Rows[0]["code_ostad"].ToString(), asanak, dtResault.Rows[0]["mobile"].ToString(), asanakStatus, smsStatusText, 8);
            }

                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString())
                , DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 167
                , "دریافت اوراق", questionID);
            grdv.DataSource = FillGridView(ostCode, "" , term);
            grdv.DataBind();
        }

        public string MiladiToShamsi(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date).ToString() + "/" + pc.GetMonth(date).ToString() + "/" + pc.GetDayOfMonth(date).ToString();
        }

        protected void grdv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var item = (e.Row.DataItem as DataRowView);
                Button btns = e.Row.FindControl("btnUpdateStatus") as Button;
                if (btns != null)
                {
                    if (Convert.ToInt32(item.Row["Status"]) == 7)
                    {
                        btns.BackColor = System.Drawing.Color.Gray;
                        btns.ForeColor = System.Drawing.Color.White;
                        btns.Enabled = false;
                    }
                    else
                    {
                        btns.BackColor = System.Drawing.Color.FromArgb(15, 201, 84);
                        btns.ForeColor = System.Drawing.Color.White;
                        // btns.Enabled = true;
                    }
                }

            }
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdv.DataSource = null;
            grdv.DataBind();
            txtCodeOst.Value = null;
            txtOstFamily.Value = "";
            txtCodeOst.Focus();
        }
    }

}