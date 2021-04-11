using System;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;
using System.Configuration;
using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.UI.ir.shaparak.bpm;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Request.Pages
{
    public partial class GovahiVaziatUI : System.Web.UI.Page
    {
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["Mellat_Request_CallBackUrl"];
        public static readonly string TerminalId = ConfigurationManager.AppSettings["Mellat_TerminalId"];
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["UserPassword"];

        public static string RefId = "";
        public static string PayDate = "";
        public static string PayTime = "";
        /// <summary>
        ///  ایجاد شده استRequestGovahiBusiness یک شئ از کلاس
        /// </summary>
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        RequestStudentCartBusiness business = new RequestStudentCartBusiness();
        /// <summary>
        /// با لود شدن صفحه، اطلاعات شامل، درخواست های قبلی دانشجو در گیرید ویو نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dtgo = GovahiBusiness.GetGovahiStatus(Session[sessionNames.userID_StudentOstad].ToString());
                if (dtgo.Rows.Count > 0)
                {
                    grd_GovahiRequestState.DataSource = dtgo;
                    grd_GovahiRequestState.DataBind();
                    grd_GovahiRequestState.Visible = true;

                }

                var dt = GovahiBusiness.GetAmountForPay(Session[sessionNames.userID_StudentOstad].ToString());
                grd_pay.DataSource = dt;
                if (dt.Rows.Count > 0 && (int.Parse(dt.Rows[0]["amount"].ToString())) > 0)
                {
                    grd_pay.Visible = true;
                }
                else
                {
                    grd_pay.Visible = false;
                }
            }


        }
        /// <summary>
        /// اطلاعات شامل، درخواست های قبلی دانشجو در گیرید ویو نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void grd_GovahiRequeststate_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtgo = new DataTable();
            dtgo = GovahiBusiness.GetGovahiStatus(Session[sessionNames.userID_StudentOstad].ToString());
            if (dtgo.Rows.Count > 0)
            {
                grd_GovahiRequestState.Visible = true;
                grd_GovahiRequestState.DataSource = dtgo;
            }
        }

        protected void lnk_Pardakht_Click(object sender, EventArgs e)
        {



        }
        void SetDefaultDateTime()
        {
            PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        }

        protected void grd_GovahiRequestState_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var cmnb = new CommonBusiness();

            try
            {
                if (e.CommandName == "EditEraeBe")
                {
                    var txtEditEraeBe = e.Item.FindControl("txt_EditEraeBe") as TextBox;
                    if (txtEditEraeBe != null)
                        GovahiBusiness.UpdateEraeBe(txtEditEraeBe.Text, int.Parse(e.CommandArgument.ToString()));
                    business.UpdateStudentRequestLogID(Session[sessionNames.userID_StudentOstad].ToString(), 6, 3, int.Parse(e.CommandArgument.ToString()));
                    GovahiBusiness.UpdateStudentPOstNumber(Session[sessionNames.userID_StudentOstad].ToString(), "", 3, int.Parse(e.CommandArgument.ToString()));
                    cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 19, "ویرایش محل ارائه");

                    var dtgo = GovahiBusiness.GetGovahiStatus(Session[sessionNames.userID_StudentOstad].ToString());
                    if (dtgo.Rows.Count > 0)
                    {
                        grd_GovahiRequestState.Visible = true;
                        grd_GovahiRequestState.DataSource = dtgo;
                        grd_GovahiRequestState.DataBind();
                    }
                }

                if (e.CommandName == "pay")
                {
                    var dtbedehi = GovahiBusiness.GetBedehkar(Session[sessionNames.userID_StudentOstad].ToString());
                    var dts = GovahiBusiness.GetStRegisterd(Session[sessionNames.userID_StudentOstad].ToString());
                    var bedehi = Convert.ToDouble((dtbedehi.Rows[0]["bedehi"].ToString()));
                    var dtmojazgovahi = GovahiBusiness.GetMojazGovahi(Session[sessionNames.userID_StudentOstad].ToString());

                    if (!GovahiBusiness.CanPay(Session[sessionNames.userID_StudentOstad].ToString()))
                    {
                        rwm_Validations.RadAlert("درخواست شما برای ترم کنونی نمی باشد لطفا درخواست ها رو حذف نمایید و مجددادرخواستی ثبت نمایید", 600, 100, "خطا", null);
                        return;
                    }
                    if (dtmojazgovahi.Rows.Count == 0 && bedehi > 0)
                    {

                        rwm_Validations.RadAlert("به علت بدهکاری درخواست گواهی اشتغال به تحصیل مقدور نمی باشد ", 420, 100, "خطا", null);
                        return;
                    }
                    else if (dts.Rows.Count == 0)
                    {

                        rwm_Validations.RadAlert("دانشجو در این ترم ثبت نام نکرده است", 300, 100, "خطا", null);
                        return;
                    }

                    long orderid;//= new bmp_PaymentBusiness().GenerateOrderId();


                    SetDefaultDateTime();
                    var pay = new PaymentDTO();
                    var bmp = new bmp_PaymentBusiness();
                    var dt = GovahiBusiness.GetAmountForPay(Session[sessionNames.userID_StudentOstad].ToString());
                    grd_pay.DataSource = dt;
                    pay.Amount = int.Parse(dt.Rows[0]["mablagh"].ToString());
                    pay.PayDate = PayDate + "_" + PayTime;
                    pay.stcode = Session[sessionNames.userID_StudentOstad].ToString();
                    pay.bankId = 2;
                    pay.tterm = ConfigurationManager.AppSettings["Term"];

                    var result = bmp.pay(pay.Amount, pay.stcode,out orderid, Convert.ToInt32(Session[sessionNames.appID_StudentOstad]), 0);
                    pay.OrderId = orderid;
                        

                    var resultArray = result.Split(',');

                    pay.ReqKey = resultArray[1];
                    pay.AppStatus = "none";
                    pay.TraceNumber = 0;
                    pay.Result = -1;
                    pay.PayType = 1;
                    var reqIdArray = Session["ReqID"].ToString().Split(new char[] { ',' });
                    var payId = Session["PayID"].ToString().Split(new char[] { ',' });

                    for (var i = 0; i < reqIdArray.Length; i++)
                    {

                        if (i == 0)
                        {
                            pay.RequestId = int.Parse(reqIdArray[i]);
                            GovahiBusiness.UpdatePymentDetail(pay.OrderId, pay.PayDate, int.Parse(payId[i]), 140000, pay.ReqKey);

                        }
                        else
                        {
                            pay.RequestId = int.Parse(reqIdArray[i]);
                            GovahiBusiness.UpdatePymentDetail(pay.OrderId, pay.PayDate, int.Parse(payId[i]), 20000, pay.ReqKey);


                        }

                    }
                    Session["ReqID"] = null;
                    Session["PayID"] = null;

                    if (resultArray[0] == "0")
                        ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
                }

                if (e.CommandName == "Del")
                {
                    var deloCommands = e.CommandArgument.ToString().Split(new char[] { ',' });
                    GovahiBusiness.DeleteGovahiRequest(int.Parse(deloCommands[0]), int.Parse(deloCommands[1]));
                    business.InsertInToStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.Date, DateTime.Now.ToShortTimeString(), 1);
                    var dtgo = GovahiBusiness.GetGovahiStatus(Session[sessionNames.userID_StudentOstad].ToString());
                    if (dtgo.Rows.Count > 0)
                    {
                        grd_GovahiRequestState.Visible = true;
                        grd_GovahiRequestState.DataSource = dtgo;
                        grd_GovahiRequestState.DataBind();
                    }
                    var dt = GovahiBusiness.GetAmountForPay(Session[sessionNames.userID_StudentOstad].ToString());
                    grd_pay.DataSource = dt;
                    grd_pay.DataBind();
                    if (dt.Rows.Count > 0 && (int.Parse(dt.Rows[0]["amount"].ToString())) > 0)
                    {
                        grd_pay.Visible = true;
                    }
                    else
                    {
                        grd_pay.Visible = false;
                    }

                }

            }
            catch (Exception exp)
            {
                Response.Write("Error: " + exp.Message);
            }
        }

        protected void grd_GovahiRequestState_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                DataTable dtgo = new DataTable();
                dtgo = GovahiBusiness.GetGovahiStatus(Session[sessionNames.userID_StudentOstad].ToString());

                GridDataItem itemAmount = (GridDataItem)e.Item;
                Button btn_pay = e.Item.FindControl("btn_Pay") as Button;
                GridDataItem item = (GridDataItem)e.Item;
                Label lbl_vaziat = e.Item.FindControl("lbl_vaziat") as Label;
                Label lbl_codeposti = e.Item.FindControl("lbl_codeposti") as Label;
                Label lbl_DalileRad = e.Item.FindControl("lbl_DalileRad") as Label;
                Button btn_Del = e.Item.FindControl("btn_Del") as Button;
                TextBox txt_EditEraeBe = e.Item.FindControl("txt_EditEraeBe") as TextBox;
                Button btn_Sabt = e.Item.FindControl("btn_Sabt") as Button;
                string cvaziat = lbl_vaziat.Text;

                if (dtgo.Rows[e.Item.ItemIndex]["AppStatus"].ToString() != "COMMIT")
                {
                    if (dtgo.Rows[e.Item.ItemIndex]["RequestLogID"].ToString() == "5")
                    {
                        btn_pay.Visible = false;
                        lbl_vaziat.Visible = true;
                        lbl_codeposti.Visible = false;
                        lbl_DalileRad.Visible = true;
                        lbl_DalileRad.Text = dtgo.Rows[e.Item.ItemIndex]["postnumber"].ToString();
                        return;

                    }
                    lbl_vaziat.Visible = true;
                    lbl_vaziat.Text = "پرداخت ناموفق";
                    btn_Del.Visible = true;
                    lbl_DalileRad.Visible = false;
                    lbl_codeposti.Visible = true;
                    lbl_codeposti.Text = dtgo.Rows[e.Item.ItemIndex]["postnumber"].ToString();
                    if (Session["ReqID"] != null)
                    {
                        Session["ReqID"] = Session["ReqID"] + "," + dtgo.Rows[e.Item.ItemIndex]["RequestId"].ToString();
                        Session["PayID"] = Session["PayID"] + "," + dtgo.Rows[e.Item.ItemIndex]["PaymentID"].ToString();
                    }
                    else
                    {
                        Session["ReqID"] = dtgo.Rows[e.Item.ItemIndex]["RequestId"].ToString();
                        Session["PayID"] = dtgo.Rows[e.Item.ItemIndex]["PaymentID"].ToString();
                    }
                }
                else if (dtgo.Rows[e.Item.ItemIndex]["RequestLogID"].ToString() == "26")
                {
                    lbl_DalileRad.Visible = true;
                    lbl_codeposti.Visible = false;
                    lbl_DalileRad.Text = dtgo.Rows[e.Item.ItemIndex]["postnumber"].ToString();
                    lbl_vaziat.Visible = true;
                    txt_EditEraeBe.Visible = true;
                    btn_Sabt.Visible = true;

                }
                else if (dtgo.Rows[e.Item.ItemIndex]["RequestLogID"].ToString() == "5")
                {
                    btn_pay.Visible = false;
                    lbl_vaziat.Visible = true;
                    lbl_codeposti.Visible = false;
                    lbl_DalileRad.Visible = true;
                    lbl_DalileRad.Text = dtgo.Rows[e.Item.ItemIndex]["postnumber"].ToString();
                }
                else
                {
                    btn_pay.Visible = false;
                    lbl_vaziat.Visible = true;
                    lbl_DalileRad.Visible = false;
                    lbl_codeposti.Visible = true;
                    lbl_codeposti.Text = dtgo.Rows[e.Item.ItemIndex]["postnumber"].ToString();
                }
            }
        }



        protected void grd_pay_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GovahiBusiness.GetAmountForPay(Session[sessionNames.userID_StudentOstad].ToString());
            grd_pay.DataSource = dt;
            if ((int.Parse(dt.Rows[0]["amount"].ToString())) > 0)
            {
                grd_pay.Visible = true;
            }
            else
            {
                grd_pay.Visible = false;
            }


        }
    }
}