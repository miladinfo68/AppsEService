using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.CommonClasses;
using System.Configuration;
using IAUEC_Apps.UI.ir.shaparak.bpm;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class PaymentInquiry : System.Web.UI.Page
    {
        public static readonly string Mellat_TerminalId = ConfigurationManager.AppSettings["Mellat_TerminalId"];
        public static readonly string Mellat_UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string Mellat_UserPassword = ConfigurationManager.AppSettings["UserPassword"];
        RequestPaymentBusiness Pb = new RequestPaymentBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_StudentNo.Text != "")
            {
                grd_Payment.DataSource = Pb.GetStudentPaymentInquiry(DTO.University.Request.PayType.studentCard,txt_StudentNo.Text);
                grd_Payment.DataBind();
            }
            else
            {
                rwm.RadAlert("لطفا شماره دانشجویی وارد گردد", null, 100, "خطا", "");

            }
        }

        protected void grd_Payment_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "estelam")
            {
                int result;
                lbl_Estelam.Text = Pb.PaymentStatusInquery(long.Parse(e.CommandArgument.ToString()), out result);

            }
        }
    }
}