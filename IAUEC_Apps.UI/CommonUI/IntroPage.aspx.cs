using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using ResourceControl.BLL;
using System.Configuration;
using IAUEC_Apps.Business.Conatct.Jobs;
using IAUEC_Apps.Business.university.Exam;
using System.Text;
using System.Security.Cryptography;
using IAUEC_Apps.Business.university.Wallet;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class IntroPage : System.Web.UI.Page
    {
        StudentBuisiness EmailBus = new StudentBuisiness();
        CommonBusiness cmnb = new CommonBusiness();
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        RequestHandler _requestHandler = new RequestHandler();
        LoginBusiness logBusiness = new LoginBusiness();
        DataTable dt_vaz = new DataTable();
        EvaluationBusiness _evaluationBusiness = new EvaluationBusiness();
        WalletBusiness _walletBusiness = new WalletBusiness();
        StudentInfoBusiness _studentInfoBusiness = new StudentInfoBusiness();
        public string bootstrapAddress = "../University/Theme/css/bootstrap-rtl.css";
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("login.aspx");
            var randomValue = Guid.NewGuid().ToString();
            bootstrapAddress = $"../University/Theme/css/bootstrap-rtl.css?{randomValue}";

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("login.aspx");
            StCard.Disabled = true;

            if (Session[sessionNames.userID_StudentOstad] == null)
            {
                Session["LogStatus"] = "0-0";

                form1.Action = ConfigurationManager.AppSettings["LMS_link"].ToString();
                LogStatus.Value = Session["LogStatus"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);

            }
            //##############
            //##############
            //##############

            #region Evaluation
            /**
            var isUserevaluated = _evaluationBusiness.IsUserevaluated(int.Parse(Session[sessionNames.userID_StudentOstad].ToString()));
            var isQuestionExist = _evaluationBusiness.IsQuestionExist();
            var azmoonCallBack = _evaluationBusiness.CallAzmoonApi(int.Parse(Session[sessionNames.userID_StudentOstad].ToString()));
            if (!isUserevaluated && isQuestionExist && azmoonCallBack)
            {
                ShowEvaluationQuestion();
            }
            */
            #endregion

            #region UpdateStudent
            //var isStudentUpdate = _studentInfoBusiness.IsStudentUpdate(Session[sessionNames.userID_StudentOstad].ToString());
            //if (!isStudentUpdate)
            //{
            //    ShowStudentInformation();
            //}
            #endregion
            if (Session[sessionNames.userID_StudentOstad].ToString().Length < 14)
                logBusiness.updatePortalStudentInfo(Session[sessionNames.userID_StudentOstad].ToString());//آپدیت اطلاعات دانشجو روی پرتال


            dt_vaz = logBusiness.GetStIdVaz(Session[sessionNames.userID_StudentOstad].ToString());
            a_LMS.Visible = true;

            Session["CountApprove"] = 1;
            DTO.CommonClasses.LoginDTO stInfo = logBusiness.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
            usernamelbl.InnerText = stInfo.Name + " " + stInfo.LastName + " خوش آمدید ";
            //ISchedule myTask = new SendSmsSchedule();

            //myTask.Run();


            if (Request["__EVENTARGUMENT"] != null && Request["__EVENTARGUMENT"] == "info")
            {

            }

            /*-----------------Wallet---------------------*/
            if (!IsPostBack)
            {
                var balance = _walletBusiness.GetStudentCurrentBalance(Session[sessionNames.userID_StudentOstad].ToString());
                lblCurrentBalance.Text = String.Format("{0:n0}", balance) + "ريال";
            }
            /*--------------------------------------------*/
        }
        private void ShowEvaluationQuestion()
        {
            var clientFunction = "openShowFileInPopup('../University/Evaluation/Questions.aspx');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popuup", clientFunction, true);
        }
        private void ShowStudentInformation()
        {
            var clientFunction = "openShowFileInPopup('../University/StudentInfo/StudentInformation.aspx');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popuup", clientFunction, true);
        }
        protected void Email_ServerClick(object sender, EventArgs e)
        {

            //if (Session["IsMehman"] != null && Session["IsMehman"].ToString() == "1")
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15"
                 && dt_vaz.Rows[0]["idvazkol"].ToString() != "7" && dt_vaz.Rows[0]["idvazkol"].ToString() != "17")
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                DataTable dt = new DataTable();
                Session["StApp"] = "2";
                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session["StApp"].ToString()), 1, "");
                dt = EmailBus.GetAllStHaveEmail(Session[sessionNames.userID_StudentOstad].ToString());
                if (dt.Rows.Count > 0)
                    Response.Redirect("../EmailReg/Pages/EmailRequestStatus.aspx");
                else
                    Response.Redirect("../EmailReg/Pages/AcceptRule.aspx");
            }
        }
        protected void EditInfo_ServerClick(object sender, EventArgs e)
        {
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15")
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                Session["StApp"] = "4";
                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session["StApp"].ToString()), 1, "");

                Response.Redirect("../University/Request/Pages/EditPersonalInformationUI.aspx");
            }


        }

        protected void govahi_ServerClick(object sender, EventArgs e)
        {
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15")
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                Session["StApp"] = "3";
                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session["StApp"].ToString()), 1, "");

                Response.Redirect("../University/Request/Pages/GovahiTazakoratUI.aspx");
            }
        }

        protected void Adobe_ServerClick(object sender, EventArgs e)
        {
            DateTime d = new DateTime(2021, 01, 18, 18, 0, 0);
            if (DateTime.Now < d)
            {
                rwm_message.RadAlert("دانشجوی گرامی؛ سامانه دانلود فایلها، تا ساعت 21 بسته است. لطفا پس از ساعت 18 دوباره مراجعه فرمایید. با تشکر.", null, 100, "پیام", "");
                return;
            }
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "2" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15")
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                Session["StApp"] = "1";
                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session["StApp"].ToString()), 1, "");

                Response.Redirect("../Adobe/Pages/ClassList.aspx");
            }

        }

        protected void StCard_ServerClick(object sender, EventArgs e)
        {
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15")
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                Session["StApp"] = "5";
                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session["StApp"].ToString()), 1, "");
                Response.Redirect("../University/Request/Pages/RequestStudentCartsUI.aspx");
            }

        }

        protected void A1_ServerClick(object sender, EventArgs e)
        {

            bool tempAccess = false;
            string[] usercode = { "976031", "969531" };
            //rwm_message.RadAlert("کاربر گرامی سامانه در حال بروزرسانی می باشد", null, 100, "پیام", "");
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15" && !usercode.Contains(Session[sessionNames.userID_StudentOstad].ToString()))
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                Response.Redirect("../University/Research/Pages/ResearchMainPage.aspx");
            }


        }

        protected void a_LMS_ServerClick(object sender, EventArgs e)
        {
            //rwm_message.RadAlert("دانشجوی گرامی، سامانه مدیریت خدمات یادگیری در حال بروز رسانی می باشد و در حال حاضر امکان ورود به این سامانه امکان پذیر نمی باشد.", null, 100, "عدم امکان ورود به سامانه", "");
            //return;
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "2" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15")
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                loginToLMS();

            }

            //rwm_message.RadAlert("کاربر گرامی سامانه مدیریت یادگیری نیمسال دوم 97 در حال آماده سازی می باشد", null, 100, "پیام", "");

        }

        protected void lmsNew_ServerClick(object sender, EventArgs e)
        {
            //rwm_message.RadAlert("دانشجوی گرامی، سامانه مدیریت خدمات یادگیری در حال بروز رسانی می باشد و در حال حاضر امکان ورود به این سامانه امکان پذیر نمی باشد.", null, 100, "عدم امکان ورود به سامانه", "");
            //return;
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "2" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15")
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                loginToLMS();
            }

            //rwm_message.RadAlert("کاربر گرامی سامانه مدیریت یادگیری نیمسال دوم 97 در حال آماده سازی می باشد", null, 100, "پیام", "");

        }

        protected void lmsOld_ServerClick(object sender, EventArgs e)
        {
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "2" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15")
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                loginToLMS();
            }

            //rwm_message.RadAlert("کاربر گرامی سامانه مدیریت یادگیری نیمسال دوم 97 در حال آماده سازی می باشد", null, 100, "پیام", "");

        }

        private void loginToLMS()
        {
            DataTable dtbedehi = new DataTable();
            double bedehi;
            dtbedehi = GovahiBusiness.GetBedehkar(Session[sessionNames.userID_StudentOstad].ToString());
            bedehi = Convert.ToDouble((dtbedehi.Rows[0]["bedehi"].ToString()));
            DataTable dtReg = new DataTable();
            dtReg = GovahiBusiness.GetStRegisterd(Session[sessionNames.userID_StudentOstad].ToString());
            DataTable dtnaghs = new DataTable();
            dtnaghs = logBusiness.StHasNaghs(Session[sessionNames.userID_StudentOstad].ToString());
            DataTable dt = new DataTable();
            dt = logBusiness.GetStIdVaz(Session[sessionNames.userID_StudentOstad].ToString());

            //if (bedehi > 0)
            //{
            //    Session["LogStatus"] = "1-2";//bedehkar
            //    Response.Redirect("../University/LMS/Pages/LmsMain.aspx");

            //}
            if (dtnaghs.Rows.Count > 0)
            {
                Session["LogStatus"] = "1-4";//naghs darad
                Response.Redirect("../University/LMS/Pages/LmsMain.aspx");

            }

            else if (dtReg.Rows.Count == 0)
            {
                Session["LogStatus"] = "1-3";//sabtenam dashte dar in term
                Response.Redirect("../University/LMS/Pages/LmsMain.aspx");

            }



            else if (dt.Rows[0]["idvazkol"].ToString() == "7")
            {
                Session["LogStatus"] = "1-5";//fareghotahsil
                Response.Redirect("../University/LMS/Pages/LmsMain.aspx");


            }
            else
            {
                Session["LogStatus"] = "1-1";//vurud
                Response.Redirect("../University/LMS/Pages/LmsMain.aspx");
            }
        }

        protected void a_exit_ServerClick(object sender, EventArgs e)
        {
            Session["LogStatus"] = "0-0";

            Session[sessionNames.userID_StudentOstad] = null;

            LogStatus.Value = Session["LogStatus"].ToString();
            form1.Action = System.Configuration.ConfigurationManager.AppSettings["LMS_link"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);


        }
        protected void a_Exam_ServerClick(object sender, EventArgs e)
        {
            //students for test
            var userTests = new string[] { "99001", "99002", "99003", "99004", "99005", "99006" };
            var testMode = false;
            if (testMode && userTests.Contains(Session[sessionNames.userID_StudentOstad].ToString()))
            {
                Response.Redirect("../University/Exam/Pages/ExamPlace.aspx");
            }

            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "2" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15")
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                DataTable dt = cmnb.GetSystemAvailability(8, 1, 0);

                Session["StApp"] = "8";
                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session["StApp"].ToString()), 1, "");
                Response.Redirect("../University/Exam/Pages/ExamPlace.aspx");

            }


        }

        protected void a_Reserve_DefenceClass(object sender, EventArgs e)
        {
            CheckOutPajooheshBusiness business = new CheckOutPajooheshBusiness();
            // DataTable dt = new DataTable();
            // dt = cmnb.GetSystemAvailability(11);

            // if (bool.Parse(dt.Rows[0]["IsOpen"].ToString()) == false)
            //&& bool.Parse(dtos.Rows[0]["IsPermited"].ToString()) == false)
            string userId = Session[sessionNames.userID_StudentOstad].ToString();
            var studentInfo = business.GetStudentInfoForPajohesh(userId);

            //#if DEBUG
            //            Session[sessionNames.userID_StudentOstad] = userId;
            //            if (_requestHandler.HasAnotherRequestInThisTermForIntro(Convert.ToInt32(userId)))
            //                Response.Redirect("~/ResourceControl/Forms/StudentReview.aspx");
            //            else
            //            {
            //                Response.Redirect("~/ResourceControl/Forms/StudentAddRequest.aspx");

            //            }
            //#endif
            //#if !DEBUG
            var falg = cmnb.CheckDifenceCondition(userId);
            if (falg != 1)
            {
                if (falg == 2)
                {
                    rwm_message.RadAlert(
                        "در حال حاظر شما امکان استفاده از این بخش را ندارید",
                        400, 100, "پیام", "");
                }
                else
                {
                    rwm_message.RadAlert(
                        "دانشجوي گرامي شما بدليل عدم تكميل فرايندهاي مربوط به پورتال پژوهش مجاز به ثبت تاريخ جلسه دفاع خود نيستيد. جهت کسب اطلاعات بیشتر به بخش مربوطه تیکت ارسال نمایید",
                        400, 100, "پیام", "");
                }
            }
            else
            {
                //*******************************************************
                RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
                /// <summary>
                /// چنانچه دانشجو در این ترم ثبت نام داشته، شماره دانشجویی آن در این جدول قرار می گیرد
                /// </summary>
                //DataTable dts = new DataTable();
                //dts = _requestHandler.GetStRegisterd2(userId);
                //if (dts.Rows.Count == 0)
                //{
                //    rwm_message.RadAlert(" دانشجو گرامی شما در این ترم ثبت نام نکرده اید", 400, 100, "پیام", "");

                //}
                //else
                //{

                //    DataTable dt = null;

                //    dt = _requestHandler.FindBedehkarForReserve(userId);
                //    if (!(dt.Rows[0]["bedehi"] == null || Convert.ToDouble(dt.Rows[0]["bedehi"].ToString()) <= 0))
                //    {
                //        var financialPermissionCondition = _requestHandler.GetFinancialPermissionCondition(userId);
                //        if (financialPermissionCondition.Rows.Count > 0)
                //        {
                //            var financialPermissionDate = financialPermissionCondition.Rows[0]["financialPermissionDate"].ToString().ToGregorian();
                //            var unitSectionDate = financialPermissionCondition.Rows[0]["unitSectionDate"].ToString().ToGregorian();
                //            var dateDiff = (financialPermissionDate.Date - unitSectionDate.Date).Days;

                //            var hasFinancialPermission = _requestHandler.HasFinancialPermission(userId);

                //            if (financialPermissionCondition.Rows[0]["stg12"].ToString() == 2.ToString()
                //                && dateDiff >= 0 && !hasFinancialPermission)
                //            {
                //                rwm_message.RadAlert(
                //                    "دانشجوي گرامي شما بدلیل داشتن بدهکاری مالی امکان رزرو جلسه دفاع را ندارید",
                //                    400, 100, "پیام", "");
                //            }
                //            else
                //            {
                //        Session[sessionNames.userID_StudentOstad] = userId;
                //        if (_requestHandler.HasAnotherRequestInThisTermForIntro(Convert.ToInt32(userId)))
                //            Response.Redirect("~/ResourceControl/Forms/StudentReview.aspx");
                //        else
                //        {
                //            Response.Redirect("~/ResourceControl/Forms/StudentAddRequest.aspx");

                //        }


                //    }
                //}
                //else
                //{

                //*******************************************************
                //        Session[sessionNames.userID_StudentOstad] = userId;
                //        if (_requestHandler.HasAnotherRequestInThisTermForIntro(Convert.ToInt32(userId)))
                //            Response.Redirect("~/ResourceControl/Forms/StudentReview.aspx");
                //        else
                //        {
                //            Response.Redirect("~/ResourceControl/Forms/StudentAddRequest.aspx");

                //        }
                //    }
                //}
                //else
                //{

                //*******************************************************
                Session[sessionNames.userID_StudentOstad] = userId;
                if (_requestHandler.HasAnotherRequestInThisTermForIntro(Convert.ToInt32(userId)))
                    Response.Redirect("~/ResourceControl/Forms/StudentReview.aspx");
                else
                {
                    Response.Redirect("~/ResourceControl/Forms/StudentAddRequest.aspx");

                }
                //}

                //}
            }

            //#endif







        }
        protected void btnCheckOut_Click(object sender, EventArgs e)
        {

        }

        protected void a_CheckOut_ServerClick(object sender, EventArgs e)
        {
            if (dt_vaz.Rows[0]["idvazkol"].ToString() == "2")
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {


                Session["StApp"] = "12";
                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session["StApp"].ToString()), 1, "");

                Response.Redirect("../University/Request/Pages/CheckOutRequestSubmit.aspx");

            }
        }

        protected void a_Reservation_ServerClick(object sender, EventArgs e)
        {
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "2" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15")
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                rwm_message.RadAlert("کاربر گرامی این بخش از سامانه فعلا راه اندازی نشده است", null, 100, "پیام", "");
            }
        }

        protected void a_LoanFound_serverClick(object sender, EventArgs e)
        {
            //rwm_message.RadAlert("کاربر گرامی این سامانه بزودی راه اندازی می شود", null, 100, "پیام", "");
            Response.Redirect("../University/WelfareAffairs/Pages/StudentLoanRequest.aspx");
        }

        protected void ChangePassword_ServerClick(object sender, EventArgs e)
        {
            Telerik.Web.UI.RadWindowManager windowManager = new Telerik.Web.UI.RadWindowManager();
            Telerik.Web.UI.RadWindow widnow1 = new Telerik.Web.UI.RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = "~/CommonUI/ChangeStudentPassword.aspx";
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
            windowManager.Windows.Add(widnow1);
            this.form1.Controls.Add(widnow1);
        }

        protected void aGameCenter_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("gamecenter.aspx");
        }

        protected void a3_ServerClick(object sender, EventArgs e)
        {
            Session[sessionNames.userID_Karbar] = User;
            Response.Redirect("../Contact/ContactStudent/ContactStudents.aspx");
        }

        protected void a_pajoheshi_ServerClick(object sender, EventArgs e)
        {

            Response.Redirect("../ResourceControl/Forms/StudentResearchAffairsPanel.aspx");
        }

        protected void btnProfessorEvaluation_ServerClick(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad].ToString() == "980013719")
            {
                var dt = logBusiness.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
                var url = "http://eval.iauec.ac.ir/Pages/ServiceLogin?ServiceUserLogin=" + HttpUtility.UrlEncode(Session[sessionNames.userID_StudentOstad].ToString());
                url += "&FirstName=" + HttpUtility.UrlEncode(dt.Name);
                url += "&LastName=" + HttpUtility.UrlEncode(dt.LastName);
                Response.Redirect(url);
            }
            else
            {
                rwm_message.RadAlert("کاربر گرامی این بخش از سامانه فعلا راه اندازی نشده است", null, 100, "پیام", "");
            }
        }

        protected void btnExamLMS_ServerClick(object sender, EventArgs e)
        {
            //rwm_message.RadAlert("سامانه در حال آماده سازی میباشد.", null, 100, "پیام", "");
            //return;
            ExamBusiness ExamBusiness = new ExamBusiness();
            var stInfo = ExamBusiness.GetStudentInfo(Session[sessionNames.userID_StudentOstad].ToString());
            bool linkIsActive;
            if (Convert.ToInt32(stInfo.Rows[0]["idvazkol"]) != 1 && Convert.ToInt32(stInfo.Rows[0]["idvazkol"]) != 2 && Convert.ToInt32(stInfo.Rows[0]["idvazkol"]) != 15)
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            else if (ConfigurationManager.AppSettings["ExamLinkActive"] == null
                || string.IsNullOrEmpty(ConfigurationManager.AppSettings["ExamLinkActive"].ToString())
                || !bool.TryParse(ConfigurationManager.AppSettings["ExamLinkActive"].ToString(), out linkIsActive)
                || !linkIsActive)
                rwm_message.RadAlert("لطفا در زمان مقرر مراجعه فرمایید", null, 100, "پیام", "");
            else
            {
                SHA256 mySHA256 = SHA256Managed.Create();
                if (stInfo.Rows.Count > 0)
                {
                    var key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["ExamLinkKey"].ToString()));
                    var IV = string.IsNullOrEmpty(ConfigurationManager.AppSettings["ExamLinkIV"].ToString()) ?
                        new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 } : Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["ExamLinkIV"].ToString());
                    var url = ConfigurationManager.AppSettings["ExamLink"].ToString();
                    var token = EncryptionClass.EncryptAES256(Session[sessionNames.userID_StudentOstad].ToString() + ";" + stInfo.Rows[0]["idd_meli"].ToString() + ";" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), key, IV);
                    //token = EncryptionClass.EncryptAES256("930572418;2648677267;" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), key, IV);
                    var script = "var form = document.createElement('form'); form.setAttribute('style', 'display: none;'); var tokenField = document.createElement('input'); form.method = 'POST'; form.action = '" + url + "';";
                    script += "tokenField.value='" + token + "'; tokenField.name='token'; form.appendChild(tokenField); document.body.appendChild(form); form.submit();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "submitform", script, true);
                }
            }
            //var token = ConfigurationManager.AppSettings["ExamGetTokenURL"].ToString() + Session[sessionNames.userID_StudentOstad].ToString();
            //Response.Redirect(ConfigurationManager.AppSettings["ExamURL"].ToString() + token);
        }
    }
}