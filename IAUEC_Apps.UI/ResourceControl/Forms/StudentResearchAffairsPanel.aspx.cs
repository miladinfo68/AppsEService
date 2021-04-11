using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Conatct;
using IAUEC_Apps.Business.university.Request;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class StudentResearchAffairsPanel : System.Web.UI.Page
    {
        RequestHandler _requestHandler = new RequestHandler();
        CommonBusiness cmnb = new CommonBusiness();
        DataTable dt_vaz = new DataTable();
        LoginBusiness logBusiness = new LoginBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            dt_vaz = logBusiness.GetStIdVaz(Session[sessionNames.userID_StudentOstad].ToString());
        }

        protected void a_PortalPajoheshi_ServerClick(object sender, EventArgs e)
        {
            bool tempAccess = false;
            Business.university.Research.ResearchBusiness b = new Business.university.Research.ResearchBusiness();

            DataTable dtAllowEnterToPortal = b.PortalEntryPermition();
            var studentPermit = dtAllowEnterToPortal.Select("stcode='" +Session[sessionNames.userID_StudentOstad].ToString()+"'").Length>0;
            //rwm_message.RadAlert("کاربر گرامی سامانه در حال بروزرسانی می باشد", null, 100, "پیام", "");
            if (dt_vaz.Rows[0]["idvazkol"].ToString() != "1" && dt_vaz.Rows[0]["idvazkol"].ToString() != "15" && !studentPermit)
            {
                rwm_message.RadAlert("شما به این آیتم دسترسی ندارید", null, 100, "پیام", "");
            }
            else
            {
                Response.Redirect("../../University/Research/Pages/ResearchMainPage.aspx");
            }
        }

        protected void a_ChattingTeacherToStudent_ServerClick(object sender, EventArgs e)
        {if (Session[sessionNames.userID_StudentOstad] != null)
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

        protected void a_Reserve_DefenceClass_ServerClick(object sender, EventArgs e)
        {
            
            DateTime dtNow = DateTime.Now;
            DateTime dtFrom = new DateTime(2020, 05, 30);
            DateTime dtTo = new DateTime(2020, 06, 24);
            if (dtNow >= dtFrom && dtNow <= dtTo)
            {
                rwm_message.RadAlert("دانشجوی گرامی, در ایام امتحانات دسترسی به سامانه رزرواسیون امکان پذیر نمی باشد", null, 100, "پیام", "");
                return;
            }
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
            if (falg != 1&&userId!="99900999")
            {
                if (falg == 2 )
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

                    //DataTable dt = null;

                    //dt = _requestHandler.FindBedehkarForReserve(userId);
                    //if (!(dt.Rows[0]["bedehi"] == null || Convert.ToDouble(dt.Rows[0]["bedehi"].ToString()) <= 0))
                    //{
                    //    var financialPermissionCondition = _requestHandler.GetFinancialPermissionCondition(userId);
                    //    if (financialPermissionCondition.Rows.Count > 0)
                    //    {
                    //        var financialPermissionDate = financialPermissionCondition.Rows[0]["financialPermissionDate"].ToString().ToGregorian();
                    //        var unitSectionDate = financialPermissionCondition.Rows[0]["unitSectionDate"].ToString().ToGregorian();
                    //        var dateDiff = (financialPermissionDate.Date - unitSectionDate.Date).Days;

                    //        var hasFinancialPermission = _requestHandler.HasFinancialPermission(userId);

                    //        if (financialPermissionCondition.Rows[0]["stg12"].ToString() == 2.ToString()
                    //            && dateDiff >= 0 && !hasFinancialPermission)
                    //        {
                    //            rwm_message.RadAlert(
                    //                "دانشجوي گرامي شما بدلیل داشتن بدهکاری مالی امکان رزرو جلسه دفاع را ندارید",
                    //                400, 100, "پیام", "");
                    //        }
                    //        else
                    //        {
                                //Session[sessionNames.userID_StudentOstad] = userId;
                                //if (_requestHandler.HasAnotherRequestInThisTermForIntro(Convert.ToInt32(userId)))
                                //    Response.Redirect("~/ResourceControl/Forms/StudentReview.aspx");
                                //else
                                //{
                                //    Response.Redirect("~/ResourceControl/Forms/StudentAddRequest.aspx");

                                //}


                           // }
                     //   }
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
                //    }

                //}
            }

            //#endif
        }

        protected void a_AssistanceRequestForDefenceConcordance_ServerClick(object sender, EventArgs e)
        {
            string userId = Session[sessionNames.userID_StudentOstad].ToString();
            if(_requestHandler.GetCountMeetingDefencesRejectByOstad(userId)>2)
                 Response.Redirect("~/ResourceControl/Forms/StudentAssistanceDefence.aspx");
            else
                rwm_message.RadAlert(
                                       "در حال حاضر شما امکان استفاده از این بخش را ندارید",
                                       400, 100, "پیام", "");

        }

        protected void a_AudioAndVideoCommunication_ServerClick(object sender, EventArgs e)
        {

        }

        protected void a_SendMessageToTeacher_ServerClick(object sender, EventArgs e)
        {

        }

        protected void a_OnlineDefensePlayback_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/StudentOnlineDefensePlayback.aspx");
        }

        protected void aTestDefence_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/StudentTestDefenceOnline.aspx");
        }
    }
}