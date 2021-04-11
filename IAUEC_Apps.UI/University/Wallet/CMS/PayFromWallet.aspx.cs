using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Wallet.CMS
{
    public partial class PayFromWallet : System.Web.UI.Page
    {
        Business.university.Wallet.WalletBusiness wBSN = new Business.university.Wallet.WalletBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpPayType.DataSource = wBSN.getDebitTypes();
                drpPayType.DataValueField = "id";
                drpPayType.DataTextField = "title";
                drpPayType.DataBind();
            }
        }
        private void getStudentInf(string stcode)
        {
            dvStudentInf.Visible = false;
            var studentInf = wBSN.getStudentWalletInformation(stcode);
            if (studentInf.Rows.Count == 1)
            {
                dvStudentInf.Visible = true;
                lblName.Text = studentInf.Rows[0]["name"].ToString();
                lblLastName.Text = studentInf.Rows[0]["family"].ToString();
                lblStudentID.Text = studentInf.Rows[0]["stcode"].ToString();
                lblNationalCode.Text = studentInf.Rows[0]["idd_meli"].ToString();
                lblRemainingMoney.Text = studentInf.Rows[0]["CurrentBalance"].ToString();
            }
        }

        protected void btnSearchStudent_Click(object sender, EventArgs e)
        {
            getStudentInf(txtSearchStudent.Text);
        }

        protected void btnShowLastTransactions_Click(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
            grdTransaction.DataSource = wBSN.GetStudentTransactions(lblStudentID.Text).OrderByDescending(o => o.CreateDate).Select(s => new
            {
                Amount = s.Amount.ToString("N0"),
                Remaining = s.CurrentBalance.ToString("N0"),
                Id = "#" + s.Id,
                Date = pc.GetYear(s.CreateDate) + "/" + pc.GetMonth(s.CreateDate) + "/" + pc.GetDayOfMonth(s.CreateDate) + " " + s.CreateDate.ToString("HH:mm:ss"),
                s.Description,
                TransactionType = TranslateTransactionTypes(s.TransactionTypeId),
                s.stcode
            }); 
            grdTransaction.DataBind();
            string scrp = "function f(){var win = $find(\"" + rwTransactionHistory.ClientID + "\"); win.show(); if (!win.isClosed()) {win.center();} Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            string msg;
            bool res = wBSN.PayByWallet(new DTO.University.Wallet.TransactionDTO() { Amount = Convert.ToDecimal(txtAmount.Text), CreateDate = DateTime.Now, stcode = lblStudentID.Text, TransactionTypeId = Convert.ToInt32(drpPayType.SelectedItem.Value),Description="wallet" }, out msg);
            if (res)
            {
                showMessage("تراکنش انجام شد");
                getStudentInf(lblStudentID.Text);
            }
            else
                showMessage(msg);
        }
        private void showMessage(string msg)
        {
            rAlert.RadAlert(msg,0,0,"پیام","");
        }
        private void setLog()
        {

        }

        protected void grdTransaction_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var amount = Convert.ToDecimal(item.DataItem.GetType().GetProperty("Amount").GetValue(item.DataItem, null).ToString().Replace(",", ""));
                if (amount > 0)
                    item.CssClass = "positive";
                if (amount < 0)
                    item.CssClass = "negative";
            }
        }

        protected void grdTransaction_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdTransaction.DataSource = wBSN.GetStudentTransactions(lblStudentID.Text);
            GridFilterMenu menu = grdTransaction.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {
                    if (item.Text == "NoFilter")
                        item.Text = "حذف فیلتر";
                    if (item.Text == "Contains")
                        item.Text = "شامل";
                    if (item.Text == "EqualTo")
                        item.Text = "مساوی با";

                }
            }
        }
        private string TranslateTransactionTypes(decimal id)
        {
            var name = ((DTO.University.Wallet.TransactionTypeEnum)id).ToString();
            switch (name)
            {
                case "Deposit":
                    return "افزایش اعتبار";
                case "WalletPurchase":
                    return "پرداخت خدمات";
                case "CancelWalletPurchase":
                    return "انصراف از پرداخت خدمات";
                case "StudentCardPurchase":
                    return "درخواست ارسال کارت دانشجویی";
                case "StampPurchase":
                    return "خرید تمبر";
                case "FileDownloadPurchase":
                    return "دانلود فایل";
                case "StudyingCertificate":
                    return "درخواست گواهی اشتغال به تحصیل";
                default:
                    return name;
            }
        }
    }
}