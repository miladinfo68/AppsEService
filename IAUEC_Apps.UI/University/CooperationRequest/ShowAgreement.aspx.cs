using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class ShowAgreement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["vs"].ToString() == "s" && Request.QueryString["uc"].ToString().Trim() == (((int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی).ToString().Trim()))
            {

                Business.university.Request.ProfessorRequestBusiness bsnRequest = new Business.university.Request.ProfessorRequestBusiness();
                int teacherCodeInSida = Convert.ToInt32(Request.QueryString["pc"]);
            //    if (bsnRequest.HasPendingRequest(teacherCodeInSida, (int)DTO.University.Request.RequestTypeId.EditContactInfo) ||
            //bsnRequest.HasPendingRequest(teacherCodeInSida, (int)DTO.University.Request.RequestTypeId.EditPersonalInfo))
            //        showMessage("این استاد دارای درخواست ویرایش تایید نشده برای اطلاعات فردی و یا اطلاعات تماس میباشد", false);
            }
            ucAgreement.teacherCode = Convert.ToInt32(Request.QueryString["pc"]);
            ucAgreement.HRCode = Convert.ToInt32(Request.QueryString["hc"]);
            ucAgreement.userCode = Convert.ToInt32(Request.QueryString["uc"]);
            ucAgreement.userType = 2;
            Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
            DataTable dtAgreement = bsn.getAgreementOfTeacher(ucAgreement.teacherCode);
            ucAgreement.signature = true;
            if (dtAgreement.Rows.Count == 1 && dtAgreement.Rows[0]["agreementFile"] != DBNull.Value)
                ucAgreement.agreementFile = dtAgreement.Rows[0]["agreementFile"].ToString();
            btnAccept.Visible = Request.QueryString["vs"].ToString() == "s";
            btnRejectSwitch.Visible = (Request.QueryString["vs"].ToString() == "s" && Convert.ToInt32(Request.QueryString["uc"]) == (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی);
            btnPrint.Visible = Request.QueryString["vs"].ToString() == "v";
        }
        private void setLog(int OstadCode, string Description, bool acceptAgreement)
        {
            DTO.eventEnum eventType = DTO.eventEnum.امضای_قرارداد_توسط_مدیر_کارگزینی;
            if (acceptAgreement)
                switch (int.Parse(Request.QueryString["uc"]))
                {
                    case (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی:
                        eventType = DTO.eventEnum.تایید_تفاهم_نامه_توسط_مدیر_کل_امور_پژوهشی;
                        break;
                    case (int)DTO.RoleEnums.سرپرست_واحد:
                        eventType = DTO.eventEnum.تایید_تفاهم_نامه_توسط_سرپرست_واحد;
                        break;
                }
            else
                eventType = DTO.eventEnum.رد_تفاهم_نامه;
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
            int userId;//کاربری که لاگین کرده
            //eventType//کد کاری که انجام شده. 
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            string description;//توضیحات اختیاری
            int modifyId;//کد استاد ...
            userId = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            appId = 13;
            modifyId = OstadCode;
            description = Description;
            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, (int)eventType, description, modifyId);
        }
        private void showMessage(string msg, bool redirect)
        {
            string redirectURL = redirect ? "RedirectToAgreementsMain()" : "";
            rwmMain.RadAlert(msg, 300, 100, "پیام سیستم", redirectURL);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
            int agreementID ;
            if (bsn.rejectTeacherAgreement(Convert.ToInt64(Request.QueryString["pc"]), txtRejectReason.Text.Trim(),out agreementID))
            {
                Business.Common.CommonBusiness common = new Business.Common.CommonBusiness();
                string msg = "استاد گرامي، تفاهم نامه سركار / جنابعالي مورد تاييد امور پژوهشی واقع نگرديد. لطفاً جهت كسب اطلاعات بيشتر به سامانه خدمات الكترونيك مراجعه و يا با شماره 42863000 تماس حاصل فرماييد.";
                
                string smsStatusText; bool sentSMS;
                common.sendSMS(2, Request.QueryString["pc"].ToString(), msg, out sentSMS, out smsStatusText);

                setLog(Convert.ToInt32(Request.QueryString["hc"]), agreementID.ToString(), false);
            }
            else
                showMessage("خطایی در رد قرارداد به وجود آمده است. لطفا مجددا تلاش فرمایید.", false);
            ScriptManager.RegisterStartupScript(uplConfirm, uplConfirm.GetType(), "CloseAndRebind", "CloseAndRebind(true);", true);
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
            int agreementID;
            string htmlToSave = ucAgreement.getContentOfAgreement();
            if (bsn.updateTeacherAgreementStatus(Convert.ToInt32(Request.QueryString["pc"]), Convert.ToInt32(Request.QueryString["uc"]), htmlToSave,out agreementID))
            {
               
                setLog(Convert.ToInt32(Request.QueryString["hc"]), agreementID.ToString(), true);
                ScriptManager.RegisterStartupScript(uplConfirm, uplConfirm.GetType(), "CloseAndRebind", "CloseAndRebind(true);", true);
            }
            else
                showMessage("خطا در تایید و امضای تفاهم نامه به وجود آمده است. لطفا مجددا تلاش فرمایید.", false);
        }
    }
}