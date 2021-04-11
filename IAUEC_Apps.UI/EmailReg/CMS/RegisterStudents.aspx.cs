using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.UI.SMS_WebReference;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.EmailClasses;
using System.Data;
using System.Configuration;
using IAUEC_Apps.Business.university.Support;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.UI.EmailReg.CMS
{
    public partial class RegisterStudents : System.Web.UI.Page
    {

        CommonBusiness cmnb = new CommonBusiness();
        PassProfessorBusiness EmailBusiness = new PassProfessorBusiness();

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                   

                    StudentBuisiness stB = new StudentBuisiness();
                    Email_ClassBusiness emB = new Email_ClassBusiness();
                    Email_Class emDTO = new Email_Class();
                  
                    Email_ConnectBusiness conB = new Email_ConnectBusiness();
                    List<Email_ConnectDTO> conDTO = new List<Email_ConnectDTO>();

                    string RequestID = Session["RequestID"].ToString();
                    //emdto to dt
                    emDTO = emB.Email_Reg_Byid(int.Parse(Session["RequestID"].ToString()));
                    string stcode = emDTO.Stcode;
                    string MailText = "<html><div dir='rtl'>"+cmnb.GetAppIDMessage(1, 2, 1, 3).Rows[0]["Text"].ToString() + "</br>" + "معاونت فنی دانشگاه آزاد اسلامی واحدالکترونیکی"+"</div></html>";
                    //ramezaninan-940409-start
                    DataTable dt_Message = cmnb.GetAppIDMessage(0, 2, 1, 3);
                    string smsText = dt_Message.Rows[0]["Text"].ToString() + "\r\n" + "معاونت فنی دانشگاه آزاد اسلامی واحدالکترونیکی";
                    //ramezaninan-940409-end
                    string error ;
                    if (stB.CreateUser_ActiveDirectory(stcode,out error))
                    {
                        emB.Update_Request(RequestID, "-", 3);                       
                        int contype = emDTO.ConnectType;
                        cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 8, stcode, int.Parse(RequestID));

                        DataTable dtMessage = new DataTable();
                      
                        if (contype == 0)
                        {
                            
                           // cmnb.SendEmail(emDTO.CEMAIL, "سامانه ایجاد پست الکترونیکی دانشگاه آزاد اسلامی واحد الکترونیکی", MailText);
                            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 27, stcode + "-status3", int.Parse(RequestID));
                        }                            
                        else if (contype == 1)
                        {
                            
                            bool sentSMS; string smsStatusText;
                            lbl_Resault.Text = cmnb.sendSMS(emDTO.Mobile, smsText, out sentSMS, out smsStatusText);
                            int asanakStatus = cmnb.getAsanakStatusID(lbl_Resault.Text);
                            cmnb.LogStatusMessage(stcode, lbl_Resault.Text, emDTO.Mobile, asanakStatus, smsStatusText, int.Parse(dt_Message.Rows[0]["ID"].ToString()));

                            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 28, stcode + "-status3", int.Parse(RequestID));
                        }
                        else
                        {
                            //send sms
                            // سامانه ارسال پیامک از طریق وب سرویس آسانک
                            //ramezaninan-940409-start
                            //lbl_Resault.Text = cmnb.SendSMSByMobile(emDTO.Mobile, smsText, username, pass, source, uri);
                            bool sentSMS;string smsStatusText;
                            lbl_Resault.Text = cmnb.sendSMS(emDTO.Mobile, smsText,out sentSMS,out smsStatusText);
                            int asanakStatus = cmnb.getAsanakStatusID(lbl_Resault.Text);
                            cmnb.LogStatusMessage(stcode, lbl_Resault.Text, emDTO.Mobile, asanakStatus, smsStatusText, int.Parse(dt_Message.Rows[0]["ID"].ToString()));
                            

                            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 29, stcode + "-status3", int.Parse(RequestID));
                        }
                        Response.Redirect("List_AfterStudentRequest.aspx?id=" + generaterandomstr(11) + "@A" + Session[sessionNames.menuID].ToString() + "-" + generaterandomstr(2), false);

                    }
                    else
                    {
                        lbl_Resault.Text = error;
                        lbl_Resault.Visible = true;
                    }
                }
                catch
                {
                    //Response.Write(ex.Message);
                    //Response.Redirect("List_AfterStudentRequest.aspx?id=" + generaterandomstr(11) + "@A" + Session["menuId"].ToString() + "-" + generaterandomstr(2));
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