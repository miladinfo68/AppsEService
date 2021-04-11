using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.DTO.EmailClasses;
using System.Data;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.university.Research;
using System.Configuration;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
//fff
namespace IAUEC_Apps.UI.CommonUI
{
    public partial class login : System.Web.UI.Page
    {
        LoginBusiness logBusiness = new LoginBusiness();
        ResearchBusiness Rbusiness = new ResearchBusiness();
        CommonBusiness CB = new CommonBusiness();

        //==============================
        LoginDTO lgn = new LoginDTO();
        //Isida4_webservice_main1.Isida4_webservice_mainservice ms = new Isida4_webservice_main1.Isida4_webservice_mainservice();
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    if (Page.IsPostBack)
        //        Session["user"] = null;
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            
            // param1: stcode/codeostad; param2: password; param3: 1=student, 2=professor; param4: 0=update, 1=insert; param5: personalKey
            //var x = ms.ctrl_elec("860121004", "123456", "1", "1");

            if (Request.UrlReferrer != null)
                urlReff.Value = Request.UrlReferrer.AbsoluteUri;

            if (Session[sessionNames.userID_StudentOstad] != null)
            {
                Session.Timeout = 90;

            }
            if (!IsPostBack)
            {
                Session["Counter"] = 0;
                string appsUrl = ConfigurationManager.AppSettings["AppsUrl"]; // == "https://apps.iauec.ac.ir/"; //exit by student and user
                string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"]; // == "https://service.iauec.ac.ir/"; //exit by theacher

                //chek whether we have came from portal pazhoohesh or not
                if (Request.UrlReferrer != null && (string.Compare(Request.UrlReferrer.ToString(), appsUrl) == 0
                                || string.Compare(Request.UrlReferrer.ToString(), serviceUrl) == 0))
                    Session[sessionNames.userID_StudentOstad] = null;
            }
            else
            {
                if (Session["Counter"] == null)
                    Session["Counter"] = 0;
            }

            if (int.Parse(Session["Counter"].ToString()) >= 2)
                cph.Visible = true;
            lblVersion.Text = "نسخه: " + ConfigurationManager.AppSettings["ApplicationVersion"];


            if (Request["__EVENTTARGET"] == "login") // b1
                ClickME();//(sender, e);
            else if (Request["__EVENTTARGET"] == "recover")
                btn_Pass_ServerClick(sender, e);
        }
        
        public class ReCaptchaClass
        {
            public static string Validate(string EncodedResponse)
            {
                var client = new System.Net.WebClient();

                string PrivateKey = "6LcMeRkTAAAAAPmL221ClcsypSe6IrGkkJJTuZOP";

                var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

                var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaClass>(GoogleReply);

                return captchaResponse.Success;
            }

            [JsonProperty("success")]
            public string Success
            {
                get { return m_Success; }
                set { m_Success = value; }
            }

            private string m_Success;
            [JsonProperty("error-codes")]
            public List<string> ErrorCodes
            {
                get { return m_ErrorCodes; }
                set { m_ErrorCodes = value; }
            }


            private List<string> m_ErrorCodes;
        }
        protected void ClickedME(object sender, EventArgs e)
        {
            ClickME();
        }

        private void ClickME()
        {
            //Session[sessionNames.userName_StudentOstad] = "";
            //test students and profs for  exams
            var testMode = false;
            var profdTests = new string[] { "1", "12", "13", "136", "1539", "4023", "4257", "5008", "5010", "5051" };
            var studentsTests = new string[] {  "99001", "99002", "99003", "99004", "99005", "99006" };
            if (testMode && profdTests.Contains(txtUserName.Value))
            {
                string name = Rbusiness.getTeacherName_Fostad(int.Parse(txtUserName.Value));
                Session[sessionNames.userID_StudentOstad] = txtUserName.Value;
                Session[sessionNames.userName_StudentOstad] = name;
                Session["Password"] = txtPassword.Value;
                Session["IsOstad"] = true;
                Session["UserType_lms"] = 2;
                Response.Redirect("~/CommonUI/TeacherIntro.aspx");
            }
            if (testMode && studentsTests.Contains(txtUserName.Value))
            {
                Session[sessionNames.userID_StudentOstad] = txtUserName.Value;
                Session["Password"] = txtPassword.Value;
                Session["LogStatus"] = "1-1";
                Session["UserType_lms"] = 3;
                Response.Redirect("~/CommonUI/IntroPage.aspx");
            }


            Session[sessionNames.userName_StudentOstad] = "";
            if (int.Parse(Session["Counter"].ToString()) < 2)
            {
                LoginMangement(txtUserName.Value.changePersianNumberToLatinNumber(), txtPassword.Value.changePersianNumberToLatinNumber());
            }
            else
            {
                string EncodedResponse = Request.Form["g-Recaptcha-Response"];
                bool IsCaptchaValid = (ReCaptchaClass.Validate(EncodedResponse) == "true" ? true : false);
                if (IsCaptchaValid)
                {
                    LoginMangement(txtUserName.Value.changePersianNumberToLatinNumber(), txtPassword.Value.changePersianNumberToLatinNumber());
                    Session["Counter"] = 0;
                }
            }
        }

