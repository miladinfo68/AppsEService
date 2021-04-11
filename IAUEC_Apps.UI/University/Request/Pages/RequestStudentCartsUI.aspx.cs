using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.Business.university.Request;

using IAUEC_Apps.UI.University.Request;
using IAUEC_Apps.Business.Common;
using System.Configuration;
using IAUEC_Apps.UI.ir.shaparak.bpm;
using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.DTO.CommonClasses;
using Telerik.Web.UI;
using IAUEC_Apps.Business.university.Wallet;
using IAUEC_Apps.DTO.University.Wallet;

namespace IAUEC_Apps.UI.University.Request.Pages
{
    public partial class RequestStudentCartsUI : System.Web.UI.Page
    {
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        CommonBusiness cBusiness = new CommonBusiness();
        WalletBusiness _walletBusiness = new WalletBusiness();
        /// <summary>
        /// این متغیر از نوع بولین می باشد و برای چک کردن اینکه دانشجو آدرس خود را ویرایش کرده یا خیر استفاده می شود
        /// </summary>
        //public static bool addresschanged;
        /// <summary>
        /// این متغیر از نوع بولین می باشد و برای چک کردن اینکه دانشجو کدپستی خود را ویرایش کرده یا خیر استفاده می شود
        /// </summary>
        //public static bool postichanged;
        /// <summary>
        /// این متغیر از نوع بولین می باشد و برای چک کردن اینکه دانشجو شماره تلفن خود را ویرایش کرده یا خیر استفاده می شود
        /// </summary>
        //public static bool phonechanged;

        public static bool state;
        /// <summary>
        /// این متغیر از نوع بولین می باشد و برای چک کردن اینکه دانشجو پیش شماره تلفن خود را ویرایش کرده یا خیر استفاده می شود
        /// </summary>
        //public static bool pishshomare;
        /// <summary>
        /// متغیر از نوع رشته می باشد و آدرس جدید در این متغیر قرار می گیرد
        /// </summary>
        //public static string newaddress;
        //Request_StudentCartDAO dastcast = new Request_StudentCartDAO();
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();

        DataTable dt = new DataTable();
        DataTable dthas = new DataTable();
        /// <summary>
        /// با لود شدن صفحه چک می شود که دانشجو قبلا درخواست داشته یا نه
        /// چنانچه قبلا درخواست داده باشد جدولی حاوی وضعیت درخواست دانشجو نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["Mellat_Request_CallBackUrl"];
        public static readonly string TerminalId = ConfigurationManager.AppSettings["Mellat_TerminalId"];
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["UserPassword"];
        public static string RefId = "";
        public static string PayDate = "";
        public static string PayTime = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                state = false;

                dthas = CartBusiness.GetstHasCartRequest(Session[sessionNames.userID_StudentOstad].ToString());

