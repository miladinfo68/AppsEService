using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using Telerik.Web.UI;
using System.Data;
using System.Configuration;
using IAUEC_Apps.UI.ir.shaparak.bpm;
using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.University.Wallet;
using IAUEC_Apps.Business.university.Wallet;

namespace IAUEC_Apps.UI.Adobe.Pages
{
    public partial class ConfirmCheckList : System.Web.UI.Page
    {
        DownloadRequestBusiness downloadreq = new DownloadRequestBusiness();
        WalletBusiness _walletBusiness = new WalletBusiness();

        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];

        public static string RefId = "";
        public static string PayDate = "";
        public static string PayTime = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Reports rpt = new Reports();
                int sum = 0;
                lst_SelectedClass.DataSource = rpt.Get_SelectedAsset_NotPay(Session[sessionNames.userID_StudentOstad].ToString());
                lst_SelectedClass.DataBind();
                foreach (RadListViewItem lvi in lst_SelectedClass.Items)
                {
                    Label lblFee = (Label)lvi.FindControl("lbl_Fee");
                    sum += int.Parse(lblFee.Text);
                }
                Session["Fee"] = sum.ToString();
                lbl_Sum.Text = sum.ToString("N0");

            }
        }

        //protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        //{
        //    RadAjaxPanel1.EnableAJAX = true;
        //    Reports rpt = new Reports();
        //    int sum = 0;
        //    downloadreq.DeleteDownloadReq(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RequestID"]));
        //    RadGrid1.DataSource = rpt.Get_SelectedAsset_NotPay(Session["user"].ToString());
        //    RadGrid1.DataBind();
        //    foreach (GridDataItem item in RadGrid1.MasterTableView.Items)
        //    {
        //        if (RadGrid1.Columns[5].UniqueName == "SumPrice")
        //        {
        //            sum += int.Parse(item["SumPrice"].Text);


        //        }
        //    }
        //    Session["Fee"] = sum.ToString();
        //}



        void SetDefaultDateTime()
        {
            PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        }
        protected void Payment_Click(object sender, EventArgs e)
        {
            var msg = string.Empty;
            var transaction = new TransactionDTO
            {
                Amount = Convert.ToInt64(Session["fee"].ToString()),
                stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                TransactionTypeId = Convert.ToDecimal(TransactionTypeEnum.FileDownloadPurchase),
            };
            if (_walletBusiness.PayByWallet(transaction, out msg))
            {
                bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                DownloadRequestBusiness drb = new DownloadRequestBusiness();
                var _orderId = _walletBusiness.GenerateOrderIdForRequests();
                var _traceNumber = Convert.ToInt64(((int)DateTime.Now.TimeOfDay.TotalSeconds).ToString() + _orderId.ToString());
                bmp.CreateStudentPayment(new DTO.CommonClasses.PaymentDTO
                {
                    Amount = Convert.ToInt64(transaction.Amount),
                    AppStatus = "COMMIT",
                    Description = "پرداخت هزینه دانلود فایل",
                    MiladiDate = DateTime.Now,
                    tterm = ConfigurationManager.AppSettings["Term"],
                    stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                    Result = 0,
                    OrderId = _orderId,
                    bankId = 700,
                    TraceNumber = _traceNumber,
                    ReqKey = "WALLETPAYMENT",
                    //RequestId = int.Parse(reqId.Text),
                });
                foreach (RadListViewItem lvi in lst_SelectedClass.Items)
                {
                    Label reqId = (Label)lvi.FindControl("RequestID");
                    bmp.CreateStudentRequestPayment(new PaymentRequest { OrderID = _orderId, RequestID = Convert.ToInt32(reqId.Text) });

                    drb.UpdateRequestDownload(_orderId, _traceNumber.ToString());
                }
                rwm.RadAlert("پرداخت شما با موفقیت انجام گردید.", null, 100, "دانلود فایل", "walletPaymentCallback");
            }
            else
            {
                rwm.RadAlert(msg, null, 100, "دانلود فایل", "");
            }

            //پرداخت مستقیم - حذف به علت جایگزینی روش پرداخت از طریق کیف پول - 990401

            /*
            try
            {
                string result;

                long orderid;//= new bmp_PaymentBusiness().GenerateOrderId();


                SetDefaultDateTime();
                PaymentDTO pay = new PaymentDTO();
                bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                pay.Amount = Convert.ToInt64(Session["fee"].ToString());
                pay.stcode = Session[sessionNames.userID_StudentOstad].ToString();
                pay.bankId = 2;
                pay.tterm = ConfigurationManager.AppSettings["Term"];


                result = bmp.pay(pay.Amount, pay.stcode, out orderid, Convert.ToInt32(Session[sessionNames.appID_StudentOstad]), 0);

                String[] resultArray = result.Split(',');
                pay.OrderId = orderid;
                pay.ReqKey = resultArray[1];
                pay.AppStatus = "none";
                pay.TraceNumber = 0;
                pay.Result = -1;
                bmp.CreateStudentPayment(pay);
                foreach (RadListViewItem lvi in lst_SelectedClass.Items)
                {
                    Label reqId = (Label)lvi.FindControl("RequestID");
                    PaymentRequest pr = new PaymentRequest();
                    pr.OrderID = orderid;
                    pr.RequestID = int.Parse(reqId.Text);
                    bmp.CreateStudentRequestPayment(pr);

                }

                if (resultArray[0] == "0")
                    ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
            }
            catch (Exception exp)
            {
                Response.Write("Error: " + exp.Message);
            }
            */
        }

        protected void Delbtn_Click(object sender, EventArgs e)
        {
            foreach (RadListViewItem lvi in lst_SelectedClass.Items)
            {
                Label RequestID = (Label)lvi.FindControl("RequestID");
                CheckBox chk = (CheckBox)lvi.FindControl("chk");
                if (chk.Checked)
                    downloadreq.DeleteDownloadReq(int.Parse(RequestID.Text));

            }
            Reports rpt = new Reports();
            int sum = 0;
            List<ReportDownloadReqDTO> req = new List<ReportDownloadReqDTO>();
            req = rpt.Get_SelectedAsset_NotPay(Session[sessionNames.userID_StudentOstad].ToString());
            lst_SelectedClass.DataSource = req;
            lst_SelectedClass.DataBind();
            foreach (RadListViewItem lvi in lst_SelectedClass.Items)
            {
                Label lblFee = (Label)lvi.FindControl("lbl_Fee");
                sum += int.Parse(lblFee.Text);
            }
            Session["Fee"] = sum.ToString();
            lbl_Sum.Text = sum.ToString("N0");
            MasterPage mp = this.Master;

            //MastePage.MasterPage msp = new MastePage.MasterPage();
            RadGrid lst = (RadGrid)mp.FindControl("grdShopping");
            lst.DataSource = req;
            lst.Rebind();

        }
    }
}