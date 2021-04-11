using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.university.Wallet;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DTO.University.Request;
using IAUEC_Apps.DTO.University.Wallet;
using IAUEC_Apps.UI.ir.shaparak.bpm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Request.Pages
{
    public partial class CheckOutRequestSubmit : System.Web.UI.Page
    {
        CheckOutRequestBusiness business = new CheckOutRequestBusiness();
        CheckOutPajooheshBusiness pajooheshBusiness = new CheckOutPajooheshBusiness();
        CheckOutMaliBusiness MaliBusiness = new CheckOutMaliBusiness();
        CheckOutRefahBusiness RefahBusiness = new CheckOutRefahBusiness();
        CommonBusiness CB = new CommonBusiness();
        WalletBusiness _walletBusiness = new WalletBusiness();
        DataTable dt_Pr_Req = null;
        const string thesisAddress_Doc = "thesisDoc";
        const string thesisAddress_PDF = "thesisPDF";
        const string reqID = "studentRequestID";
        int flag = 0;

        public static long StampAmount = 0;
        public static long DefenceAmount = 0;
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static string RefId = "";
        public static string PayDate = "";
        public static string PayTime = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState[thesisAddress_Doc] = "";
            ViewState[thesisAddress_PDF] = "";

            if (!IsPostBack)
            {
                LoadPage();
            }
        }

        private void LoadPage()
        {
            string pasdaranAddress = "", sandoghPosti = "";
            var address = CB.getBasicInformation((int)DTO.basicType.آدرس, 2);
            var sandogh = CB.getBasicInformation((int)DTO.basicType.کدپستی, 1);
            if (address.Rows.Count > 0 && address.Rows[0]["value"] != DBNull.Value)
                pasdaranAddress = address.Rows[0]["value"].ToString();
            if (sandogh.Rows.Count > 0 && sandogh.Rows[0]["value"] != DBNull.Value)
                sandoghPosti = sandogh.Rows[0]["value"].ToString();
            lbldaneshjooyi.Text = string.Format("{0} {1} {2} {3}", "توجه : دانشجوی گرامی شما باید کارت دانشجویی خود را به آدرس پستی ", pasdaranAddress + " و یا صندوق پستی", sandoghPosti, " - امور دانشجویی ارسال نمایید.در صورت مفقودی کارت برای دادن تعهد حضورا به امور دانشجویی مراجعه نمایید.در صورت بروز هر گونه مشکل با پست الکترونیک :STD@IAUEC.AC.IR مکاتبه نمایید.");
            lblEkhrajEnseraf.Text = string.Format("{0} {1} {2}", "*توجه : دانشجویان انصرافی یا اخراجی بعد از اتمام تسویه حساب ابتدا به بایگانی آموزش واقع در ساختمان پاسداران به آدرس", pasdaranAddress, " جهت اخذ مدارک مشمولیت و سپس به امور دانشجویی واقع در همان ساختمان مراجعه نمایند.");
            string stCode = Session[sessionNames.userID_StudentOstad].ToString();
            var bach = business.GetIsBachelor(stCode);
            drpCheckOutType.Items.Add(new ListItem { Text = "انتخاب کنید", Value = "0" });
            drpCheckOutType.Items.Add(new ListItem { Text = "تسویه حساب فارغ التحصیل", Value = "15" });
            drpCheckOutType.Items.Add(new ListItem { Text = "تسویه حساب انصراف", Value = "16" });
            drpCheckOutType.Items.Add(new ListItem { Text = "تسویه حساب اخراج", Value = "14" });
            //var lnkProp = pnlThesisInfo.Visible;
            //pnlProposalInfo.Visible = false;
            //if (bach != 1)
            //    drpCheckOutType.Items.Add(new ListItem { Text = "تسویه حساب تغییر رشته", Value = "13" });
            dt_Pr_Req = business.GetCheckOutInfoByStCode(stCode);
            int Erae = 0;
            CheckOutNaghsBusiness CheckBusiness = new CheckOutNaghsBusiness();
            if (dt_Pr_Req.Rows.Count > 0)
            {
                DataRow[] drActiveRequests = dt_Pr_Req.Select("RequestLogID<>5");
                if (drActiveRequests.Length > 0)
                {
                    ViewState[reqID] = drActiveRequests[0]["studentrequestID"].ToString();
                    ViewState["requestTypeID"] = Convert.ToInt32(drActiveRequests[0]["RequesttypeID"]);
                    //Erae = Convert.ToInt32(dtNaghsArray[0]["RequestLogID"]);
                    Erae = Convert.ToInt32(drActiveRequests[0]["Erae_Be"]);

                    var RId = Convert.ToInt64(drActiveRequests[0]["StudentRequestID"]);
                    ViewState.Add("Erae", Erae);
                    ViewState.Add("stcode", stCode);
                    ViewState.Add("requestId", drActiveRequests[0]["StudentRequestID"]);
                    if (showPayPanel(RId, stCode, Convert.ToInt32(drActiveRequests[0]["RequestLogId"]), Erae))
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalStampPay();", true);

                    var dtAllNaghs = CheckBusiness.GetallNotResolvedNaghsByReqId(Convert.ToInt32(ViewState[reqID].ToString()));

                    if (dtAllNaghs.Rows.Count > 0)
                    {
                        lblMessage.Visible = true;
                        //  lblMessage.Text = naghsMsg;
                        lblMessage.Text += "<ul>";

                        for (int i = 0; i < dtAllNaghs.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dtAllNaghs.Rows[i]["RequestLogId"]) != 22 && Convert.ToInt32(dtAllNaghs.Rows[i]["RequestLogId"]) != 23)
                            {
                                if (Convert.ToInt32(dtAllNaghs.Rows[i]["RequestLogId"]) == 30 && dtAllNaghs.Select("RequestLogId=29") != null)
                                    break;
                                lblMessage.Text += "<li>" + dtAllNaghs.Rows[i]["NaghsMessage"] + "</li>";
                            }

                            if (Convert.ToInt32(dtAllNaghs.Rows[i]["RequestLogId"]) == 22 || Convert.ToInt32(dtAllNaghs.Rows[i]["RequestLogId"]) == 23)
                            {
                                lblMessage.Text += "<li>" + "دانشجوی گرامی پرونده شما نیاز به بازنگری دارد در صورت هرگونه سوال به کارشناس دانشکده تیکت ارسال نمایید. " + "</li>";
                            }

                        }
                        lblMessage.Text += "</ul>";
                    }

                }

            }
            ViewState.Add("Erae", Erae);
            ViewState.Add("stcode", stCode);
            if (!hasRequest(stCode))
            {
                ToAddMode();
            }
            else
            {
                ToReviewMode(dt_Pr_Req);
            }

            DataTable naghsMsg = CheckBusiness.GetNaghsMessage(stCode);
            //var dtNaghs = CheckBusiness.GetNaghsByStCode(stCode);
            DataTable dt = pajooheshBusiness.GetStudentInfoForPajohesh(stCode);
            if (naghsMsg != null && naghsMsg.Rows.Count > 0)
            {
                lblMessage.Visible = true;
                lblMessage.Text += "<ul>";
                for (int i = 0; i < naghsMsg.Rows.Count; i++)
                {
                    if (Convert.ToInt32(naghsMsg.Rows[i]["RequestLogId"]) == 27)
                    {
                        lblMessage.Text += "<li>" + "نقص فرم انصراف از مقاله" + "</li>";
                        lnkCancelArticle.Visible = true;
                    }
                    if (Convert.ToInt32(naghsMsg.Rows[i]["RequestLogId"]) == 28)
                    {
                        lblMessage.Text += "<li>" + "پایان نامه اصلاحات دارد" + "</li>";
                        lnkCancelArticle.Visible = true;


                        lblEditThes.Text = dt.Rows[0]["EditThes"].ToString();
                        dveditThes.Visible = true;
                        lblEditThes.Visible = true;
                        lnkEditThes.Visible = true;
                    }

                }
                lblMessage.Text += "</ul>";
            }

        }

        private bool showPayPanel(Int64 reqID, string stcode, int checkoutStatus, int eraeBe)
        {
            commitRollbackPayments();

            bool show;
            show = showPanelPayStampAmount(reqID, stcode, checkoutStatus, eraeBe);
            show = showPanelPayDefenceAmount(stcode,(int)reqID) || show;
            if (!show)
            {
                CheckOutNaghsBusiness CNB = new CheckOutNaghsBusiness();
                //if (eraeBe == 31)
                //{
                StudentRequest SR = new StudentRequest();
                SR.StudentRequestId = (int)reqID;
                SR.StCode = stcode;
                SR.RequestLogId = checkoutStatus;
                SR.Erae_Be = eraeBe== ((int)CheckOutStatusEnum.CheckOutAllStatusEnum.stampPay)?((int)CheckOutStatusEnum.CheckOutAllStatusEnum.maali).ToString(): eraeBe.ToString();
                SR.HasStamp= !business.hasStampDefect(reqID);
                CNB.UpdateStudentRequest(SR);
            }
            //txtTotalPay.Value = (StampAmount+DefenceAmount).ToString() + "  ريال";
            return show;
        }

        private bool showPanelPayDefenceAmount(string stcode, int reqID)
        {
            RequestPaymentBusiness RPB = new RequestPaymentBusiness();
            txtDefence_payment.Value = "0";
            dvPayDefence.Visible = false;
            DefenceAmount = 0;
            var dt = RPB.GetDefencePaymentByStcode(stcode);
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("AppStatus='commit'");
                if (dr.Length == 0)
                {
                    DefenceAmount = Convert.ToInt64(dt.Rows[0]["Money"]);
                    txtDefence_payment.Value = dt.Rows[0]["Money"].ToString() + "  ريال";
                    dvPayDefence.Visible = true;
                    return true;

                }
                else
                {
                    setLog("پرداخت هزینه دفاع آنلاین دانشجو", (int)reqID, 53);
                    return false;

                }

            }
            return false;
        }

        private bool showPanelPayStampAmount(Int64 reqID, string stcode, int checkoutStatus, int eraeBe)
        {
            dvPayStamp.Visible = false;
            StampAmount = 0;
            if (Convert.ToInt32(ViewState["requestTypeID"]) == (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil && checkoutStatus < (int)CheckOutStatusEnum.CheckOutAllStatusEnum.maali)
            {
                RequestPaymentBusiness RPB = new RequestPaymentBusiness();
                var Payment = RPB.GetPaymentByRequestID(reqID, 3);
                //_ = Payment.Select();
                if (Payment.Rows.Count > 0)
                {
                    DataRow[] dr = Payment.Select("AppStatus = 'COMMIT'");
                    if (dr.Length > 0)
                    {
                        bool hasDefenc = business.hasStampDefect(reqID);
                        if (hasDefenc)
                        {
                            CheckOutNaghsBusiness CNB = new CheckOutNaghsBusiness();
                            //if (eraeBe == 31)
                            //{
                            StudentRequest SR = new StudentRequest();
                            SR.StudentRequestId = (int)reqID;
                            SR.StCode = stcode;
                            SR.RequestLogId = checkoutStatus;
                            SR.Erae_Be =  eraeBe.ToString();
                            SR.HasStamp = true;
                            CNB.UpdateStudentRequest(SR);
                            //}
                            CNB.ResolveNaghsByMessage((int)reqID, 29, dr[0]["PaymentID"].ToString());
                            setLog("پرداخت مبلغ تمبر", (int)reqID, 49);
                            return false;
                        }
                    }
                }
                bool hasDefect = business.hasStampDefect(reqID);
                if (hasDefect)
                {
                    StampAmount = Convert.ToInt64(ConfigurationManager.AppSettings["StampAmount"]);
                    dvPayStamp.Visible = true;
                    txtStamp_payment.Value = StampAmount.ToString() + "  ريال";

                }
                return hasDefect;
            }
            return false;
        }

        private bool showPanelRequeststatus(Int64 reqID, int eraeBe)
        {
            if (Convert.ToInt32(ViewState["requestTypeID"]) == (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil)
            {
                var stamp = business.hasStampDefect(reqID);
                if (stamp && eraeBe > (int)(CheckOutStatusEnum.CheckOutAllStatusEnum.refah))
                {
                    return false;
                }

            }
            return true;
        }

        private void commitRollbackPayments()
        {
            RequestPaymentBusiness Pb = new RequestPaymentBusiness();

            Pb.checkRollbackPayments(DTO.University.Request.PayType.stamp, Session[sessionNames.userID_StudentOstad].ToString());

        }

        private void ToReviewMode(DataTable dt)
        {
            string studentCode = Session[sessionNames.userID_StudentOstad].ToString();
            pnlAddMode.Visible = false;
            dvReqType.Visible = false;
            dvHelpGraduate.Visible = false;

            if (business.GetIsBachelor(studentCode) == 1)
            {
                lblPlzUplLoad.Visible = false;
            }


            if (dt.Rows.Count > 0 && dt.Rows[0]["sex"].ToString() == "1")
            {
                if (dt.Rows[0]["RequestTypeID"].ToString() != "13")
                {
                    lblNezamVazife.Visible = true;
                }
                else
                {
                    lblNezamVazife.Visible = false;
                }
            }

            int status = Convert.ToInt32(dt.Rows[0]["requestLogId"]);
            if (status >= (int)CheckOutStatusEnum.CheckOutAllStatusEnum.pajohesh)
            {
                lblPlzUplLoad.Visible = false;
                lblHasThes.Visible = false;
            }
            if (status >= (int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshjooyi)
            {
                lbldaneshjooyi.Visible = false;
                if (status == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshjooyi)
                {
                    if (!showThesisUploader())
                    {
                        lblHasThes.Text = "دانشجوی گرامی پایان نامه شما بارگذاری شده و بعد از بررسی روند تسویه حساب ادامه پیدا خواهد کرد.";
                        lblHasThes.Visible = true;
                    }
                }

            }
            pnlThesisInfo.Visible = showThesisUploader();

            var LastRow = dt.Rows.Count - 1;
            int requestType = Convert.ToInt32(dt.Rows[LastRow]["RequestTypeID"]);
            if (requestType == (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil)
            {
                dvHelpGraduate.Visible = true;

                    flag = 0;
                string stCode = Session[sessionNames.userID_StudentOstad].ToString();
                //var tabelMadarek = business.GetStudentMadrakInfo(stCode);
                FeraghatTahsilBusiness bFeraghat1 = new FeraghatTahsilBusiness();
                var oFeraghat1 = bFeraghat1.GetFeraghatMadrekStatus(Convert.ToInt32(dt.Rows[LastRow]["StudentRequestID"]));

                if (oFeraghat1 != null || dt.Rows[LastRow]["RequestLogID"].ToString() == ((int)CheckOutStatusEnum.FareghReqStatus.ersal_sodoor_ok).ToString())
                {
                    pnlFeraghatTahsil.Visible = true;
                    lblTitle.Text = "سامانه فراغت از تحصیل غیر حضوری";

                    if (oFeraghat1 != null)
                    {
                        if (Convert.ToBoolean(oFeraghat1.DaneshNameh))
                        {
                            lblDaneshNameh.Text = "آماده شده";
                        }
                        if (Convert.ToBoolean(oFeraghat1.GovahiMovaghat))
                        {
                            var mash = business.isMashmoolferaghat(oFeraghat1.Stcode);
                            if (mash != null)
                            {
                                if (mash > 0)
                                {
                                    string pasdaranAddress = "";
                                    var address = CB.getBasicInformation((int)DTO.basicType.آدرس, 2);
                                    if (address.Rows.Count > 0 && address.Rows[0]["value"] != DBNull.Value)
                                        pasdaranAddress = address.Rows[0]["value"].ToString();
                                    string alert = "دانشجوی گرامی گواهینامه موقت شما درمحل دانشگاه واقع در " + pasdaranAddress + "  آماده تحویل می باشد. تحویل گواهینامه موقت فقط به خود فارغ التحصیل یا وکیل قانونی وی و در صورت ارائه ی برگه اعزام به خدمت ،گواهی اشتغال به تحصیل در مقاطع بالاتر و یا همراه داشتن کارت پایان خدمت، امکان پذیر است،  لطفاً قبل از مراجعه به تقويم كاري دانشگاه (سايت واحد) توجه نماييد";
                                    RadWindowManager1.RadAlert(alert, 0, 200, "هشدار", "");
                                }

                            }

                            lblGovahiMovaghat.Text = "آماده شده";
                        }
                        if (Convert.ToBoolean(oFeraghat1.RizNomarat))
                        {
                            lblRizNomarat.Text = "آماده شده";
                        }
                    }
                }
                else
                    if (Convert.ToInt32(dt.Rows[0]["RequestLogID"]) >= (int)CheckOutStatusEnum.CheckOutAllStatusEnum.end + 1)
                {

                    pnlFeraghatTahsil.Visible = true;
                    lblTitle.Text = "سامانه فراغت از تحصیل غیر حضوری";
                    FeraghatTahsilBusiness bFeraghat = new FeraghatTahsilBusiness();
                    var oFeraghat = bFeraghat.GetFeraghatMadrekStatus(Convert.ToInt32(dt.Rows[0]["StudentRequestID"]));
                    if (oFeraghat != null)
                    {
                        if (Convert.ToBoolean(oFeraghat.DaneshNameh))
                        {
                            lblDaneshNameh.Text = "آماده شده";
                        }
                        if (Convert.ToBoolean(oFeraghat.GovahiMovaghat))
                        {
                            lblGovahiMovaghat.Text = "آماده شده";
                        }
                        if (Convert.ToBoolean(oFeraghat.RizNomarat))
                        {
                            lblRizNomarat.Text = "آماده شده";
                        }
                    }
                }
                else
                {
                    Panel2.Visible = true;
                    pnlFeraghatTahsil.Visible = false;
                    lblTitle.Text = "سامانه تسویه حساب غیر حضوری";
                    CheckOutNaghsBusiness _NaghsBusiness = new CheckOutNaghsBusiness();

                    //var Msg = dt.Rows[0]["message"].ToString();
                    // DataTable NaghsTable = _NaghsBusiness.GetallNotResolvedNaghsByReqId(Convert.ToInt32(dt.Rows[LastRow]["StudentRequestID"].ToString()));


                    grdPreviousReq.DataSource = dt;
                    grdPreviousReq.DataBind();
                    grdPreviousReq.Visible = showPanelRequeststatus(Convert.ToInt64(dt.Rows[0]["studentrequestid"]), Convert.ToInt32(dt.Rows[0]["erae_be"]));
                }
            }

            else if (requestType == (int)CheckOutStatusEnum.CheckOutType.ekhraj || requestType == (int)CheckOutStatusEnum.CheckOutType.enseraf || requestType == (int)CheckOutStatusEnum.CheckOutType.taqir_reshte || requestType == (int)CheckOutStatusEnum.CheckOutType.enteqali)
            {
                lblEkhrajEnseraf.Visible = true;
                Panel2.Visible = true;
                lblTitle.Text = "سامانه تسویه حساب غیر حضوری";
                grdPreviousReq.DataSource = dt;
                grdPreviousReq.DataBind();
                //if (Convert.ToInt32(ViewState["Erae_Be"]) == (int)CheckOutStatusEnum.FareghReqStatus.end)
                //{
                //    lblTakmil.Visible = true;
                //}
                //else
                //{
                //    lblTakmil.Visible = false;
                //}
            }


            if (requestType == (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil)
            {
                if (Convert.ToInt32(ViewState["Erae"]) == (int)CheckOutStatusEnum.FareghReqStatus.archive_ok)
                {
                    // lblTakmil.Visible = true;
                }
                else
                {
                    lblTakmil.Visible = false;
                }
            }
            else
            {
                lblTakmil.Visible = false;

            }
            //********************************************
        }

        private void ToAddMode()
        {
            dvReqType.Visible = true;
            pnlThesisInfo.Visible = false;
            lblTitle.Text = "سامانه فراغت از تحصیل غیر حضوری";
        }

        protected void btnSubmitCheckOutRequest_Click(object sender, EventArgs e)
        {
            string stCode = Session[sessionNames.userID_StudentOstad].ToString();
            int isbachelor = business.GetIsBachelor(stCode);
            var lastID = business.exist_IdMelli(stCode);
            if (lastID > 0)
            {
                string alert = "دانشجوی گرامی درخواست دیگری با همین شماره ملی در سامانه تسویه حساب  با شماره درخواست " + lastID + " ثبت شده است لطفا پس از تعیین تکلیف درخواست قبلی مجددا تلاش کنید";
                RadWindowManager1.RadAlert(alert, 0, 200, "هشدار", "");
            }
            else
            {
                var mash = business.isMashmoolferaghat(stCode);
                if (mash != null)
                {
                    if (mash > 0)
                    {
                        string alert = "دانشجوی گرامی با توجه به اینکه شما مشمول خدمت نظام وظیفه می باشید لازم است بموقع نسبت به ثبت درخواست تسویه حساب اقدام نمایید . تا مشمول غیبت سربازی نشوید.";
                        RadWindowManager1.RadAlert(alert, 0, 200, "هشدار", "");
                    }

                }
                if (int.Parse(drpCheckOutType.SelectedValue) != (int)CheckOutStatusEnum.CheckOutType.enseraf)
                    NewRequest(stCode, isbachelor);
                else
                    openReasonPopup();
            }
        }

        private void saveCheckoutReason(string stcode, Int64 reqid, string reason)
        {
            int reasonid = business.insertCheckoutReason(reqid, stcode, reason);
            setLog("علت تسویه حساب:" + reason, reasonid, 51);

        }

        private void openReasonPopup()
        {
            string scrp5 = "function f(){$find(\"" + rwCheckoutReason.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp5, true);
        }
        private Int64 NewRequest(string stCode, int isbachelor)
        {
            CheckOutNaghsBusiness CheckBusiness = new CheckOutNaghsBusiness();
            bool hasNaghs = CheckBusiness.HasNaghs(stCode);
            Int64 reqID = 0;
            if (hasNaghs == true)
            {
                string alert = "دانشجوی گرامی مدارک شما نقص دارد";
                RadWindowManager1.RadAlert(alert, 0, 200, "هشدار", "");
            }
            if (int.Parse(drpCheckOutType.SelectedValue) == (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil)
            {
                reqID = newRequest_Faregh(stCode, isbachelor);
            }
            else
            {
                reqID = RegisterCheckOut(stCode, isbachelor);
            }
            return reqID;
        }

        private Int64 newRequest_Faregh(string stCode, int isbachelor)
        {
            Int64 reqID = 0;

            if (business.hasPassedCoursesToSubmitGraduateRequest(stCode, isbachelor))
            {
                reqID = RegisterCheckOut(stCode, isbachelor);
            }
            else
            {
                string alert = "تعداد واحدهای گذرانده شما کمتر از سقف مجاز برای ثبت درخواست تسویه حساب می باشد!";
                RadWindowManager1.RadAlert(alert, 0, 200, "هشدار", "");
            }

            return reqID;
        }

        private Int64 RegisterCheckOut(string stCode, int isbachelor)
        {
            string Status = "";
            if (drpCheckOutType.SelectedIndex != 0)
            {
                // string stCode = Session["user"].ToString();
                int currentstatus = Convert.ToInt32(CheckOutStatusEnum.FareghReqStatus.submited);
                int nextstatus = 0;

                switch (Convert.ToInt32(drpCheckOutType.SelectedValue))
                {
                    case (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil:
                        if (isbachelor != 1)
                        {


                            if (pajooheshBusiness.IsFinilized(stCode))
                            {
                                nextstatus = (int)(CheckOutStatusEnum.FareghReqStatus.daneshkade_ok);
                            }
                            else
                            {
                                string alert = "شما به دلیل عدم تکمیل موارد مربوط به پایان نامه قادر به ثبت درخواست تسویه حساب نیستید!";
                                RadWindowManager1.RadAlert(alert, 0, 200, "هشدار", "");
                                return 0;
                            }
                        }
                        Status = "فارغ التحصیلی";
                        break;
                    case (int)CheckOutStatusEnum.CheckOutType.ekhraj:
                        if (chkAcceptTrems.Checked == true)
                        {
                            nextstatus = (int)(((CheckOutStatusEnum.EkhrajStatus)currentstatus).Next());
                        }
                        else
                        {
                            string alert = "لطفا تایید کنید که بخشنامه امور مربوط به وقفه تحصیلی ، انصراف از تحصیل و... را مطالعه و شرایط آن را قبول دارید. ";
                            RadWindowManager1.RadAlert(alert, 0, 200, "هشدار", "");
                            return 0;
                        }
                        Status = "اخراج";
                        break;
                    case (int)CheckOutStatusEnum.CheckOutType.taqir_reshte:
                        nextstatus = (int)(((CheckOutStatusEnum.TaghirReshteStatus)currentstatus).Next());
                        Status = "تغییر رشته";
                        break;
                    case (int)CheckOutStatusEnum.CheckOutType.enteqali:
                        nextstatus = (int)(((CheckOutStatusEnum.EnteghaliStatus)currentstatus).Next());
                        Status = "انتقالی";
                        break;

                    case (int)CheckOutStatusEnum.CheckOutType.enseraf:
                        if (chkAcceptTrems.Checked == true)
                        {
                            nextstatus = (int)(((CheckOutStatusEnum.EnserafReqStatus)currentstatus).Next());
                        }
                        else
                        {
                            string alert = "لطفا تایید کنید که بخشنامه امور مربوط به وقفه تحصیلی ، انصراف از تحصیل و... را مطالعه و شرایط آن را قبول دارید. ";
                            RadWindowManager1.RadAlert(alert, 0, 200, "هشدار", "");
                            return 0;
                        }
                        Status = "انصراف";
                        break;
                    default:
                        break;
                }


                DataTable check;
                check = business.checkExistingRequest(stCode);

                Session.Add("stCode", stCode);
                Session.Add("currentstatus", currentstatus);
                Session.Add("nextstatus", nextstatus);

                Int64 RequestID;
                if (check.Rows.Count == 0)//condition //////
                {
                    lblConfirmMessage.ForeColor = Color.Red;
                    lblConfirmMessage.Text = Status;

                    //radConfirm.VisibleOnPageLoad = true; 

                    string CreateDate = DateTime.Now.ToPeString();
                    RequestID = business.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), Convert.ToInt32(drpCheckOutType.SelectedValue), (int)Session["currentstatus"],
                        Session["nextstatus"].ToString(), "", CreateDate, "در حال بررسی", 1, true);
                    setLog("ثبت درخواست تسویه حساب " + (CheckOutStatusEnum.CheckOutTypeFarsi)Convert.ToInt32(drpCheckOutType.SelectedValue), (int)RequestID, 26);
                    Page.Response.Redirect(Page.Request.Url.ToString(), false);
                }
                else
                {
                    string alert = "درخواست قبلی شما در حال بررسی است";
                    RadWindowManager1.RadAlert(alert, 0, 200, "هشدار", "");
                    return 0;
                }
                if (Convert.ToInt32(drpCheckOutType.SelectedValue) == (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil)
                {
                    DataTable dtNaghs = pajooheshBusiness.GetStudentsNaghs(stCode);
                    if (dtNaghs.Rows.Count > 0)
                    {
                        //if (dtNaghs.Rows[0]["StudentRequestID"] == DBNull.Value)
                        //{
                        //    busi.UpdateStudentsReqIDinNaghs(stCode, RequestID);
                        //}
                        foreach (DataRow row in dtNaghs.Rows)
                            if (row["StudentRequestID"] == DBNull.Value)
                                pajooheshBusiness.UpdateStudentsReqIDinNaghs(stCode, (int)RequestID);
                    }
                }
                return RequestID;
            }
            else
            {
                string alert = "لطفا نوع تسویه حساب خود را مشخص نمایید";
                RadWindowManager1.RadAlert(alert, 0, 100, "هشدار", "");
                drpCheckOutType.Style.Add("border", "1px solid red");
                return 0;
            }
        }

        private bool hasRequest(string stCode)
        {
            if (dt_Pr_Req.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void grdPreviousReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Reqtype = null;
                string stCode = ViewState["stcode"].ToString();
                CheckBoxList chbk = (CheckBoxList)e.Row.FindControl("chkStatus");
                int counter = 0;
                DataRowView dr = (DataRowView)e.Row.DataItem;
                Type en = typeof(CheckOutStatusEnum.EnserafReqStatus);
                int Erae_Be = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Erae_Be"));
                ViewState.Add("erae_be", Erae_Be);
                int RequestLogID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RequestLogID"));

                if (RequestLogID != 5 || flag == 1)
                {
                    divDetails.Visible = true;
                    // divTel.Visible = true;
                    btnNewRequest.Visible = false;
                    var bach = business.GetIsBachelor(stCode);
                    var requestTypeId = dr["RequestTypeID"].ToString();
                    switch (Convert.ToInt32(dr["RequestTypeID"].ToString()))
                    {
                        case (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil:
                            if (bach == 1)
                            {
                                en = typeof(CheckOutStatusEnum.FareghReqStatusBacheLor);
                            }
                            else
                            {
                                en = typeof(CheckOutStatusEnum.FareghReqStatus);
                            }

                            break;
                        case (int)CheckOutStatusEnum.CheckOutType.enseraf:
                            en = typeof(CheckOutStatusEnum.EnserafReqStatus);
                            break;
                        case (int)CheckOutStatusEnum.CheckOutType.ekhraj:
                            en = typeof(CheckOutStatusEnum.EkhrajStatus);
                            break;
                        case (int)CheckOutStatusEnum.CheckOutType.taqir_reshte:
                            en = typeof(CheckOutStatusEnum.TaghirReshteStatus);
                            break;
                        case (int)CheckOutStatusEnum.CheckOutType.enteqali:
                            en = typeof(CheckOutStatusEnum.EnteghaliStatus);
                            break;
                        default:
                            break;
                    }

                    foreach (var status in Enum.GetValues(en))
                    {

                        ListItem li = new ListItem();

                        li.Text = business.GetStatusNote((int)status);
                        li.Value = Convert.ToInt32(status).ToString();


                        //     if (Convert.ToInt32(status) != (int)CheckOutStatusEnum.FareghReqStatus.fareghotahsilan_ok)

                        #region if
                        if (Convert.ToInt32(requestTypeId) != (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil)
                        {
                            if (Convert.ToInt32(status) != (int)CheckOutStatusEnum.FareghReqStatus.vrood_moavenat_ok && Convert.ToInt32(status) != (int)CheckOutStatusEnum.FareghReqStatus.ersal_sodoor_ok && Convert.ToInt32(status) != (int)CheckOutStatusEnum.FareghReqStatus.takmil_parvande_ok)
                            {

                                if (li.Text != "پایان")
                                {
                                    chbk.Items.Add(li);

                                    if (Convert.ToInt32(chbk.Items[counter].Value) <= RequestLogID)
                                    {
                                        chbk.Items[counter].Selected = true;

                                    }

                                    //if (Erae_Be == (int)CheckOutStatusEnum.FareghReqStatus.end)
                                    //{
                                    //    chbk.Items[counter].Selected = true;

                                    //}

                                    else if (Convert.ToInt32(chbk.Items[counter].Value) == Erae_Be)
                                    {
                                        li.Attributes.Add("id", "current");

                                    }
                                    counter++;
                                }
                            }
                            else
                            {
                                chbk.Items[counter - 1].Attributes.Add("id", "current");

                            }

                        }
                        else
                        {
                            if (li.Value != 25.ToString())
                            {
                                if (li.Value != 31.ToString())
                                {
                                    chbk.Items.Add(li);

                                    if (Convert.ToInt32(chbk.Items[counter].Value) <= RequestLogID && Convert.ToInt32(chbk.Items[counter].Value) != (int)CheckOutStatusEnum.CheckOutAllStatusEnum.end)
                                    {
                                        chbk.Items[counter].Selected = true;

                                    }

                                    if (Erae_Be == (int)CheckOutStatusEnum.FareghReqStatus.end)
                                    {
                                        //TODO Opening Window
                                        chbk.Items[counter].Selected = true;
                                    }

                                    else if (Convert.ToInt32(chbk.Items[counter].Value) == Erae_Be)
                                    {
                                        li.Attributes.Add("id", "current");
                                    }
                                }
                            }
                            counter++;
                        }
                        #endregion
                        //if (Erae_Be == (int)CheckOutStatusEnum.FareghReqStatus.fareghotahsilan_ok)
                        //{
                        //    chbk.Items[counter - 1].Attributes.Add("id", "current");
                        //}

                    }

                    if (Erae_Be == (int)CheckOutStatusEnum.EnserafReqStatus.refah_ok)
                    {
                        DataTable dt = RefahBusiness.GetAllDebitByStcode(stCode);

                        var result = dt.AsEnumerable()
                                        .Where(r => r.Field<int>("DebitTypeID") != (int)CheckOutRefahEnum.DebitType.Bestankar);
                        if (result.Any())
                        {
                            DataTable debits = result.CopyToDataTable();
                            pnlBedehkar.Visible = true;
                            grdBedehi.DataSource = debits;
                            grdBedehi.DataBind();
                        }

                        var resultVezaratLoan = dt.AsEnumerable()
                                                .Where(r => r.Field<int>("DebitTypeID") == (int)CheckOutRefahEnum.DebitType.Vezarat_Loan);
                        if (resultVezaratLoan.Any())
                        {
                            dvVezaratLoan.Visible = true;
                            if (hasAddress(stCode))
                            {
                                dvAddress.Visible = false;
                            }
                            ViewState.Add("magh", dr["magh"].ToString());
                            drpProvince.DataSource = CB.GetState();
                            drpProvince.DataTextField = "STATE_NAME";
                            drpProvince.DataValueField = "STATE_CODE";
                            drpProvince.DataBind();
                            drpProvince.Items.Insert(0, "انتخاب کنید");
                            string scrp3 = "var objCal4 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDateStudyEnd', {extraInputID: 'ContentPlaceHolder1_txtDateStudyEnd',extraInputFormat: 'yyyy/mm/dd'}); ";
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp3, true);
                        }

                        var bestankari = dt.AsEnumerable()
                                          .Where(r => r.Field<int>("DebitTypeID") == (int)CheckOutRefahEnum.DebitType.Bestankar);
                        if (bestankari.Any() && !HasAcountInfo(stCode))
                        {
                            pnlBestankar.Visible = true;
                        }
                    }

                    if (Erae_Be >= (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
                    {
                        CheckMaliStatus(stCode);
                    }
                    //if (Erae_Be == (int)CheckOutStatusEnum.EnserafReqStatus.archive_ok)
                    //{
                    //    lblTakmil.Visible = true;
                    //}
                    Button btn = (Button)e.Row.FindControl("btnSubmitMsg");
                    if (dr["message"].ToString() != "")//ستون عملیات نشان داده شود یا خیر؟
                    {
                        //  grdPreviousReq.Columns[3].Visible = true;
                        //  btnSubmitMsg.Visible = true;
                        btn.Visible = true;

                    }
                    else
                    {
                        // grdPreviousReq.Columns[3].Visible = false;
                        // btnSubmitMsg.Visible = false;
                        btn.Visible = false;

                    }

                    Label lb = new Label();
                    Panel pnl = new Panel();
                    // Button btn = (Button)e.Row.FindControl("btnSubmitMsg");

                    if (!String.IsNullOrWhiteSpace(dr["StudentMessage"].ToString()))
                    {
                        lb.Text = dr[11].ToString();
                        e.Row.Cells[3].Controls.Remove(btn);
                        pnl.Controls.Add(lb);
                        e.Row.Cells[3].Controls.Add(pnl);
                    }
                }
                else
                {
                    btnNewRequest.Visible = true;
                    switch (dr["RequestTypeID"].ToString())
                    {
                        case "15":
                            Reqtype = "فارغ التحصیلی";
                            break;
                        case "16":
                            Reqtype = "انصراف";
                            break;
                        case "14":
                            Reqtype = "اخراج";
                            break;
                        case "13":
                            Reqtype = "تغییر رشته";
                            break;
                        case "17":
                            Reqtype = "انتقالی";
                            break;
                        default:
                            break;
                    }
                    Label lblDeny = (Label)e.Row.FindControl("lblDeny");
                    lblDeny.Text = " درخواست " + Reqtype + " شما رد شده است ";
                    lblDeny.Visible = true;
                    grdPreviousReq.Columns[3].Visible = false;
                    //lblNezamVazife.Visible = false;
                    //lbldaneshjooyi.Visible = false;
                    //lblEkhrajEnseraf.Visible = false;
                    //lblRequestState.Visible = false;
                    divDetails.Visible = false;
                    //  divTel.Visible = false;
                }
            }
        }

        private bool hasAddress(string stCode)
        {
            if (RefahBusiness.HasAddress(stCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CheckMaliStatus(string stCode)
        {
            DataTable dtbedehi = MaliBusiness.GetAllMaliDebitByStcode(stCode);
            if (dtbedehi.Rows.Count > 0)
            {
                if (dtbedehi.Columns.Count > 2)
                {
                    var bestankari = dtbedehi.AsEnumerable()
                        .Where(r => r.Field<int>("DebitTypeID") == (int)CheckOutRefahEnum.DebitType.Bestankar);
                    if (bestankari.Any())
                    {
                        if (!HasAcountInfo(stCode))
                        {
                            pnlBestankar.Visible = true;
                        }
                    }

                    //var bedehkari = dtbedehi.AsEnumerable()
                    //    .Where(r => r.Field<int>("DebitTypeID") != (int)CheckOutRefahEnum.DebitType.Bestankar)
                    //    .ToList();
                    DataTable bedehi = new DataTable();

                    foreach (DataColumn item in dtbedehi.Columns)
                    {
                        bedehi.Columns.Add(item.ColumnName, item.DataType);
                    }

                    foreach (DataRow item in dtbedehi.Rows)
                    {
                        if (item["DebitTypeID"].ToString() != "8")
                        {
                            bedehi.Rows.Add(item.ItemArray);
                        }
                    }
                    var refID = Convert.ToUInt32(dtbedehi.Rows[0]["RefID"].ToString());
                    if (bedehi.Rows.Count > 0 && refID != 0)
                    {
                        pnlBedehkar.Visible = true;
                        grdBedehi.DataSource = bedehi;
                        grdBedehi.DataBind();
                    }
                }

                //int bedehiCount = MaliBusiness.CheckMaliCheckOut(stCode);

                //if (bedehiCount > 0)
                //{
                //    var result = dtbedehi.AsEnumerable().Where(r => r.Field<int>("DebitTypeID") != (int)CheckOutRefahEnum.DebitType.Vezarat_Loan
                //                                              && r.Field<int>("DebitTypeID") != (int)CheckOutRefahEnum.DebitType.Bestankar);
                //    if (result.Any())
                //    {
                //        DataTable bedehi = result.CopyToDataTable();
                //        pnlBedehkar.Visible = true;
                //        grdBedehi.DataSource = bedehi;
                //        grdBedehi.DataBind();
                //    }
                //}
            }
        }

        private bool HasAcountInfo(string stCode)
        {
            if (MaliBusiness.HasAcountInfo(stCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        protected void grdPreviousReq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "msg")
            {
                int reqID = Convert.ToInt32(e.CommandArgument);
                RadWindow2.VisibleOnPageLoad = true;
                ViewState.Add("reqID", reqID);
            }
            if (e.CommandName == "history")
            {
                //int reqID = Convert.ToInt32(e.CommandArgument);
                HiddenField hdnReqId = (HiddenField)((Button)e.CommandSource).Parent.FindControl("hdnReqId");
                if (hdnReqId != null)
                {
                    int reqID = Convert.ToInt32(hdnReqId.Value);
                    string stCode = Session["user"].ToString();
                    DataTable dtHistory = new DataTable();
                    dtHistory = CB.GetUserLogByModifyId(reqID, 12);
                    var dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("Name") });
                    if ((dtHistory.AsEnumerable().Any(r => r.Field<int>("EventId") == 37 || r.Field<int>("EventId") == 168)))
                    {
                        lst_history.Visible = true;

                        var result = dtHistory.AsEnumerable().Where(r => r.Field<int>("EventId") == 37 || r.Field<int>("EventId") == 168).CopyToDataTable();//.Select<(s=> dt.NewRow().SetField<string>(0, s.Field<string>("Name")));//37 : ارسال پیام
                        lst_history.DataSource = result;
                        lst_history.DataBind();
                    }
                    else
                    {
                        lst_history.Visible = false;

                    }
                    //info1.InnerText = "نام دانشجو:" + stName;
                    //info2.InnerText = "شماره درخواست:" + reqID;
                    //info3.InnerText = "تاریخ درخواست:" + reqDate;
                    //string scrp4 = "function f(){$find(\"" + rwnd_history.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    //ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp4, true);

                    //RadScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){ $('#exampleModal').modal('show');}, 1000);", true);
                    //modalPopup.OpenerElementID = ((Button)e.CommandSource).ClientID;
                    modalPopup.VisibleOnPageLoad = true;
                }
            }
            LoadPage();
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        protected void btnConfirmOk_Click(object sender, EventArgs e)
        {
            string CreateDate = DateTime.Now.ToPeString();
            int requestID = business.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), Convert.ToInt32(drpCheckOutType.SelectedValue), (int)Session["currentstatus"], Session["nextstatus"].ToString(), "", CreateDate, "در حال بررسی", 1, true);
            setLog("ثبت درخواست تسویه حساب " + (CheckOutStatusEnum.CheckOutTypeFarsi)Convert.ToInt32(drpCheckOutType.SelectedValue), (int)requestID, 26);
            Page.Response.Redirect(Page.Request.Url.ToString(), false);

        }
        protected void btnSubmitMsg_Click(object sender, EventArgs e)
        {
            string issuerID = Session["user"].ToString();
            int reqID = Convert.ToInt32(ViewState["reqID"]);
            string sys_msg;
            try
            {
                business.SendMessageStudent(issuerID, reqID, txtMsg.Text);
                sys_msg = "پیام شما با موفقیت ارسال شد ";
                business.UpdateStatusOfStMsgUnRead(reqID);
                business.UpdateStudentLastUpdate(reqID);
            }
            catch (Exception)
            {
                sys_msg = "خطایی در سیستم رخ داده ، لطفا دوباره تلاش کنید";
                throw;
            }
            RadWindowManager1.RadAlert(sys_msg, 0, 200, "پیام سیستم", "");
            RadWindow2.VisibleOnPageLoad = false;
            //LoadPage();
            Response.Redirect("~/University/Request/Pages/CheckOutRequestSubmit.aspx");
        }

        protected void drpCheckOutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpCheckOutType.SelectedValue == "15")
            {

                pnlAddMode.Visible = true;
                pnlGraduate.Visible = true;
                pnlEnseraf.Visible = false;

            }
            if (drpCheckOutType.SelectedValue == "16")
            {

                pnlAddMode.Visible = true;
                pnlEnseraf.Visible = true;
                flpGovahiEshteghal.Visible = true;
                dvGovahiEshteghal.Visible = true;
                pnlGraduate.Visible = false;
            }
            if (drpCheckOutType.SelectedValue == "14")
            {

                pnlAddMode.Visible = true;
                pnlEnseraf.Visible = true;
                dvGovahiEshteghal.Visible = false;
                flpGovahiEshteghal.Visible = false;
                pnlGraduate.Visible = false;
            }
            if (drpCheckOutType.SelectedValue == "13")
            {

                pnlAddMode.Visible = true;
                pnlEnseraf.Visible = false;
                pnlGraduate.Visible = false;
                //وضعیت تغییر رشته
            }
            if (drpCheckOutType.SelectedValue == "17")
            {

                pnlAddMode.Visible = true;
                pnlEnseraf.Visible = false;
                pnlGraduate.Visible = false;
                //وضعیت انتقالی
            }
            if (drpCheckOutType.SelectedIndex == 0)
            {
                pnlAddMode.Visible = false;
            }
        }

        protected void rdblBankType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdblBankType.SelectedValue == "1")
            {
                dvBankeMeli.Visible = true;
                dvBankOther.Visible = false;
            }
            else
            {
                dvBankOther.Visible = true;
                dvBankeMeli.Visible = false;
            }
        }

        protected void btnSubmitAcount_Click(object sender, EventArgs e)
        {
            string msg;
            string stCode = Session["user"].ToString();
            if (rdblBankType.SelectedValue == "1")
            {
                if (!string.IsNullOrWhiteSpace(txtBankMeliID.Text) && !string.IsNullOrWhiteSpace(txtAcountOwner.Text))
                {
                    msg = MaliBusiness.AddBankMeliAcountInfo(stCode, txtBankMeliID.Text, txtAcountOwner.Text, txtPhoneNumber.Text);
                    RadWindowManager1.RadAlert(msg, 0, 100, "پیام سیستم", "reloadpage");
                }
            }
            if (rdblBankType.SelectedValue == "2")
            {
                if (!string.IsNullOrWhiteSpace(txtSheba.Text) && !string.IsNullOrWhiteSpace(txtBankName.Text) && !string.IsNullOrWhiteSpace(txtAcountOwner.Text))
                {
                    msg = MaliBusiness.AddBankOtherAcountInfo(stCode, txtSheba.Text, txtBankName.Text, txtAcountOwner.Text, txtPhoneNumber.Text);
                    RadWindowManager1.RadAlert(msg, 0, 100, "پیام سیستم", "reloadpage");
                }
            }
        }

        protected void grdBedehi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                if (!string.IsNullOrWhiteSpace(drv["DebitTypeID"].ToString()))
                {
                    e.Row.Cells[0].Text = GetPersianDataType(drv["DebitTypeID"].ToString());
                }

                if (!string.IsNullOrWhiteSpace(drv["FishNumber"].ToString()))
                {
                    Button btn = (Button)e.Row.FindControl("btnSubmitFish");
                    btn.Text = "ویرایش فیش";
                }
            }
        }

        private string GetPersianDataType(string p)
        {
            string str;
            switch (p)
            {
                case "1":
                    str = "وام وزارت علوم";
                    break;
                case "2":
                    str = "چک برگشتی";
                    break;
                case "3":
                    str = "وام ازدواج";
                    break;
                case "4":
                    str = "وام تامین شهریه";
                    break;
                case "5":
                    str = "وام کمک هزینه تحصیلی";
                    break;
                case "6":
                    str = "وام مسکن";
                    break;
                case "7":
                    str = "وام ضروری";
                    break;
                case "8":
                    str = "بستانکاری از دانشگاه";
                    break;
                case "9":
                    str = "شهریه";
                    break;
                case "10":
                    str = "وام بلند مدت";
                    break;
                case "11":
                    str = "پرداخت در آموزشیار";
                    break;
                default:
                    str = "خطا";
                    break;
            }
            return str;
        }

        protected void grdBedehi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int DebitID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "submitFish")
            {
                GridViewRow curruntRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                lblDebitTypeName.Text = curruntRow.Cells[0].Text;
                lblAmount.Text = curruntRow.Cells[2].Text;
                txtFishNumber.Text = "";
                txtFishDate.Text = "";
                txtDebitNote.Text = "";
                hfDebit.Value = DebitID.ToString();
                RadWindow1.VisibleOnPageLoad = true;
            }
        }

        protected void btnSubmitFish_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string fishNumber = "^[A-Za-z0-9\\u0600-\u06FF\\s _]*[A-Za-z0-9\\u0600-\\u06FF\\s][A-Za-z0-9\\u0600-\\u06FF\\s _]{1,20}$";
                string textAndNumber = "^[A-Za-z0-9\\u0600-\u06FF\\s _]*[A-Za-z0-9\\u0600-\\u06FF\\s][A-Za-z0-9\\u0600-\\u06FF\\s _]{0,200}$";
                string date = "\\d{4}(?:/\\d{1,2}){2}";
                string strError = null;
                if (!new Regex(fishNumber).IsMatch(txtFishNumber.Text))
                {
                    strError = "شماره فیش وارد شده نامعتبر است";
                    goto label;
                }
                if (!new Regex(date).IsMatch(txtFishDate.Text))
                {
                    strError = "فرمت تاریخ وارد شده صحیح نمی باشد";
                    goto label;
                }
                if (!string.IsNullOrWhiteSpace(txtDebitNote.Text))
                {
                    if (!new Regex(textAndNumber).IsMatch(txtDebitNote.Text))
                    {
                        strError = "توضیحات فقط می تواند شامل حروف و اعداد باشد";
                    }
                }
            label: if (!string.IsNullOrWhiteSpace(strError))
                {
                    lblFishError.Text = strError;
                    lblFishError.Visible = true;
                    RadWindow1.VisibleOnPageLoad = true;
                    return;
                }

                int erae_be = Convert.ToInt32(ViewState["erae_be"]);

                try
                {
                    if (erae_be == (int)CheckOutStatusEnum.EnserafReqStatus.refah_ok)
                    {
                        RefahBusiness.AddFishInfo(Session["user"].ToString(), Convert.ToInt32(hfDebit.Value), txtFishNumber.Text, txtFishDate.Text, txtDebitNote.Text);
                        business.UpdateStudentLastUpdate(Convert.ToInt32(ViewState[reqID]));
                    }
                    if (erae_be == (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
                    {
                        MaliBusiness.UpdateDebit(Session["user"].ToString(), Convert.ToInt32(hfDebit.Value), txtFishNumber.Text, txtFishDate.Text, txtDebitNote.Text);
                        business.UpdateStudentLastUpdate(Convert.ToInt32(ViewState[reqID]));
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void flpGovahiEshteghal_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            if (e.IsValid)
            {
                if (flpGovahiEshteghal.UploadedFiles.Count != 0)
                {
                    string newfilename = Session["user"].ToString() + e.File.GetExtension();
                    if (!Directory.Exists(Server.MapPath(flpGovahiEshteghal.TargetFolder)))
                        Directory.CreateDirectory(Server.MapPath(flpGovahiEshteghal.TargetFolder));
                    e.File.SaveAs(Path.Combine(Server.MapPath(flpGovahiEshteghal.TargetFolder), newfilename));
                }
            }
        }

        protected void drpProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpProvince.SelectedIndex != 0)
            {
                drpCity.DataSource = CB.getCity(Convert.ToInt32(drpProvince.SelectedValue));
                drpCity.DataTextField = "CITY_NAME";
                drpCity.DataValueField = "CITY_CODE";
                drpCity.DataBind();
                drpCity.Items.Insert(0, "انتخاب کنید");
                ScriptManager.RegisterStartupScript(this, GetType(), ClientID, "SetDatePicker();", true);
            }
        }

        protected void dvVezaratLoan_Load(object sender, EventArgs e)
        {
            int maghNow = Convert.ToInt32(ViewState["magh"]);
            int maghPast = RefahBusiness.GetStudentPastMaghta(ViewState["stcode"].ToString());

            DataTable dtMaghaate = RefahBusiness.GetLastUniInfo(ViewState["stcode"].ToString());
            grdPastMaghta.DataSource = dtMaghaate;
            grdPastMaghta.DataBind();
        }

        protected void btnPastUniInfoSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                CheckOutStLastUniInfo AddressInfo = new CheckOutStLastUniInfo();
                AddressInfo.StCode = Session["user"].ToString();
                AddressInfo.ProvinceID = Convert.ToInt32(drpProvince.SelectedValue);
                AddressInfo.CityID = Convert.ToInt32(drpCity.SelectedValue);
                AddressInfo.Street = txtStreet.Text;
                AddressInfo.Alley = txtAlley.Text;
                AddressInfo.Pelak = txtPelak.Text;
                AddressInfo.ZipCode = txtZipCode.Text;
                AddressInfo.Phone = txtTel.Text;
                AddressInfo.Mobile = txtMobile.Text;
                AddressInfo.Email = txtEmail.Text;
                AddressInfo.RabetMobile = txtRabetMobile.Text;
                AddressInfo.RabetPhone = txtRabetTel.Text;

                try
                {
                    RefahBusiness.AddStudentAddressInfo(AddressInfo);
                    string strMessage = "آدرس شما با موفقیت ثبت گردید.";
                    RadWindowManager1.RadAlert(strMessage, 0, 200, "پیام سیستم", "refresgGrid");
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        protected void btnShowMaghat_Click(object sender, EventArgs e)
        {
            RadWindow3.VisibleOnPageLoad = true;
        }

        protected void btnSubmitMaghta_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                CheckOutStLastUniInfo MaghtaInfo = new CheckOutStLastUniInfo();
                MaghtaInfo.StCode = Session["user"].ToString();
                MaghtaInfo.TypeOfUni = Convert.ToInt32(rdblLastUniType.SelectedValue);
                MaghtaInfo.UniName = txtUniName.Text;
                MaghtaInfo.FieldName = txtFieldName.Text;
                int maghta = 0;
                if (rdblMadrakType.SelectedValue == "1")
                {
                    if (rdblMaghta.SelectedValue == "2")
                    {
                        maghta = 8;
                    }
                    else
                    {
                        maghta = Convert.ToInt32(rdblMaghta.SelectedValue);
                    }
                }
                else
                {
                    if (rdblMaghta.SelectedValue == "2")
                    {
                        maghta = 2;
                    }
                    if (rdblMaghta.SelectedValue == "1")
                    {
                        maghta = 3;
                    }
                    if (rdblMaghta.SelectedValue == "4")
                    {
                        maghta = 5;
                    }
                }

                if (RefahBusiness.HasThisMaghta(Session["user"].ToString(), maghta))
                {
                    string errorString = "شما قبلا مشخصات این مقطع را ثبت کرده اید ";
                    RadWindowManager1.RadAlert(errorString, 200, 100, "خطا", "");
                    return;
                }

                MaghtaInfo.Maghta = maghta;
                MaghtaInfo.FeraghatYear = txtFareghYear.Text;
                MaghtaInfo.FeraghatHalfYear = Convert.ToInt32(rdblNimsalFeraghat.SelectedValue);
                MaghtaInfo.FeraghatType = Convert.ToInt32(rdblStudyEndType.SelectedValue);
                MaghtaInfo.CheckOutStatus = Convert.ToInt32(rdblCheckOutTypeLastUni.SelectedValue);
                MaghtaInfo.FeraghatDate = txtDateStudyEnd.Text;
                MaghtaInfo.IsMashmool = Convert.ToInt32(rdblPayanKhedmat.SelectedValue);
                MaghtaInfo.LoanAmount = txtVezaratLoanAmount.Text;

                if (flpMadrakImage.UploadedFiles.Count > 0)
                {
                    UploadedFile obj = flpMadrakImage.UploadedFiles[0];
                    string subPath = Server.MapPath("~/University/Request/Pages/ScanMadarek/") + MaghtaInfo.StCode.ToString() + "\\";

                    bool exists = System.IO.Directory.Exists(subPath);

                    if (!exists)
                        System.IO.Directory.CreateDirectory(subPath);
                    try
                    {
                        MaghtaInfo.ScanMadarekURL = subPath + maghta.ToString() + obj.GetName();
                        obj.SaveAs(MaghtaInfo.ScanMadarekURL, true);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
                else
                {
                    MaghtaInfo.ScanMadarekURL = "بدون تصویر";
                }

                try
                {
                    RefahBusiness.AddStudentLastUniInfo(MaghtaInfo);
                    string strMessage = "اطلاعات مقطع  " + rdblMaghta.SelectedItem + " شما با موفقیت ثبت گردید.";
                    RadWindowManager1.RadAlert(strMessage, 0, 200, "پیام سیستم", "");
                    RadWindow3.VisibleOnPageLoad = false;
                    DataTable dtMaghaate = RefahBusiness.GetLastUniInfo(MaghtaInfo.StCode);
                    grdPastMaghta.DataSource = dtMaghaate;
                    grdPastMaghta.DataBind();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        protected void btnNewRequest_Click(object sender, EventArgs e)
        {
            flag = 1;
            ToAddMode();
            btnNewRequest.Visible = false;
            Panel2.Visible = false;
            divDetails.Visible = false;
            // divTel.Visible = false;
        }

        public string GetReqNote(int UserId)
        {
            LoginBusiness lngB = new LoginBusiness();
            DataTable userdt = lngB.Get_UserRoles(UserId.ToString());
            string RoleName;
            if (UserId != Convert.ToInt32(Session[sessionNames.userID_StudentOstad]))
            {
                RoleName = userdt.Rows[0]["RoleName"].ToString();
            }
            else
            {
                RoleName = "دانشجو";
            }

            //CheckOutNaghsBusiness NaghsBus = new CheckOutNaghsBusiness();
            //DataTable request = business.GetCheckOutInfoByReqId(modifyID);

            ////var desc = request.Rows[0]["message"].ToString();
            //     int reqLogID = -1;
            //if (desc.Contains("نقص: "))
            //{

            //    reqLogID = ((int)CheckOutStatusEnum.FareghReqStatus.ersal_sodoor_ok); 
            //}
            //else
            //{
            //    reqLogID =Convert.ToInt32( request.Rows[0]["RequestLogId"].ToString());
            //   // reqLogID -= 1;
            //}
            return RoleName;// business.GetPersianStatus(Convert.ToInt32(reqLogID));
        }

        protected void ChkUpProp_CheckedChanged(object sender, EventArgs e)
        {
            divUploadThesis.Visible = ChkUpProp.Checked;
        }

        private bool showThesisUploader()
        {
            DataTable dt = business.GetCheckOutInfoByStCode(ViewState["stcode"].ToString());
            DataRow[] dr = dt.Select("RequestLogID <>5 and RequestTypeID=15");
            if (dr.Length == 1)
            {
                int eraeBe = Convert.ToInt32(dr[0]["erae_be"]);
                if (eraeBe == (int)CheckOutStatusEnum.FareghReqStatus.pajohesh_ok)
                {
                    DataTable dtThesis = business.getThesisByStudentID(Session[sessionNames.userID_StudentOstad].ToString());
                    bool payanNameNeedUpload = true;
                    if (dtThesis.Rows.Count > 0)
                    {
                        if (dtThesis.Rows[0]["status"] != DBNull.Value)
                            payanNameNeedUpload = dtThesis.Rows[0]["status"].ToString() != "1";
                    }
                    bool isStudentFaregh = dr[0]["requestTypeID"].ToString() == ((int)CheckOutStatusEnum.CheckOutType.fareq_tahsil).ToString();// ViewState["requestTypeID"].ToString() == "15";
                    if (payanNameNeedUpload && isStudentFaregh)
                        return true;

                }
            }

            return false;
        }

        protected void thesisDocUpload_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            uploadThesis(thesisDocUpload, thesisAddress_Doc, ref e);

        }

        protected void thesisPdfUpload_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            uploadThesis(thesisPdfUpload, thesisAddress_PDF, ref e);
        }

        private void uploadThesis(RadAsyncUpload RadUpload, string viewStateKey, ref FileUploadedEventArgs e)
        {
            ViewState[viewStateKey] = "";
            if (e.IsValid)
            {
                if (ChkUpProp.Checked)
                {
                    if (RadUpload.UploadedFiles.Count != 0)
                    {
                        string propfilename = Session[sessionNames.userID_StudentOstad].ToString() + e.File.GetExtension();
                        if (!Directory.Exists(Server.MapPath(RadUpload.TargetFolder)))
                            Directory.CreateDirectory(Server.MapPath(RadUpload.TargetFolder));
                        try
                        {
                            e.File.SaveAs(Path.Combine(Server.MapPath(RadUpload.TargetFolder), propfilename));
                            ViewState[viewStateKey] = RadUpload.TargetFolder + "\\" + propfilename;// Path.Combine(Server.MapPath(RadUpload.TargetFolder), propfilename);
                        }
                        catch
                        {
                            e.IsValid = false;
                            ViewState[viewStateKey] = "error";
                            RadWindowManager1.RadAlert("زمان آپلود پایان نامه خطایی رخ داده است. لطفا مجددا تلاش فرمایید.", 0, 200, "خطا", "");
                        }

                    }
                }
                else
                {
                    e.IsValid = false;
                    ViewState[viewStateKey] = "error";
                    string strMessage = "دانشجوی گرامی اگر آیین نامه نگارش پایان نامه را مطالعه کرده اید باید تیک مطالعه آیین نامه نگارش پایان نامه را بزنید";
                    RadWindowManager1.RadAlert(strMessage, 0, 200, "خطا", "");
                }
            }
        }

        protected void btnUploadThesis_Click(object sender, EventArgs e)
        {
            if (ViewState[thesisAddress_Doc].ToString() != "error" && ViewState[thesisAddress_PDF].ToString() != "error")
            {

                if (ViewState[thesisAddress_Doc].ToString() == "" || ViewState[thesisAddress_PDF].ToString() == "")
                {
                    RadWindowManager1.RadAlert("دانشجوی گرامی، پایان نامه باید به دو صورت پی دی اف و ورد آپلود شود. لطفا در آپلود پایان نامه دقت فرمایید.", 200, 100, "پیام", "");

                }
                else if (File.Exists(Server.MapPath(ViewState[thesisAddress_Doc].ToString())) && File.Exists(Server.MapPath(ViewState[thesisAddress_PDF].ToString())) &&
                    business.insertThesisFile(Session[sessionNames.userID_StudentOstad].ToString(), ViewState[thesisAddress_Doc].ToString(), ViewState[thesisAddress_PDF].ToString()))
                {
                    CB.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToString("HH:mm"),
                        Convert.ToInt32(Session[sessionNames.appID_StudentOstad]), 39, "", Convert.ToInt32(ViewState[reqID].ToString()));
                    RadWindowManager1.RadAlert("پایان نامه جهت بررسی ارسال شد.", 200, 100, "پیام", "");
                    business.UpdateStudentLastUpdate(Convert.ToInt32(ViewState[reqID]));
                    LoadPage();
                }
                else
                {
                    RadWindowManager1.RadAlert("خطایی زمان ارسال پایان نامه رخ داده است. لطفا مجددا تلاش فرمایید.", 200, 100, "خطا", "");
                }
            }
        }

        protected void BtnStampPay_Click(object sender, EventArgs e)
        {
            var msg = string.Empty;
            var transaction = new TransactionDTO
            {
                Amount = 60000,
                stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                TransactionTypeId = Convert.ToDecimal(TransactionTypeEnum.StampPurchase),
            };
            if (_walletBusiness.PayByWallet(transaction, out msg))
            {
                bmp_PaymentBusiness bmp = new bmp_PaymentBusiness();
                bmp.CreateRequestStudentPayment(new DTO.CommonClasses.PaymentDTO
                {
                    Amount = Convert.ToInt64(transaction.Amount),
                    AppStatus = "COMMIT",
                    Description = "پرداخت هزینه تمبر از طریق کیف پول",
                    MiladiDate = DateTime.Now,
                    tterm = ConfigurationManager.AppSettings["Term"],
                    stcode = Session[sessionNames.userID_StudentOstad].ToString(),
                    RequestId = int.Parse(ViewState["requestId"].ToString()),
                    Result = 0,
                    OrderId = _walletBusiness.GenerateOrderIdForRequests(),
                    bankId = 700,
                    TraceNumber = 0,
                    PayType = 3,
                    ReqKey = "WALLETPAYMENT",
                });
                RadWindowManager1.RadAlert("پرداخت شما با موفقیت انجام گردید", null, 100, "پرداخت هزینه تمبر", "walletPaymentCallback");
            }
            else
            {
                RadWindowManager1.RadAlert(msg, null, 100, "پرداخت هزینه تمبر", "");
            }

            //پرداخت مستقیم - حذف به علت جایگزینی روش پرداخت از طریق کیف پول - 990401
            /*
            var pay = new PaymentDTO();
            var bmp = new bmp_PaymentBusiness();


            pay.Amount = StampAmount;
            pay.stcode = Session[sessionNames.userID_StudentOstad].ToString();
            pay.bankId = 2;
            pay.tterm = ConfigurationManager.AppSettings["Term"];
            pay.Description = "پرداخت تمبر";
            pay.RequestId = int.Parse(ViewState["requestId"].ToString());
            pay.PayType = 3;//تمبر

            long orderID;
            var result = bmp.pay(pay.Amount, pay.stcode, out orderID, Convert.ToInt32(Session[sessionNames.appID_StudentOstad]), 3);

            pay.OrderId = orderID;
            String[] resultArray = result.Split(',');
            pay.ReqKey = resultArray[1];
            bmp.CreateRequestStudentPayment(pay);


            if (resultArray[0] == "0")
                ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
                */
        }

        protected void btnSaveReason_Click(object sender, EventArgs e)
        {
            if (txtCheckoutReason.Text.Trim() != "")
            {
                string stCode = Session[sessionNames.userID_StudentOstad].ToString();
                int isbachelor = business.GetIsBachelor(stCode);
                Int64 reqID = NewRequest(stCode, isbachelor);
                if (reqID > 0)
                {
                    saveCheckoutReason(stCode, reqID, txtCheckoutReason.Text.Trim());
                }

            }
        }

        private void setLog(string description, int modifyID, int eventID)
        {
            CB.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToString("HH:mm"), 12, eventID, description, modifyID);
        }

        protected void btnDefencePay_ServerClick(object sender, EventArgs e)
        {
            var pay = new DTO.CommonClasses.PaymentDTO();
            var bmp = new bmp_PaymentBusiness();


            pay.Amount = DefenceAmount;
            pay.stcode = Session[sessionNames.userID_StudentOstad].ToString();
            pay.bankId = 2;
            pay.tterm = ConfigurationManager.AppSettings["Term"];
            pay.Description = "پرداخت هزینه دفاع آنلاین دانشجو";
            pay.RequestId = int.Parse(ViewState["requestId"].ToString());
            pay.PayType = 4;//دفاع آنلاین

            long orderID;
            var result = bmp.pay(pay.Amount, pay.stcode, out orderID, Convert.ToInt32(Session[sessionNames.appID_StudentOstad]), 3);

            pay.OrderId = orderID;
            String[] resultArray = result.Split(',');
            pay.ReqKey = resultArray[1];
            bmp.CreateRequestStudentPayment(pay);


            if (resultArray[0] == "0")
                ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
        }
    }



    public static class StringExtension
    {
        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }
    }
}