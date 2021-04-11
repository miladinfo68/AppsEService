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

namespace IAUEC_Apps.UI.University.Wallet.CMS
{
    public partial class WalletTransactions : System.Web.UI.Page
    {
        WalletBusiness _walletBusiness = new WalletBusiness();
        PersianCalendar pc = new PersianCalendar();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void trgTransactions_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            trgTransactions.DataSource = _walletBusiness.GetStudentTransactions().OrderByDescending(o => o.CreateDate).Select(s => new
            {
                Amount = s.Amount.ToString("N0"),
                Remaining = s.CurrentBalance.ToString("N0"),
                Id = "#" + s.Id,
                Date = pc.GetYear(s.CreateDate) + "/" + pc.GetMonth(s.CreateDate) + "/" + pc.GetDayOfMonth(s.CreateDate) + " " + s.CreateDate.ToString("HH:mm:ss"),
                s.Description,
                TransactionType = TranslateTransactionTypes(s.TransactionTypeId),
                s.stcode
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

        protected void trgTransactions_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                case "شهريه":
                    return "شهريه";
            }
            return string.Empty;
        }

        protected void trgTransactions_ExcelMLWorkBookCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExcelMLWorkBookCreatedEventArgs e)
        {

        }
    }
}