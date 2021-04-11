using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Exam.MasterPages
{
    public partial class TeacherExamMaster : System.Web.UI.MasterPage
    {
        protected void Page_init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null )
                Response.Redirect("~/CommonUI/login.aspx",false);
            else if (!IsPostBack)
                usernamelbl.InnerText = Session[sessionNames.userName_StudentOstad].ToString();
        }
        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../../CommonUI/TeacherIntro.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            if (!(Session["IsGroupManger"] == null) && Convert.ToBoolean(Session["IsGroupManger"]))
            {
                Session[sessionNames.userID_Karbar] = null;
                Session["LogStatus"] = "0-0";
            }

            Session[sessionNames.userID_StudentOstad] = null;
            Response.Redirect("~/CommonUI/login.aspx",false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);
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