                if (dthas.Rows.Count > 0)
                {

                    endMessage();
                   DataTable dts = new DataTable();
                    dts = CartBusiness.GetRequestsStatus(Session[sessionNames.userID_StudentOstad].ToString());
                    if (dts.Rows.Count > 0)
                    {
                        //grd_CartRequestState.DataSource = dts;
                        //grd_CartRequestState.DataBind();
                        grd_CartRequestState.Visible = true;
                    }
                    else
                    {
                        lblPardakht.Visible = true;
                        //btn_pardakht.Visible = false;
                        btn_pardakht.Visible = true;
                        rwm_Validations.RadAlert("لطفا برای تسویه حساب مالی اقدام نمایید", null, 100, "درخواست کارت دانشجویی", "");
                        return;
                    }

                }
                else
                {
                  //  DataTable dtbedehi = new DataTable();
                   // dtbedehi = GovahiBusiness.GetBedehkar(Session[sessionNames.userID_StudentOstad].ToString());
                    DataTable dtst = new DataTable();
                    dtst = GovahiBusiness.GetStRegisterd(Session[sessionNames.userID_StudentOstad].ToString());

                    //double bedehi = Convert.ToDouble((dtbedehi.Rows[0]["bedehi"].ToString()));
                    DataTable dtmojazgovahi = new DataTable();
                    dtmojazgovahi = GovahiBusiness.GetMojazGovahi(Session[sessionNames.userID_StudentOstad].ToString());

                    //آقای منوچهری و خانم کبیری گفتند هیچ محدودیت مالی برای دریافت کارت وجود ندارد - 98/07/23
                    //if (dtmojazgovahi.Rows.Count == 0 && bedehi > 0)////////////////////////////////////////////////////////////????????
                    //{
                    //    endMessage();
                    //    rwm_Validations.RadAlert("به علت بدهکاری درخواست کارت دانشجویی مقدور نمی باشد ", null, 100, "خطا", "CallBackConfirm2");

                    //}
                    //else
                    if (dtst.Rows.Count == 0)
                    {
                        //dtst = GovahiBusiness.GetStRegisterdAmoozehyar(Session[sessionNames.userID_StudentOstad].ToString());
                        //if (dtst.Rows.Count == 0)
                        {
                            endMessage();
                             rwm_Validations.RadAlert("دانشجو در این ترم ثبت نام نکرده است", null, 100, "خطا", "CallBackConfirm2");
                            }
                      
                    }
                    if (dtst.Rows.Count != 0)
                    {
                        btn_Taeid.Visible = true;
                        dt = CartBusiness.GetStudentsInfo(Session[sessionNames.userID_StudentOstad].ToString());
                       
                        DataColumnCollection columns = dt.Columns;
                        lbl_NamePrev.Text = dt.Rows[0]["firstName"].ToString();
                        lbl_FamilyPrev.Text = dt.Rows[0]["lastName"].ToString();
                        lbl_GerayeshPrev.Text = columns.Contains("namegeraesh") == false || dt.Rows[0]["namegeraesh"]?.ToString() == "" || dt.Rows[0]["namegeraesh"] == null ? "" : dt.Rows[0]["namegeraesh"]?.ToString();
                        lbl_MaghtaPrev.Text = dt.Rows[0]["magh"].ToString();
                        lbl_PhonePrev.Text = columns.Contains("homePhone") == false || dt.Rows[0]["homePhone"]?.ToString() == "" || dt.Rows[0]["homePhone"] == null ? "" : dt.Rows[0]["homePhone"]?.ToString();
                        lbl_CodepostiPrev.Text = columns.Contains("home_postalCode") == false || dt.Rows[0]["home_postalCode"]?.ToString() == "" || dt.Rows[0]["home_postalCode"] == null ? "" : dt.Rows[0]["home_postalCode"]?.ToString();
                        if (columns.Contains("workPlaceAddress") == true && dt.Rows[0]["workPlaceAddress"].ToString() != "")
                            lbl_AddressPrev.Text = dt.Rows[0]["workPlaceAddress"].ToString();
                        lbl_ReshteTahsiliPrev.Text = dt.Rows[0]["nameresh"].ToString();
                        lbl_SalVorudPrev.Text = dt.Rows[0]["enterYear"].ToString();
                        lbl_ShomareDaneshjuPrev.Text = Session[sessionNames.userID_StudentOstad].ToString();
                        NimsalVorudiCheck(int.Parse((dt.Rows[0]["enterTerm"]).ToString()));
                        lbl_MobilePrev.Text = dt.Rows[0]["mobile"].ToString();



                        DataTable dts = new DataTable();
                        dts = CartBusiness.GetRequestsStatus(Session[sessionNames.userID_StudentOstad].ToString());
                        if (dts.Rows.Count > 0)
                        {
                            grd_CartRequestState.DataSource = dts;
                            grd_CartRequestState.Visible = true;
                        }

                    }


                }
            }
        }

        /// <summary>
        /// این متد برای تشخیص چک کردن نیمسال ورودی دانشجو می باشد
        /// </summary>
        /// <param name="nimsal">اگر مقدار نیمسال یک باشد به این معنی است که دانشجو ورودی مهر است و اگر مقدار آن برابر 2 باشد به این معنی است که دانشجو ورودی بهمن است</param>
        /// <returns>خروجی به صورت متن می باشد که یا مهر و یا بهمن خواهد بود </returns>
        public void NimsalVorudiCheck(int nimsal)
        {
            if (nimsal == 1)
                lbl_VorudiPrev.Text = "مهر";
            if (nimsal == 2)
                lbl_VorudiPrev.Text = "بهمن";
        }

        /// <summary>
        /// این متد برای تشخیص تغییر کدپستی می باشد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>

        /// <summary>
        /// این متد برای غیر فعال کردن و پنهان کردن تمامی کنترل ها می باشد که در اتمام فرایند درخواست اجرا می شود تا دانشجو نتواند مجددا درخواست ارسال کارت بدهد
        /// </summary>
        public void endMessage()
        {

            lbl_AddressPrev.Visible = false;
            lbl_MobilePrev.Visible = false;
            lbl_PhonePrev.Visible = false;
            lbl_CodepostiPrev.Visible = false;
            lbl_Family.Visible = false;
            lbl_FamilyPrev.Visible = false;
            lbl_Gerayesh.Visible = false;
            lbl_GerayeshPrev.Visible = false;
            lbl_Maghta.Visible = false;
            lbl_MaghtaPrev.Visible = false;
            lbl_Name.Visible = false;
            lbl_NamePrev.Visible = false;
            lbl_ReshteTahsili.Visible = false;
            lbl_ReshteTahsiliPrev.Visible = false;
            lbl_SalVorud.Visible = false;
            lbl_SalVorudPrev.Visible = false;
            lbl_ShomareDaneshju.Visible = false;
            lbl_ShomareDaneshjuPrev.Visible = false;
            lbl_Vorudi.Visible = false;
            chk_Taeid.Visible = false;
            lbl_VorudiPrev.Visible = false;
            lbl_Tazakor1.Visible = false;
            lbl_Tazakor2.Visible = false;
            lbl_Tazakor3.Visible = false;
            lbl_Address.Visible = false;
            lbl_Phone.Visible = false;
            lbl_Posti.Visible = false;
            btn_Taeid.Visible = false;
            pnl_tazakor.Visible = false;
            lbl_Mobile.Visible = false;

        }

        /// <summary>
        /// با فشردن کلید تایید،این متد اجرا می شود و در نتیجه درخواست ارسال کارت دانشجو در دیتابیس ذخیره می گردد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btn_taeid_Click(object sender, EventArgs e)
        {


            if (chk_Taeid.Checked == true)
            {
                CommonBusiness cmnb = new CommonBusiness();
                var isStcodeExist = CartBusiness.CheckStCodeIsExistInStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 1, 6);
                if (isStcodeExist)
                {
                    rwm_Validations.RadAlert("این درخواست قبلا برای شما ثبت شده است.", null, 100, "درخواست کارت دانشجویی", "");
                    return;
                }
                int requestID = int.Parse(CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 1, 6, "Null", "", 1).ToString());
                endMessage();
                if (requestID > 0)
                {
                    DataTable dts = new DataTable();
                    dts = CartBusiness.GetRequestsStatus(Session[sessionNames.userID_StudentOstad].ToString());

                    if (dts.Rows.Count > 0)
                    {
                        grd_CartRequestState.DataSource = dts;
                        grd_CartRequestState.DataBind();
                        grd_CartRequestState.Visible = true;
                    }

                    var msg = string.Empty;
                    var transaction = new TransactionDTO
                    {
                        Amount = 140000,
                        stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                        TransactionTypeId = Convert.ToDecimal(TransactionTypeEnum.StudentCardPurchase),
                        Description = "درخواست شماره " + requestID
                    };
                    if (_walletBusiness.PayByWallet(transaction, out msg))
                    {
                        cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 14, requestID.ToString());
                        bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                        bmp.CreateRequestStudentPayment(new DTO.CommonClasses.PaymentDTO
                        {
                            Amount = Convert.ToInt64(transaction.Amount),
                            AppStatus = "COMMIT",
                            Description = "پرداخت هزینه ارسال کارت دانشجویی از طریق کیف پول",
                            MiladiDate = DateTime.Now,
                            tterm = ConfigurationManager.AppSettings["Term"],
                            stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                            RequestId = requestID,
                            Result = 0,
                            OrderId = _walletBusiness.GenerateOrderIdForRequests(),
                            bankId = 700,
                            TraceNumber = 0,
                            PayType = 2,
                            ReqKey = "WALLETPAYMENT",
                        });
                        rwm_Validations.RadAlert("درخواست شما با موفقیت ثبت گردید؛ کد رهگیری درخواست: " + requestID, null, 100, "درخواست کارت دانشجویی", "walletPaymentCallback");
                    }
                    else
                    {
                        rwm_Validations.RadAlert(msg, null, 100, "درخواست کارت دانشجویی", "");
                    }

                    //پرداخت مستقیم - حذف به علت جایگزینی روش پرداخت از طریق کیف پول - 990401
                    /*
                            /*
                            try
                            {
                                string result;

                                long orderid;//= new bmp_PaymentBusiness().GenerateOrderId();


                                SetDefaultDateTime();
                                DTO.CommonClasses.PaymentDTO pay = new DTO.CommonClasses.PaymentDTO();
                                bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                                pay.Amount = Convert.ToInt64("140000");
                                pay.PayDate = PayDate + "_" + PayTime;
                                pay.stcode = Session[sessionNames.userID_StudentOstad].ToString();
                                pay.bankId = 2;
                                pay.tterm = ConfigurationManager.AppSettings["Term"];
                                pay.Description = "درخواست کارت";


                                //PaymentGatewayImplService bpService = new PaymentGatewayImplService();
                                //long PayerId = long.Parse(pay.stcode + (new bmp_PaymentBusiness().PayerIdGenerator(pay.stcode).ToString()));
                                //var terminalIDD = string.IsNullOrWhiteSpace(TerminalId) ? "-1" : TerminalId;
                                result = bmp.pay(pay.Amount, pay.stcode, out orderid, Convert.ToInt32(Session[sessionNames.appID_StudentOstad]), 0);
                                pay.OrderId = orderid;

                                //bpService.bpPayRequest(Int64.Parse(terminalIDD),
                                //    UserName,
                                //    UserPassword,
                                //    pay.OrderId,
                                //    pay.Amount,
                                //    PayDate,
                                //    PayTime,
                                //    pay.stcode,
                                //    CallBackUrl, PayerId);

                                String[] resultArray = result.Split(',');

                                pay.ReqKey = resultArray[1];
                                pay.AppStatus = "none";
                                pay.TraceNumber = 0;
                                pay.Result = -1;
                                pay.PayType = 2;
                                pay.Description = "";
                                pay.RequestId = requestID;
                                bmp.CreateRequestStudentPayment(pay);


                                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 14, requestID.ToString());

                                if (resultArray[0] == "0")
                                    ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
                            }
                            catch (Exception exp)
                            {
                                Response.Write("Error: " + exp.Message);
                            }
                            */
                }
                else
                    rwm_Validations.RadAlert("دانشجوی گرامی شما قبلا درخواست کارت را ثبت کرده اید و امکان درخواست مجدد برای شما وجود ندارد", null, 100, "پیام", "");

            }
            else
            {

                rwm_Validations.RadAlert("لطفا چک باکس را تیک بزنید", null, 100, "درخواست کارت دانشجویی", "");



            }
        }


        void SetDefaultDateTime()
        {
            PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        }







        /// <summary>
        /// داده هایی را جهت نشان دادن وضعیت درخواست کارت برای گرید تامین می نماید
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void grd_CartRequeststate_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dts = new DataTable();
            dts = CartBusiness.GetRequestsStatus(Session[sessionNames.userID_StudentOstad].ToString());
            if (dts.Rows.Count > 0)
            {
                grd_CartRequestState.DataSource = dts;
                grd_CartRequestState.Visible = true;
            }

        }

        protected void grd_CartRequestState_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                
                Button btn_pay = e.Item.FindControl("btn_Pay") as Button;
                Label lbl_vaziat = e.Item.FindControl("lbl_vaziat") as Label;
                HiddenField hdnAppStatus = e.Item.FindControl("hdnAppStatus") as HiddenField;
                
                string cvaziat = lbl_vaziat.Text;
                string cappStatus = hdnAppStatus.Value;
                if (cappStatus != "COMMIT")
                {
                    lbl_vaziat.Visible = true;
                    lbl_vaziat.Text = "در انتظار پرداخت";
                    btn_pay.Visible = true;
                }
                else
                {
                    btn_pay.Visible = false;
                    lbl_vaziat.Visible = true;
                }
            }

        }

        protected void grd_CartRequestState_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "pay")
                {

                    string[] COMMANDS = e.CommandArgument.ToString().Split(new char[] { ',' });
                    CommonBusiness cmnb = new CommonBusiness();
                    int requestID = int.Parse(COMMANDS[0].ToString());

                    var msg = string.Empty;
                    var transaction = new TransactionDTO
                    {
                        Amount = 140000,
                        stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                        TransactionTypeId = Convert.ToDecimal(TransactionTypeEnum.StudentCardPurchase),
                        Description = "درخواست شماره " + requestID
                    };
                    if (_walletBusiness.PayByWallet(transaction, out msg))
                    {
                        cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 14, requestID.ToString());
                        bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                        bmp.CreateRequestStudentPayment(new DTO.CommonClasses.PaymentDTO
                        {
                            Amount = Convert.ToInt64(transaction.Amount),
                            AppStatus = "COMMIT",
                            Description = "پرداخت هزینه ارسال کارت دانشجویی از طریق کیف پول",
                            MiladiDate = DateTime.Now,
                            tterm = ConfigurationManager.AppSettings["Term"],
                            stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                            RequestId = requestID,
                            Result = 0,
                            OrderId = _walletBusiness.GenerateOrderIdForRequests(),
                            bankId = 700,
                            TraceNumber = 0,
                            PayType = 2,
                            ReqKey = "WALLETPAYMENT",
                        });
                        rwm_Validations.RadAlert("درخواست شما با موفقیت ثبت گردید؛ کد رهگیری درخواست: " + requestID, null, 100, "درخواست کارت دانشجویی", "walletPaymentCallback");
                    }
                    else
                    {
                        rwm_Validations.RadAlert(msg, null, 100, "درخواست کارت دانشجویی", "");
                    }

                    //پرداخت مستقیم - حذف به علت جایگزینی روش پرداخت از طریق کیف پول - 990401
                    /*
                    string result;

                    long orderid;//= new bmp_PaymentBusiness().GenerateOrderId();


                    SetDefaultDateTime();
                    DTO.CommonClasses.PaymentDTO pay = new DTO.CommonClasses.PaymentDTO();
                    bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                    pay.Amount = Convert.ToInt64("140000");
                    pay.PayDate = PayDate + "_" + PayTime;
                    pay.stcode = Session[sessionNames.userID_StudentOstad].ToString();
                    pay.bankId = 2;
                    pay.tterm = ConfigurationManager.AppSettings["Term"];
                    //PaymentGatewayImplService bpService = new PaymentGatewayImplService();
                    //long PayerId = long.Parse(pay.stcode + (new bmp_PaymentBusiness().PayerIdGenerator(pay.stcode).ToString()));
                    result = bmp.pay(pay.Amount, pay.stcode, out orderid, Convert.ToInt32(Session[sessionNames.appID_StudentOstad]), 0);
                    pay.OrderId = orderid;

                    //bpService.bpPayRequest(Int64.Parse(TerminalId),
                    //UserName,
                    //UserPassword,
                    //pay.OrderId,
                    //pay.Amount,
                    //PayDate,
                    //PayTime,
                    //pay.stcode,
                    //CallBackUrl, PayerId);
                    String[] resultArray = result.Split(',');

                    pay.ReqKey = resultArray[1];
                    pay.AppStatus = "none";
                    pay.TraceNumber = 0;
                    pay.Result = -1;
                    pay.PayType = 1;
                    pay.Description = "درخواست کارت";
                    pay.RequestId = int.Parse(COMMANDS[0].ToString());
                    GovahiBusiness.UpdatePymentDetail(pay.OrderId, pay.PayDate, int.Parse(COMMANDS[1].ToString()), 140000, pay.ReqKey);


                    if (resultArray[0] == "0")
                        ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
                        */
                }
            }
            catch (Exception exp)
            {
                Response.Write("Error: " + exp.Message);
            }
        }

        protected void btn_pardakht_Click(object sender, EventArgs e)
        {


            CommonBusiness cmnb = new CommonBusiness();
            int requestID = int.Parse(CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 1, 6, "Null", "", 1).ToString());

            var msg = string.Empty;
            var transaction = new TransactionDTO
            {
                Amount = 140000,
                stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                TransactionTypeId = Convert.ToDecimal(TransactionTypeEnum.StudentCardPurchase),
                Description = "درخواست شماره " + requestID
            };
            if (_walletBusiness.PayByWallet(transaction, out msg))
            {
                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 14, requestID.ToString());
                bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                bmp.CreateRequestStudentPayment(new DTO.CommonClasses.PaymentDTO {
                    Amount = Convert.ToInt64(transaction.Amount),
                    AppStatus = "COMMIT",
                    Description = "پرداخت هزینه ارسال کارت دانشجویی از طریق کیف پول",
                    MiladiDate = DateTime.Now,
                    tterm = ConfigurationManager.AppSettings["Term"],
                    stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                    RequestId = requestID,
                    Result = 0,
                    OrderId = _walletBusiness.GenerateOrderIdForRequests(),
                    bankId = 700,
                    TraceNumber = 0,
                    PayType = 2,
                    ReqKey = "WALLETPAYMENT",
                });
                rwm_Validations.RadAlert("درخواست شما با موفقیت ثبت گردید؛ کد رهگیری درخواست: " + requestID, null, 100, "درخواست کارت دانشجویی", "walletPaymentCallback");
            }
            else
            {
                rwm_Validations.RadAlert(msg, null, 100, "درخواست کارت دانشجویی", "");
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
                pay.Amount = Convert.ToInt64("140000");
                pay.PayDate = PayDate + "_" + PayTime;
                pay.stcode = Session[sessionNames.userID_StudentOstad].ToString();
                pay.bankId = 2;
                pay.tterm = ConfigurationManager.AppSettings["Term"];
                pay.Description = "درخواست کارت";


                //PaymentGatewayImplService bpService = new PaymentGatewayImplService();
                //long PayerId = long.Parse(pay.stcode + (new bmp_PaymentBusiness().PayerIdGenerator(pay.stcode).ToString()));
                var terminalIDD = string.IsNullOrWhiteSpace(TerminalId) ? "-1" : TerminalId;
                //var a = 1;
                result = bmp.pay(pay.Amount, pay.stcode, out orderid, Convert.ToInt32(Session[sessionNames.appID_StudentOstad]), 0);
                pay.OrderId = orderid;

                //bpService.bpPayRequest(Int64.Parse(terminalIDD),
                //UserName,
                //UserPassword,
                //pay.OrderId,
                //pay.Amount,
                //PayDate,
                //PayTime,
                //pay.stcode,
                //CallBackUrl, PayerId);

                String[] resultArray = result.Split(',');
                try
                {
                    pay.ReqKey = resultArray[1];

                }
                catch (Exception xx)
                {
                    var errr = string.Format("{0} ", xx.Message);
                    rwm_Validations.RadAlert(errr, null, 100, "پیام", "");
                    return;
                    //throw;
                }
                pay.AppStatus = "none";
                pay.TraceNumber = 0;
                pay.Result = -1;
                pay.PayType = 2;
                pay.Description = "";
                pay.RequestId = requestID;
                bmp.CreateRequestStudentPayment(pay);


                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 14, requestID.ToString());

                if (resultArray[0] == "0")
                    ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
            }
            catch (Exception exp)
            {
                Response.Write("Error: " + exp.Message);
            }
            */
        }

    }
}