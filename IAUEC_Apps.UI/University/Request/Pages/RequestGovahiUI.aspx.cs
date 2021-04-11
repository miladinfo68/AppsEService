using System;
using System.Linq;
using System.Web.UI;

using System.Data;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;
using System.Configuration;
using IAUEC_Apps.UI.ir.shaparak.bpm;
using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DTO.University.Wallet;
using IAUEC_Apps.Business.university.Wallet;

namespace IAUEC_Apps.UI.University.Request.Pages
{
    public partial class RequestGovahi : System.Web.UI.Page
    {
        public string MashmulNumber;

        /// <summary>
        ///  ایجاد می نماییمRequestStudentCartBusinessd یک شئ از کلاس 
        /// </summary>
        RequestStudentCartBusiness business = new RequestStudentCartBusiness();
        /// <summary>
        /// ایجاد می نماییمRequest_StudentCartDAO یک شئ از کلاس
        /// </summary>
        //Request_StudentCartDAO DAO = new Request_StudentCartDAO();
        /// <summary>
        /// ایجاد می نماییمRequest_GovahiDAO یک شئ از کلاس
        /// </summary>
        //Request_GovahiDAO DAOg = new Request_GovahiDAO();
        /// <summary>
        /// ایجاد می نماییمRequestGovahiBusiness یک شئ از کلاس
        /// </summary>
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        WalletBusiness _walletBusiness = new WalletBusiness();
        /// <summary>
        /// اطلاعات دانشجو در این جدول ذخیره می گردد
        /// </summary>
        DataTable dt = new DataTable();
        /// <summary>
        /// چنانچه دانشجو در این ترم ثبت نام داشته، شماره دانشجویی آن در این جدول قرار می گیرد
        /// </summary>
        DataTable dts = new DataTable();
        /// <summary>
        /// چنانچه دانشجو بدهی داشته باشد، مبلغ بدهی او در این جدول قرار می گیرد
        /// </summary>
       // DataTable dtbedehi = new DataTable();


        /// <summary>
        /// چنانچه دانشجو بدهکار باشد این متغیر تورو می شود
        /// </summary>
    //    public static double bedehi;
        /// <summary>
        ///در هنگام لود شدن صفحه وضعیت بدهی و ثبت نام کردن دانشجو در ترم جاری را چک می کند
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

            tr_msg.Visible = true;
            img_btn.Visible = true;
            if (!canRequest())
                return;
            if (!IsPostBack)
            {
                Session["EraeBeArray"] = null;
                if (dts.Rows.Count != 0)
                {
                    dt = business.GetStudentsInfo(Session[sessionNames.userID_StudentOstad].ToString());

                    DataColumnCollection columns = dt.Columns;
                    lbl_NamePrev.Text = dt.Rows[0]["firstName"].ToString();
                    lbl_FamilyPrev.Text = dt.Rows[0]["lastName"].ToString();
                    lbl_GerayeshPrev.Text = columns.Contains("namegeraesh") == false || dt.Rows[0]["namegeraesh"]?.ToString() == "" || dt.Rows[0]["namegeraesh"] == null ? "" : dt.Rows[0]["namegeraesh"]?.ToString();
                    lbl_MaghtaPrev.Text = dt.Rows[0]["magh"].ToString();
                    lbl_PhonePrev.Text = columns.Contains("homePhone") == false || dt.Rows[0]["homePhone"]?.ToString() == "" || dt.Rows[0]["homePhone"] == null ? "" : dt.Rows[0]["homePhone"]?.ToString();
                    lbl_PostiPrev.Text = columns.Contains("home_postalCode") == false || dt.Rows[0]["home_postalCode"]?.ToString() == "" || dt.Rows[0]["home_postalCode"] == null ? "" : dt.Rows[0]["home_postalCode"]?.ToString();

                    if (columns.Contains("workPlaceAddress") == true && dt.Rows[0]["workPlaceAddress"].ToString() != "")
                        lbl_AddressPrev.Text = dt.Rows[0]["workPlaceAddress"].ToString();
                    lbl_ReshteTahsiliPrev.Text = dt.Rows[0]["nameresh"].ToString();
                    lbl_SalVorudPrev.Text = dt.Rows[0]["enterYear"].ToString();
                    lbl_ShomareDaneshjuPrev.Text = Session[sessionNames.userID_StudentOstad].ToString();
                    NimsalVorudiCheck(int.Parse((dt.Rows[0]["enterTerm"]).ToString()));
                    lbl_MobilePrev.Text = dt.Rows[0]["mobile"].ToString();

                }

            }
        }
        /// <summary>
        /// برای چک کردن ورودی دانشجو می باشد که ورودی مهر است یا بهمن
        /// </summary>
        /// <param name="nimsal">چنانچه نیمسال 1 باشد یعنی ورودی مهر و چنانچه 2 باشد یعنی ورودی بهمن است</param>
        public void NimsalVorudiCheck(int nimsal)
        {
            if (nimsal == 1)
                lbl_VorudiPrev.Text = "مهر";
            if (nimsal == 2)
                lbl_VorudiPrev.Text = "بهمن";
        }

