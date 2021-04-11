using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Runtime.CompilerServices;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class ShowContract : System.Web.UI.Page
    {

        Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
        string _typeOfContract;

        UserControls.Contract ucContract = new UserControls.Contract();
        //UserControls.Contract_DeputyGroup ucContract_DeputyGroup = new UserControls.Contract_DeputyGroup();
        UserControls.Contract_HeadOfDepartment ucContract_HeadOfDepartment = new UserControls.Contract_HeadOfDepartment();
        const string logPath = "~/university/cooperationRequest/userControls/logContract.txt";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["vs"].ToString() == "s" && (Request.QueryString["uc"] == ((int)DTO.RoleEnums.مسئول_حق_التدریس).ToString()||Request.QueryString["uc"] == ((int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی).ToString()))
            {

                Business.university.Request.ProfessorRequestBusiness bsnRequest = new Business.university.Request.ProfessorRequestBusiness();
                int teacherCodeInSida = Convert.ToInt32(Request.QueryString["sc"]);
                if (bsnRequest.HasPendingRequest(teacherCodeInSida, (int)DTO.University.Request.RequestTypeId.EditContactInfo) ||
            bsnRequest.HasPendingRequest(teacherCodeInSida, (int)DTO.University.Request.RequestTypeId.EditPersonalInfo))
                    showMessage("این استاد دارای درخواست ویرایش تایید نشده برای اطلاعات فردی و یا اطلاعات تماس می باشد", false);
            }
            _typeOfContract = Request.QueryString["TC"];
            DataTable dtContract;
            writeLog("_typeOfContract:"+_typeOfContract);

            switch (_typeOfContract)
            {
                case DTO.contract.educationContract:
                    try
                    {
                        writeLog("_typeOfContract:" + _typeOfContract);
                        ucContract = LoadControl("UserControls/Contract.ascx") as UserControls.Contract;
                        ucContract.Attributes.Add("EnableViewState", "false");
                        plcUserControl.Controls.Add(ucContract);

                        writeLog("count of pnlContract controls:"+plcUserControl.Controls.Count);
                        ucContract.TeacherCode = Convert.ToInt32(Request.QueryString["sc"]);
                        ucContract.HRCode = Convert.ToInt32(Request.QueryString["hc"]);
                        ucContract.userCode = Convert.ToInt32(Request.QueryString["uc"]);
                        ucContract.userType = 2;
                        writeLog("ucContract.TeacherCode:" + ucContract.TeacherCode + " , ucContract.HRCode:" + ucContract.HRCode + " , ucContract.userCode:" + ucContract.userCode + " ,userType:" + ucContract.userType);
                        writeLog("Session[term]:"+(Session["term"] == null ? "null" : Session["term"].ToString()));
                        if (Session["term"] != null)
                            ucContract.term = string.IsNullOrEmpty(Session["term"].ToString()) ? "this" : Session["term"].ToString();
                        else
                            ucContract.term = "this";
                        writeLog("ucContract.term:" + ucContract.term);
                        ucContract.signature = chbConfirm.Checked;
                        writeLog("ucContract.signature:" + ucContract.signature);
                        dtContract = bsn.getContractOfTeacher(ucContract.HRCode, ucContract.term);
                        writeLog("dtContract.Rows.Count:" + dtContract.Rows.Count);
                        if (dtContract.Rows.Count == 1 && dtContract.Rows[0]["contractFile"] != DBNull.Value)
                            ucContract.contractFile = dtContract.Rows[0]["contractFile"].ToString();
                        writeLog("ucContract.contractFile.Length:" + ucContract.contractFile.Length);
                    }
                    catch (Exception ex)
                    {
                        writeLog(ex.Message);
                    }
                    break;
                case DTO.contract.HeadOfDepartment:
                    ucContract_HeadOfDepartment = LoadControl("UserControls/Contract_HeadOfDepartment.ascx") as UserControls.Contract_HeadOfDepartment;
                    plcUserControl.Controls.Add(ucContract_HeadOfDepartment);
                    ucContract_HeadOfDepartment.TeacherCode = Convert.ToInt32(Request.QueryString["sc"].Trim());
                    ucContract_HeadOfDepartment.HRCode = Convert.ToInt32(Request.QueryString["hc"].Trim());
                    ucContract_HeadOfDepartment.userCode = Convert.ToInt32(Request.QueryString["uc"].Trim());
                    ucContract_HeadOfDepartment.userType = 2;
                    if (Session["term"] != null)
                        ucContract_HeadOfDepartment.year = string.IsNullOrEmpty(Session["term"].ToString()) ? DateTime.Now.ToPeString().Substring(0, 4) : Session["term"].ToString();
                    else
                        ucContract_HeadOfDepartment.year = DateTime.Now.ToPeString().Substring(0, 4);
                    ucContract_HeadOfDepartment.signature = chbConfirm.Checked;
                    dtContract = bsn.getContractOfTeacher(ucContract_HeadOfDepartment.HRCode, ucContract_HeadOfDepartment.year);
                    if (dtContract.Rows.Count == 1 && dtContract.Rows[0]["contractFile"] != DBNull.Value)
                        ucContract_HeadOfDepartment.contractFile = dtContract.Rows[0]["contractFile"].ToString();
                    break;
            }


            btnAccept.Visible = Request.QueryString["vs"].ToString().Trim() == "s";
            btnRejectSwitch.Visible = (Request.QueryString["vs"].ToString().Trim() == "s" && (Convert.ToInt32(Request.QueryString["uc"].Trim()) == (int)DTO.RoleEnums.مسئول_حق_التدریس||Convert.ToInt32(Request.QueryString["uc"].Trim()) == (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی));
            btnLastStep.Visible = (Request.QueryString["vs"].ToString().Trim() == "s" && Convert.ToInt32(Request.QueryString["uc"].Trim()) == (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی);
            btnPrint.Visible = Request.QueryString["vs"].ToString().Trim() == "v";

        }

        private void writeLog(string txt, [CallerLineNumber] int lineNumber=0,[CallerMemberName] string caller=null,[CallerFilePath] string path="")
        {
            return;
            File.AppendAllText(Server.MapPath(logPath),DateTime.Now+" - "+path+" - "+caller+" - " + lineNumber+ " - " + txt +"\r\n");
        }

        private void showMessage(string msg, bool redirect)
        {
            string redirectURL = redirect ? "RedirectToContractsMain()" : "";
            rwmMain.RadAlert(msg, 300, 100, "پیام سیستم", redirectURL);
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            string htmlToSave = "", term = "", description = "";
            switch (_typeOfContract)
            {
                case DTO.contract.educationContract:
                    htmlToSave = ucContract.getContentOfContract();
                    term = ucContract.term;

                    DataTable dt = bsn.getTerm_Contract(term);
                    if (dt.Rows.Count > 0)
                        description += string.Format("نیم سال {0} سال تحصیلی {1}", dt.Rows[0]["nimsal"].ToString(), dt.Rows[0]["sal"].ToString());
                    break;
                case DTO.contract.HeadOfDepartment:
                    htmlToSave = ucContract_HeadOfDepartment.getContentOfContract();
                    term = ucContract_HeadOfDepartment.year;
                    description += string.Format(" سال {0} ", term);
                    break;
            }
            if (bsn.updateTeacherContractStatus(Convert.ToInt32(Request.QueryString["hc"].Trim()), Convert.ToInt32(Request.QueryString["uc"].Trim()), htmlToSave, term))
            {
                setLog(Convert.ToInt32(Request.QueryString["hc"].Trim()), description, true);
                ScriptManager.RegisterStartupScript(uplConfirm, uplConfirm.GetType(), "CloseAndRebind", "CloseAndRebind(true);", true);

            }
            else
                showMessage("خطا در تایید و امضای قرارداد به وجود آمده است. لطفا مجددا تلاش فرمایید.", false);

        }



        protected void chbConfirm_CheckedChanged(object sender, EventArgs e)
        {
            btnAccept.Enabled = chbConfirm.Checked;

        }

        private void setLog(int OstadCode, string Description, bool acceptContract)
        {
            DTO.eventEnum eventType = DTO.eventEnum.امضای_قرارداد_توسط_مدیر_کارگزینی;
            if (acceptContract)
                switch (int.Parse(Request.QueryString["uc"].Trim()))
                {
                    case (int)DTO.RoleEnums.مسئول_حق_التدریس:
                    case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی:
                        eventType = DTO.eventEnum.امضای_قرارداد_توسط_مسئول_کارگزینی;
                        break;
                    case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی:
                        eventType = DTO.eventEnum.امضای_قرارداد_توسط_مدیر_کارگزینی;
                        break;
                    case (int)DTO.RoleEnums.سرپرست_واحد:
                        eventType = DTO.eventEnum.امضای_قرارداد_توسط_سرپرست_دانشکده;
                        break;
                }
            else
                eventType = DTO.eventEnum.رد_قرارداد_توسط_کارگزینی;
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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            string description = "علت:" + txtRejectReason.Text.Trim(), term = "";
            switch (_typeOfContract)
            {
                case DTO.contract.educationContract:
                    DataTable dt = bsn.getTerm_Contract(ucContract.term);
                    if (dt.Rows.Count > 0)
                        description += string.Format("  نیم سال {0} سال تحصیلی {1}", dt.Rows[0]["nimsal"].ToString(), dt.Rows[0]["sal"].ToString());
                    term = ucContract.term;
                    break;
                case DTO.contract.HeadOfDepartment:
                    description += string.Format("  سال {0} ", ucContract_HeadOfDepartment.year);
                    term = ucContract_HeadOfDepartment.year;
                    break;
            }
            if (bsn.rejectTeacherContract(Convert.ToInt32(Request.QueryString["hc"].Trim()), txtRejectReason.Text.Trim(), term))
            {
                Business.Common.CommonBusiness common = new Business.Common.CommonBusiness();
                string msg = "استاد گرامي، قرارداد ترم " + term + " سركار / جنابعالي مورد تاييد اداره كارگزيني هيات علمي واقع نگرديد. لطفاً جهت كسب اطلاعات بيشتر به سامانه خدمات الكترونيك مراجعه و يا با شماره 42863288 تماس حاصل فرماييد.";
                //common.sendSMS(2, Request.QueryString["sc"].ToString(), msg);
                //common.SendSMSByUserIdAndType(msg, Request.QueryString["sc"].ToString(), 2);
                string smsStatusText; bool sentSMS;
                common.sendSMS(2, Request.QueryString["sc"].Trim().ToString(), msg, out sentSMS, out smsStatusText);

                setLog(Convert.ToInt32(Request.QueryString["hc"].Trim()), description, false);
            }
            else
                showMessage("خطایی در رد قرارداد به وجود آمده است. لطفا مجددا تلاش فرمایید.", false);
            ScriptManager.RegisterStartupScript(uplConfirm, uplConfirm.GetType(), "CloseAndRebind", "CloseAndRebind(true);", true);
        }

        protected void btnLastStep_Click(object sender, EventArgs e)
        {
            if (bsn.ChangeContractStatusToLastStep(Convert.ToInt32(Request.QueryString["hc"].Trim()), Convert.ToInt32(Request.QueryString["uc"].Trim()), _typeOfContract == DTO.contract.HeadOfDepartment ? ucContract_HeadOfDepartment.year : ucContract.term))
            {
                string description = "  بازگشت به کارتابل قبل";
                switch (_typeOfContract)
                {
                    case DTO.contract.educationContract:
                        DataTable dt = bsn.getTerm_Contract();
                        if (dt.Rows.Count > 0)
                            description += string.Format("نیم سال {0} سال تحصیلی {1}", dt.Rows[0]["nimsal"].ToString(), dt.Rows[0]["sal"].ToString());
                        break;
                    case DTO.contract.HeadOfDepartment:
                        description += string.Format(" سال {0} ", ucContract_HeadOfDepartment.year);
                        break;
                }
                setLog(Convert.ToInt32(Request.QueryString["hc"].Trim()), description, false);
                ScriptManager.RegisterStartupScript(uplConfirm, uplConfirm.GetType(), "CloseAndRebind", "CloseAndRebind(true);", true);

            }
            else
                showMessage("خطایی در بازگشت قرارداد به مرحله قبل به وجود آمده است. لطفا مجددا تلاش فرمایید.", false);
        }
    }
}