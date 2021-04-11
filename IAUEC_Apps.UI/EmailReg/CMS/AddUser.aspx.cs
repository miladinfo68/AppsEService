using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Email;

namespace IAUEC_Apps.UI.EmailReg.CMS
{
    public partial class AddUser : System.Web.UI.Page
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
                UserAccessBusiness useraccessB = new UserAccessBusiness();
                User_LoginBusiness userLoginB = new User_LoginBusiness();
                if (ddl_Role.SelectedValue != "0")
                {
                    userLoginB.Insert_NewUser(txt_Name.Text, txt_UserName.Text, useraccessB.EncryptPass(txt_Pass.Text), int.Parse(ddl_Role.SelectedValue));
                    RadWindowManager1.RadAlert("کاربر جدید با موفقیت ثبت شد", null, 50, "پیام", "");
                }
                else
                    RadWindowManager1.RadAlert("سمت انتخاب نشده است", null, 50, "هشدار", "");

            }
            catch
            {
            }
        }
    }
}