using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using IAUEC_Apps.Business.Email;
using System.Configuration;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.EmailClasses;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.UI.EmailReg.Pages
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        //Email_ClassDAO da = new Email_ClassDAO();
        UserAccessBusiness userB = new UserAccessBusiness();
        StudentBuisiness business = new StudentBuisiness();
        CommonBusiness commonBusiness = new CommonBusiness();
        Email_ClassBusiness Ebusiness = new Email_ClassBusiness();
        ActiveDirectoryBusiness actBbusiness = new ActiveDirectoryBusiness();

       
        List<StudentDTO> dtinf = new List<StudentDTO>();
       
        protected void Page_Load(object sender, EventArgs e)
        {



            DataTable dtsecCode = new DataTable();
            dtsecCode = business.GetStSecurityCode(Session[sessionNames.userID_StudentOstad].ToString());
            if (dtsecCode.Rows[0]["SecurityCodeStatus"].ToString() =="" || Boolean.Parse(dtsecCode.Rows[0]["SecurityCodeStatus"].ToString()) == true)
            {
                div_Reset.Visible = false;
                div_NationalCode.Visible = true;
                

            }
            else
            {
                div_Reset.Visible = true;
                div_NationalCode.Visible = false;
                
            }

        }

        protected void btn_SaveNationalCode_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt = da.CheckEmailStudent_ByStcode(Session["User"].ToString());
            DataTable dtsecCode = new DataTable();
            dtsecCode = business.GetStSecurityCode(Session[sessionNames.userID_StudentOstad].ToString());

            dtinf = business.Giveinfo(Session[sessionNames.userID_StudentOstad].ToString());
            if (dtinf[0].idd_Meli.ToString() == txt_NationalCode.Text)
            {
                
               string mobile = business.GetMobileByStcode(Session[sessionNames.userID_StudentOstad].ToString());
               if (mobile == "")
               {
                   lbl_Message.Visible = true;
                   lbl_Message2.Visible = true;
                   lnk_Edit.Visible = true;
               }
               else 
               {
                   string SecurityCode = generaterandomstr(5);
                   Ebusiness.UpdateSecurityCode(Session[sessionNames.userID_StudentOstad].ToString(), SecurityCode);

                   DataTable dtMessage = commonBusiness.GetAppIDMessage(1, 2, 1, 5);
                   string smsText = dtMessage.Rows[0]["Text"].ToString() + " " + SecurityCode + "\r\n" + "معاونت فنی دانشگاه آزاد اسلامی واحدالکترونیکی";
                    //
                    //string Result = commonBusiness.SendSMSByMobile(mobile, smsText, username, pass, source, uri);
                    // string Result = commonBusiness.sendSMS(mobile, smsText);

                    // string codeAsanak = Result.Substring(1, (Result.Length) - 2);
                    //string status = commonBusiness.ShowStatusSMS(codeAsanak);
                    //if ( status.Substring(12, ( status.Length) - 15) == "NotFound")
                    //{
                    //    string ss = "-1";
                    //    int statusmsg = Convert.ToInt32(ss);
                    //    DataTable dt = new DataTable();
                    //    DataTable dtmessageStatus = commonBusiness.GetMessage(ss);
                    //    int id_msg = int.Parse(dtmessageStatus.Rows[0]["ID"].ToString());
                    //    commonBusiness.LogStatusMessage(Session[sessionNames.userID_StudentOstad].ToString(), codeAsanak, mobile, statusmsg, dtMessage.Rows[0]["ID"].ToString(), int.Parse(dtMessage.Rows[0]["ID"].ToString()));
                    //}
                    //else
                    //{
                    //    string ss = (status.Substring(32, (status.Length) - 104));
                    //     ss = Regex.Replace(ss, @"[^\d]", "");
                    //     int statusmsg = Convert.ToInt32(ss);
                    //    DataTable dtmessageStatus = commonBusiness.GetMessage(ss);
                    //    //int id_msg = int.Parse(dtmessageStatus.Rows[0]["ID"].ToString());
                    //    //string date = DateTime.Now.ToString();
                    //    commonBusiness.LogStatusMessage(Session[sessionNames.userID_StudentOstad].ToString(), codeAsanak, mobile, statusmsg, dtMessage.Rows[0]["ID"].ToString(), int.Parse(dtMessage.Rows[0]["ID"].ToString()));
                    //}
                    //
                    bool sentSMS; string smsStatusText;
                    string asanak=commonBusiness.sendSMS(mobile, smsText, out sentSMS, out smsStatusText);
                    int asanakStatus = commonBusiness.getAsanakStatusID(asanak);
                    commonBusiness.LogStatusMessage(Session[sessionNames.userID_StudentOstad].ToString(), asanak, mobile, asanakStatus, smsStatusText, int.Parse(dtMessage.Rows[0]["ID"].ToString()));

                    //string Result = commonBusiness.SendSMSByMobile(mobile, smsText, username, pass, source, uri);
                    //string codeAsanak = Result.Substring(2, (Result.Length) - 4);
                    //string status = commonBusiness.ShowStatusSMS(codeAsanak, username, pass, uriStatus);
                    //string ss = (status.Substring(32, (status.Length) - 104));
                    //int statusmsg = Convert.ToInt32(ss);
                    //DataTable dtmessageStatus = commonBusiness.GetMessage(ss);
                    //commonBusiness.LogStatusMessage(Session["User"].ToString(), codeAsanak, mobile, statusmsg, dtmessageStatus.Rows[0][0].ToString(), int.Parse(dtMessage.Rows[0]["ID"].ToString()));
                    div_lnkReqSecurityCode.Visible = false;
                   div_NationalCode.Visible = false;
                   div_Reset.Visible = true;
                   rwm.RadAlert("کد امنیتی برای شما ارسال خواهد شد، از این کد برای تغییر رمز عبور خود استفاده نمایید", null, 100, "خطا", "");
                  
                       
               }

            }
            else
            {
                rwm.RadAlert("کد ملی وارد شده صحیح نمی باشد", null, 100, "خطا", "");
                //اگر کد ملی صحیح نباشد پیغام بدهد، پیغام در ویندوز بیاید نه به صورت متنی
            }
        }
        public string generaterandomstr(int count)
        {
            var chars = "0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            DataTable dtsecCode = new DataTable();
            dtsecCode = business.GetStSecurityCode(Session[sessionNames.userID_StudentOstad].ToString());
            if (txt_NewPass.Text == "" || txt_NewPassRepeat.Text == "")
            {
                rwm.RadAlert("رمز عبور را وارد نمایید"
                    , 500, 200, "خطا", "");

            }
            else if (txt_SecurityCode.Text == "")
            {
                rwm.RadAlert("کد امنیتی را وارد نمایید", 500, 200, "خطا", "");
            }
            else if (txt_NewPass.Text != txt_NewPassRepeat.Text)
            {
                rwm.RadAlert("کلمه عبور و تکرار کلمه عبور مطابقت ندارد"
                                 , 500, 200, "خطا", "");
            }
            else if (!commonBusiness.CheckPasswordIsValidate(txt_NewPass.Text, dtsecCode.Rows[0]["Email_Address"].ToString()))
            {
                rwm.RadAlert("کلمه عبور وارد شده مطابق قوانین ذکر شده نمی باشد"
                    , 500, 200, "خطا", "");
            }
            else
            {
                if (dtsecCode.Rows[0]["SecurityCode"].ToString() == txt_SecurityCode.Text)
                { 
                    string pass = userB.EncryptPass(txt_NewPass.Text);
                    Ebusiness.UpdateEmailPass(Session[sessionNames.userID_StudentOstad].ToString(), pass);
                    actBbusiness.ResetPassword(dtsecCode.Rows[0]["Email_Address"].ToString(), txt_NewPass.Text);
                    commonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 17, "تغییر رمز عبور");
                    rwm.RadAlert("رمز عبور شما با موفقیت تغییر یافت", 500, 200, "پیام", "CallBackConfirmok");
                }
                else
                {
                    rwm.RadAlert("کد امنیتی صحیح نمی باشد", 500, 200, "خطا", "");
                    div_lnkReqSecurityCode.Visible = true;
                    div_NationalCode.Visible = false;
                    div_Reset.Visible = false;

                }
            } 
        }

        protected void lnk_ReqSeqCode_Click(object sender, EventArgs e)
        {
            div_Reset.Visible = false;
            div_NationalCode.Visible = true;
            div_lnkReqSecurityCode.Visible = false;
        }
    }
}