        protected void decodeText(object sender,EventArgs e)
        {
            var txt = Text1.Value;
            var code = CommonBusiness.DecryptPass(txt);
            text2.Value = code;

        }



        protected void btn_Pass_ServerClick(object sender, EventArgs e)
        {

            bool sent; string smsStatusText;

            var isStudent = !(txtuser.Value.Length < 8 && CommonBusiness.IsNumeric(txtuser.Value));
            var dt = logBusiness.SendUserPassword(txtuser.Value.changePersianNumberToLatinNumber(), idd_meli.Value.changePersianNumberToLatinNumber(), "", isStudent);
            if(dt.Rows.Count==0)
             dt = logBusiness.SendUserPassword(txtuser.Value.changePersianNumberToLatinNumber(), idd_meli.Value.changePersianNumberToLatinNumber(), "", !isStudent);
            if (dt.Rows.Count > 0)
            {
                Random generator = new Random();
                var code = generator.Next(0, 999999).ToString("D6");
                var token = CommonBusiness.EncryptPass(code);
                var expDate = DateTime.Now.AddMinutes(30);
                if (logBusiness.SetChangePasswordToken(txtuser.Value.changePersianNumberToLatinNumber(), isStudent, token, expDate))
                {
                    var result = CB.sendSMS(dt.Rows[0]["mobile"].ToString(), "کد بازیابی شما: " + code, out sent, out smsStatusText);
                    if (sent)
                    {
                        Session["RecoveryInfo"] = txtuser.Value;
                        pnlSendCode.Visible = false;
                        pnlEnterCode.Visible = true;
                        rdw.RadAlert("کد بازیابی از طریق پیامک برای شما ارسال گردید. مهلت ورود کد بازیابی و تغییر رمز عبور 30 دقیقه می باشد.", null, null, "پیام", "");
                    }
                    else
                    {
                        rdw.RadAlert(smsStatusText, null, null, "پیام", "");
                    }
                    int asanakStatus = CB.getAsanakStatusID(result);
                    CB.LogStatusMessage(txtuser.Value, result, dt.Rows[0]["mobile"].ToString(), asanakStatus, result, 13);
                }
                else
                {
                    rdw.RadAlert("امکان ایجاد کد بازیابی برای شما وجود ندارد. لطفا با پشتیبانی تماس بگیرید.", null, null, "پیام", "");
                }
            }
            else
                rdw.RadAlert("اطلاعات وارد شده صحیح نمی باشد", null, null, "پیام", "");

        }
        protected void LoginMangement(string strUserName, string strPassword)
        {
            Session[sessionNames.userName_StudentOstad] = "";
            if (!CommonBusiness.IsNumeric(strUserName))
            {
                rdw.RadAlert("نام کاربری یا رمز عبور صحیح نمی باشد", null, null, "پیام", "");
                Session["Counter"] = int.Parse(Session["Counter"].ToString()) + 1;
                return;
            }
            if (string.IsNullOrWhiteSpace(strUserName.Trim()) || string.IsNullOrWhiteSpace(strPassword.Trim()))
            {
                txtUserName.Value = "";
                txtPassword.Value = "";
                return;
            }
            hd_un.Value = strUserName;
            if (string.IsNullOrEmpty(Session[sessionNames.userID_StudentOstad]?.ToString())) //user before have not logged in 
            {
              
                lgn = logBusiness.User_Login(strUserName);
                if (lgn != null && !string.IsNullOrEmpty(lgn.UserName) && strPassword == CommonBusiness.DecryptPass(lgn.Password)) //login student
                {
                    Session[sessionNames.userID_StudentOstad] = lgn.UserName;
                    Session["Password"] = txtPassword.Value;
                    Session["LogStatus"] = "1-1";
                    Session["UserType_lms"] = 3;
                    Response.Redirect("~/CommonUI/IntroPage.aspx");

                }
                else if (strUserName.Length < 8 && CommonBusiness.IsNumeric(strUserName)) //login teacher
                {  
                    DataTable ost = new DataTable();
                    ost = Rbusiness.GetTeacherUserPass(int.Parse(strUserName));
                    string name = Rbusiness.getTeacherName_Fostad(int.Parse(strUserName));
                    if (ost.Rows.Count > 0 && CommonBusiness.DecryptPass(ost.Rows[0]["Password"].ToString()) == strPassword)
                    {
                        Session[sessionNames.userID_StudentOstad] = strUserName;
                        Session[sessionNames.userName_StudentOstad] = name;
                        Session["Password"] = txtPassword.Value;

                        Session["IsOstad"] = true;
                        Session["UserType_lms"] = 2;
                        Response.Redirect("~/CommonUI/TeacherIntro.aspx");
                    }
                    else
                    {
                        rdw.RadAlert("نام کاربری یا رمز عبور صحیح نمی باشد", null, null, "پیام", "");
                        Session["Counter"] = int.Parse(Session["Counter"].ToString()) + 1;
                    }
                }
                else
                {
                    rdw.RadAlert("نام کاربری یا رمز عبور صحیح نمی باشد", null, null, "پیام", "");
                    Session["Counter"] = int.Parse(Session["Counter"].ToString()) + 1;
                }
            }
            else   //if (Session["user"] !=null)
            {
                if (string.Compare(hd_un.Value, Session[sessionNames.userID_StudentOstad].ToString()) != 0)//user avz shode
                {
                    rdw.RadAlert("کاربر محترم امکان ورود همزمان بیش از یک کاربر در یک مرورگر وجود ندارد لطفا جهت ورود با کاربری دیگر از مرورگری غیر از مرورگر جاری استفاده نمایید", null, null, "پیام", "");
                    txtUserName.Value = "";
                    txtPassword.Value = "";
                    Session["Counter"] = int.Parse(Session["Counter"].ToString()) + 1;
                    return;
                }

                lgn = logBusiness.User_Login(strUserName);
                if (lgn != null && !string.IsNullOrEmpty(lgn.UserName) && strPassword == CommonBusiness.DecryptPass(lgn.Password)) //login student
                {
                    Session[sessionNames.userID_StudentOstad] = lgn.UserName;
                    Session["Password"] = txtPassword.Value;
                    Session["LogStatus"] = "1-1";
                    Session["UserType_lms"] = 3;
                    Response.Redirect("~/CommonUI/IntroPage.aspx");

                }
                else if (strUserName.Length < 8 && CommonBusiness.IsNumeric(strUserName)) //login teacher
                {
                    DataTable ost = new DataTable();
                    ost = Rbusiness.GetTeacherUserPass(int.Parse(txtUserName.Value));
                    string name = Rbusiness.getTeacherName_Fostad(int.Parse(strUserName));
                    if (ost.Rows.Count > 0 && CommonBusiness.DecryptPass(ost.Rows[0]["Password"].ToString()) == strPassword)
                    {
                        Session[sessionNames.userID_StudentOstad] = strUserName;
                        Session[sessionNames.userName_StudentOstad] = name;
                        Session["UserType_lms"] = 2;
                        Response.Redirect("~/CommonUI/TeacherIntro.aspx");
                    }
                    else
                    {
                        rdw.RadAlert("نام کاربری یا رمز عبور صحیح نمی باشد", null, null, "پیام", "");
                        Session["Counter"] = int.Parse(Session["Counter"].ToString()) + 1;
                    }
                }
                else
                {
                    rdw.RadAlert("نام کاربری یا رمز عبور صحیح نمی باشد", null, null, "پیام", "");
                    Session["Counter"] = int.Parse(Session["Counter"].ToString()) + 1;
                }
            }
        }
        
