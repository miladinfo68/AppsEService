using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;
using System.Data;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Request.MasterPages
{
    public partial class CMSRequestMaster : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx",false);
            else
            {
                Session[sessionNames.appID_Karbar] = "5";
                LoginBusiness logBusiness = new LoginBusiness();
                //stName.InnerText = Session["name"].ToString();
                DataTable dt = new DataTable();
                DataTable dtmenu = new DataTable();
                dtmenu.Columns.Add("MenuLink");
                dtmenu.Columns.Add("linkCode");
                dtmenu.Columns.Add("icon");
                dtmenu.Columns.Add("MenuName");
                DataRow dr;

                dt = logBusiness.Get_Menu_ByUserIdAndAppId(int.Parse(Session[sessionNames.appID_Karbar].ToString()), int.Parse(Session[sessionNames.userID_Karbar].ToString()), int.Parse(Session["SectionId"].ToString()));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dtmenu.NewRow();
                    dr["MenuLink"] = dt.Rows[i]["MenuLink"].ToString();
                    dr["icon"] = dt.Rows[i]["icon"].ToString();
                    dr["MenuName"] = dt.Rows[i]["MenuName"].ToString();
                    dr["linkCode"] = generaterandomstr(11) + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr(2);
                    dtmenu.Rows.Add(dr);
                }
                lstmenu.DataSource = dtmenu;
                lstmenu.DataBind();

              


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
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx",false);
        }
        protected void changePass_ServerClick(object sender, EventArgs e)
        {
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            
            widnow1.NavigateUrl = "../../../CommonUI/ChangePassword.aspx";
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true;  
            windowManager.Windows.Add(widnow1);
            this.form1.Controls.Add(widnow1);
        }

        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../../CommonUI/CommonCmsIntro.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session[sessionNames.userID_Karbar] = null;

            Response.Redirect("~/CommonUI/LoginRequestCMS.aspx",false);

        }
    }
}