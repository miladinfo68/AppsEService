using IAUEC_Apps.Business.university.Wallet;
using IAUEC_Apps.DTO.University.Wallet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Wallet.Pages
{
    public partial class WalletTransactions : System.Web.UI.Page
    {
        WalletBusiness _walletBusiness = new WalletBusiness();
        PersianCalendar pc = new PersianCalendar();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session[sessionNames.userID_StudentOstad]?.ToString()))
                Response.Redirect("~/CommonUI/login.aspx", false);
        }

        protected void trgTransactions_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (string.IsNullOrEmpty(Session[sessionNames.userID_StudentOstad]?.ToString()))
                return;
            trgTransactions.DataSource = _walletBusiness.GetStudentTransactions(Session[sessionNames.userID_StudentOstad].ToString()).OrderByDescending(o=> o.CreateDate).Select(s => new
            {
                Amount = s.Amount.ToString("N0"),
                Remaining = s.CurrentBalance.ToString("N0"),
                Id = "#" + s.Id,
                Date = pc.GetYear(s.CreateDate) + "/" + pc.GetMonth(s.CreateDate) + "/" + pc.GetDayOfMonth(s.CreateDate) + " " + s.CreateDate.ToString("HH:mm:ss"),
                s.Description,
                TransactionType = TranslateTransactionTypes(s.TransactionTypeId)
            });
            GridFilterMenu menu = trgTransactions.FilterMenu;
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
                {    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                        //item.Remove();
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                        //item.Remove();
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                        //item.Remove();
                    }

                }
            }
        }

        private string TranslateTransactionTypes(decimal id)
        {
            var name = ((TransactionTypeEnum)id).ToString();
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
            }
            return string.Empty;
        }

        protected void trgTransactions_ItemDataBound(object sender, GridItemEventArgs e)
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
    }
}