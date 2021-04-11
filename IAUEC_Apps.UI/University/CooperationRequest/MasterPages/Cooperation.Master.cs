using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.CooperationRequest.MasterPages
{
    public partial class Cooperation : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
            else
            {
                //Session["AppId"] = "10";
                if (!IsPostBack)
                {
                    Session[sessionNames.appID_Karbar] = "13";
                    //if (Session["AppId"].ToString() == "10")
                    //{
                    //    appTitle.InnerText = "گزارشات سیدا";
                    //}
                    if (Session[sessionNames.appID_Karbar].ToString() == "13")
                    {
                        appTitle.InnerText = "کارگزینی هیأت علمی";
                    }
                    usernamelbl.InnerHtml = Session[sessionNames.userName_Karbar].ToString();
                    LoginBusiness logBusiness = new LoginBusiness();
                    //stName.InnerText = Session["name"].ToString();
                    DataTable dt = new DataTable();

                    //DataTable dtmenu = new DataTable();
                    //dtmenu.Columns.Add("MenuLink");
                    //dtmenu.Columns.Add("linkCode");
                    //dtmenu.Columns.Add("icon");
                    //dtmenu.Columns.Add("MenuName");
                    //dtmenu.Columns.Add("CategoryName");
                    //DataRow dr;
                    dt = logBusiness.Get_Menu_ByUserIdAndAppId(int.Parse(Session[sessionNames.appID_Karbar].ToString()), int.Parse(Session[sessionNames.userID_Karbar].ToString()), int.Parse(Session["SectionId"].ToString()));
                    string cat = "";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (cat == "")
                        {
                            cat = dt.Rows[i]["CategoryId"].ToString();
                            lstreportMenu.Text += "<li><a>" + dt.Rows[i]["CategoryName"].ToString() + "<span class='fa fa-chevron-down'> </span></a><ul class='nav child_menu' style='display: none'>";

                        }

                        if (cat != dt.Rows[i]["CategoryId"].ToString())
                        {

                            lstreportMenu.Text += " </ul></li>";
                            cat = dt.Rows[i]["CategoryId"].ToString();
                            lstreportMenu.Text += "<li><a>" + dt.Rows[i]["CategoryName"].ToString() + "<span class='fa fa-chevron-down'> </span></a><ul class='nav child_menu' style='display: none'>";
                            lstreportMenu.Text += "  <li><a href='" + "/" + dt.Rows[i]["MenuLink"].ToString() + "?id=" + generaterandomstr() + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr() + "'><span>" + dt.Rows[i]["MenuName"].ToString() + "</span></a></li>";
                        }
                        else
                            lstreportMenu.Text += "  <li><a href='" + "/" + dt.Rows[i]["MenuLink"].ToString() + "?id=" + generaterandomstr() + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr() + "'><span>" + dt.Rows[i]["MenuName"].ToString() + "</span></a></li>";
                    }
                }
                //lstmenu.DataSource = dtmenu;
                //lstmenu.DataBind();
            }


            if (!(Session["IsGroupManger"] == null) && Convert.ToBoolean(Session["IsGroupManger"]))
            {
                changePass.Visible = false;

            }

        }
        public string generaterandomstr()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        protected void changePass_ServerClick(object sender, EventArgs e)
        {
            RadWindowManager windowManager = new RadWindowManager();
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
            Response.Redirect("~/CommonUI/CommonCmsIntro.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session[sessionNames.userID_Karbar] = null;

            if (!(Session["IsGroupManger"] == null) && Convert.ToBoolean(Session["IsGroupManger"]))
            {
                Session[sessionNames.userID_StudentOstad] = null;

                Session["LogStatus"] = "0-0";
                Response.Redirect("~/CommonUI/login.aspx");
            }
            Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
        }
    }
}