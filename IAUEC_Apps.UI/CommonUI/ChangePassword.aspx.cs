using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnchange_Click(object sender, EventArgs e)
        {
            LoginBusiness cmb = new LoginBusiness();
            CommonBusiness CommonBusiness = new CommonBusiness();     
            if (Session["p"].ToString() == txt_OldPass.Text)
            {
                if (txt_NewPass.Text == txt_ConfNewPass.Text)
                {
                    try
                    {
                        cmb.changePassword(CommonBusiness.EncryptPass(txt_NewPass.Text), int.Parse(Session[sessionNames.userID_Karbar].ToString()));
                        int appID = 0;
                        if (Session[sessionNames.appID_Karbar] != null)
                        {
                            appID = int.Parse(Session[sessionNames.appID_Karbar].ToString());
                        }
                        CommonBusiness.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), appID, 97, "تغییر رمز عبور");
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