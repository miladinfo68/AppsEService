using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.ResourceControl.MasterPages
{
    public partial class ResourceControl : System.Web.UI.MasterPage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx");
            //else if (!IsPostBack)
            //{
            //    if (Session[sessionNames.userName_StudentOstad].ToString().Trim() == "")
            //    {
            //        LoginBusiness logBusiness = new LoginBusiness();
            //        LoginDTO stInfo = logBusiness.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
            //        usernamelbl.InnerText = stInfo.Name + " " + stInfo.LastName;
            //    }
            //    else
            //         usernamelbl.InnerText = Session[sessionNames.userName_StudentOstad].ToString();
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx");
            else if (!IsPostBack)
            {
                //if (Session[sessionNames.userName_StudentOstad].ToString().Trim() == "" && Session["UserType_lms"]==3.ToString())
                //{
                //    LoginBusiness logBusiness = new LoginBusiness();
                //    LoginDTO stInfo = logBusiness.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
                //    usernamelbl.InnerText = stInfo.Name + " " + stInfo.LastName;
                //}
                //else
                    usernamelbl.InnerText = Session[sessionNames.userName_StudentOstad].ToString();
            }
        }

        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../CommonUI/TeacherIntro.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            if (!(Session["IsGroupManger"] ==null) && Convert.ToBoolean(Session["IsGroupManger"]))
            {
                Session[sessionNames.userID_Karbar] = null;
                Session["LogStatus"] = "0-0";
            }
            Session[sessionNames.userID_StudentOstad] = null;
            Response.Redirect("~/CommonUI/login.aspx");
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
    }
}