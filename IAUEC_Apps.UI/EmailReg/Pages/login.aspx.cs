using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.DTO.EmailClasses;
using System.Data;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.EmailReg.Pages
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (Session[sessionNames.userID_StudentOstad] != null)
                Response.Redirect("VerificationPersonalData.aspx");
        }

        protected void ClickedME(object sender, EventArgs e)
        {
          
                if (Page.IsValid)
                {

                    Email_ClassBusiness emB = new Email_ClassBusiness();
                    StudentBuisiness stB = new StudentBuisiness();
                    DataTable dt = new DataTable();
                    CommonBusiness cmnB = new CommonBusiness();
                    ActiveDirectoryBusiness adB = new Business.Common.ActiveDirectoryBusiness();
                    List<Email_Class> stDTO = new List<Email_Class>();
                    stDTO = emB.CheckEmailStudent_ByStcode(txt_UserName.Text.ToString());
                    bool check = stB.CheckUser(txt_UserName.Text.ToString(), txt_password.Text.ToString());
                    int Status = 0;

                    if (check == false)
                        errormsg.InnerText = "کاربر مورد نظر یافت نشد";
                    else if (dt != null && dt.Rows.Count > 0)
                    {
                        Status = int.Parse(dt.Rows[0]["Status"].ToString());
                        if (Status == 0)
                            errormsg.InnerText = "شما قبلا درخواست خود را ثبت کرده اید که در حال بررسی می باشد";
                        else if (Status > 1)
                            errormsg.InnerText = "آدرس پست الکترونیکی شما: " + dt.Rows[0]["Email_Address"] + " می باشد ";
                        else
                        {
                            Session[sessionNames.userID_StudentOstad] = txt_UserName.Text.ToString();

                            if (adB.Get_FindUser_SamAccountName(txt_UserName.Text.ToString()))
                                errormsg.InnerText = "کاربر گرامی، شما آدرس ایمیلی با نام دانشجویی خود دارید، درصورت تمایل  میتوانید درخواست جدیدی ثبت کنید ";

                            else
                                errormsg.InnerText = "درخواست پست الکترونیک قبلی شما رد شده، درصورت تمایل  میتوانید درخواست جدیدی ثبت کنید";

                        }
                    }
                    else if (check)
                    {
                        Session[sessionNames.userID_StudentOstad] = txt_UserName.Text.ToString();
                        if (adB.Get_FindUser_SamAccountName(txt_UserName.Text.ToString()))
                            errormsg.InnerText = "کاربر گرامی، شما آدرس ایمیلی با نام دانشجویی خود دارید، درصورت تمایل  میتوانید درخواست جدیدی ثبت کنید ";

                        else
                            Response.Redirect("VerificationPersonalData.aspx");
                    }
                    else
                        errormsg.InnerText = "کاربر مورد نظر یافت نشد";

                
            }
           
        }
    }
}