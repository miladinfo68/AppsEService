using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Request.CMS
{


    public partial class TextBoxCodePost : System.Web.UI.UserControl
    {
        
        ///ایجاد نموده ایم RequestGovahiBusiness یک شئ از کلاس
        /// </summary>
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        /// <summary>
        ///ایجاد نموده ایم RequestStudentCartBusiness یک شئ از کلاس
        /// </summary>
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();

        

        private object _dataItem = null;
        /// <summary>
        ///ایجاد نموده ایم Request_StudentCartDAO یک شئ از کلاس
        /// </summary>
        RequestStudentCartBusiness dastcast = new RequestStudentCartBusiness();
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.DataBinding += new System.EventHandler(this.NewsDetail_DataBinding);

        }
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }




        protected void NewsDetail_DataBinding(object sender, System.EventArgs e)
        {
            
            if (DataBinder.Eval(DataItem, "stcode").ToString() != "")
            {  Session["stcode"] = (DataBinder.Eval(DataItem, "stcode").ToString());
            Session["RequestTypeID"] = (DataBinder.Eval(DataItem, "RequestTypeID").ToString());
            Session["StudentRequestID"] = (DataBinder.Eval(DataItem, "StudentRequestID").ToString());
            }
           

        }


        /// <summary>
        /// کد مرسوله پستی وارد شده را در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btn_anjam_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            

            if (!CommonBusiness.IsNumeric(txt_CodePost.Text))
            {
                rwm_Validations.RadAlert("کد مرسوله پستی را صحیح وارد نمایید", null, 100, "خطا ", "");
            }
            else if (!ValidatorBusiness.ValidateMarsulePostiCode(txt_CodePost.Text))
            {
                rwm_Validations.RadAlert("کد مرسوله پستی باید 10 یا 20 رقم باشد", null, 100, "خطا ", "");
            }
            else
            {
                GovahiBusiness.UpdateStudentPOstNumber(Session["stcode"].ToString(), txt_CodePost.Text, int.Parse(Session["RequestTypeID"].ToString()), int.Parse(Session["StudentRequestID"].ToString()));
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 3, Session["StudentRequestID"].ToString());
                
                DataTable dt = new DataTable();
                dt = CartBusiness.GetCartRequest(1);
                CartBusiness.UpdateStudentRequestLogID(Session["stcode"].ToString(), 2, int.Parse(Session["RequestTypeID"].ToString()), int.Parse(Session["StudentRequestID"].ToString()));
                //CommonBusiness.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.Date, DateTime.Now.Date.ToShortTimeString(), Session["stcode"].ToString(), 2);
                //sabte code marsule posti
                RequestStudentCartBusiness bda = new RequestStudentCartBusiness();
                int orderid;
                do
                {

                    orderid = bda.RandomNumber(100, 1000000);

                } while (dastcast.CheckOrderId(orderid, 70));

                //CartBusiness.InsertIntoPayinterm(Session["stcode"].ToString(), orderid, 70, 2000, bda.PersianCalander(), bda.PersianCalander());
                if (int.Parse(Session["RequestTypeID"].ToString()) == 1)

                    Response.Redirect("ConfirmStudentCardNotPrinted.aspx?id=" + generaterandomstr(11) + "@A" + Session[sessionNames.menuID].ToString() + "-" + generaterandomstr(2), false);

                if (int.Parse(Session["RequestTypeID"].ToString()) == 3)

                    Response.Redirect("ConfirmStudentGovahiNotPrinted.aspx?id=" + generaterandomstr(11) + "@A" + Session[sessionNames.menuID].ToString() + "-" + generaterandomstr(2), false);

            } 
            
           
        }

        public string generaterandomstr(int count)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}