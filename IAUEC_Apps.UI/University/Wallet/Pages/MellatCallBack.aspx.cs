using IAUEC_Apps.Business.ir.shaparak.bpm1;
using IAUEC_Apps.Business.university.Wallet;
using IAUEC_Apps.DTO.University.Wallet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Wallet.Pages
{
    public partial class MellatCallBack : System.Web.UI.Page
    {
        WalletBusiness _walletBusiness = new WalletBusiness();
        PersianCalendar pc = new PersianCalendar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    PaymentDTO pay = new PaymentDTO();
                    pay.BankId = (decimal)BanksEnum.MellatBank;
                    pay.RequestKey = Request.Params["RefId"]; 
                    if (!string.IsNullOrEmpty(Request.Params["lbl_SaleOrderID"]))
                        pay.OrderId = long.Parse(Request.Params["SaleOrderId"]);

                    if (!string.IsNullOrEmpty(Request.Params["SaleReferenceId"]))
                        pay.TraceNumber = long.Parse(Request.Params["SaleReferenceId"]);

                    pay.Result = int.Parse(Request.Params["ResCode"]);

                    var payRes = _walletBusiness.AcceptPayment(pay);
                    if (payRes.Result)
                    {
                        pnlResult.CssClass = "alert alert-success successBox";
                        lblDateTime.Text = pc.GetYear(DateTime.Now) + "/" + pc.GetMonth(DateTime.Now) + "/" + pc.GetDayOfMonth(DateTime.Now) + " " + DateTime.Now.ToString("HH:mm:ss");
                        lblMessage.Text = payRes.Message;
                        lblOrderId.Text = Request.Params["SaleOrderId"];
                        lblTraceNumber.Text = Request.Params["SaleReferenceId"];
                        pnlDetails.Visible = true;
                    }
                    else
                    {
                        pnlResult.CssClass = "alert alert-danger dangerBox";
                        lblMessage.Text = payRes.Message;
                        pnlDetails.Visible = false;
                    }
                    pnlResult.Visible = true;
                }
                catch (Exception ex)
                {
                    pnlResult.CssClass = "alert alert-danger dangerBox";
                    lblMessage.Text = "خطا در تایید اطلاعات پرداخت";
                    pnlDetails.Visible = false;
                    pnlResult.Visible = true;
                }
            }
        }
    }
}