using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class ChangeStudentPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ViewState["lastUrl"] = Request.UrlReferrer.AbsoluteUri;
        }

        protected void btn_Change_Click(object sender, EventArgs e)
        {
            CommonBusiness cmb = new CommonBusiness();
            if (Session["password"].ToString() == txt_OldPass.Text)
            {
                if (txt_NewPass.Text == txt_ConfNewPass.Text && txt_NewPass.Text.Trim()!="")
                {
                    try
                    {
                        //if (cmb.ChangeStudentPassword(Session[sessionNames.userID_StudentOstad].ToString(), txt_NewPass.Text))
                        if (cmb.ChangeStudentPassword(Session[sessionNames.userID_StudentOstad].ToString(), txt_NewPass.Text))
                        {
                            lbl_Message.Text = "رمز عبور با موفقیت تغییر پیدا کرد";
                        }
                        else
                            lbl_Message.Text = "رمز عبور تغییر نکرد";
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
                lbl_Message.Text = "رمز جدید نمیتواند با رمز قبلی یکسان باشد";
        }


    }
}