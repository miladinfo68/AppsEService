using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using System.Data;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class login : System.Web.UI.Page
    {
        ExamBusiness ExamBusiness = new ExamBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //dt =ExamBusiness.UserLogin(txt_UserName.Text, txt_Pass.Text);
            if (dt.Rows.Count > 0)
            {
                Session["RoleId"] = dt.Rows[0]["RoleID"].ToString();
                Session[sessionNames.userID_Karbar] = dt.Rows[0]["UserID"].ToString();
                switch (Session["RoleId"].ToString())
                {
                    case "1":
                        Response.Redirect("classlistUI.aspx");
                        break;
                    case "2":
                        Response.Redirect("classlistUI.aspx");
                        break;
                    case "3":
                        Response.Redirect("ExamPresentList.aspx");
                        break;
                }
                
            }
            else
            {
                lbl_Msg.Text = "نام کاربری یا رمز عبور صحیح نمی باشد";
            }
        }
    }
}