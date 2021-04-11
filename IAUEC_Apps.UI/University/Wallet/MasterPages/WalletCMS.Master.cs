using IAUEC_Apps.Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Wallet.MasterPages
{
    public partial class WalletCMS : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx", true);
            else
            {
                Session[sessionNames.appID_Karbar] = "8";//???????????????????????????????????
                LoginBusiness logBusiness = new LoginBusiness();
                DataTable dt = new DataTable();
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
                        lstreportMenu.Text += "  <li><a href='" + "../../Exam/CMS/" + dt.Rows[i]["MenuLink"].ToString() + "?id=" + generaterandomstr(8) + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr(8) + "'><span>" + dt.Rows[i]["MenuName"].ToString() + "</span></a></li>";
                    }
                    else
                        lstreportMenu.Text += "  <li><a href='" + "../../Exam/CMS/" + dt.Rows[i]["MenuLink"].ToString() + "?id=" + generaterandomstr(8) + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr(8) + "'><span>" + dt.Rows[i]["MenuName"].ToString() + "</span></a></li>";
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../../CommonUI/CommonCmsIntro.aspx");
        }
        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session[sessionNames.userID_Karbar] = null;
            Response.Redirect("~/CommonUI/LoginRequestCMS.aspx", false);
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
    }
}