using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Support.CMS
{
    public partial class resetPasswordStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearchStudent_Click(object sender, EventArgs e)
        {
            searchStatus.InnerText = "";btnCreatePassword.Visible = false;
            if (txtStcode.Text.Trim() != "" && Business.Common.CommonBusiness.IsNumeric(txtStcode.Text))
            {
                Business.Common.LoginBusiness logBusiness = new Business.Common.LoginBusiness();

                var dt = logBusiness.SendUserPassword(txtStcode.Text.Trim(), txtNationalCode.Text.Trim(), "", true);
                if (dt.Rows.Count > 0)
                {
                    var lgn = logBusiness.User_Login(txtStcode.Text.Trim());
                    if (lgn != null && lgn.Password==null )
                    {
                        searchStatus.InnerText = "";
                        btnCreatePassword.Visible = true;
                    }
                    else
                    {
                        searchStatus.InnerText = "برای این شماره دانشجویی قبلا پسورد ساخته شده است.";
                    }
                }
                else
                {
                    searchStatus.InnerText = "دانشجویی با اطلاعات وارد شده یافت نشد";
                }
            }
            else
            {
                searchStatus.InnerText = "شماره دانشجویی وارد شده نامعتبر می باشد";
            }
            
        }

        protected void btnCreatePassword_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        private bool resetPassword(string stcode,string newPassword)
        {
            try
            {
                Business.Common.CommonBusiness cb = new Business.Common.CommonBusiness();
                return cb.insertStudentPassword(txtStcode.Text.Trim(), txtNationalCode.Text.Trim());
            }
            catch(Exception ex){
                return false;
            }
        }

        protected void rbResetPassword_Click(object sender, EventArgs e)
        {
            var res=resetPassword(txtStcode.Text.Trim(), txtNationalCode.Text.Trim());
            switch (res)
            {
                case true:
                    searchStatus.InnerText = "رمز برای دانشجو ایجاد شد.";
                    break;
                case false:
                    searchStatus.InnerText = "عملیات ایجاد رمز ناموفق بود. لطفا دوباره تلاش کنید.";
                    break;
            }
            btnCreatePassword.Visible = false;
        }
    }
}