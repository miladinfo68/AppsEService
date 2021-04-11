using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Tuitional.masterPages
{
    public partial class tuitionalMasterpage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            LoginBusiness logBusiness = new LoginBusiness();
            Session[sessionNames.appID_Karbar] = 15;
            if (Session[sessionNames.appID_Karbar] == null || Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
            usernamelbl.InnerHtml = Session[sessionNames.userName_Karbar].ToString();
            dt = logBusiness.Get_Menu_ByUserIdAndAppId(int.Parse(Session[sessionNames.appID_Karbar].ToString()), int.Parse(Session[sessionNames.userID_Karbar].ToString()), 0);
            string cat = "";
            lstreportMenu.Text = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (cat == "")
                {
                    cat = dt.Rows[i]["CategoryId"].ToString();
                    lstreportMenu.Text += "<li><a>" + dt.Rows[i]["CategoryName"].ToString() + "<span class='fa fa-chevron-down'> </span></a><ul class='nav child_menu' style='display: none'>";

                }

                string ran1 = generaterandomstr();
                string ran2 = generaterandomstr();
                if (cat != dt.Rows[i]["CategoryId"].ToString())
                {
                    lstreportMenu.Text += " </ul></li>";
                    cat = dt.Rows[i]["CategoryId"].ToString();
                    lstreportMenu.Text += "<li><a>" + dt.Rows[i]["CategoryName"].ToString() + "<span class='fa fa-chevron-down'> </span></a><ul class='nav child_menu' style='display: none'>";
                    lstreportMenu.Text += "  <li><a href='" + "/" + dt.Rows[i]["MenuLink"].ToString() + "?id=" + ran1 + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + ran2 + "'><span>" + dt.Rows[i]["MenuName"].ToString() + "</span></a></li>";
                }
                else
                    lstreportMenu.Text += "  <li><a href='" + "/" + dt.Rows[i]["MenuLink"].ToString() + "?id=" + ran1 + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + ran2 + "'><span>" + dt.Rows[i]["MenuName"].ToString() + "</span></a></li>";
            }
        }
        public string generaterandomstr()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            //var result = 
                return 
                new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            //return result;
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session[sessionNames.userID_Karbar] = null;
            Response.Redirect("~/CommonUI/LoginRequestCMS.aspx", false);
        }

        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/commonui/CommonCmsIntro.aspx");
        }

        protected void changePass_ServerClick(object sender, EventArgs e)
        {
            Telerik.Web.UI.RadWindowManager windowManager = new Telerik.Web.UI.RadWindowManager();
            Telerik.Web.UI.RadWindow widnow1 = new Telerik.Web.UI.RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = "~/CommonUI/ChangePassword.aspx";
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
            windowManager.Windows.Add(widnow1);
            this.form1.Controls.Add(widnow1);
        }
    }
}