        /// <summary>
        /// در پایان درخواست گواهی،کلیه کنترل ها غیرفعال و پنهان می شوند
        /// </summary>
        public void EndMessage()
        {
            tbl_Main.Visible = false;
            lbl_AddressPrev.Visible = false;
            lbl_PhonePrev.Visible = false;
            lbl_PostiPrev.Visible = false;
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
            img_btn.Visible = false;
            lbl_VorudiPrev.Visible = false;

            lbl_JahateErae.Visible = false;
            txt_JahateErae.Visible = false;
            Image1.Visible = false;
            lbl_AddressPosti.Visible = false;
            lbl_Phone.Visible = false;
            lbl_Posti.Visible = false;
            btn_Taeid.Visible = false;
            lbl_Mobile.Visible = false;
            lbl_MobilePrev.Visible = false;



        }
        /// <summary>
        /// چنانچه کلید تایید زده شود درخواست ارسال گردیده و در دیتابیس ثبت می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btn_taeid_Click(object sender, EventArgs e)
        {
            if (!canRequest())
                return;
            if (string.IsNullOrEmpty(txt_JahateErae.Text.Trim()) && grd_AcceptEdit.Items.Count <= 0)
            {
                Session["RequestIdArray"] = null;
                Session["EraeBeArray"] = null;

                rwm_Validations.RadAlert("امکان خالی بودن جهت ارائه به وجود ندارد", null, 100, "خطا", null);
                return ;
            }
            if (!string.IsNullOrEmpty(txt_JahateErae.Text))
            {
                if (Session["EraeBeArray"] != null)
                    Session["EraeBeArray"] = Session["EraeBeArray"].ToString() + "," + txt_JahateErae.Text;
                else
                    Session["EraeBeArray"] = txt_JahateErae.Text;
            }

            CommonBusiness cmnb = new CommonBusiness();
            var dtgo = GovahiBusiness.GetGovahiStatus(Session[sessionNames.userID_StudentOstad].ToString());
            MashmulNumber = "";
            if (dtgo.Rows.Count > 0)
            {
                for (int i = 0; i < dtgo.Rows.Count; i++)
                {
                    if (dtgo.Rows[i]["MashmulNumber"].ToString() != "")
                    {
                        MashmulNumber = dtgo.Rows[i]["MashmulNumber"].ToString();
                        break;
                    }
                }
            }

            var eraeBeArray = Session["EraeBeArray"].ToString().Split(new char[] { ',' });

            var requestId = 0;
            int countRequest = 0;
            long mablagh = 120000 + (eraeBeArray.Length * 20000);
            var studentBalance = _walletBusiness.GetStudentCurrentBalance(Session[sessionNames.userID_StudentOstad].ToString());
            if (studentBalance >= mablagh)
            {
                for (var i = 0; i < eraeBeArray.Length; i++)
                {
                    if (Session["RequestIdArray"] != null)
                    {
                        var isEreaBeExists = business.CheckEraeBeExist(Session[sessionNames.userID_StudentOstad].ToString(), 3, 6, eraeBeArray[i]);
                        if (!isEreaBeExists)
                        {
                            requestId = int.Parse(business.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 3, 6, eraeBeArray[i], MashmulNumber, 1).ToString());
                            Session["RequestIdArray"] = Session["RequestIdArray"] + "," + requestId;
                        }
                        else
                        {
                            rwm_Validations.RadAlert("این درخواست قبلا برای شما ثبت شده است.", 300, 100, "پیام سیستم", "RedirectToGovahiVaziatUI");
                            return;//با این خط کد، اگه هر تعدادی درخواست رو ثبت کرده باشه و آخری تکراری باشه، کلا از پرداخت صرف نظر میشه!!
                        }
                    }

                    else
                    {
                        var isEreaBeExists = business.CheckEraeBeExist(Session[sessionNames.userID_StudentOstad].ToString(), 3, 6, eraeBeArray[i]);
                        if (!isEreaBeExists)
                        {
                            requestId = int.Parse(business.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 3, 6, eraeBeArray[i], MashmulNumber, 1).ToString());
                            Session["RequestIdArray"] = requestId.ToString();
                        }
                        else
                        {
                            rwm_Validations.RadAlert("این درخواست قبلا برای شما ثبت شده است.", 300, 100, "پیام سیستم", "RedirectToGovahiVaziatUI");
                            return;//با این خط کد، اگه هر تعدادی درخواست رو ثبت کرده باشه و آخری تکراری باشه، کلا از پرداخت صرف نظر میشه!!
                        }

                    }
                    if (requestId != 0)
                        countRequest++;
                }
            }
            else
            {
                rwm_Validations.RadAlert("موجودی کیف پول شما برای ثبت این درخواست کافی نیست.", 300, 100, "پیام سیستم", "RedirectToGovahiVaziatUI");
                return;
            }
            mablagh = 0;
            if (countRequest > 0)
            {
                mablagh = 120000 + (countRequest * 20000);

                var paymentStatus = GovahiBusiness.PaymentStatus(Session["RequestIdArray"].ToString(),
                     Session[sessionNames.userID_StudentOstad].ToString());//??? 3tasho bargardoond

                if (paymentStatus.Any(x => x.HasBeenPaid))
                {
                    if (paymentStatus.All(x => x.HasBeenPaid))
                    {
                        var paymentByPastPayments = GovahiBusiness.PaymentByPastPayments(Session["RequestIdArray"].ToString(),
                            Session[sessionNames.userID_StudentOstad].ToString());

                        Session["RequestIdArray"] = null;
                        Session["EraeBeArray"] = null;

                        rwm_Validations.RadAlert("شما به دلیل پرداخت قبلی نیاز به پرداخت ندارید", 300, 100, "پیام سیستم", "RedirectToGovahiVaziatUI");
                        return;
                    }
                    else
                    {
                        paymentStatus.ForEach(x =>
                        {
                            if (x.HasBeenPaid)
                            {
                                mablagh = mablagh - x.AmountTrans;

                            }
                        });
                    }
                }


                var msg = string.Empty;
                var transaction = new TransactionDTO
                {
                    Amount = mablagh,
                    stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                    TransactionTypeId = Convert.ToDecimal(TransactionTypeEnum.StudyingCertificate),
                };
                if (_walletBusiness.PayByWallet(transaction, out msg))
                {
                    var requestIdArray = Session["RequestIdArray"].ToString().Split(new char[] { ',' });
                    var payByPastPayments = GovahiBusiness.PaymentByPastPayments(Session["RequestIdArray"].ToString(),
                        Session[sessionNames.userID_StudentOstad].ToString());//???
                    Session["paymentStatus"] = payByPastPayments;



                    bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                    for (var j = 0; j < requestIdArray.Length; j++)
                    {
                        if (!(payByPastPayments[j].HasBeenPaid))
                        {
                            bmp.CreateRequestStudentPayment(new DTO.CommonClasses.PaymentDTO
                            {
                                Amount = payByPastPayments[j].AmountTrans,
                                AppStatus = "COMMIT",
                                Description = eraeBeArray[payByPastPayments[j].RowNumber - 1],
                                MiladiDate = DateTime.Now,
                                tterm = ConfigurationManager.AppSettings["Term"],
                                stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                                RequestId = payByPastPayments[j].Number,
                                Result = 0,
                                OrderId = _walletBusiness.GenerateOrderIdForRequests(),
                                bankId = 700,
                                TraceNumber = 0,
                                PayType = 1,
                                ReqKey = "WALLETPAYMENT",
                            });
                        }
                    }
                    cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 15, requestId.ToString());
                    Session["RequestIdArray"] = null;
                    Session["EraeBeArray"] = null;
                    rwm_Validations.RadAlert("پرداخت شما با موفقیت انجام گردید", null, 100, "گواهی اشتغال به تحصیل", "walletPaymentCallback");
                }
                else
                {
                    rwm_Validations.RadAlert(msg, null, 100, "گواهی اشتغال به تحصیل", "");
                }
            }
            #region
            //پرداخت مستقیم - حذف به علت جایگزینی روش پرداخت از طریق کیف پول - 990401
            /*
            try
            {
                long orderid;//= new bmp_PaymentBusiness().GenerateOrderId();


                SetDefaultDateTime();
                PaymentDTO pay = new PaymentDTO();
                bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                pay.Amount = mablagh;
                pay.PayDate = PayDate + "_" + PayTime;
                pay.stcode = Session[sessionNames.userID_StudentOstad].ToString();
                pay.bankId = 2;
                pay.tterm = ConfigurationManager.AppSettings["Term"];

                var result = bmp.pay(pay.Amount, pay.stcode, out orderid, Convert.ToInt32(Session[sessionNames.appID_StudentOstad]), 0);
                pay.OrderId = orderid;
                    //bpService.bpPayRequest(long.Parse(TerminalId),
                    //UserName,
                    //UserPassword,
                    //pay.OrderId,
                    //pay.Amount,
                    //PayDate,
                    //PayTime,
                    //pay.stcode,
                    //CallBackUrl, payerId);

                String[] resultArray = result.Split(',');

                //String[] resultArray = { "E5F656213D33E807", "E5F656213D33E805" };

                pay.ReqKey = resultArray[1];
                pay.AppStatus = "none";
                pay.TraceNumber = 0;
                pay.Result = -1;
                pay.PayType = 1;

                var requestIdArray = Session["RequestIdArray"].ToString().Split(new char[] { ',' });


                var paymentByPastPayments = GovahiBusiness.PaymentByPastPayments(Session["RequestIdArray"].ToString(),
                    Session[sessionNames.userID_StudentOstad].ToString());

                Session["paymentStatus"] = paymentByPastPayments;

                for (var j = 0; j < requestIdArray.Length; j++)
                {
                    if (!(paymentByPastPayments[j].HasBeenPaid))
                    {
                        pay.Amount = paymentByPastPayments[j].AmountTrans;
                        pay.Description = eraeBeArray[paymentByPastPayments[j].RowNumber - 1];
                        pay.RequestId = paymentByPastPayments[j].Number;
                        bmp.CreateRequestStudentPayment(pay);
                    }
                }


                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 15, requestId.ToString());


                if (resultArray[0] == "0")
                    ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
                Session["RequestIdArray"] = null;
                Session["EraeBeArray"] = null;

            }
            catch (Exception exp)
            {
                Response.Write("Error: " + exp.Message);
            }
            */
            #endregion

        }
        void SetDefaultDateTime()
        {
            PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        }

        private bool canRequest()
        {
            dts = GovahiBusiness.GetStRegisterd(Session[sessionNames.userID_StudentOstad].ToString());
            if (dts.Rows.Count == 0)
            {
                Session["RequestIdArray"] = null;
                Session["EraeBeArray"] = null;

                rwm_Validations.RadAlert("دانشجوی گرامی شما در این نیمسال ثبت نام نکرده اید.", 300, 100, "خطا", null);
                return false;
            }
            

            return true;
        }


        protected void img_btn_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_JahateErae.Text != "")
            {
                if (Session["EraeBeArray"] != null)
                {
                    Session["EraeBeArray"] = Session["EraeBeArray"].ToString() + "," + txt_JahateErae.Text;
                }
                else
                {

                    Session["EraeBeArray"] = txt_JahateErae.Text;
                }
                txt_JahateErae.Text = "";
                DataTable dtErae = new DataTable();

                dtErae.Columns.AddRange(new DataColumn[1] { new DataColumn("mahal") });
                if (Session["EraeBeArray"] != null)
                {
                    string[] EraeBeArray = Session["EraeBeArray"].ToString().Split(new char[] { ',' });
                    for (int i = 0; i < EraeBeArray.Length; i++)
                    {
                        dtErae.Rows.Add(EraeBeArray[i].ToString());

                    }
                }
                grd_AcceptEdit.DataSource = dtErae;
                grd_AcceptEdit.DataBind();

            }
            else
            {
                rwm_Validations.RadAlert("محل ارائه به باید تکمیل گردد ", null, 100, "خطا", "");

            }

        }

        protected void grd_AcceptEdit_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void grd_AcceptEdit_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DataTable dtErae = new DataTable();
            dtErae.Columns.AddRange(new DataColumn[1] { new DataColumn("mahal") });
            if (e.CommandName == "del")
            {
                string[] EraeBeArray = Session["EraeBeArray"].ToString().Split(new char[] { ',' });




                for (int i = 0; i < EraeBeArray.Length; i++)
                {
                    dtErae.Rows.Add(EraeBeArray[i].ToString());

                }
                dtErae.Rows[int.Parse(e.CommandArgument.ToString())].Delete();
                if (dtErae.Rows.Count > 0)
                {
                    for (int j = 0; j < dtErae.Rows.Count; j++)
                    {
                        if (j == 0)
                        {
                            Session["EraeBeArray"] = dtErae.Rows[j]["mahal"].ToString();
                        }
                        else
                        {
                            Session["EraeBeArray"] = Session["EraeBeArray"].ToString() + "," + dtErae.Rows[j]["mahal"].ToString();
                        }
                    }
                }
                else
                {
                    Session["EraeBeArray"] = null;
                }
            }
            grd_AcceptEdit.DataSource = dtErae;
            grd_AcceptEdit.DataBind();



        }





    }



}
