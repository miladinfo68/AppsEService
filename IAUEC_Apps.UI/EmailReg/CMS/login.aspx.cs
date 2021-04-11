using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.DTO.EmailClasses;

namespace IAUEC_Apps.UI.EmailReg.CMS
{
    public partial class login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

       
        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            List<User_LoginDTO> userDTO = new List<User_LoginDTO>();
            User_LoginBusiness ulB = new User_LoginBusiness();
            DataTable dt = new DataTable();
            userDTO = ulB.User_Login(UserName.Text, password.Text);
            if (userDTO.Count > 0)
            {
                Session[sessionNames.userID_Karbar] = userDTO[0].id.ToString();
                Session["RoleId"] = userDTO[0].RoleID.ToString();
                if (userDTO[0].RoleID.ToString() == "1")
                    Response.Redirect("List_AfterStudentRequest.aspx");
                if (userDTO[0].RoleID.ToString() == "2")
                    Response.Redirect("List_FinalEmailOk.aspx");
                if (userDTO[0].RoleID.ToString() == "3")
                    Response.Redirect("List_AfterStudentRequest.aspx");

            }
            else
                errormsg.InnerText = "نام کاربری یا رمز عبور صحیح نمی باشد";
        }
    }
}