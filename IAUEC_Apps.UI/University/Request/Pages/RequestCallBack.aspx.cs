using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.UI.ir.shaparak.bpm;
using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.DTO.CommonClasses;
using System.Configuration;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;

namespace IAUEC_Apps.UI.University.Request.Pages
{
    public partial class RequestCallBack : System.Web.UI.Page
    {
        bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        RequestStudentCartBusiness business = new RequestStudentCartBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                try
                {
                    PaymentDTO pay = new PaymentDTO();
                    pay.ReqKey = Request.Params["RefId"];

                    if (Request.Params["lbl_SaleOrderID"] != "")
                    {
                        lbl_SaleOrderID.Text = Request.Params["SaleOrderId"];
                        pay.OrderId = long.Parse(lbl_SaleOrderID.Text);
                    }


                    if (Request.Params["SaleReferenceId"] != "")
                    {
                        pay.TraceNumber = long.Parse(Request.Params["SaleReferenceId"]);
                        lbl_SaleReferenceIdLabel.Text = pay.TraceNumber.ToString();

                    }

                    pay.Result = int.Parse(Request.Params["ResCode"]);
                    if (pay.Result.ToString() == "0")
                    {
                        if (VerifyPayment(pay) == "0")
                        {


                            PaymentDTO payinfo = bmp.GetRequestPaymentByRefID(pay.ReqKey);


                            lbl_Stcode.Text = payinfo.stcode;
                            Session.Timeout = 20;
                            Session[sessionNames.userID_StudentOstad] = lbl_Stcode.Text;
                            pay.stcode = payinfo.stcode;

                            pay.AppStatus = "COMMIT";
                            //bmp.UpdateMellatRquestPayment(pay);
                            lbl_ResCode.Text = "ﺗﺮﺍﻛﻨﺶ ﺑﺎ ﻣﻮﻓﻘﻴﺖ ﺍﻧﺠﺎﻡ ﺷﺪ";
                        }
                        else
                        {
                            lbl_ResCode.Text = ".وجه مبلغ به حساب واحد دانشگاهی واریز نشد چنانچه پس از 72 ساعت آینده به حساب شما برگشت داده نشد مجددا پرداخت نمایید ";
                            pay.AppStatus = "ROLLBACK";
                            //bmp.UpdateMellatRquestPayment(pay);
                        }

                    }

                    else
                    {
                        pay.AppStatus = "FAILED";

                    }

                    if (!string.IsNullOrEmpty(pay.AppStatus) && pay.AppStatus != "COMMIT")
                    {
                        var paymnetStatus = Session["paymentStatus"] as List<GovahiRequest>;
                        if (
                            paymnetStatus != null &&
                            paymnetStatus.Any(x => x.HasBeenPaid == false && x.AmountTrans == 140000))
                        {
                            if (paymnetStatus.Any(x => x.HasBeenPaid))
                            {
                                paymnetStatus.ForEach(x =>
                                {
                                    if (x.HasBeenPaid)
                                    {
                                        GovahiBusiness.UpdateRollBackingPastPayment(x.Number, x.trasferRequestID,
                                            paymnetStatus.FirstOrDefault(y => y.AmountTrans == 140000).Number);
                                    }
                                });
                            }
                        }
                    }
                    bmp.UpdateMellatRquestPayment(pay);
                    lbl_ResCode.Text = bmp.bmp_PaymentResult(pay.Result);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Pages/ControlErrors.aspx?id=" + ex.Message);
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