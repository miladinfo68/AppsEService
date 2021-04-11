using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.DTO.CommonClasses;
using Telerik.Web.UI;


namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class PaymentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void btn_select_Click(object sender, EventArgs e)
        {
            List<PaymentDTO> lstDto = new List<PaymentDTO>();
            PaymentBusiness PB = new PaymentBusiness();
            lstDto = PB.GetAllPayment(date_input_1.Value.ToString(), date_input_2.Value.ToString());
            grd_PaymentList.DataSource = lstDto;
            grd_PaymentList.DataBind();
            long sum = 0;
            for (int i = 0; i < lstDto.Count; i++)
            {
                if (lstDto[i].AppStatus == "پرداخت موفق")
                    sum += lstDto[i].Amount;
            }
            lblsum.Text = sum.ToString("N0") + " " + "ریال";
        }

        protected void grd_PaymentList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<PaymentDTO> lstDto = new List<PaymentDTO>();
            PaymentBusiness PB = new PaymentBusiness();
            lstDto = PB.GetAllPayment(date_input_1.Value.ToString(), date_input_2.Value.ToString());
            grd_PaymentList.DataSource = lstDto;
        }

        protected void ExportToExcelImg_Click(object sender, ImageClickEventArgs e)
        {
            if (grd_PaymentList.AllowPaging == true)
                grd_PaymentList.AllowPaging = false;
            string alternateText = (sender as ImageButton).AlternateText;
            grd_PaymentList.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            grd_PaymentList.ExportSettings.IgnorePaging = true;
            grd_PaymentList.ExportSettings.ExportOnlyData = true;
            grd_PaymentList.ExportSettings.OpenInNewWindow = true;
            grd_PaymentList.ExportSettings.UseItemStyles = true;
            grd_PaymentList.ExportSettings.FileName = "PaymentListReport-" + DateTime.Now.ToShortDateString();
            grd_PaymentList.MasterTableView.ExportToExcel();
            grd_PaymentList.AllowPaging = true;
        }

        protected void btnDetail_Click(object sender, EventArgs e)
        {

        }

        protected void grd_PaymentList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {

                var orderId = Convert.ToInt32(e.CommandArgument.ToString()).ToString();
                var index = e.Item.RowIndex;
                GridDataItem item = (GridDataItem)e.Item;
                GridEditableItem item1 = (GridEditableItem)e.Item;
                var studentCode = item["studentCode"].Text;
                         
                List<PaymentDTO> lstDto = new List<PaymentDTO>();
                PaymentBusiness PB = new PaymentBusiness();
                DownloadRequestBusiness dnlDAO = new DownloadRequestBusiness();
                // Common.UserAccessBusiness uab = new Common.UserAccessBusiness();
                var payStudent = dnlDAO.Get_Selected_DetailPayment(studentCode, orderId);


                grdDateTime.DataSource = payStudent;
                 grdDateTime.DataBind();

                string scrp = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
                //**************
                //GridViewRow curruntRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                //imgStatus2.ImageUrl = ((Image)curruntRow.Cells[2].FindControl("imgStatus")).ImageUrl;
                //var requestDetails = rh.GetRequestDetails(reqId);
                //lblRequestId.Text = requestDetails.ID.ToString();
                //lblDarkhast.Text = requestDetails.CourseName;
                //lbldateOfRequest.Text = requestDetails.Issue_time;
                //RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
                //_dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
                //var dateTime = _dateTimeList.OrderBy(c => c.Date).FirstOrDefault(c => c.Date != null);
                //if (dateTime != null)
                //    lblRequest.Text = dateTime.Date;
                //***************






            }
        }

        //anything

        protected void grdDateTime_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdDateTime_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void grdDateTime_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

       
    }
}