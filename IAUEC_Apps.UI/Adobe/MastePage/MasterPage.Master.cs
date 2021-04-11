using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Adobe;
using Telerik.Web.UI;
using System.Configuration;
using IAUEC_Apps.UI.ir.shaparak.bpm;
using System.Data;

namespace IAUEC_Apps.UI.Adobe.MastePage
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {

        DownloadRequestBusiness downloadreq = new DownloadRequestBusiness();
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["Mellat_CallBackUrl"];
        public static readonly string TerminalId = ConfigurationManager.AppSettings["Mellat_TerminalId"];
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["UserPassword"];

        public static string RefId = "";
        public static string PayDate = "";
        public static string PayTime = "";

        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx");          

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx");

            if (Session[sessionNames.userID_StudentOstad] != null)
            {
                Label1.Text = Session[sessionNames.userID_StudentOstad].ToString();
                if (!IsPostBack)
                {
                    LoginBusiness lgnB = new LoginBusiness();
                    StuImg st = lgnB.User_Img(Label1.Text);
                    PersonalImg.DataValue = st.img;
                    LoginDTO stInfo = lgnB.Get_StInfo(Label1.Text);
                    stName.InnerText = stInfo.Name;
                    stLastName.InnerText = stInfo.LastName;
                    stField.InnerText = stInfo.StReshte;
                    Reports rpt = new Reports();
                    List<ReportDownloadReqDTO> counter = new List<ReportDownloadReqDTO>();
                    int sum = 0;
                    counter = rpt.Get_SelectedAsset_NotPay(Label1.Text);
                    ShoppingCounter.InnerText = counter.Count.ToString();
                    grdShopping.DataSource = counter;
                    grdShopping.DataBind();
                    DownloadRequestBusiness dnlB = new DownloadRequestBusiness();
                    List<AssetDTO> assetlst = new List<AssetDTO>();
                    assetlst = dnlB.GetValidAssets(Label1.Text);
                    if (assetlst.Count > 0)
                        dnlNav.Visible = true;
                    if (grdShopping.MasterTableView.Items.Count > 0)
                    {
                        Paybtn.Visible = true;

                    }
                    foreach (GridDataItem item in grdShopping.MasterTableView.Items)
                    {
                        if (grdShopping.Columns[2].UniqueName == "SumPrice")
                        {
                            sum += int.Parse(item["SumPrice"].Text.Replace(",", ""));


                        }
                    }
                    Session["Fee"] = sum.ToString();

                }
                Session[sessionNames.userID_StudentOstad] = Label1.Text;
            }
            else
                Response.Redirect("~/CommonUI/login.aspx",false);
        }

        protected void grdShopping_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Reports rpt = new Reports();
            int sum = 0;

            downloadreq.DeleteDownloadReq(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RequestID"]));
            List<ReportDownloadReqDTO> lstdnl = new List<ReportDownloadReqDTO>();
            lstdnl = rpt.Get_SelectedAsset_NotPay(Session[sessionNames.userID_StudentOstad].ToString());
            grdShopping.DataSource = lstdnl;
            grdShopping.DataBind();
            RadListView lst = (RadListView)ContentPlaceHolder1.FindControl("lstSelectedClass");
            if (lst != null)
            {
                lst.DataSource = lstdnl;
                lst.DataBind();
            }
            //Pages.ConfirmCheckList chk = new Pages.ConfirmCheckList();
            //RadListView lst = (RadListView)chk.FindControl("lstClass");
            //lst.DataSource = lstdnl;
            //lst.DataBind();
            foreach (GridDataItem item in grdShopping.MasterTableView.Items)
            {
                if (grdShopping.MasterTableView.Items.Count > 0)
                {
                    Paybtn.Visible = true;
                }
                if (grdShopping.Columns[3].UniqueName == "SumPrice")
                {
                    sum += int.Parse(item["SumPrice"].Text.Replace(",", ""));


                }
            }
            Session["Fee"] = sum.ToString();
        }

        protected void exitButton_Click(object sender, EventArgs e)
        {
            //if (Session["user"].ToString() == "99900999")
            //{

            Session[sessionNames.userID_StudentOstad] = null;
            Session[sessionNames.userID_Karbar] = null;
            Session["Password"] = null;
            Session["LogStatus"] = "0-0";
            LogStatus.Value = Session["LogStatus"].ToString();
            form1.Action = "http://lms.iauec.ac.ir/Authentication.php";
            ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);

            //}
            //else
            //{
            //    Session["user"] = null;
            //    Session["Password"] = null;
            //    Response.Redirect("~/CommonUI/login.aspx",false);
            //}
        }
        protected void btn_home_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../../CommonUI/Intro.aspx");
            Response.Redirect("../../CommonUI/IntroPage.aspx");
        }

        void SetDefaultDateTime()
        {
            PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        }
        protected void Paybtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Pages/ConfirmCheckList.aspx");
            //try
            //{
            //    string result;

            //    long orderid = new bmp_PaymentBusiness().GenerateOrderId();


            //    SetDefaultDateTime();
            //    PaymentDTO pay = new PaymentDTO();
            //    bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
            //    bmp.BypassCertificateError();
            //    pay.Amount = Convert.ToInt64(Session["fee"].ToString());
            //    pay.PayDate = PayDate + "_" + PayTime;
            //    pay.stcode = Session["user"].ToString();
            //    pay.OrderId = orderid;
            //    pay.bankId = 2;
            //    pay.tterm = ConfigurationManager.AppSettings["Term"];

            //    //string additionalInfo = stcode;

            //    PaymentGatewayImplService bpService = new PaymentGatewayImplService();
            //    long PayerId = long.Parse(pay.stcode + (new bmp_PaymentBusiness().PayerIdGenerator(pay.stcode).ToString()));
            //    result = bpService.bpPayRequest(Int64.Parse(TerminalId),
            //        UserName,
            //        UserPassword,
            //        pay.OrderId,
            //        pay.Amount,
            //        PayDate,
            //        PayTime,
            //        pay.stcode,
            //        CallBackUrl, PayerId);

            // //  Response.Write(result);
            //    String[] resultArray = result.Split(',');

            //    pay.ReqKey = resultArray[1];
            //    pay.AppStatus = "none";
            //    pay.TraceNumber = 0;
            //    pay.Result = -1;
            //    try
            //    {
            //        bmp.CreateStudentPayment(pay);
            //    }
            //    catch (Exception ex2)
            //    {

            //        Response.Write("Error: new" + ex2.Message );
            //    }


            //    if (resultArray[0] == "0")
            //        Master.Page.ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
            //}
            //catch (Exception exp)
            //{
            //  //  Response.Write("Error: " + exp.Message);
            //}
        }


    }
}