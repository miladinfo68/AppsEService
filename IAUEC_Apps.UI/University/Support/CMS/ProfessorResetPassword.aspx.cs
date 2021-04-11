using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Support.CMS
{
    public partial class ProfessorResetPassword : System.Web.UI.Page
    {
        ResetPasswordBusiness _resetPasswordBusiness = new ResetPasswordBusiness();
        CommonBusiness _commonBusiness = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            manageAccessControl();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            var professorCode = Convert.ToDecimal(txtProfessorCode.Text.Trim());
            var newPassword = txtNewPassword.Text.Trim();
            var isProfessorCodeExist = _resetPasswordBusiness.IsProfessorCodeExist(professorCode);
            if (!isProfessorCodeExist)
            {
                rwm_Validations.RadAlert("کد استاد وارد شده وجود ندارد.", null, 100, "تغییر رمز عبور", "");
                return;
            }
            var result = _resetPasswordBusiness.ChangeProfessorPassword(professorCode, newPassword);
            if (!result)
                rwm_Validations.RadAlert("ذخییره ی اطلاعات با مشکل مواجه شده است.", null, 100, "تغییر رمز عبور", "");
            rwm_Validations.RadAlert("اطلاعات با موفقیت ذخیره شد.", null, 100, "تغییر رمز عبور", "");
            if (result)
            {
                var msg = $"پسورد شما به {newPassword} تغییر پیدا کرد \r\n دانشگاه آزاد-واحد الکترونیکی";
                bool sentSMS;
                string smsStatus;
                _commonBusiness.sendSMS(2, txtProfessorCode.Text.Trim(), msg, out sentSMS, out smsStatus);
            }


        }
        private void manageAccessControl()
        {
            if (Request.QueryString["id"] != null)
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
                AccessControl.MenuId = menuId;
                Session[sessionNames.menuID] = menuId;
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
            else
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
        }
    }
}