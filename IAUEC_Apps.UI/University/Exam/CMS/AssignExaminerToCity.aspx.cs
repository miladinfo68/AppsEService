using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.university.Exam;
using System.Configuration;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class AssignExaminerToCity : System.Web.UI.Page
    {
        ExamBusiness ExamBusiness = new ExamBusiness();
        IAUEC_Apps.Business.Common.CommonBusiness CB = new IAUEC_Apps.Business.Common.CommonBusiness();
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                DataTable dtExamPlace = new DataTable();
                dtExamPlace = ExamBusiness.GetCityNameFilterByExaminerExamPlace();

                ddl_city.DataSource = dtExamPlace;
                ddl_city.DataTextField = "Name_City";
                ddl_city.DataValueField = "ID";
                ddl_city.DataBind();
                ddl_city.Items.Insert(0,new ListItem("انتخاب کنید"));
                //ddl_city.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_city.Items[ddl_city.Items.Count - 1].Selected = true;

                ddl_user.DataSource = ExamBusiness.GetExaminerUser();
                ddl_user.DataTextField = "UserName";
                ddl_user.DataValueField = "UserId";
                ddl_user.DataBind();
                ddl_user.Items.Insert(0, new ListItem("انتخاب کنید"));
                //ddl_user.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_user.Items[ddl_user.Items.Count - 1].Selected = true;
                grd_Examiner.DataSource = ExamBusiness.GetExaminer();
                grd_Examiner.DataBind();
            }
        }
        public string generaterandomstr(int count)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            ExamBusiness.Insert_ExaminerInfo(int.Parse(ddl_user.SelectedValue), int.Parse(ddl_city.SelectedValue), txt_username.Text,txt_mobile.Text,txt_Email.Text,date_input_1.Value,date_input_2.Value,generaterandomstr(7),int.Parse(ddl_user.SelectedValue));
            CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 98, "تخصیص شهر به ممتحن", int.Parse(ddl_user.SelectedValue));
            grd_Examiner.DataSource = ExamBusiness.GetExaminer();
            grd_Examiner.DataBind();
            DataTable dtExamPlace = new DataTable();
            dtExamPlace = ExamBusiness.GetCityNameFilterByExaminerExamPlace();

            ddl_city.DataSource = dtExamPlace;
            ddl_city.DataTextField = "Name_City";
            ddl_city.DataValueField = "ID";
            ddl_city.DataBind();
            ddl_city.Items.Insert(0, new ListItem("انتخاب کنید"));
            //ddl_city.Items.Add(new ListItem("انتخاب کنید", "0"));
            //ddl_city.Items[ddl_city.Items.Count - 1].Selected = true;

            ddl_user.DataSource = ExamBusiness.GetExaminerUser();
            ddl_user.DataTextField = "UserName";
            ddl_user.DataValueField = "UserId";
            ddl_user.DataBind();
            ddl_user.Items.Insert(0, new ListItem("انتخاب کنید"));
            //ddl_user.Items.Add(new ListItem("انتخاب کنید", "0"));
            //ddl_user.Items[ddl_user.Items.Count - 1].Selected = true;
        }

        protected void grd_Examiner_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "SMS")
            {
                DTO.University.Exam.ExaminerDTO exmDTO = new DTO.University.Exam.ExaminerDTO();
                exmDTO = ExamBusiness.SendEmailSMSToExaminer(int.Parse(e.CommandArgument.ToString()));
                string Text = "با سلام " + "\r\n" + "جهت ورود به پست الکترونیکی به آدرس زیر می توانید از" + "\r\n" + "نام کاربری :" + exmDTO.euser + "\r\n" + "رمز عبور :" + exmDTO.epass + "استفاده نمایید." + "\r\n" + "لطفا با توجه به محرمانگی رمز در حفظ و نگهداری آن کوشا باشید " + "\r\n" + "معاونت فنی دانشگاه آزاد اسلامی واحد الکترونیکی" + "\r\n" + "https://mail.iauec.ac.ir/owa";
                //lbl_Resault.Text = CB.SendSMSByMobile(exmDTO.Mobile, Text, username, pass, source, uri);
                //lbl_Resault.Text = CB.sendSMS(exmDTO.Mobile, Text);
                //CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 119, "ارسال پیامک رمز سایت خدمات ممتحن", int.Parse(e.CommandArgument.ToString()));

                //if (lbl_Resault.Text != "1")
                //{
                //    string codeAsanak = lbl_Resault.Text.Substring(1, (lbl_Resault.Text.Length) - 2);
                //    Lbl_Status.Text = CB.ShowStatusSMS(codeAsanak);
                //    if (Lbl_Status.Text.Substring(12, (Lbl_Status.Text.Length) - 15) == "NotFound")
                //    {
                //        string ss = "-1";
                //        int status = Convert.ToInt32(ss);
                //        DataTable dt3 = new DataTable();
                //        dt3 = CB.GetMessage(ss);

                //    }
                //    else
                //    {
                //        string ss = (Lbl_Status.Text.Substring(32, (Lbl_Status.Text.Length) - 104));
                //        ss = Regex.Replace(ss, @"[^\d]", "");
                //        int status = Convert.ToInt32(ss);
                //        DataTable dt2 = new DataTable();
                //        dt2 = CB.GetMessage(ss);
                //        //rdw.RadAlert("رمز عبور برای شما از طریق پیامک و پست الکترونیکی ارسال گردید", null, null, "پیام", "");

                //    }
                //}
                bool sentSMS; string smsStatusText;
                CB.sendSMS(exmDTO.Mobile, Text, out sentSMS, out smsStatusText);

                

                // ارسال رمز مدیریت یاگیری
                //string Text2 = "با سلام" + "\r\n" + "جهت ورود به سامانه مدیریت یادگیری به آدرس زیر می توانید از" + "\r\n" + "نام کاربری :" + exmDTO.UserName + "\r\n" + "رمز عبور :" + exmDTO.UserName.ToString().Substring(0, 1) + "123@456" + "استفاده نمایید." + "\r\n" + "لطفا پس از ورود رمز عبور خودرا تغییر دهید " + "\r\n" + "معاونت فنی دانشگاه آزاد اسلامی واحد الکترونیکی" + "\r\n" + "http://lms952.iauec.ac.ir";
                //lbl_Resault.Text = CB.SendSMSByMobile(exmDTO.Mobile, Text2, username, pass, source, uri);
                //CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 119, "ارسال پیامک رمز سایت مدیریت یادگیری ممتحن", int.Parse(e.CommandArgument.ToString()));

                //if (lbl_Resault.Text != "1")
                //{
                //    string codeAsanak = lbl_Resault.Text.Substring(1, (lbl_Resault.Text.Length) - 2);
                //    Lbl_Status.Text = CB.ShowStatusSMS(codeAsanak, username, pass, uriStatus);
                //    if (Lbl_Status.Text.Substring(12, (Lbl_Status.Text.Length) - 15) == "NotFound")
                //    {
                //        string ss = "-1";
                //        int status = Convert.ToInt32(ss);
                //        DataTable dt3 = new DataTable();
                //        dt3 = CB.GetMessage(ss);

                //    }
                //    else
                //    {
                //        string ss = (Lbl_Status.Text.Substring(32, (Lbl_Status.Text.Length) - 104));
                //        int status = Convert.ToInt32(ss);
                //        DataTable dt2 = new DataTable();
                //        dt2 = CB.GetMessage(ss);
                //        //rdw.RadAlert("رمز عبور برای شما از طریق پیامک و پست الکترونیکی ارسال گردید", null, null, "پیام", "");

                //    }
                //}

            }
            if (e.CommandName == "Email")
            {
                DTO.University.Exam.ExaminerDTO exmDTO = new DTO.University.Exam.ExaminerDTO();
                exmDTO = ExamBusiness.SendEmailSMSToExaminer(int.Parse(e.CommandArgument.ToString()));

                string Text = "با سلام و احترام" + "</br>" + "به استحضار می رساند اطلاعات کاربری جنابعالی جهت ورود به سامانه خدمات الکترونیکی واحد الکترونیکی به شرح زیر میباشد :" + "</br>"
                + " جهت ورود به سامانه، آدرس زیر را در مرورگر خود وارد نموده و از نام کاربری و رمز عبور زیر استفاده نمایید." + "</br>" + "</br>"
                + "http://service.iauec.ac.ir/commonUI/loginrequestcms.aspx" + "</br>" + "نام کاربری :" + exmDTO.UserName + "</br>" + "رمز عبور :" + exmDTO.Pass + "</br>" + "</br>"
                + "1-  جهت دریافت سوالات و گزارشات روی بخش امتحانات کلیک نمایید" + "</br>" + "</br>"
                //+ "2- جهت ورود به سامانه مدیریت یادگیری (LMS) روی بخش سامانه مدیریت یادگیری کلیک نمایید و از نام کاربری و رمز عبور زیر استفاده نمایید." + "</br>"                
                //+   "نام کاربری :" + exmDTO.UserName + "</br>" + "رمز عبور :" + exmDTO.lms_Pass + "</br>" + "</br>"
                + "2- جهت ورود به سامانه مدیریت یادگیری (LMS) روی بخش سامانه مدیریت یادگیری کلیک نمایید ." + "</br>" + "</br>"
                + "تلفن پشتیبانی فنی : 02142863963 " + "</br>" + "ایمیل پشتیبانی:support@iauec.ac.ir" + "</br>" + "</br> "
                + "<b>***************" + "لطفا پس از اولین ورود به سامانه رمز عبور خود را تغییر دهید و با توجه به مسئولیت و محرمانگی رمز در حفظ و نگهداری آن کوشا باشید " 
                + "***************</b>" + " </br>" + "</br>" + "معاونت  فنی دانشگاه آزاد اسلامی واحد الکترونیکی";

                string desc = CB.SendEmail(exmDTO.Email, "نام کاربری و رمز عبور ", Text);
                CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 119, desc, int.Parse(e.CommandArgument.ToString()));

            }
        }
    }
}