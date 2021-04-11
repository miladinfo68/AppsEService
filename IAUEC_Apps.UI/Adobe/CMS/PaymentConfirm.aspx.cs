using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Drawing;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class PaymentConfirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PaymentBusiness pb = new PaymentBusiness();
           

                sumprice.InnerText=pb.GetSumPayment().ToString("N0")+" "+"ریال";
                string mId = Request.QueryString["id"].ToString();
                string[] id = mId.ToString().Split(new char[] { '@' });
                string menuId = "";
                for (int i = 0; i < id[1].Length; i++)
                {
                    string s = id[1].Substring(i + 1, 1);
                    if (s != "-")
                        menuId += s;
                    else
                        break;
                }
                Session[sessionNames.menuID] = menuId;
                AccessControl1.MenuId = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            
            
        }

        protected void PayGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
               
                GridDataItem item = (GridDataItem)e.Item;
                TableCell cell = (TableCell)item["AppStatus"];

                Button btn = e.Item.FindControl("btn_ConfirmPay") as Button;
                if (cell.Text == "بازگشت داده شده")
                {
                    btn.Enabled = true;
                    btn.BackColor = Color.DarkSeaGreen;
                    btn.ForeColor = Color.White;
                }
                else
                {
                    btn.Enabled = false;
                    btn.BackColor = Color.Wheat;
                    btn.ForeColor = Color.Black;
                }
            }
        }

        protected void PayGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();

            PaymentBusiness PB = new PaymentBusiness();
            if (e.CommandName == "ConfirmPay")
            {               
                PaymentDTO payDTO = new PaymentDTO();
                payDTO.OrderId=long.Parse(e.CommandArgument.ToString());
                PB.UpdatePaymentStatus(payDTO);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 7, "");

                List<PaymentDTO> lstDto = new List<PaymentDTO>();

                lstDto = PB.GetAllPayment(date_input_1.Value.ToString(), date_input_2.Value.ToString());
                grd_Pay.DataSource = lstDto;
                grd_Pay.DataBind();
            }
        }

        protected void PayGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<PaymentDTO> lstDto = new List<PaymentDTO>();
            PaymentBusiness PB = new PaymentBusiness();
            lstDto = PB.GetAllPayment(date_input_1.Value.ToString(), date_input_2.Value.ToString());
            grd_Pay.DataSource = lstDto;

        }

        protected void btn_select_Click(object sender, EventArgs e)
        {
            List<PaymentDTO> lstDto = new List<PaymentDTO>();
            PaymentBusiness PB = new PaymentBusiness();
            lstDto = PB.GetAllPayment(date_input_1.Value.ToString(), date_input_2.Value.ToString());
            grd_Pay.DataSource = lstDto;
            grd_Pay.DataBind();
        }

       

    }
}