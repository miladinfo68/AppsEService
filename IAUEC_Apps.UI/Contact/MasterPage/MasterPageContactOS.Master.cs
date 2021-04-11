using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.CommonClasses;
using System;
using System.Linq;
using Telerik.Web.UI;
using System.Data;
using IAUEC_Apps.Business.Conatct;
using IAUEC_Apps.UI.Contact.Functions;
namespace IAUEC_Apps.UI.Contact.MasterPage
{
    public partial class MasterPageContactOS : System.Web.UI.MasterPage
    {
        protected string userId = "200";
        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session[sessionNames.userID_StudentOstad] == null) Response.Redirect("~/CommonUI/login.aspx");
            else
            {
                Session["CountUnRead"] = null;
                Session["ContactUnread"] = null;
                LoginBusiness logBusiness = new LoginBusiness();
                System.Data.DataTable dt = new System.Data.DataTable();
                userId += Session[sessionNames.userID_StudentOstad].ToString();
                user.Text = userId;
                if (Request.QueryString["Flag_Grp"] != null)
                {
                    MessageJs.DeleteUnreadStudent(userId, Request.QueryString["Flag_Grp"],
                    Request.QueryString["Flag_Grp"].Trim() != "True" && Request.QueryString["IdGrpOrPerson"] != null
                    ? Request.QueryString["IdGrpOrPerson"] : "-1");
                }
           //     if (!IsPostBack)
                {
                    stName.InnerText = Session[sessionNames.userName_StudentOstad].ToString(); 
                    DataTable dtCountUnread;
                    DataTable dtContactUnread;
                    dtCountUnread =MsgUnReadOstadBuisnes.GetUnReadMsgCountOstad(userId);
  
                    if (dtCountUnread != null && dtCountUnread.Rows.Count > 0)
                    {
                        Session["CountUnRead"] = dtCountUnread.Rows[0]["CountUnRead"].ToString();
                        dtContactUnread = MsgUnReadOstadBuisnes.GetUnReadMsgOstad(userId);
                        Session.Add("ContactUnread", dtContactUnread);
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
            widnow1.NavigateUrl = "../../CommonUI/ChangePassword.aspx";
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
            windowManager.Windows.Add(widnow1);
            this.form1.Controls.Add(widnow1);
        }

        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../CommonUI/TeacherIntro.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session[sessionNames.userID_StudentOstad] = null;
            Session["LogStatus"] = "0-0";
            LogStatus.Value = Session["LogStatus"].ToString();
            Response.Redirect("../../CommonUI/login.aspx");
        }

        protected void A1_ServerClick(object sender, EventArgs e)
        {
     
                Response.Redirect("../ContactOstad/ContactOstads.aspx");
       
        }

        protected void nav_StudentAndTheacherChatting_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Contact/ContactOstad/ContactOstads.aspx");
        }

        protected void nav_PortalResearch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/University/Research/CMS/TeacherMainPage.aspx");
        }

        protected void nav_OnlineDefensePlayback_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/TeacherOnlineDefensePlayback.aspx");
        }

        protected void nav_DefenceMeetingConcordance_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/TeacherReview.aspx");
        }

        protected void nav_AcceptScore_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/TeacherAcceptScore.aspx");
        }

        protected void navTestDefence_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/TeacherTestDefenceOnline.aspx");
        }
    }
}
