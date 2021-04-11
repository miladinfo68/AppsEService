using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.CooperationRequest.Teachers
{
    public partial class ConfirmAgreement : System.Web.UI.Page
    {
        const string hrID = "hrID";
        protected void Page_Load(object sender, EventArgs e)
        {
            bool hasAgreement = false;

            ucAgreement.teacherCode = Convert.ToInt32(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            ucAgreement.signature = chbConfirm.Checked;
            ucAgreement.userType = 1;

            if (!IsPostBack)
            {
                ucAgreement.teacherCode = Convert.ToInt64(Session[sessionNames.userID_StudentOstad]);
                Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();
                var ostadInf = FRB.getOstadInfoFromPortal(ucAgreement.teacherCode);
                if (ostadInf.codeOstad == 0)
                {
                    showMessage("شما دسترسی به این قسمت را ندارید");
                }
                else
                {
                    ViewState[hrID] = ostadInf.hrId;
                    Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();

                    var agreements = bsn.getAgreementOfTeacher(ostadInf.codeOstad);
                    if (agreements.Rows.Count == 1)
                    {
                        if (agreements.Rows[0]["agreementFile"] != DBNull.Value)
                        {
                            hasAgreement = true;
                        }
                    }
                    if (hasAgreement)
                    {
                        showMessage("شما تفاهم نامه خود را امضا کرده اید و امکان مشاهده دوباره تفاهم نامه برای شما وجود ندارد.");
                        return;
                    }
                }

            }
        }

        protected void chbConfirm_CheckedChanged(object sender, EventArgs e)
        {
            btnConfirm.Enabled = chbConfirm.Checked;
        }

        protected void hdnBtnConfirm_Click(object sender, EventArgs e)
        {
            Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();

            var agreements = bsn.getAgreementOfTeacher(Convert.ToInt64(Session[sessionNames.userID_StudentOstad]));
            if (agreements.Rows.Count == 0)
            {
                if (!ucAgreement.canSign && !string.IsNullOrEmpty(ucAgreement.incompletedInf))
                {
                    string msg = string.Format("{0} {1} {2}", "اطلاعات", ucAgreement.incompletedInf, "شما دارای نقص میباشد. لطفا با ورود به صفحه ویرایش اطلاعات فردی، اطلاعات خود را تکمیل نموده و سپس اقدام به امضای تفاهم نامه فرمایید");
                    showMessage(msg);
                    return;
                }
                string htmlFile = ucAgreement.getContentOfAgreement();
                int agreementId;
                if (Session[sessionNames.userID_StudentOstad] != null && Convert.ToInt32(Session[sessionNames.userID_StudentOstad]) != 0)
                {
                    bool result = bsn.insertTeacherAgreement(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]), htmlFile, Convert.ToInt32(ViewState[hrID]), out agreementId);
                    if (result)
                    {
                        setLog(agreementId);
                        showMessage("تفاهم نامه شما جهت بررسی به بخش پژوهش ارسال شد. شما میتوانید در صفحه اصلی تفاهم نامه از مراحل ثبت تفاهم نامه خود مطلع شوید");
                    }
                    else
                        showMessage("در ارسال تفاهم نامه خطایی به وجود آمده است. لطفا مجددا تلاش فرمایید.");
                }
                else
                {
                    showMessage("در ارسال تفاهم نامه خطایی به وجود آمده است. لطفا مجددا تلاش فرمایید.");
                }
            }
        }
        private void showMessage(string msg)
        {
            rwmMain.RadAlert(msg, 300, 100, "پیام سیستم", "RedirectToEditMain");

        }

        private void setLog(int modifyID)
        {
            Business.Common.CommonBusiness commonBSN = new Business.Common.CommonBusiness();

            string userId;//کاربری که لاگین کرده
            //eventType//کد کاری که انجام شده. 
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13

            appId = Convert.ToInt32(Session[sessionNames.appID_StudentOstad]);

            commonBSN.InsertIntoStudentLog(ViewState[hrID].ToString(), DateTime.Now.ToString("HH:mm"), 13, 50, Session[sessionNames.userID_StudentOstad].ToString(), modifyID);
        }
    }
}