        protected void btnResetPassword_ServerClick(object sender, EventArgs e)
        {
            if (Session["RecoveryInfo"] == null)
            {
                rdw.RadAlert("خطا در تغییر رمز عبور! لطفاً مجدداً صفحه را بارگذاری کنید.", null, null, "پیام", "");
                return;
            }
            if (string.IsNullOrEmpty(txtRecoveryPass.Value.changePersianNumberToLatinNumber()) || string.IsNullOrEmpty(txtRepetRecoveryPass.Value.changePersianNumberToLatinNumber()) || string.IsNullOrEmpty(txtRecoveryCode.Value.changePersianNumberToLatinNumber()))
            {
                rdw.RadAlert("تکمیل تمام موارد الزامی است.", null, null, "پیام", "");
                return;
            }
            if (!txtRecoveryPass.Value.Equals(txtRepetRecoveryPass.Value.changePersianNumberToLatinNumber()))
            {
                rdw.RadAlert("رمزعبور و تکرار آن یکسان نیست، لطفاً مجدداً تلاش نمائید.", null, null, "پیام", "");
                return;
            }
            if (!ValidateToken(Session["RecoveryInfo"].ToString(), txtRecoveryCode.Value.changePersianNumberToLatinNumber()))
            {
                rdw.RadAlert("کد بازیابی صحیح نیست و یا مدت زمان اعتبار آن پایان یافته است.", null, null, "پیام", "");
                return;
            }

            if (Session["RecoveryInfo"].ToString().Length < 8)
            {
                if (!CB.ChangeTeacherPassword(Convert.ToInt64(Session["RecoveryInfo"]), txtRecoveryPass.Value.changePersianNumberToLatinNumber()))
                    rdw.RadAlert("عملیات با خطا مواجه شد.", null, null, "خطا", "");
            }
            else
                if (!CB.ChangeStudentPassword(Session["RecoveryInfo"].ToString(), txtRecoveryPass.Value.changePersianNumberToLatinNumber()))
                    rdw.RadAlert("عملیات با خطا مواجه شد.", null, null, "خطا", "");

            Session["RecoveryInfo"] = null;

            pnlEnterCode.Visible = false;
            pnlSendCode.Visible = false;
            pnlMessage.Visible = true;
        }

