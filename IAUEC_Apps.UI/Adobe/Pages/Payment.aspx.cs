using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.DTO.CommonClasses;
using System.Configuration;
using IAUEC_Apps.UI.ir.shaparak.bpm;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.Pages
{
    public partial class Payment : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public string PayDate { get; set; }

        public string PayTime { get; set; }

        void SetDefaultDateTime()
        {
            PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                string result;

                long orderid;// = new bmp_PaymentBusiness().GenerateOrderId();



                PaymentDTO pay = new PaymentDTO();
                bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                pay.Amount = Convert.ToInt64(Session["fee"].ToString());
                //pay.PayDate = PayDate + "_" + PayTime;
                pay.stcode = Session[sessionNames.userID_StudentOstad].ToString();
                pay.bankId = 2;
                pay.tterm = ConfigurationManager.AppSettings["Term"];

                string additionalInfo = pay.stcode;

                PaymentGatewayImplService bpService = new PaymentGatewayImplService();
                result = bmp.pay(pay.Amount, pay.stcode, out orderid, Convert.ToInt32(Session[sessionNames.appID_StudentOstad]), 0);
                pay.OrderId = orderid;

                Response.Write(result);
               String[] resultArray = result.Split(',');

                pay.ReqKey = resultArray[1];
                pay.AppStatus = "none";
                pay.TraceNumber = 0;
                pay.Result = -1;
              
            }
            catch (Exception)
            {
                // PayOutputLabel.Text = "Error: " + exp.Message;
            }
        }


        
    }
    }
