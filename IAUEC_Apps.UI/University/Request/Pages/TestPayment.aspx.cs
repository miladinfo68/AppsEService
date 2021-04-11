using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.UI.ir.shaparak.bpm;


namespace IAUEC_Apps.UI.University.Request.Pages
{
    public partial class TestPayment : System.Web.UI.Page
    {
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["Mellat_Request_CallBackUrl"];
        public static readonly string TerminalId = ConfigurationManager.AppSettings["Mellat_TerminalId"];
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["UserPassword"];

        public static string RefId = "";
        public static string PayDate = "";
        public static string PayTime = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        void SetDefaultDateTime()
        {
            PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string result;

            long orderid;//= new bmp_PaymentBusiness().GenerateOrderId();


            SetDefaultDateTime();        
            bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();

            PaymentGatewayImplService bpService = new PaymentGatewayImplService();
            result = bmp.pay(1000, "99900999", out orderid, 0, 0);

            String[] resultArray = result.Split(',');

          
            string[] ReqIdArray = Session["ReqID"].ToString().Split(new char[] { ',' });
            string[] PayID = Session["PayID"].ToString().Split(new char[] { ',' });

          
            Session["ReqID"] = null;
            Session["PayID"] = null;

            if (resultArray[0] == "0")
                ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
        }
    }
}