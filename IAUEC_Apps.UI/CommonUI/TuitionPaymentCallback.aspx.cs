using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.UI.ir.shaparak.bpm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class TuitionPaymentCallback : System.Web.UI.Page
    {
        bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
        PersianCalendar pc = new PersianCalendar();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var pay = bmp.GetTuitionPaymentByRefId(Convert.ToDecimal(Request.Params["RefId"]));
                if (Request.Params["SaleReferenceId"] != "")
                    pay.TraceNumber = long.Parse(Request.Params["SaleReferenceId"]);
                pay.Result = int.Parse(Request.Params["ResCode"]);
                if (pay.Result.ToString() == "0")
                {
                    if (VerifyPayment(pay) == "0")
                    {
                        bmp.UpdateTuitionPayment(pay);
                        lblOrderId.Text = pay.OrderId.ToString();
                        lblTraceNumber.Text = pay.TraceNumber.ToString();
                        lblPaymentDate.Text = pay.PayDate;
                        pnlError.Visible = false;
                        pnlSuccess.Visible = true;
                    }
                    else
                    {
                        lblErrorMessage.Text = "وجه مبلغ به حساب واحد دانشگاهی واریز نشد چنانچه پس از 72 ساعت آینده به حساب شما برگشت داده نشد مجددا پرداخت نمایید.";
                        pnlError.Visible = true;
                        pnlSuccess.Visible = false;
                    }
                }
                else
                {
                    lblErrorMessage.Text = bmp.bmp_PaymentResult(15);
                    pnlError.Visible = true;
                    pnlSuccess.Visible = false;
                }

            }
        }
        public string VerifyPayment(PaymentDTO pay)
        {

            string result;
            bmp.BypassCertificateError();
            PaymentGatewayImplService bpService = new PaymentGatewayImplService();
            result = bpService.bpVerifyRequest(Int64.Parse(ConfigurationManager.AppSettings["Mellat_TerminalId"]),
           ConfigurationManager.AppSettings["UserName"],
           ConfigurationManager.AppSettings["UserPassword"],
           pay.OrderId,
           pay.OrderId,
           pay.TraceNumber);
            return result;

        }
    }
}