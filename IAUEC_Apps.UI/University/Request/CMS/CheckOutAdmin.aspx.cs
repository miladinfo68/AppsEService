using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutAdmin : System.Web.UI.Page
    {
        CheckOutRequestBusiness business = new CheckOutRequestBusiness();
        CommonBusiness CommonBusiness = new CommonBusiness();
        LoginBusiness lngB = new LoginBusiness();

        DataTable dt = new DataTable();
        string userID;
        bool isAdmin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            userID = Session[sessionNames.userID_Karbar].ToString();

            if (!IsPostBack)
            {
                btnDlExcel.Visible = false;
                DataTable userdt = lngB.Get_UserRoles(userID);
                isAdmin = userdt.Select().ToList().Exists(row => row["RoleId"].ToString() == "36");
                if (userdt.Rows[0]["RoleId"].ToString() == "1")
                {
                    isAdmin = true;
                }
                if (isAdmin)
                {
                    grd_CheckOutRequest.MasterTableView.GetColumn("act").Display = true;
                    //dvRequestType.Visible = true;
                    //((DataControlField)grd_CheckOutRequest.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "عملیات").SingleOrDefault()).Visible = true;
                }
                else
                {
                    grd_CheckOutRequest.MasterTableView.GetColumn("act").Display = false;
                    //dvRequestType.Visible = false;
                    //((DataControlField)grd_CheckOutRequest.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "عملیات").SingleOrDefault()).Visible = false;
                }
                BindData();
            }
        }

        private void BindData()
        {
            drpCheckOutType.DataSource = business.GetCheckOutTypes();
            drpCheckOutType.DataValueField = "RequestTypeID";
            drpCheckOutType.DataTextField = "RequestTypeName";
            drpCheckOutType.DataBind();
            drpCheckOutType.Items.Insert(0, "نوع تسویه را انتخاب کنید");
        }

        protected void btnSreach_Click(object sender, EventArgs e)
        {
            grd_CheckOutRequest.DataSource = business.GetCheckOutInfoByStCode(txtStCode.Text);
            grd_CheckOutRequest.DataBind();
            btnDlExcel.Visible = true;
        }

        protected void drpCheckOutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpCheckOutType.SelectedIndex != 0)
            {
                txtStCode.Text = "";
                BindGrid(Convert.ToInt32(drpCheckOutType.SelectedValue));
            }
        }

        private void BindGrid(int typeID)
        {
            grd_CheckOutRequest.DataSource = business.GetListOFRequestByTypeID(typeID);
            btnDlExcel.Visible = true;

            grd_CheckOutRequest.DataBind();
              GridFilterMenu menu = grd_CheckOutRequest.FilterMenu;
              if (menu.Items.Count > 3)
              {
                  int im = 0;
                  while (im < menu.Items.Count)
                  {
                      if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                      {
                          im++;
                      }
                      else
                      {
                          menu.Items.RemoveAt(im);
                      }
                  }
                  foreach (RadMenuItem item in menu.Items)
                  {    //change the text for the "StartsWith" menu item  
                      if (item.Text == "NoFilter")
                      {
                          item.Text = "حذف فیلتر";
                          //item.Remove();
                      }
                      if (item.Text == "Contains")
                      {
                          item.Text = "شامل";
                          //item.Remove();
                      }
                      if (item.Text == "EqualTo")
                      {
                          item.Text = "مساوی با";
                          //item.Remove();
                      }

                  }
              }
        }

        protected void grdCheckOutRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drpReqStatus = (DropDownList)e.Row.FindControl("drpReqStatus");
                Type en = typeof(CheckOutStatusEnum.EnserafReqStatus);
                string reqType = rowView["RequestTypeID"].ToString();
                switch (reqType)
                {
                    case "15":
                        en = typeof(CheckOutStatusEnum.FareghReqStatus);
                        break;
                    case "16":
                        en = typeof(CheckOutStatusEnum.EnserafReqStatus);
                        break;
                    case "14":
                        en = typeof(CheckOutStatusEnum.EkhrajStatus);
                        break;
                    case "13":
                        en = typeof(CheckOutStatusEnum.TaghirReshteStatus);
                        break;
                    case "17":
                        en = typeof(CheckOutStatusEnum.EnteghaliStatus);
                        break;
                    default:
                        break;
                }
                foreach (int status in Enum.GetValues(en))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(status);
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10")
                    {
                        drpReqStatus.Items.Add(li);
                    }
                }
                if (rowView["RequestLogID"].ToString() == "5")
                {
                    ListItem li = new ListItem();
                    li.Text = "درخواست رد شده است";
                    li.Value = "5";
                    drpReqStatus.Items.Add(li);
                }
                else
                {
                    drpReqStatus.SelectedValue = rowView["RequestLogID"].ToString();
                }

                /***************************************************************************/
                DataRowView row = (DataRowView)e.Row.DataItem;
                string stcode = row["stCode"].ToString();

                //Session["colorRow"] = e;

                dt = business.GetonlineStatus(stcode);
                if (dt.Rows.Count > 0)
                {
                    int isOnline = Convert.ToInt32(dt.Rows[0][0].ToString());

                    if (isOnline == 0)
                    {
                        e.Row.BackColor = Color.FromName("#FFFCBC");
                    }
                    else
                    {
                        e.Row.BackColor = Color.FromName("#d9ffbc");
                    }
                }
            }
        }

        protected void btnSubmitMsg_Click(object sender, EventArgs e)
        {
            string msg;
            int status = Convert.ToInt32(ViewState["status"]);
            int reqID = Convert.ToInt32(ViewState["reqID"]);
            string stcode = ViewState["stcode"].ToString();
            int reqType = Convert.ToInt32(ViewState["reqType"]);
            userID = Session[sessionNames.userID_Karbar].ToString();
            msg = business.SendMessage(userID, reqID, txtMsg.Text);
            RadWindow2.VisibleOnPageLoad = false;
            if (drpCheckOutType.SelectedIndex != 0)
            {
                BindGrid(Convert.ToInt32(drpCheckOutType.SelectedValue));
            }
            else
            {
                grd_CheckOutRequest.DataSource = business.GetCheckOutInfoByStCode(stcode);
                grd_CheckOutRequest.DataBind();
                btnDlExcel.Visible = true;

            }
        }

        protected void grdCheckOutRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //grdCheckOutRequest.PageIndex = e.NewPageIndex;
            //BindGrid(Convert.ToInt32(drpCheckOutType.SelectedValue));
        }

        protected void grd_CheckOutRequest_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int reqID = Convert.ToInt32(e.CommandArgument);
                GridDataItem itemAmount = (GridDataItem)e.Item;
                //TableCell st = (TableCell)itemAmount["stcode"];

                DropDownList drpReqStatus = (DropDownList)itemAmount.FindControl("drpReqStatus") as DropDownList;
                string stcode = itemAmount["stcode"].Text;
                string stat = drpReqStatus.SelectedValue;
                string msg;
                int reqType = int.Parse(itemAmount["RequestTypeID"].Text);
                string stName = itemAmount["name"].Text;
                string reqDate = itemAmount["CreateDate"].Text;
                ViewState.Add("reqID", reqID);
                ViewState.Add("status", stat);
                ViewState.Add("stcode", stcode);
                ViewState.Add("stName", stName);
                ViewState.Add("reqDate", reqDate);
                
                if (e.CommandName == "send")
                {

                   var currentStatus= business.GetCheckOutInfoByStCode(txtStCode.Text).Rows[0]["erae_be"];
                    var CurStatus = Convert.ToInt32(currentStatus);
                    msg = business.SendCheckOutRequestToStatus(userID, Convert.ToInt32(stat), reqID, reqType, CurStatus);
                    if (!String.IsNullOrWhiteSpace(txtStCode.Text) && drpCheckOutType.SelectedIndex == 0)
                    {
                        grd_CheckOutRequest.DataSource = business.GetCheckOutInfoByStCode(txtStCode.Text);
                        grd_CheckOutRequest.DataBind();
                        btnDlExcel.Visible = true;

                    }
                    else
                    {
                        BindGrid(Convert.ToInt32(drpCheckOutType.SelectedValue));
                    }
                    RadWindowManager1.RadAlert(msg, 0, 100, " پیام سیستم", "");

                }
                if (e.CommandName == "msg")
                {
                  
                    RadWindow2.VisibleOnPageLoad = true;
                    ViewState.Add("reqID", reqID);
                    ViewState.Add("status", stat);
                    ViewState.Add("stcode", stcode);
                    if (drpCheckOutType.SelectedIndex != 0)
                    {
                        BindGrid(Convert.ToInt32(drpCheckOutType.SelectedValue));
                    }
                    else
                    {
                        grd_CheckOutRequest.DataSource = business.GetCheckOutInfoByStCode(stcode);
                        grd_CheckOutRequest.DataBind();
                        btnDlExcel.Visible = true;

                    }
                }
                if (e.CommandName == "History")
                {
                    RadWindow2.VisibleOnPageLoad = false;
                    CommonBusiness cmb = new CommonBusiness();

                    lst_history.DataSource = cmb.GetUserLogByModifyId(int.Parse(e.CommandArgument.ToString()), 12);
                    lst_history.DataBind();
                    StringBuilder sb = new StringBuilder();

                    info1.InnerText = "نام دانشجو:" + ViewState["stName"].ToString();
                    info2.InnerText = "شماره درخواست:" + ViewState["reqID"].ToString();
                    info3.InnerText = "تاریخ درخواست:" + ViewState["reqDate"].ToString();
                    if (drpCheckOutType.SelectedIndex != 0)
                    {
                        BindGrid(Convert.ToInt32(drpCheckOutType.SelectedValue));
                    }
                    else
                    {
                        grd_CheckOutRequest.DataSource = business.GetCheckOutInfoByStCode(stcode);
                        grd_CheckOutRequest.DataBind();
                        btnDlExcel.Visible = true;

                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
        }

        protected void grd_CheckOutRequest_ItemDataBound(object sender, GridItemEventArgs e)
        {   
            if (e.Item is GridDataItem)
            {
                DropDownList drpReqStatus = (DropDownList)e.Item.FindControl("drpReqStatus");
                Type en = typeof(CheckOutStatusEnum.EnserafReqStatus);
                GridDataItem itemAmount = (GridDataItem)e.Item;
                string reqType = itemAmount["RequestTypeID"].Text;
                int reqLog = Convert.ToInt32(itemAmount["Erae_Be"].Text);

                switch (reqType)
                {
                    case "15":
                        en = typeof(CheckOutStatusEnum.FareghReqStatus);
                        break;
                    case "16":
                        en = typeof(CheckOutStatusEnum.EnserafReqStatus);
                        break;
                    case "14":
                        en = typeof(CheckOutStatusEnum.EkhrajStatus);
                        break;
                    case "13":
                        en = typeof(CheckOutStatusEnum.TaghirReshteStatus);
                        break;
                    case "17":
                        en = typeof(CheckOutStatusEnum.EnteghaliStatus);
                        break;
                    default:
                        break;
                }
                foreach (int status in Enum.GetValues(en))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(status);
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10" & status <= reqLog )
                    {
                        drpReqStatus.Items.Add(li);
                    }
                }
                if (itemAmount["RequestLogID"].Text == "5")
                {
                    ListItem li = new ListItem();
                    li.Text = "درخواست رد شده است";
                    li.Value = "5";
                    drpReqStatus.Items.Add(li);
                }
                else
                {
                    drpReqStatus.SelectedValue = itemAmount["Erae_Be"].Text;
                }

                /***************************************************************************/
              
                string stcode = itemAmount["stCode"].Text;

                //Session["colorRow"] = e;

                dt = business.GetonlineStatus(stcode);
                if (dt.Rows.Count > 0)
                {
                    int isOnline = Convert.ToInt32(dt.Rows[0][0].ToString());

                    if (isOnline == 0)
                    {
                    
                        e.Item.BackColor = Color.FromName("#FFFCBC");
                    }
                    else
                    {
                        e.Item.BackColor = Color.FromName("#d9ffbc");
                    }
                }
            }
        }

        protected void grd_CheckOutRequest_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (drpCheckOutType.SelectedIndex != 0)
                grd_CheckOutRequest.DataSource = business.GetListOFRequestByTypeID(int.Parse(drpCheckOutType.SelectedValue));
            if(txtStCode.Text!="")
                grd_CheckOutRequest.DataSource = business.GetCheckOutInfoByStCode(txtStCode.Text);
            btnDlExcel.Visible = true;

            GridFilterMenu menu = grd_CheckOutRequest.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                        //item.Remove();
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                        //item.Remove();
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                        //item.Remove();
                    }

                }
            }
        }

        protected void btnDlExcel_Click(object sender, ImageClickEventArgs e)
        {
            grd_CheckOutRequest.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            grd_CheckOutRequest.ExportSettings.IgnorePaging = true;
            grd_CheckOutRequest.ExportSettings.ExportOnlyData = true;
            grd_CheckOutRequest.ExportSettings.OpenInNewWindow = true;
            grd_CheckOutRequest.ExportSettings.UseItemStyles = true;
            grd_CheckOutRequest.ExportSettings.FileName =DateTime.Now.ToPeString()+" - "+ "مدیریت تسویه حساب - "+( drpCheckOutType.SelectedIndex==0?txtStCode.Text: drpCheckOutType.SelectedItem.Text);
            grd_CheckOutRequest.MasterTableView.ExportToExcel();
        }
        
    }
}