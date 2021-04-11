using IAUEC_Apps.Business.university.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.University.Wallet;

namespace IAUEC_Apps.UI.University.Wallet.Pages
{
    public partial class WalletTopup : System.Web.UI.Page
    {
        WalletBusiness _walletBusiness = new WalletBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRequestPayment_Click(object sender, EventArgs e)
        {
            decimal amount;
            var message = "";
            if (string.IsNullOrEmpty(Session[sessionNames.userID_StudentOstad]?.ToString()))
            {
                var script = "top.window.location = window.location.origin + '/CommonUI/login.aspx';";
                ScriptManager.RegisterStartupScript(this, GetType(), "submitform", script, true);
                return;
            }
            if (!decimal.TryParse(txtAmount.Text.Replace(",", ""), out amount) || amount <= 0)
                message += "<div>مبلغ را وارد نمایید.</div>";
            if (rblBanks.SelectedItem == null)
                lblServerValidationMessage.Text = "<div>درگاه بانکی را انتخاب نمایید.</div>";


            var res = _walletBusiness.AddPayment(new PaymentDTO
            {
                stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                BankId = decimal.Parse(rblBanks.SelectedItem.Value),
                Amount = amount,
            });
            if (res.Result != "0")
                message += "<div>" + res.Result + "</div>";
            System.IO.File.AppendAllText(@"D:\web-folder\IAUEC\University\wallet\walletError.txt", "\r\n" + "result: "+res.Result+" * message: "+message);

            if (message != "")
            {
                lblServerValidationMessage.Text = message;
                pnlValidationError.Visible = true;
                return;
            }
            else
            {
                var scriptSubmit = @"var form = document.createElement('form');
                                    form.setAttribute('method', 'POST');
                                    form.setAttribute('action', '" + res.GateWay + @"');
                                    form.setAttribute('target', '_top');
                                    var hiddenField = document.createElement('input');
                                    hiddenField.setAttribute('name', 'RefId');
                                    hiddenField.setAttribute('value', '" + res.RefId + @"');
                                    form.appendChild(hiddenField);
                                    document.body.appendChild(form);
                                    form.submit();
                                    document.body.removeChild(form); ";
                System.IO.File.AppendAllText(@"D:\web-folder\IAUEC\University\wallet\walletError.txt", "\r\n" + scriptSubmit);

                ScriptManager.RegisterStartupScript(this, GetType(), "submitform", scriptSubmit, true);
                return;
            }
        }

    }
}