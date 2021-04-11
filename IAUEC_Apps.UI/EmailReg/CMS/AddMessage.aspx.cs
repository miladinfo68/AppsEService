using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.EmailReg.CMS
{
    public partial class AddMessage : System.Web.UI.Page
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
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
        }

        protected void BtnReg_Click(object sender, EventArgs e)
        {
            try
            {
                Email_ConnectBusiness con = new Email_ConnectBusiness();
                con.CreateEmailConnect(txt_Sms.Text, Emailtxt.Content, int.Parse(ddl_Status.SelectedValue));
                RadWindowManager1.RadAlert("اطلاعات با موفقیت ثبت شد", null, 50, "پیام", null);
                txt_Sms.Text = "";
                Emailtxt.Content = "";


                CommonBusiness cmnb = new CommonBusiness();
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 30, "");
           
            }
            catch
            {
                RadWindowManager1.RadAlert("خطا در ثبت", null, 50, "پیام", null);
            }
        }
    }
}