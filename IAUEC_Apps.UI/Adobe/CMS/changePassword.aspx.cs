using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI
{
    public partial class changePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mId = Request.QueryString["id"].ToString();
            string[] id = mId.ToString().Split(new char[] { '@' });
            string menuId = "";
            for (int i = 0; i < id[1].Length; i++)
            {
                string s = id[1].Substring(i+1, 1);
                if (s != "-")
                    menuId += s;
                else
                    break;
            }
            AccessControl1.MenuId = menuId;
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
        }

        protected void btnchange_Click(object sender, EventArgs e)
        {
            LoginBusiness cmb=new LoginBusiness ();
            CommonBusiness CommonBusiness = new CommonBusiness();
            if (Session["p"].ToString() == txt_OldPass.Text)
            {
                if (txt_NewPass.Text == txt_ConfNewPass.Text)
                {
                    try
                    {
                        cmb.changePassword(CommonBusiness.EncryptPass(txt_NewPass.Text), int.Parse(Session[sessionNames.userID_Karbar].ToString()));
                        lbl_Message.Text = "رمز عبور با موفقیت تغییر پیدا کرد";
                    }
                    catch
                    {
                        lbl_Message.Text = "خطا در انجام عملیات";
                    }
                }
                else
                    lbl_Message.Text = "رمز عبور جدید صحیح وارد نشده است";
            }
            else
                lbl_Message.Text = "رمز فعلی اشتباه وارد شده است";
        }
    }
}