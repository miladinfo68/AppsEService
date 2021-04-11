using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.Business.university.Wallet;
using IAUEC_Apps.DTO.University.Wallet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Wallet.Pages
{
    public partial class PayTuition : System.Web.UI.Page
    {
        WalletBusiness _walletBusiness = new WalletBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRequestPayment_Click(object sender, EventArgs e)
        {
            var msg = string.Empty;
            decimal amount;
            if (!decimal.TryParse(txtAmount.Text.Replace(",", ""), out amount) || amount <= 0)
            {
                msg += "<div>مبلغ را وارد نمایید.</div>";
                lblServerValidationMessage.Text = msg;
                pnlValidationError.Visible = true;
                return;
            }
            var transaction = new TransactionDTO
            {
                Amount = amount,
                stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                TransactionTypeId = Convert.ToDecimal(TransactionTypeEnum.شهريه),
            };
            if (_walletBusiness.PayByWallet(transaction, out msg))
            {
                //bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                //bmp.CreateRequestStudentPayment(new DTO.CommonClasses.PaymentDTO
                //{
                //    Amount = Convert.ToInt64(transaction.Amount),
                //    AppStatus = "COMMIT",
                //    Description = "پرداخت هزینه ارسال کارت دانشجویی از طریق کیف پول", 
                //    MiladiDate = DateTime.Now,
                //    tterm = ConfigurationManager.AppSettings["Term"],
                //    stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                //    RequestId = 0,
                //    Result = 0,
                //    OrderId = _walletBusiness.GenerateOrderIdForRequests(),
                //    bankId = 700,
                //    TraceNumber = 0,
                //    PayType = 2,
                //    ReqKey = "WALLETPAYMENT",
                //});
                ClientScript.RegisterStartupScript(typeof(Page), "CloseTuitionModal", "<script>parent.CloseTuitionModal();alert('پرداخت شما با موفقیت انجام شد.'); </script>");
                //rwm_Validations.RadAlert("درخواست شما با موفقیت ثبت گردید؛ کد رهگیری درخواست: " + requestID, null, 100, "درخواست کارت دانشجویی", "walletPaymentCallback");
            }
            else
            {
                lblServerValidationMessage.Text = msg;
                pnlValidationError.Visible = true;
            }
        }
    }
}