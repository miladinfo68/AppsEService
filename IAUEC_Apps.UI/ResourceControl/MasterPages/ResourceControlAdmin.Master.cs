using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using System.Data;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.ResourceControl.MasterPages
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
            else
            {
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
                    dr["linkCode"] = generaterandomstr() + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr();
                    dtmenu.Rows.Add(dr);
                }
                lstmenu.DataSource = dtmenu;
                lstmenu.DataBind();
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
    }
}