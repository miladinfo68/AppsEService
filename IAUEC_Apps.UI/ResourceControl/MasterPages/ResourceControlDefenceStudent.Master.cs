using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.ResourceControl.MasterPages
{
    public partial class ResourceControlDefenceStudent : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx");
            //else if (!IsPostBack)
            //    usernamelbl.InnerText = Session["userName"].ToString();


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx");
            //else if (!IsPostBack)
            //    usernamelbl.InnerText = Session["userName"].ToString();
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



        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../CommonUI/IntroPage.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session[sessionNames.userID_StudentOstad] = null;
            Response.Redirect("~/CommonUI/Login.aspx");
        }

    }
}