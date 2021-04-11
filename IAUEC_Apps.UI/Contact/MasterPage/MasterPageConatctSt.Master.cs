using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.CommonClasses;
using System;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Conatct;
using IAUEC_Apps.UI.Contact.Functions;
using ResourceControl.BLL;
using IAUEC_Apps.Business.university.Request;

namespace IAUEC_Apps.UI.Contact
{
    public partial class MasterPageConatct : System.Web.UI.MasterPage
    {
        LoginBusiness logBusiness = new LoginBusiness();
        RequestHandler _requestHandler = new RequestHandler();
        CommonBusiness cmnb = new CommonBusiness();
        DataTable dt_vaz = new DataTable();
        protected string userId="";
        protected void Page_Init(object sender, EventArgs e)
        {
           
            if (Session[sessionNames.userID_StudentOstad] == null) Response.Redirect("~/CommonUI/login.aspx");
            else
            {
                Session["CountUnRead"] =null;
                Session["ContactUnread"] = null;
                LoginBusiness logBusiness = new LoginBusiness();
                System.Data.DataTable dt = new System.Data.DataTable();
                string userId = Session[sessionNames.userID_StudentOstad].ToString();
                user.Text = userId;
                //if (Request.QueryString["Flag_Grp"] != null )
                //{
                    MessageJs.DeleteUnreadStudent(userId, Request.QueryString["Flag_Grp"] != null? Request.QueryString["Flag_Grp"]: "True",
                   Request.QueryString["Flag_Grp"]!=null && Request.QueryString["Flag_Grp"].Trim()!="True" && Request.QueryString["IdGrpOrPerson"]!=null
                    ? Request.QueryString["IdGrpOrPerson"] :"-1");
               // }
                if (!IsPostBack)
                {
                    StuImg st = logBusiness.User_Img(Session[sessionNames.userID_StudentOstad].ToString());
                    PersonalImage.DataValue = st.img;
                    LoginDTO stInfo = logBusiness.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
                    stName.InnerText = stInfo.Name + " " + stInfo.LastName;

                   
                    DataTable dtCountUnread;
                    DataTable dtUnReadContact;
                    dtCountUnread = MsgUnReadStudentBuisnes.GetUnReadMsgCountStudent(userId);
  
                    if (dtCountUnread != null&& dtCountUnread.Rows.Count>0)
                    {
                        Session["CountUnRead"]  = dtCountUnread.Rows[0]["CountUnRead"].ToString();
                        dtUnReadContact = MsgUnReadStudentBuisnes.GetUnReadMsgStudent(userId);
                        Session.Add("ContactUnread", dtUnReadContact);
                    }


                }

            }
        }

        public string generaterandomstr(int count)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session[sessionNames.userID_Karbar] == null)
            //    Response.Redirect("~/CommonUI/LoginRequestCMS.aspx", false);
        }
        protected void changePass_ServerClick(object sender, EventArgs e)
        {
            Telerik.Web.UI.RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = "~/CommonUI/ChangePassword.aspx";
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
            windowManager.Windows.Add(widnow1);
            this.form1.Controls.Add(widnow1);
        }

        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/CommonUI/IntroPage.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session[sessionNames.userID_StudentOstad] = null;
            Session[sessionNames.userID_Karbar] = null;
            Response.Redirect("~/", false);
        }


        protected void nav_StudentAndTheacherChatting_ServerClick(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] != null)
            {
                DataTable dt = ContactBuisnes.GetStages(Session[sessionNames.userID_StudentOstad].ToString());
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["stg2"].ToString() != "0")
                {

                    Response.Redirect("../../Contact/ContactStudent/ContactStudents.aspx");
                }
                else
                    rwm_message.RadAlert(
                                      "در حال حاضر شما امکان استفاده از این بخش را ندارید",
                                      400, 100, "پیام", "");

            }
 
        }

        protected void nav_PortalPajoheshi_ServerClick(object sender, EventArgs e)
        {
            bool tempAccess = false;
            var usercode = "976031";
            dt_vaz = logBusiness.GetStIdVaz(Session[sessionNames.userID_StudentOstad].ToString());
            //rwm_message.RadAlert("کاربر گرامی سامانه در حال بروزرسانی می باشد", null, 100, "پیام", "");
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15" && usercode != Session[sessionNames.userID_StudentOstad].ToString())
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                Response.Redirect("~/University/Research/Pages/ResearchMainPage.aspx");
            }
        }

        protected void nav_OnlineDefensePlayback_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/StudentOnlineDefensePlayback.aspx");
        }

        protected void nav_DefenceMeetingConcordance_ServerClick(object sender, EventArgs e)
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
                //                Session[sessionNames.userID_StudentOstad] = userId;
                //                if (_requestHandler.HasAnotherRequestInThisTermForIntro(Convert.ToInt32(userId)))
                //                    Response.Redirect("~/ResourceControl/Forms/StudentReview.aspx");
                //                else
                //                {
                //                    Response.Redirect("~/ResourceControl/Forms/StudentAddRequest.aspx");

                //                }


                //            }
                //        }
                //        else
                //        {

                //            //*******************************************************
                     //       Session[sessionNames.userID_StudentOstad] = userId;
                     //       if (_requestHandler.HasAnotherRequestInThisTermForIntro(Convert.ToInt32(userId)))
                     //           Response.Redirect("~/ResourceControl/Forms/StudentReview.aspx");
                     //       else
                     //       {
                     //           Response.Redirect("~/ResourceControl/Forms/StudentAddRequest.aspx");

                     //       }
                     ////   }
                 //   }
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
                    }

            //    }
            //}

            //#endif

        }

        protected void nav_AssistanceRequestForDefenceConcordance_ServerClick(object sender, EventArgs e)
        {
            string userId = Session[sessionNames.userID_StudentOstad].ToString();
            if (_requestHandler.GetCountMeetingDefencesRejectByOstad(userId) > 2)
                Response.Redirect("~/ResourceControl/Forms/StudentAssistanceDefence.aspx");
            else
                rwm_message.RadAlert(
                                       "در حال حاضر شما امکان استفاده از این بخش را ندارید",
                                       400, 100, "پیام", "");

        }

        protected void navTestDefence_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/StudentTestDefenceOnline.aspx");
        }
    }
}