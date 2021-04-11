using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Support.CMS
{
    public partial class SendEmail : System.Web.UI.Page
    {
        CommonBusiness ProfCommonBusiness = new CommonBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_SendEmail_Click(object sender, EventArgs e)
        {


            try
            {
                //SendsEmail(txt_Email.Text);
                SendsEmail("noreply@iauec.local", txt_Email.Text, "test", "این یک پیغام تستی است");
                RadWindowManager1.RadAlert("ایمیل با موفقیت ارسال شد", null, 100, "پیغام", "");
            }

            catch (Exception e1)
            {

                RadWindowManager1.RadAlert("خطا در ارسال پست الکترونیکی:" + e1.Message, null, 100, "خطا", "");
            }



        }
        public void SendsEmail(string strFrom, string strTo, string strSubject, string strBody)
        {
            MailMessage objMailMessage = new MailMessage();
            System.Net.NetworkCredential objSMTPUserInfo =
                new System.Net.NetworkCredential();
            SmtpClient objSmtpClient = new SmtpClient();

            try
            {
                objMailMessage.From = new MailAddress(strFrom);
                objMailMessage.To.Add(new MailAddress(strTo));
                objMailMessage.Subject = strSubject;
                objMailMessage.Body = strBody;

                objSmtpClient = new SmtpClient("192.168.1.7");
                objSmtpClient.Port = 25;
                objSMTPUserInfo = new System.Net.NetworkCredential
                ("noreply", "A_rohani");
                objSmtpClient.Credentials = objSMTPUserInfo;

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            { throw ex; }

            finally
            {
                objMailMessage = null;
                objSMTPUserInfo = null;
                objSmtpClient = null;
            }

        }
    }
}