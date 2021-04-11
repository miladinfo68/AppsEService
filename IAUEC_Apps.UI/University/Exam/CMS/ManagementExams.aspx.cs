using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Education;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ManagementExams : System.Web.UI.Page
    {

        ExamBusiness ExamBusiness = new ExamBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindControlInFirstTime();
            }
           
            RegPcal();
        }
        //#################################
        private void RegPcal()
        {
            string scrp1 = "var objCal1 = new AMIB.persianCalendar('" + pcal1.ClientID + "', {extraInputID: '" + pcal1.ClientID + "',extraInputFormat: 'yyyy/mm/dd'        ,onchange: function( pdate ){checkCal();}}); ";
            string scrp2 = "var objCal2 = new AMIB.persianCalendar('" + pcal2.ClientID + "', {extraInputID: '" + pcal2.ClientID + "',extraInputFormat: 'yyyy/mm/dd'        ,onchange: function( pdate ){checkCal();}}); ";
            string scrp3 = "var objCal3 = new AMIB.persianCalendar('" + txtPcal1.ClientID + "',  {extraInputID: '" + txtPcal1.ClientID + "',extraInputFormat: 'yyyy/mm/dd' ,onchange: function( pdate ){checkCal_pnlSecond();}}); ";
            string scrp4 = "var objCal4 = new AMIB.persianCalendar('" + txtPcal2.ClientID + "',  {extraInputID: '" + txtPcal2.ClientID + "',extraInputFormat: 'yyyy/mm/dd' ,onchange: function( pdate ){checkCal_pnlSecond();}}); ";


            string scrp = "setTimeout(function(){ " + scrp1 + scrp2 + scrp3 + scrp4 + "}, 300);";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, scrp, true);
        }

        //#################################
        void BindControlInFirstTime()
        {
            DataTable dt_student = ExamBusiness.GetSystemAvailability(MiddleClass.appID, MiddleClass.userKind_Student, MiddleClass.userStatus);
            DataTable dt_prof = ExamBusiness.GetSystemAvailability(MiddleClass.appID, MiddleClass.userKind_Prof, MiddleClass.userStatus);
            if (dt_student != null)
            {
                string fDate = string.IsNullOrWhiteSpace(dt_student.Rows[0].Field<string>("FromDate")) ? "1396/03/01" : dt_student.Rows[0].Field<string>("FromDate");
                string tDate = string.IsNullOrWhiteSpace(dt_student.Rows[0].Field<string>("ToDate")) ? "1396/03/01" : dt_student.Rows[0].Field<string>("ToDate");
                TimeSpan strtTime = TimeSpan.Parse(string.IsNullOrWhiteSpace(dt_student.Rows[0].Field<string>("StartTime")) ? "07:00" : dt_student.Rows[0].Field<string>("StartTime"));
                TimeSpan lastTime = TimeSpan.Parse(string.IsNullOrWhiteSpace(dt_student.Rows[0].Field<string>("EndTime")) ? "22:00" : dt_student.Rows[0].Field<string>("EndTime"));

                this.pcal1.Text = fDate;
                this.pcal2.Text = tDate;
                this.RadTimePicker1.SelectedTime = strtTime;
                this.RadTimePicker2.SelectedTime = lastTime;
            }



            if (dt_prof != null)
            {
                string fDate = string.IsNullOrWhiteSpace(dt_prof.Rows[0].Field<string>("FromDate")) ? "1396/03/01" : dt_prof.Rows[0].Field<string>("FromDate");
                string tDate = string.IsNullOrWhiteSpace(dt_prof.Rows[0].Field<string>("ToDate")) ? "1396/03/01" : dt_prof.Rows[0].Field<string>("ToDate");
                TimeSpan strtTime = TimeSpan.Parse(string.IsNullOrWhiteSpace(dt_prof.Rows[0].Field<string>("StartTime")) ? "07:00" : dt_prof.Rows[0].Field<string>("StartTime"));
                TimeSpan lastTime = TimeSpan.Parse(string.IsNullOrWhiteSpace(dt_prof.Rows[0].Field<string>("EndTime")) ? "22:00" : dt_prof.Rows[0].Field<string>("EndTime"));

                this.txtPcal1.Text = fDate;
                this.txtPcal2.Text = tDate;
                this.RadTimePicker3.SelectedTime = strtTime;
                this.RadTimePicker4.SelectedTime = lastTime;
            }
        }
        //#################################
        private void EnableShowResultMessage(string msg)
        {
            lblShowErrorMessage.Text = msg;
            lblShowErrorMessage.Visible = true;
        }
        //#################################
        protected void btnStudentPanel_Click(object sender, EventArgs e)
        {           
            if (lblShowErrorMessage.Visible == true) lblShowErrorMessage.Visible = false;
            if (this.btnStudentPanel.Text == MiddleClass.EditText)//"ویــــرایش")
            {
                this.pnl_First.Enabled = true;
                this.btnStudentPanel.Text = MiddleClass.InsertText;//"ثبـــــت"; 
                             
                return;
            }
            else//دکمه ثبت زده شد
            {
                var fromDate = this.pcal1.Text;
                var toDate = this.pcal2.Text;
                var startTime = RadTimePicker1.SelectedTime?.ToString().Substring(0, 5);
                var endTime = RadTimePicker2.SelectedTime?.ToString().Substring(0, 5);

                if (!MiddleClass.AreAllFieldsEmpty(fromDate, toDate, startTime, endTime))
                {

                    if ((string.Compare(fromDate, toDate) == 1) || (string.Compare(startTime, endTime) == 1))
                    {
                        //EnableShowResultMessage(MiddleClass.msgError);                       
                        radWindowManager.RadAlert(MiddleClass.msgError, 400, 100, "پیام سیستم", "");
                        return;
                    }

                    var item1 = ExamBusiness.GetSystemAvailability(MiddleClass.appID, MiddleClass.userKind_Student, MiddleClass.userStatus);
                    if (item1 != null)
                    {
                        var check = ExamBusiness.UpdateSystemAvailabilityByParams(MiddleClass.appID, MiddleClass.userKind_Student, MiddleClass.userStatus, fromDate, startTime, toDate, endTime);
                        if (check)
                            radWindowManager.RadAlert(MiddleClass.msgSuccuss, 400, 100, "پیام سیستم", "");
                        //EnableShowResultMessage(MiddleClass.msgSuccuss);

                        else
                            radWindowManager.RadAlert(MiddleClass.msgwarrning, 400, 100, "پیام سیستم", "");
                        //EnableShowResultMessage(MiddleClass.msgwarrning);
                    }
                }

                this.btnStudentPanel.Text = MiddleClass.EditText;//"ویــــرایش";
                this.pnl_First.Enabled = false;

            }
        }
        //#################################
        protected void btnProfpannel_Click(object sender, EventArgs e)
        {
            if (lblShowErrorMessage.Visible == true) lblShowErrorMessage.Visible = false;
            if (this.btnProfpannel.Text == MiddleClass.EditText)//"ویــــرایش")
            {
                this.pnl_Second.Enabled = true;
                this.btnProfpannel.Text = MiddleClass.InsertText;//"ثبـــــت";
                return;
            }
            else//دکمه ثبت زده شد
            {
                var fromDate = this.txtPcal1.Text;
                var toDate = this.txtPcal2.Text;
                var startTime = RadTimePicker3.SelectedTime?.ToString().Substring(0, 5);
                var endTime = RadTimePicker4.SelectedTime?.ToString().Substring(0, 5);

                if (!MiddleClass.AreAllFieldsEmpty(fromDate,toDate,startTime,endTime))  //update 1
                {

                    if ((string.Compare(fromDate, toDate) == 1) || (string.Compare(startTime, endTime) == 1))
                    {
                        //EnableShowResultMessage(MiddleClass.msgError);
                        radWindowManager.RadAlert(MiddleClass.msgError, 400, 100, "پیام سیستم", "");
                        return;
                    }

                    var item1 = ExamBusiness.GetSystemAvailability(MiddleClass.appID, MiddleClass.userKind_Prof, MiddleClass.userStatus);
                    if (item1 != null)
                    {
                        var check = ExamBusiness.UpdateSystemAvailabilityByParams(MiddleClass.appID, MiddleClass.userKind_Prof, MiddleClass.userStatus, fromDate, startTime, toDate, endTime);
                        if (check)
                            //EnableShowResultMessage(MiddleClass.msgSuccuss);
                            radWindowManager.RadAlert(MiddleClass.msgSuccuss, 400, 100, "پیام سیستم", "");
                        else
                            //EnableShowResultMessage(MiddleClass.msgwarrning);
                            radWindowManager.RadAlert(MiddleClass.msgwarrning, 400, 100, "پیام سیستم", "");
                    }
                }

                this.btnProfpannel.Text = MiddleClass.EditText;//ویراش
                this.pnl_Second.Enabled = false;
            }
        }
        //#################################

        public static class MiddleClass
        {
            public static int appID { get; } = 8;//Applications امتحانات در جدول 
            public static Byte userKind_Student { get; } = 1;
            public static Byte userKind_Prof { get; } = 2;
            public static Byte userStatus { get; } = 0;

            public static string msgError { get; } = "تاریخ یا زمان شروع نمی تواند بیشتر از مقدار نهایی باشد";
            public static string msgSuccuss { get; } = "عملیات ویرایش با موفقیت انجام شد";
            public static string msgwarrning { get; } = "مقاددیر ورودی با فرمت صحیح وارد شوند ";

            public static string EditText { get; } = "ویــــرایش";
            public static string InsertText { get; } = "ثبـــــت";


            public static bool AreAllFieldsEmpty(string fromDate, string toDate, string startTime, string endTime)
            {
                return (    string.IsNullOrEmpty(fromDate) &&
                            string.IsNullOrEmpty(toDate) &&
                            string.IsNullOrEmpty(startTime) &&
                            string.IsNullOrEmpty(endTime)
                            );
            }
        }
    }
}