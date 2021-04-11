using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.UI.SMS_WebReference;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.Business.Common;
using System.Data;
using System.Configuration;
using IAUEC_Apps.DTO.EmailClasses;
using IAUEC_Apps.Business.university.Support;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.UI.EmailReg.CMS
{
    public partial class RequestRejected_Act : System.Web.UI.Page
    {

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    CommonBusiness cmnb = new CommonBusiness();
                    
                    PassProfessorBusiness EmailBusiness = new PassProfessorBusiness();

                    
                    string RequestID = Session["RequestID"].ToString();
                    StudentBuisiness stB = new StudentBuisiness();
                    Email_ClassBusiness emB = new Email_ClassBusiness();
                    Email_Class emDTO = new Email_Class();
                    DataTable dt = new DataTable();
                    Email_ConnectBusiness conB = new Email_ConnectBusiness();
                    List<Email_ConnectDTO> conDTO = new List<Email_ConnectDTO>();

                    emDTO = emB.Email_Reg_Byid(int.Parse(Session["RequestID"].ToString()));
                    string stcode = emDTO.Stcode;
                    
                    emB.Update_Request(RequestID, Session["Description"].ToString(), 2);
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 14, stcode, int.Parse(RequestID));

                    int contype = emDTO.ConnectType;
                    string MailText = "<html><div dir='rtl'>" + cmnb.GetAppIDMessage(1, 2, 1, 2).Rows[0]["Text"].ToString() + "<br/>" + Session["Description"].ToString() + "</br>" + "معاونت فنی دانشگاه آزاد اسلامی واحدالکترونیکی" + "</div></html>";
                    //ramezanian-940409-start
                    DataTable dtMessage = cmnb.GetAppIDMessage(0, 2, 1, 2);
                    string smsText = dtMessage.Rows[0]["Text"].ToString() + "\r\n" + "معاونت فنی دانشگاه آزاد اسلامی واحدالکترونیکی";
                    //ramezaninan-940409-end
                    if (contype == 0)
                    {

                        cmnb.SendEmail(emDTO.CEMAIL, "سامانه ایجاد پست الکترونیکی دانشگاه آزاد اسلامی واحد الکترونیکی", MailText);
                        cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 27, stcode + "-status2", int.Parse(RequestID));
                    }
                    else if (contype == 1)
                    {

                        //send sms
                        // ارسال پیامک از طریق وب سرویس آسانک
                        //ramezanian-940409-start
                        //lbl_Resault.Text = cmnb.SendSMSByMobile(emDTO.Mobile, smsText, username, pass, source, uri);
                        //lbl_Resault.Text = cmnb.sendSMS(emDTO.Mobile, smsText);
                        //string codeAsanak = lbl_Resault.Text.Substring(1, (lbl_Resault.Text.Length) - 2);
                        //lbl_Status.Text = cmnb.ShowStatusSMS(codeAsanak);
                        //if (lbl_Status.Text.Substring(12, (lbl_Status.Text.Length) - 15) == "NotFound")
                        //{
                        //    string ss = "-1";
                        //    int statusmsg = Convert.ToInt32(ss);
                        //    DataTable dtmessageStatus = cmnb.GetMessage(ss);
                        //    cmnb.LogStatusMessage(stcode, codeAsanak, emDTO.Mobile, statusmsg, dtmessageStatus.Rows[0][0].ToString(), int.Parse(dtMessage.Rows[0]["ID"].ToString()));
                        //}
                        //else
                        //{
                        //    string ss = (lbl_Status.Text.Substring(32, (lbl_Status.Text.Length) - 104));
                        //    ss = Regex.Replace(ss, @"[^\d]", "");
                        //    int statusmsg = Convert.ToInt32(ss);
                        //    DataTable dtmessageStatus = cmnb.GetMessage(ss);
                        //    cmnb.LogStatusMessage(stcode, codeAsanak, emDTO.Mobile, statusmsg, dtmessageStatus.Rows[0][0].ToString(), int.Parse(dtMessage.Rows[0]["ID"].ToString()));
                        //}
                        //ramezaninan-940409-end
                        bool sentSMS; string smsStatusText;
                        lbl_Resault.Text = cmnb.sendSMS(emDTO.Mobile, smsText, out sentSMS, out smsStatusText);
                        int asanakStatus = cmnb.getAsanakStatusID(lbl_Resault.Text);
                        cmnb.LogStatusMessage(stcode, lbl_Resault.Text, emDTO.Mobile, asanakStatus, smsStatusText, int.Parse(dtMessage.Rows[0]["ID"].ToString()));

                        cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 28, stcode + "-status2", int.Parse(RequestID));
                    }
                    else
                    {

                        //send sms
                        //از طریق آسانک
                        //ramezaninan-940409-start
                        //lbl_Resault.Text = cmnb.SendSMSByMobile(emDTO.Mobile, smsText, username, pass, source, uri);
                        //lbl_Resault.Text = cmnb.sendSMS(emDTO.Mobile, smsText);

                        //string codeAsanak = lbl_Resault.Text.Substring(1, (lbl_Resault.Text.Length) - 2);
                        //lbl_Status.Text = cmnb.ShowStatusSMS(codeAsanak);
                        //if (lbl_Status.Text.Substring(12, (lbl_Status.Text.Length) - 15) == "NotFound")
                        //{
                        //    string ss = "-1";
                        //    int statusmsg = Convert.ToInt32(ss);
                        //    DataTable dtmessageStatus = cmnb.GetMessage(ss);
                        //    cmnb.LogStatusMessage(stcode, codeAsanak, emDTO.Mobile, statusmsg, dtmessageStatus.Rows[0][0].ToString(), int.Parse(dtMessage.Rows[0]["ID"].ToString()));
                        //}
                        //else
                        //{
                        //    string ss = (lbl_Status.Text.Substring(32, (lbl_Status.Text.Length) - 104));
                        //    ss = Regex.Replace(ss, @"[^\d]", "");
                        //    int statusmsg = Convert.ToInt32(ss);
                        //    DataTable dtmessageStatus = cmnb.GetMessage(ss);
                        //    cmnb.LogStatusMessage(stcode, codeAsanak, emDTO.Mobile, statusmsg, dtmessageStatus.Rows[0][0].ToString(), int.Parse(dtMessage.Rows[0]["ID"].ToString()));
                        //}

                        bool sentSMS; string smsStatusText;
                        lbl_Resault.Text = cmnb.sendSMS(emDTO.Mobile, smsText, out sentSMS, out smsStatusText);
                        int asanakStatus = cmnb.getAsanakStatusID(lbl_Resault.Text);
                        cmnb.LogStatusMessage(stcode, lbl_Resault.Text, emDTO.Mobile, asanakStatus, smsStatusText, int.Parse(dtMessage.Rows[0]["ID"].ToString()));
                        //ramezaninan-940409-end


                        //send email

                        cmnb.SendEmail(emDTO.CEMAIL, "سامانه ایجاد پست الکترونیکی دانشگاه آزاد اسلامی واحد الکترونیکی", MailText);

                        cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 29, stcode + "-status2", int.Parse(RequestID));
                    }
                    Response.Redirect("List_AfterStudentRequest.aspx?id=" + generaterandomstr(11) + "@A" + Session[sessionNames.menuID].ToString() + "-" + generaterandomstr(2));
                }
                catch (Exception)
                {
                    Response.Redirect("List_AfterStudentRequest.aspx?id=" + generaterandomstr(11) + "@A" + Session[sessionNames.menuID].ToString() + "-" + generaterandomstr(2));
                }
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
    }
}