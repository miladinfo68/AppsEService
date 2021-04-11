using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.DTO.EmailClasses;
using IAUEC_Apps.Business.university.Request;

using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.CommonClasses;
using System.Configuration;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class LoginRequestCMS : System.Web.UI.Page
    {
        CommonBusiness CommonBusiness = new CommonBusiness();
        //Request_StudentCartDAO DAO = new Request_StudentCartDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "نسخه: " + ConfigurationManager.AppSettings["ApplicationVersion"];
        }

        protected void ClickedME(object sender, EventArgs e)
        {
            List<LoginDTO> userDTO = new List<LoginDTO>();

            DataTable dt = new DataTable();
            if (Page.IsValid)
            {


                LoginBusiness lngB = new LoginBusiness();
                userDTO = lngB.Get_UserLogin(UserName.Text, password.Text);


                if (userDTO.Count > 0)
                {
                    Session[sessionNames.userID_Karbar] = userDTO[0].UserId;
                    Session["Enable"] = userDTO[0].Enable;
                    Session[sessionNames.userName_Karbar] = userDTO[0].Name;
                    Session[sessionNames.roleID] = userDTO[0].RoleId;
                    Session[sessionNames.sectionID] = userDTO[0].sectionId;
                    Session[sessionNames.user_Karbar] = userDTO[0].UserName;
                    Session[sessionNames.roleText] = userDTO[0].RoleName;
                    if (Convert.ToBoolean(Session["Enable"].ToString()))
                    {
                            var gm = CommonBusiness.GetGroupMangerInformation(userDTO[0].UserName);
                        if (gm.Count > 0)
                        {
                            Business.university.Research.ResearchBusiness Rbusiness = new Business.university.Research.ResearchBusiness();
                            Session["IsGroupManger"] = true;
                            var lgn = Rbusiness.GetTeacherUserPass(int.Parse(gm[0].ProfessorCode.ToString()));
                            Session["Password"] = CommonBusiness.DecryptPass(lgn.Rows[0]["Password"].ToString());
                            Session["IsOstad"] = true;
                            Session["UserType_lms"] = 2;
                        }
                        Session["p"] = CommonBusiness.DecryptPass(userDTO[0].Password);
                        Session["UserType_lms"] = 1;
                        Response.Redirect("CommonCmsIntro.aspx");

                        //CommonBusiness.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.Date, DateTime.Now.ToShortTimeString(), "0", 14);

                    }
                    else
                    {
                        errormsg.Visible = true;
                        errormsg.InnerText = "نام کاربری یا رمز عبور صحیح نمی باشد";
                    }
                    


                }
                else
                    errormsg.Visible = true;
                errormsg.InnerText = "نام کاربری یا رمز عبور صحیح نمی باشد";
            }
            else
            {
                errormsg.Visible = true;
                errormsg.InnerText = "کد امنیتی صحیح وارد نشده است";
            }
        }
    }
}