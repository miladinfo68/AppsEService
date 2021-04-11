using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
//using IAUEC_Apps.Business.Conatct.Jobs;
//==============================
using System.Web.Management;
using System.Web.UI;

namespace IAUEC_Apps.UI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
          // var SendSmsSchedule = new SendSmsSchedule();
         //   SendSmsSchedule.Run();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            var httpException = ex as HttpException ?? ex.InnerException as HttpException;
            if (httpException == null) return;

            if (httpException.WebEventCode == WebEventCodes.RuntimeErrorPostTooLarge)
            {
                Response.Write("<Script Language='javascript'> alert('حجم فایل نمی تواند بیشتر از 2مگابایت باشد');</script>");
            }
            if (ex.InnerException != null)
            {
                var str = "Maximum request length exceeded.";
                if (string.Compare(ex.InnerException.Message.ToString(), str) == 0)
                {
                    Response.Write("<Script Language='javascript'> alert('طول فایل نباید بیشتر از 2مگابایت باشد');</script>");
                    Server.ClearError();
                }
            }    
           
        }

        protected void Session_End(object sender, EventArgs e)
        {
            string filePath = Session["viewstateFilPath"] as string;
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}