        protected bool ValidateToken(string userCode, string recoveryCode)
        {
            var dt = logBusiness.GetUserLoginByUserCode(userCode, userCode.Length >= 8);
            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["ChangePasswordToken"].ToString()) && dt.Rows[0]["ChangePasswordTokenExpiration"] != null)
            {
                var expDate = Convert.ToDateTime(dt.Rows[0]["ChangePasswordTokenExpiration"]);
                if (expDate >= DateTime.Now)
                {
                    var code = CommonBusiness.DecryptPass(dt.Rows[0]["ChangePasswordToken"].ToString());
                    if (code.Equals(recoveryCode))
                        return true;
                }
            }
            return false;
        }

        protected void btnResendCode_ServerClick(object sender, EventArgs e)
        {
            if (Session["RecoveryInfo"] != null)
            {
                bool sent; string smsStatusText;
                var dt = logBusiness.ResendChangePasswordToken(txtuser.Value.changePersianNumberToLatinNumber(), DateTime.Now.AddMinutes(30), txtuser.Value.Length >= 8);
                var result = CB.sendSMS(dt.Rows[0]["mobile"].ToString(), "کد بازیابی شما: " + CommonBusiness.DecryptPass(dt.Rows[0]["ChangePasswordToken"].ToString()), out sent, out smsStatusText);
                if (sent)
                {
                    rdw.RadAlert("کد بازیابی مجدداً از طریق پیامک برای شما ارسال شد.", null, null, "پیام", "");
                }
                else
                    rdw.RadAlert("خطا در ارسال مجدد کد بازیابی! لطفاً مجدداً تلاش نمائید.", null, null, "پیام", "");
            }
            else
            {
                pnlEnterCode.Visible = false;
                pnlSendCode.Visible = true;
                pnlMessage.Visible = false;
                rdw.RadAlert("خطا در ارسال مجدد کد بازیابی! لطفاً مجدداً تلاش نمائید.", null, null, "پیام", "");
            }
        }

        protected void btnReturn_ServerClick(object sender, EventArgs e)
        {
            pnlEnterCode.Visible = false;
            pnlMessage.Visible = false;
            pnlSendCode.Visible = true;
        }

        protected void btnLastReturn_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        /**/
    }
}