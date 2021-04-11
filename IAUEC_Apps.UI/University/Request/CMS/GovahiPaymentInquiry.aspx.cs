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
    public partial class GovahiPaymentInquiry : System.Web.UI.Page
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

                grd_Payment.DataSource = Pb.GetStudentGovahiPaymentInquiry(txt_StudentNo.Text);
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
                DataTable dt = new DataTable();
                PaymentDTO pay = new PaymentDTO();
                pay = Pb.GetPaymentInfoByOrderID(long.Parse(e.CommandArgument.ToString()));
                PaymentGatewayImplService bpService = new PaymentGatewayImplService();
                string result = bpService.bpInquiryRequest(Int64.Parse(Mellat_TerminalId), Mellat_UserName, Mellat_UserPassword, pay.OrderId, pay.OrderId, pay.TraceNumber);
                switch (result)
                {
                    case "0":
                        lbl_Estelam.Text = "تراكنش با موفقيت انجام شد";
                        break;
                    case "11":
                        lbl_Estelam.Text = "ﺷﻤﺎﺭﻩ ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ ";
                        break;
                    case "12":
                        lbl_Estelam.Text = "ﻣﻮﺟﻮﺩﻱ ﻛﺎﻓﻲ ﻧﻴﺴﺖ ";
                        break;
                    case "13":
                        lbl_Estelam.Text = "ﺭﻣﺰ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ ";
                        break;
                    case "14":
                        lbl_Estelam.Text = "ﺗﻌﺪﺍﺩ ﺩﻓﻌﺎﺕ ﻭﺍﺭﺩ ﻛﺮﺩﻥ ﺭﻣﺰ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ ";
                        break;
                    case "15":
                        lbl_Estelam.Text = "ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case "16":
                        lbl_Estelam.Text = "ﺩﻓﻌﺎﺕ ﺑﺮﺩﺍﺷﺖ ﻭﺟﻪ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ";
                        break;
                    case "17":
                        lbl_Estelam.Text = "ﻛﺎﺭﺑﺮ ﺍﺯ ﺍﻧﺠﺎﻡ ﺗﺮﺍﻛﻨﺶ ﻣﻨﺼﺮﻑ ﺷﺪﻩ ﺍﺳﺖ ";
                        break;
                    case "18":
                        lbl_Estelam.Text = "ﺗﺎﺭﻳﺦ ﺍﻧﻘﻀﺎﻱ ﻛﺎﺭﺕ ﮔﺬﺷﺘﻪ ﺍﺳﺖ";
                        break;
                    case "19":
                        lbl_Estelam.Text = "ﻣﺒﻠﻎ ﺑﺮﺩﺍﺷﺖ ﻭﺟﻪ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ";
                        break;
                    case "111":
                        lbl_Estelam.Text = "ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case "112":
                        lbl_Estelam.Text = "ﺧﻄﺎﻱ ﺳﻮﻳﻴﭻ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ ";
                        break;
                    case "113":
                        lbl_Estelam.Text = "ﭘﺎﺳﺨﻲ ﺍﺯ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ ﺩﺭﻳﺎﻓﺖ ﻧﺸﺪ";
                        break;
                    case "114":
                        lbl_Estelam.Text = "ﺩﺍﺭﻧﺪﻩ ﻛﺎﺭﺕ ﻣﺠﺎﺯ ﺑﻪ ﺍﻧﺠﺎﻡ ﺍﻳﻦ ﺗﺮﺍﻛﻨﺶ ﻧﻴﺴﺖ";
                        break;
                    case "21":
                        lbl_Estelam.Text = "ﭘﺬﻳﺮﻧﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case "23":
                        lbl_Estelam.Text = "ﺧﻄﺎﻱ ﺍﻣﻨﻴﺘﻲ ﺭﺥ ﺩﺍﺩﻩ ﺍﺳﺖ";
                        break;
                    case "24":
                        lbl_Estelam.Text = "ﺍﻃﻼﻋﺎﺕ ﻛﺎﺭﺑﺮﻱ ﭘﺬﻳﺮﻧﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case "25":
                        lbl_Estelam.Text = "ﻣﺒﻠﻎ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case "31":
                        lbl_Estelam.Text = "ﭘﺎﺳﺦ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ ";
                        break;
                    case "32":
                        lbl_Estelam.Text = "ﻓﺮﻣﺖ ﺍﻃﻼﻋﺎﺕ ﻭﺍﺭﺩ ﺷﺪﻩ ﺻﺤﻴﺢ ﻧﻤﻲ ﺑﺎﺷﺪ";
                        break;
                    case "33":
                        lbl_Estelam.Text = "ﺣﺴﺎﺏ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case "34":
                        lbl_Estelam.Text = "ﺧﻄﺎﻱ ﺳﻴﺴﺘﻤﻲ";
                        break;
                    case "35":
                        lbl_Estelam.Text = "ﺗﺎﺭﻳﺦ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case "41":
                        lbl_Estelam.Text = "ﺷﻤﺎﺭﻩ ﺩﺭﺧﻮﺍﺳﺖ ﺗﻜﺮﺍﺭﻱ ﺍﺳﺖ ";
                        break;
                    case "42":
                        lbl_Estelam.Text = "ﻳﺎﻓﺖ ﻧﺸﺪ Sale ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case "43":
                        lbl_Estelam.Text = " ﺩﺍﺩﻩ ﺷﺪﻩ ﺍﺳﺖ Verify ﻗﺒﻼ ﺩﺭﺧﻮﺍﺳﺖ ";
                        break;
                    case "44":
                        lbl_Estelam.Text = " ﻳﺎﻓﺖ ﻧﺸﺪ Verfiy ﺩﺭﺧﻮﺍﺳﺖ ";
                        break;
                    case "45":
                        lbl_Estelam.Text = "  ﺷﺪﻩ ﺍﺳﺖ Settle ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case "46":
                        lbl_Estelam.Text = "  ﻧﺸﺪﻩ ﺍﺳﺖ Settle ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case "47":
                        lbl_Estelam.Text = "ﻳﺎﻓﺖ ﻧﺸﺪ Settle ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case "48":
                        lbl_Estelam.Text = "  ﺷﺪﻩ ﺍﺳﺖ Reverse ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case "49":
                        lbl_Estelam.Text = "ﻳﺎﻓﺖ ﻧﺸﺪ Refund ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case "412":
                        lbl_Estelam.Text = "ﺷﻨﺎﺳﻪ ﻗﺒﺾ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ";
                        break;
                    case "413":
                        lbl_Estelam.Text = "ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ";
                        break;
                    case "414":
                        lbl_Estelam.Text = "ﺳﺎﺯﻣﺎﻥ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻗﺒﺾ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case "415":
                        lbl_Estelam.Text = "ﺯﻣﺎﻥ ﺟﻠﺴﻪ ﻛﺎﺭﻱ ﺑﻪ ﭘﺎﻳﺎﻥ ﺭﺳﻴﺪﻩ ﺍﺳﺖ";
                        break;
                    case "416":
                        lbl_Estelam.Text = " ﺧﻄﺎ ﺩﺭ ﺛﺒﺖ ﺍﻃﻼﻋﺎﺕ";
                        break;
                    case "417":
                        lbl_Estelam.Text = "ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﻛﻨﻨﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case "418":
                        lbl_Estelam.Text = "ﺍﺷﻜﺎﻝ ﺩﺭ ﺗﻌﺮﻳﻒ ﺍﻃﻼﻋﺎﺕ ﻣﺸﺘﺮﻱ";
                        break;
                    case "419":
                        lbl_Estelam.Text = "ﺗﻌﺪﺍﺩ ﺩﻓﻌﺎﺕ ﻭﺭﻭﺩ ﺍﻃﻼﻋﺎﺕ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﮔﺬﺷﺘﻪ ﺍﺳﺖ";
                        break;
                    case "421":
                        lbl_Estelam.Text = "ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ IP";
                        break;
                    case "51":
                        lbl_Estelam.Text = "ﺗﺮﺍﻛﻨﺶ ﺗﻜﺮﺍﺭﻱ ﺍﺳﺖ ";
                        break;
                    case "54":
                        lbl_Estelam.Text = "ﺗﺮﺍﻛﻨﺶ ﻣﺮﺟﻊ ﻣﻮﺟﻮﺩ ﻧﻴﺴﺖ";
                        break;
                    case "55":
                        lbl_Estelam.Text = "ﺗﺮﺍﻛﻨﺶ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case "61":
                        lbl_Estelam.Text = "ﺧﻄﺎ ﺩﺭ ﻭﺍﺭﻳﺰ";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}