using IAUEC_Apps.Business.Common;
using ResourceControl.BLL;
using System;
using System.Data;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class StudentAssistanceDefence : System.Web.UI.Page
    {
        private CommonBusiness commonBusiness = new CommonBusiness();
        private RequestHandler _requestHandler = new RequestHandler();
        public static string msgBodyModal = "";
        public static string msgHeaderModal = "";
        public static string msgBodyHeader = "";
        public static bool ShowConfirm = false;
        protected void SetCustomTime()
        {
            txtTime.TimeView.CustomTimeValues = _requestHandler.GetTime();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetCustomTime();
            var stcode = Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtAssistance = null;
            if (!IsPostBack)
            {
                if (stcode != "")
                    dtAssistance = _requestHandler.GetAssistanceDefence(stcode);
                if (dtAssistance == null || dtAssistance.Rows.Count == 0)
                {
                    btnSave.Enabled = true;
                    btnSave.Visible = true;
                    lblInfo.Visible = false;
                    ShowConfirm = false;
                }
                else
                {
                    btnSave.Enabled = false;
                    btnSave.Visible = false;
                    lblInfo.Visible = true;
                    ShowConfirm = true;
                    /* AssistanceDefence 
                    *status 0 ثبت شده
                    *status 1  تایید شده
                    *status 2 رد شده
                    */
                    if (int.Parse(dtAssistance.Rows[0]["status"].ToString()) == 0)
                    {
                        msgBodyHeader = "درخواست شما درحال بررسی توسط کارشناسان دانشکده می باشد.";
                        msgBodyModal = "";
                        msgHeaderModal = "در حال بررسی";
                        lblInfo.CssClass = "label label-info";
                        btnAlert.CssClass = "btn btn-info";
                    }
                    else if (int.Parse(dtAssistance.Rows[0]["status"].ToString()) == 1)
                    {
                        msgBodyHeader = "درخواست شما توسط دانشکده تایید شد";
                        msgBodyModal ="پاسخ:"+dtAssistance.Rows[0]["Answer"].ToString();
                        
                        msgHeaderModal = "تایید درخواست";
                        lblInfo.CssClass = "label label-success";
                        btnAlert.CssClass = "btn btn-success";
                    }
                    else if (int.Parse(dtAssistance.Rows[0]["status"].ToString()) == 2)
                    {
                        msgBodyHeader = "درخواست شما توسط دانشکده رد شد";
                        msgBodyModal = "پاسخ:" + dtAssistance.Rows[0]["Answer"].ToString();
                        msgHeaderModal = "رد درخواست";
                        lblInfo.CssClass = "label label-danger";
                        btnAlert.CssClass = "btn btn-danger";
                    }
        

                }

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var stcode = Session[sessionNames.userID_StudentOstad].ToString();
            string msg = Condtion();
            if (msg == "ok")
            {
                if (_requestHandler.Enter_AssistanceDefence(stcode,txtDate.Text, txtTime.SelectedTime.Value.ToString()))
                {
                    commonBusiness.InsertIntoStudentLog(stcode, DateTime.Now.ToString("HH:mm")
                        , 11, 51, "ثبت درخواست مساعدت برای دفاع");
                    btnSave.Visible = false;
                    lblInfo.Text = "درخواست شما درحال بررسی توسط کارشناسان دانشکده می باشد.";
                    lblInfo.CssClass = "label label-info";
                    lblInfo.Visible = true;
                    txtDate.Enabled = false;
                    txtTime.Enabled = false;
                }
                else
                {
                    btnSave.Visible = false;
                    lblInfo.Text = "مشکلی در سیستم رخ داده است";
                    lblInfo.CssClass = "label label-danger ";
                    lblInfo.Visible = true;
                }
            }
            else
                RadWindowManager1.RadAlert(msg, 500, 100, "خطا", "");


        }

        protected void btnAlert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/StudentResearchAffairsPanel.aspx");

        }
        public string Condtion()
        {
            DateTime Date = DateTime.Parse(txtDate.Text);
            //var diffDateFor72 = (Date - RequestHandler.ThreeWorkingDays(DateTime.Now.Date)).TotalDays;

            //if (diffDateFor72 < 0)
            //{
            //    return "باید از زمان درخواست شما تا زمان کنونی حداقل 72 ساعت گذشته باشد";
            //}
            double diffDateFor3day;
 
                 diffDateFor3day = (Date - RequestHandler.ThreeWorkingDays(DateTime.Now.Date)).TotalDays;

            if (diffDateFor3day < 0)
            { 
               
                return "دانشجوی گرامی، باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع حداقل 3 روز کاری باشد ";
            }
            if (Date.DayOfWeek == DayOfWeek.Friday || Date.DayOfWeek == DayOfWeek.Thursday)
            {
                return "امکان برگزاری جلسه دفاع در روزهای پنجشنبه و جمعه وجود ندارد";
            }
            return "ok";
        }
    }
}