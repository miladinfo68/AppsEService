using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Support;
using System.Data;
using System.Net.Mail;
using System.Net;
using IAUEC_Apps.DTO.EmailClasses;

namespace IAUEC_Apps.Business.university.Support
{
    public class PassProfessorBusiness
    {
        PassProfessorDAO Prof = new PassProfessorDAO();

        public DataTable SelectRowOstad(string CodeOstad, string NameOstad, string CodeMelli, IAUEC_Apps.DTO.University.Support.PassProfessorDTO.ostadType typeOfOstad)
        {
            switch (typeOfOstad)
            {
                case DTO.University.Support.PassProfessorDTO.ostadType.استاد_پژوهشی:
                    return Prof.GetOstadPazhuheshInformation(CodeOstad, NameOstad, CodeMelli);
                default:

                    return Prof.GetOstadInformation(CodeOstad, NameOstad, CodeMelli);

            }
            //return Prof.GetSelectRowOstad(CodeOstad, NameOstad, CodeMelli);
        }
        //public DataTable GetUserNamePassword(string mobile)
        //{
        //    return Prof.GetUserNamePassword(mobile);
        //}
        public DataTable GetSendMessage(int type, int App_ID, int Category, int Status)
        {
            return Prof.GetSendMessage(type, App_ID, Category, Status);
        }
        public DataTable GetTypeRequest()
        {
            return Prof.GetTypeRequest();
        }
        public bool SendEmail(int code1, string password, string email)
        {
            string to = email;
            string Username = "noreply";
            string Password = "A_rohani";
            string SmtpServer = "mail.iauec.ac.ir";
            string From = Username + "@iauec.local";
            SmtpClient s = new SmtpClient(SmtpServer);
            s.UseDefaultCredentials = false;
            s.Credentials = new NetworkCredential(From, Password);
            s.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage m1 = new MailMessage();
            m1.IsBodyHtml = true;
            m1.Body = "<HTML> " + " استاد گرامی : " + "<br/>" + " دسترسی شما به سیستم خدمات الکترونیکی به شرح زیر می باشد" +
             "<br/>" + "شناسه کاربری:  " + "<br/>" + "ins" + code1 + "رمز عبور" + password + "</HTML>";
            string text = m1.Body;
            MailMessage m = new MailMessage(From, to, "سیستم ثبت رایان نامه واحد الکترونیکی", text);
            s.Send(m);
            return true;
        }

        public DataTable GetProfessorInfoGroup(int Daneshkade, int Departman, IAUEC_Apps.DTO.University.Support.PassProfessorDTO.ostadType typeOfOstad)
        {
            switch (typeOfOstad)
            {
                case DTO.University.Support.PassProfessorDTO.ostadType.استاد_پژوهشی:
                    return Prof.GetInfoGroupProf_Pazhuhesh(Daneshkade, Departman);
                default:
                    return Prof.GetInfoGroupProf_Amuzesh(Daneshkade, Departman);
            }
        }
    }
}

