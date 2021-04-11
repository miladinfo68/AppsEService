using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.DTO.CommonClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class TuitionPayment : System.Web.UI.Page
    {
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];

        public static string PayDate = "";
        public static string PayTime = "";
        CommonBusiness cmnb = new CommonBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.Form["stcode"]) || string.IsNullOrEmpty(Request.Form["amount"]))
                {
                    pnlError.Visible = true;
                    pnlWaiting.Visible = false;
                }
                else
                {
                    pnlError.Visible = false;
                    pnlWaiting.Visible = true;

                    SetDefaultDateTime();
                    string result;
                    long orderid;
                    PaymentDTO pay = new PaymentDTO();
                    bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                    pay.Amount = Convert.ToInt64(Request.Form["amount"]);
                    pay.PayDate = PayDate + "_" + PayTime;
                    pay.stcode = Request.Form["stcode"];
                    pay.bankId = 2;
                    pay.tterm = ConfigurationManager.AppSettings["Term"];
                    pay.Description = "پرداخت شهریه";

                    result = bmp.TuitionPayment(pay, out orderid);
                    String[] resultArray = result.Split(',');
                    if (resultArray[0] == "0")
                        ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
                }
            }
        }

        void SetDefaultDateTime()
        {
            PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        }
    }
}