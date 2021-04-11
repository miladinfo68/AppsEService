using IAUEC_Apps.Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.DTO.CommonClasses;
using Telerik.Web.UI;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class TeacherIntro : System.Web.UI.Page
    {
        UserAccessBusiness UserAccessBus = new UserAccessBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        ExamBusiness eBusiness = new ExamBusiness();
        public string bootstrapAddress = "../University/Theme/css/bootstrap-rtl.css";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("login.aspx");


            if (Session[sessionNames.userID_StudentOstad] == null)
            {
                Session["LogStatus"] = "0-0";

                form1.Action = System.Configuration.ConfigurationManager.AppSettings["LMS_link"].ToString() ;
                LogStatus.Value = Session["LogStatus"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);
            }


            //****************************


            var userId = Session[sessionNames.userID_StudentOstad];
            var isOstad = Session["IsOstad"];
            if (userId != null && isOstad != null)
            {
                //var isUserevaluated= _evaluationBusiness.IsUserevaluated(int.Parse(userId.ToString()));
                //var isQuestionExist = _evaluationBusiness.IsQuestionExist();
                //if (!isUserevaluated && isQuestionExist)
                //{
                //    ShowEvaluationQuestion();
                //}
                var groupManger = cmnb.GetGroupMangerInformation(Convert.ToInt64(userId.ToString()));
                var grpManger = groupManger.FirstOrDefault();
                if (grpManger != null && (groupManger.Count > 0 && grpManger.IsActive.Value))
                {
                    //* for test
                    var lngB = new LoginBusiness();
                    //var userDto = lngB.Get_UserLogin(grpManger.GroupManagerUser,"");
                    var userDto = cmnb.GetProfessorUser(grpManger.GroupManagerUser);

                    if (userDto.Count > 0 && userDto[0].Enable)
                    {
                        Session["IsGroupManger"] = true;
                        Session[sessionNames.userID_Karbar] = userDto[0].UserId;
                        Session["Enable"] = userDto[0].Enable;
                        Session[sessionNames.userName_Karbar] = userDto[0].Name;
                        Session[sessionNames.roleID] = userDto[0].RoleId;
                        Session[sessionNames.sectionID] = userDto[0].sectionId;
                        Session[sessionNames.user_Karbar] = userDto[0].UserName;
                        Session["p"] = CommonBusiness.DecryptPass(userDto[0].Password);
                        headOfDepartmentPanel.Visible = true;
                    }
                }
            }

            //****************************

            usernamelbl.InnerText = Session[sessionNames.userName_StudentOstad].ToString() + " خوش آمدید ";

        }
        //private void ShowEvaluationQuestion()
        //{
        //    var clientFunction = "openShowFileInPopup('../University/Evaluation/Question.aspx');";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popuup", clientFunction, true);
        //}
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("login.aspx");
            var randomValue = Guid.NewGuid().ToString();
            bootstrapAddress = $"../University/Theme/css/bootstrap-rtl.css?{randomValue}";
        }
        protected void A1_ServerClick(object sender, EventArgs e)
        {

            //rwm_message.RadAlert("استاد گرامی سامانه در حال بروزرسانی می باشد", null, 100, "پیام", "");
            //Response.Redirect("../University/Research/CMS/TeacherMainPage.aspx");

            Response.Redirect("../ResourceControl/Forms/TeacherResearchAffairsPanel.aspx");
        }
        protected void a_Reservation_ServerClick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = cmnb.GetSystemAvailability(11);
            if (bool.Parse(dt.Rows[0]["IsOpen"].ToString()) == false)
            //&& bool.Parse(dtos.Rows[0]["IsPermited"].ToString()) == false)
            {

                rwm_message.RadAlert("سامانه راه اندازی نشده است", null, 100, "پیام", "");
            }
            else
            {
                Session[sessionNames.appID_StudentOstad] = 11;
                Response.Redirect("../ResourceControl/Forms/ProfessorReview.aspx");
              //  Response.Redirect("../ResourceControl/Forms/TeacherDefencePanel.aspx");

            }
        }
        protected void a_Exam_ServerClick(object sender, EventArgs e)
        {


            DataTable dt = new DataTable();
            dt = cmnb.GetSystemAvailability(8, 2, 0);
            DataTable dtos = new DataTable();
            //dtos = eBusiness.GetOstadPermision(int.Parse(Session["user"].ToString()));
            //if (dt.Rows.Count > 0)
            //{


            if (dt.Rows[0]["IsSysOpen"].ToString() == "0")
            //&& bool.Parse(dtos.Rows[0]["IsPermited"].ToString()) == false)
            {

                rwm_message.RadAlert("در حال حاضر سامانه امتحانات بسته است", null, 100, "پیام", "");
            }
            else
            {
                Session[sessionNames.appID_StudentOstad] = 8;
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_StudentOstad].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 1, "");
                Response.Redirect("../University/Exam/Teacher/InsertQuestionPaper.aspx");
            }
            //}
        }

        protected void EditInfo_ServerClick(object sender, EventArgs e)
        {
            //rwm_message.RadAlert("سامانه در حال بروز رسانی است. لطفا ساعاتی دیگر مراجعه فرمایید", null, 100, "پیام", "");
            //return;
            DataTable dt = new DataTable();
            dt = cmnb.GetSystemAvailability(4);
            if (bool.Parse(dt.Rows[0]["IsOpen"].ToString()) == false)
            {

                rwm_message.RadAlert("در حال حاضر سامانه بسته است", null, 100, "پیام", "");
            }
            else
            {
                Session[sessionNames.appID_StudentOstad] = 4;
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_StudentOstad].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 1, "");
                Response.Redirect(url: "~/University/CooperationRequest/Teachers/EditMain.aspx");
            }
        }

        protected void ConfirmContract_ServerClick(object sender, EventArgs e)
        {
            //rwm_message.RadAlert("سامانه در حال بروز رسانی است. لطفا ساعاتی دیگر مراجعه فرمایید", null, 100, "پیام", "");
            //return;
            
            Response.Redirect(url: "~/University/CooperationRequest/Teachers/contractsMain.aspx");
            //}
        }

        protected void a_LMS_ServerClick(object sender, EventArgs e)
        {
            //rwm_message.RadAlert("استاد گرامی، سامانه مدیریت خدمات یادگیری در حال بروز رسانی می باشد و در حال حاضر امکان ورود به این سامانه امکان پذیر نمی باشد.", null, 100, "عدم امکان ورود به سامانه", "");
            //return;
            //rwm_message.RadAlert("استاد گرامی سامانه مدیریت یادگیری نیمسال دوم 97 در حال آماده سازی می باشد", null, 100, "پیام", "");

            //DateTime d = new DateTime(2021, 1, 8, 18, 0, 0);

            //bool seprationLMS = DateTime.Now >= d;
            //if (seprationLMS)
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openLmsModal();", true);
            //else
            //{
                
                loginToLMS();
            //}
        }
        protected void lmsNew_ServerClick(object sender, EventArgs e)
        {
            //rwm_message.RadAlert("دانشجوی گرامی، سامانه مدیریت خدمات یادگیری در حال بروز رسانی می باشد و در حال حاضر امکان ورود به این سامانه امکان پذیر نمی باشد.", null, 100, "عدم امکان ورود به سامانه", "");
            //return;
            
                loginToLMS();
            

            //rwm_message.RadAlert("کاربر گرامی سامانه مدیریت یادگیری نیمسال دوم 97 در حال آماده سازی می باشد", null, 100, "پیام", "");

        }

        protected void lmsOld_ServerClick(object sender, EventArgs e)
        {
            
                loginToLMS();
            

            //rwm_message.RadAlert("کاربر گرامی سامانه مدیریت یادگیری نیمسال دوم 97 در حال آماده سازی می باشد", null, 100, "پیام", "");

        }

        private void loginToLMS()
        {
            if (!(Session["IsOstad"] == null) && Convert.ToBoolean(Session["IsOstad"]))
                Session["UserType_lms"] = 2;
            Session["LogStatus"] = "1-1";//vurud
            Response.Redirect("../University/LMS/Pages/LmsMain.aspx");

        }

        protected void ChangePassword_ServerClick(object sender, EventArgs e)
        {
            Telerik.Web.UI.RadWindowManager windowManager = new Telerik.Web.UI.RadWindowManager();
            Telerik.Web.UI.RadWindow widnow1 = new Telerik.Web.UI.RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = "~/CommonUI/ChangeTeacherPassword.aspx";
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
            windowManager.Windows.Add(widnow1);
            this.form1.Controls.Add(widnow1);
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            if (!(Session["IsGroupManger"] == null) && Convert.ToBoolean(Session["IsGroupManger"]))
            {
                Session[sessionNames.userID_Karbar] = null;
                Session["LogStatus"] = "0-0";

                LogStatus.Value = Session["LogStatus"].ToString();
                Session["IsGroupManger"] = null;
                form1.Action = System.Configuration.ConfigurationManager.AppSettings["LMS_link"].ToString() ;
                ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);
            }
            Session[sessionNames.userID_StudentOstad] = null;
            Response.Redirect("~/CommonUI/login.aspx");
        }

        protected void OnEnterToHeadOfDepartmentPanel(object sender, EventArgs e)
        {

            Response.Redirect("CommonCmsIntro.aspx");
        }

        protected void aGameCenter_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("gamecenter.aspx");
        }

        protected void btnExamLMS_ServerClick(object sender, EventArgs e)
        {
            //rwm_message.RadAlert("سامانه در حال آماده سازی میباشد.", null, 100, "پیام", "");
            //return;
            bool linkIsActive;
            if (ConfigurationManager.AppSettings["ExamLinkActive"] == null
                || string.IsNullOrEmpty(ConfigurationManager.AppSettings["ExamLinkActive"].ToString())
                || !bool.TryParse(ConfigurationManager.AppSettings["ExamLinkActive"].ToString(), out linkIsActive)
                || !linkIsActive)
                rwm_message.RadAlert("لطفا در زمان مقرر مراجعه فرمایید", null, 100, "پیام", "");
            else 
            {
                ExamBusiness ExamBusiness = new ExamBusiness();
                SHA256 mySHA256 = SHA256Managed.Create();
                var profInfo = ExamBusiness.GetProfessorInfoByProfessorCode(Session[sessionNames.userID_StudentOstad].ToString());
                if (profInfo.Rows.Count > 0)
                {
                    var key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["ExamLinkKey"].ToString()));
                    var IV = string.IsNullOrEmpty(ConfigurationManager.AppSettings["ExamLinkIV"].ToString()) ?
                        new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 } : Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["ExamLinkIV"].ToString());
                    var url = ConfigurationManager.AppSettings["ExamLink"].ToString();
                    var token = EncryptionClass.EncryptAES256(Session[sessionNames.userID_StudentOstad].ToString() + ";" + profInfo.Rows[0]["idd_meli"].ToString() + ";" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), key, IV);
                    var script = "var form = document.createElement('form');  form.setAttribute('style', 'display: none;');  var tokenField = document.createElement('input'); form.method = 'POST'; form.action = '" + url + "';";
                    script += "tokenField.value='" + token + "'; tokenField.name='token'; form.appendChild(tokenField); document.body.appendChild(form); form.submit();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "submitform", script, true);
                }
            }
        }
    }
}