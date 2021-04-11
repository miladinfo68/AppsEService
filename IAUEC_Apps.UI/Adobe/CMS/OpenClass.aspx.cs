using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class OpenClass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mId = Request.QueryString["id"].ToString();
            string[] id = mId.ToString().Split(new char[] { '@' });
            string menuId = "";
            for (int i = 0; i < id[1].Length; i++)
            {
                string s = id[1].Substring(i + 1, 1);
                if (s != "-")
                    menuId += s;
                else
                    break;
            }
            AccessControl1.MenuId = menuId;
            Session[sessionNames.menuID] = menuId;
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
        }

        protected void btn_OpenClass_Click(object sender, EventArgs e)
        {
            try
            {
                ClassBusiness cls = new ClassBusiness();
                CommonBusiness cmnb = new CommonBusiness();
                cls.OpenClass(TextBox1.Text);
                rdw.RadAlert("عملیات با موفقیت انجام گرفت", null, null, "پیام", "");
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 0, "باز شدن دسترسی دانشجو",int.Parse(TextBox1.Text));
             
            }
            catch
            {
                rdw.RadAlert("خطا در انجام عملیات", null, null, "پیام", "");
            }
        }
    }
}