using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Education.MasterPages
{
    public partial class UnivercityMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx",false);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx",false);
            else
            {
                if (!IsPostBack)
                {
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
                            lstreportMenu.Text += "<li class='has-sub'><a href='#'><span>" + dt.Rows[i]["CategoryName"].ToString() + "</span></a><ul>";

                        }
                        lstreportMenu.Text += "  <li><a href='" + "../../" + dt.Rows[i]["MenuLink"].ToString() + "?id=" + generaterandomstr() + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr() + "'><span>" + dt.Rows[i]["MenuName"].ToString() + "</span></a></li>";
                        if (cat != dt.Rows[i]["CategoryId"].ToString())
                        {
                            cat = "";
                            lstreportMenu.Text += " </ul></li>";
                        }
                    }
                }
                //lstmenu.DataSource = dtmenu;
                //lstmenu.DataBind();
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

        protected void exitButton_Click(object sender, EventArgs e)
        {
            //CommonB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.Date, DateTime.Now.ToShortTimeString(), "0", 15);
            Session[sessionNames.userID_Karbar] = null;
            Response.Redirect("~/CommonUI/LoginRequestCMS.aspx",false);

        }

        protected void btn_home_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../../CommonUI/CommonCmsIntro.aspx");
        }
        protected void btn_changePassword_Click(object sender, EventArgs e)
        {
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = "../../../CommonUI/ChangePassword.aspx";
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
            windowManager.Windows.Add(widnow1);
            this.form1.Controls.Add(widnow1);
        }

        protected void btn_TraceMessage_Click(object sender, EventArgs e)
        {
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = "../../../CommonUI/TraceMessage.aspx";
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code 
            widnow1.Height = 400;
            widnow1.Width = 600;
            windowManager.Windows.Add(widnow1);
            windowManager.Height = 600;
            windowManager.Width = 800;
            this.form1.Controls.Add(widnow1);
        }
    }
}