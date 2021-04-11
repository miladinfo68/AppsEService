using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutPrint : System.Web.UI.Page
    {
        CheckOutRequestBusiness business = new CheckOutRequestBusiness();
        StuPresentBusiness StudentBusiness = new StuPresentBusiness();
        LoginBusiness lngB = new LoginBusiness();
        
        DataTable userdt = new DataTable();
        int userRole;
        string userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userID = Session[sessionNames.userID_Karbar].ToString();
                userdt = lngB.Get_UserRoles(userID);
                if (userdt.Rows.Count > 1 || (Convert.ToInt32(userdt.Rows[0][1]) == 1))
                {
                    ToMultipleRoleMode(userdt);
                }
                else
                {
                    ToSingleRoleMode(userdt);
                }
            }
        }

        private void ToSingleRoleMode(DataTable userdt)
        {
            int status = business.GetStatusOfRole(Convert.ToInt32(userdt.Rows[0][1]));
            BindGrid(status,false);
        }

        private void ToMultipleRoleMode(DataTable dtu)
        {
            drpUserRoles.Enabled = true;
            drpUserRoles.Visible = true;
            grd_CheckOutList.Visible = true;
            if (Convert.ToInt32(dtu.Rows[0][1]) == 1)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }
            else
            {
                foreach (DataRow row in userdt.Rows)
                {
                    if (row["RoleId"].ToString() != "36")
                    {
                        CheckOutStatusEnum.EnserafReqStatus status = (CheckOutStatusEnum.EnserafReqStatus)business.GetStatusOfRole(Convert.ToInt32(row[1]));
                        ListItem li = new ListItem();
                        li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                        li.Value = Convert.ToInt32(status).ToString();
                        if (li.Text != "")
                        {
                            drpUserRoles.Items.Add(li);
                        }
                    }
                }
            }
            if (drpUserRoles.Items.Count > 0)
            {
                drpUserRoles.Enabled = true;
                drpUserRoles.Visible = true;
                drpUserRoles.Items.Insert(0, "انتخاب کنید");
            }
            
            if (drpUserRoles.SelectedIndex != 0)
            {
                BindGrid((Convert.ToInt32(drpUserRoles.SelectedValue) ),false);
            }
        }

        private void BindGrid(int status, bool isneeddatasource)
        {
            DataTable reqList = null;
            if (status == 12 || status==20)
            {
                int roleId = Convert.ToInt32(Session["roleId"]);
                int daneshId = business.GetDaneshKadeIdByRoleId(roleId);
                if (daneshId == 0)
                {
                    reqList = business.GetListOFRequestByCurrentStatus(status);
                }
                else
                {
                    reqList = business.GetListOFRequestByCurrentStatusDaneshId(status, daneshId);
                }
            }
            else
            {
                reqList = business.GetListOFRequestByCurrentStatus(status);
            }
            ViewState.Add("status", status);
            grd_CheckOutList.DataSource = reqList;
          
            if (!isneeddatasource)
                grd_CheckOutList.DataBind();
            GridFilterMenu menu = grd_CheckOutList.FilterMenu;
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

        protected void drpUserRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpUserRoles.SelectedIndex != 0)
            {
                grd_CheckOutList.Visible = true;
                BindGrid((Convert.ToInt32(drpUserRoles.SelectedValue) ),false);
            }
            else
            {
                grd_CheckOutList.Visible = false;
            }
            lblMessage.Text = "";
        }

        //protected void grdCheckOutList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DataRowView rowView = (DataRowView)e.Row.DataItem;
        //        int nextstatus = Convert.ToInt32(ViewState["status"]);
        //        int reqType = Convert.ToInt32(rowView["RequestTypeID"]);
        //        string stcode = rowView["StCode"].ToString();
        //        if (nextstatus == (int)CheckOutStatusEnum.EnserafReqStatus.refah_ok)
        //        {
        //            Button btnPrintVezaratLoan = (Button)e.Row.FindControl("btnPrintVezaratLoan");
        //            (grd_CheckOutList.MasterTableView.GetColumn("prtloan") as GridTemplateColumn).Visible = tr;
        //            ((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "چاپ گواهی بدهی وام وزارت علوم").SingleOrDefault()).Visible = true;
        //            if (rowView["LoanStatus"].ToString() == "True")
        //            {
        //                btnPrintVezaratLoan.Enabled = true;
        //            }
        //        }
        //        else
        //        {
        //            ((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "چاپ گواهی بدهی وام وزارت علوم").SingleOrDefault()).Visible = false;
        //        }
        //        if (nextstatus == (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
        //        {
        //            ((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "چاپ اطلاعات حساب").SingleOrDefault()).Visible = true;
        //            ((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "ساعات حضور دانشجو در ترم جاری").SingleOrDefault()).Visible = true;
        //            ((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "تاریخ انتخاب واحد").SingleOrDefault()).Visible = true;
        //            Label lblHozoorHour = (Label)e.Row.FindControl("lblHozoorHour");
        //            Label lblDateSabt = (Label)e.Row.FindControl("lblEntekhabVahedDate");
        //            if (reqType == 14 || reqType == 16)
        //            {
        //                lblDateSabt.Text = rowView["datesabtv"].ToString();
        //                DataTable dt = StudentBusiness.GetTotalTimeInAllClassesByStcode(stcode, "Class");
        //                if (dt.Rows.Count > 0)
        //                {
        //                    lblHozoorHour.Text = dt.Rows[0]["SumOfTime"].ToString();
        //                }
        //                else
        //                {
        //                    lblHozoorHour.Text = "صفر ساعت";
        //                }
        //            }
        //            else
        //            {
        //                lblHozoorHour.Text = "-";
        //            }
        //        }
        //        else
        //        {
        //            ((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "چاپ اطلاعات حساب").SingleOrDefault()).Visible = false;
        //        }
        //    }
        //}

        protected void grdCheckOutList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int reqID = Convert.ToInt32(e.CommandArgument);
            userID = Session[sessionNames.userID_Karbar] as string;
            userRole = Convert.ToInt32(ViewState["userRole"]);
            int status = Convert.ToInt32(ViewState["status"]);
            string msg = "";
            
            if (e.CommandName == "print")
            {
                RadWindowManager windowManager = new RadWindowManager();
                RadWindow window1 = new RadWindow();
                // Set the window properties   
                window1.NavigateUrl = "CheckOutPrintPopUp.aspx?r=" + e.CommandArgument.ToString()+"&s="+status;
                window1.ID = "RadWindow1";
                windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(500);
                windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(400);
                window1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
                windowManager.Windows.Add(window1);
                this.parent.Controls.Add(window1);
                business.UpdatePrintStatus(reqID);
            }
            if (e.CommandName=="printBedehi")
            {
                RadWindowManager windowManager = new RadWindowManager();
                RadWindow window2 = new RadWindow();
                // Set the window properties   
                window2.NavigateUrl = "CheckOutPrintGovahiBedehiPopUp.aspx?r=" + e.CommandArgument.ToString() + "&s=" + status;
                window2.ID = "RadWindow2";
                windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(500);
                windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(400);
                window2.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
                windowManager.Windows.Add(window2);
                this.parent.Controls.Add(window2);
                business.UpdatePrintStatus(reqID);
            }
            if (e.CommandName == "printAcountInfo")
            {
                RadWindowManager windowManager = new RadWindowManager();
                RadWindow window3 = new RadWindow();
                // Set the window properties   
                window3.NavigateUrl = "CheckOutPrintBankAcountInfo.aspx?r=" + e.CommandArgument.ToString() + "&s=" + status;
                window3.ID = "RadWindow3";
                windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(500);
                windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(400);
                window3.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
                windowManager.Windows.Add(window3);
                this.parent.Controls.Add(window3);
                business.UpdatePrintStatus(reqID);
            }
            if (msg != null)
            {
                lblMessage.Text = msg;
                lblMessage.Visible = true;
            }
            BindGrid(status,false);
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                int status = Convert.ToInt32(ViewState["status"]);
                BindGrid(status,false);
            }
        }

        protected void grd_CheckOutList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem rowView = (GridDataItem)e.Item;
                int nextstatus = Convert.ToInt32(ViewState["status"]);
                int reqType = Convert.ToInt32(rowView["RequestTypeID"].Text);
                string stcode = rowView["StCode"].Text;
                if (nextstatus == (int)CheckOutStatusEnum.EnserafReqStatus.refah_ok)
                {
                    Button btnPrintVezaratLoan = (Button)rowView.FindControl("btnPrintVezaratLoan");
                    (grd_CheckOutList.MasterTableView.GetColumn("prtloan") as GridTemplateColumn).Visible = true;
                    //((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "چاپ گواهی بدهی وام وزارت علوم").SingleOrDefault()).Visible = true;
                    if (rowView["LoanStatus"].ToString() == "True")
                    {
                        btnPrintVezaratLoan.Enabled = true;
                    }
                }
                else
                {
                    (grd_CheckOutList.MasterTableView.GetColumn("prtloan") as GridTemplateColumn).Visible = false;
                    //((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "چاپ گواهی بدهی وام وزارت علوم").SingleOrDefault()).Visible = false;
                }
                if (nextstatus == (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
                {
                    (grd_CheckOutList.MasterTableView.GetColumn("prtinfo") as GridTemplateColumn).Visible = true;
                    //(grd_CheckOutList.MasterTableView.GetColumn("prtClass") as GridTemplateColumn).Visible = true;
                    (grd_CheckOutList.MasterTableView.GetColumn("HourInTerm") as GridTemplateColumn).Visible = true;
                    (grd_CheckOutList.MasterTableView.GetColumn("DateVahed") as GridTemplateColumn).Visible = true;
                    //((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "چاپ اطلاعات حساب").SingleOrDefault()).Visible = true;
                    //((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "ساعات حضور دانشجو در ترم جاری").SingleOrDefault()).Visible = true;
                    //((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "تاریخ انتخاب واحد").SingleOrDefault()).Visible = true;
                    Label lblHozoorHour = (Label)rowView.FindControl("lblHozoorHour");
                    Label lblDateSabt = (Label)rowView.FindControl("lblEntekhabVahedDate");
                    if (reqType == 14 || reqType == 16)
                    {
                        lblDateSabt.Text = rowView["datesabtv"].Text;
                        DataTable dt = StudentBusiness.GetTotalTimeInAllClassesByStcode(stcode);
                        if (dt.Rows.Count > 0)
                        {
                            lblHozoorHour.Text = dt.Rows[0]["SumOfTime"].ToString();
                        }
                        else
                        {
                            lblHozoorHour.Text = "0";
                        }
                    }
                    else
                    {
                        lblHozoorHour.Text = "-";
                    }
                }
                else
                {
                    (grd_CheckOutList.MasterTableView.GetColumn("prtinfo") as GridTemplateColumn).Visible = false;
                    //(grd_CheckOutList.MasterTableView.GetColumn("prtClass") as GridTemplateColumn).Visible = false;
                    (grd_CheckOutList.MasterTableView.GetColumn("HourInTerm") as GridTemplateColumn).Visible = false;
                    (grd_CheckOutList.MasterTableView.GetColumn("DateVahed") as GridTemplateColumn).Visible = false;
                    //((DataControlField)grdCheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "چاپ اطلاعات حساب").SingleOrDefault()).Visible = false;
                }
            }
        }

        protected void grd_CheckOutList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int reqID = Convert.ToInt32(e.CommandArgument);
                userID = Session[sessionNames.userID_Karbar] as string;
                userRole = Convert.ToInt32(ViewState["userRole"]);
                int status = Convert.ToInt32(ViewState["status"]);
                string msg = "";

                if (e.CommandName == "print")
                {
                    RadWindowManager windowManager = new RadWindowManager();
                    RadWindow window1 = new RadWindow();
                    // Set the window properties   
                    window1.NavigateUrl = "CheckOutPrintPopUp.aspx?r=" + e.CommandArgument.ToString() + "&s=" + status;
                    window1.ID = "RadWindow1";
                    windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(500);
                    windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(400);
                    window1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
                    windowManager.Windows.Add(window1);
                    this.parent.Controls.Add(window1);
                    business.UpdatePrintStatus(reqID);
                }
                if (e.CommandName == "printBedehi")
                {
                    RadWindowManager windowManager = new RadWindowManager();
                    RadWindow window2 = new RadWindow();
                    // Set the window properties   
                    window2.NavigateUrl = "CheckOutPrintGovahiBedehiPopUp.aspx?r=" + e.CommandArgument.ToString() + "&s=" + status;
                    window2.ID = "RadWindow2";
                    windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(500);
                    windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(400);
                    window2.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
                    windowManager.Windows.Add(window2);
                    this.parent.Controls.Add(window2);
                    business.UpdatePrintStatus(reqID);
                }
                if (e.CommandName == "printAcountInfo")
                {
                    RadWindowManager windowManager = new RadWindowManager();
                    RadWindow window3 = new RadWindow();
                    // Set the window properties   
                    window3.NavigateUrl = "CheckOutPrintBankAcountInfo.aspx?r=" + e.CommandArgument.ToString() + "&s=" + status;
                    window3.ID = "RadWindow3";
                    windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(500);
                    windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(400);
                    window3.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
                    windowManager.Windows.Add(window3);
                    this.parent.Controls.Add(window3);
                    business.UpdatePrintStatus(reqID);
                }
                if (e.CommandName == "printClassPresent")
                {
                    RadWindowManager windowManager = new RadWindowManager();
                    RadWindow window4 = new RadWindow();
                    // Set the window properties   
                    window4.NavigateUrl = "checkOutPrintClassPresent.aspx?r=" + e.CommandArgument.ToString() + "&s=" + status;
                    window4.ID = "RadWindow4";
                    windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(500);
                    windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(400);
                    window4.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
                    windowManager.Windows.Add(window4);
                    this.parent.Controls.Add(window4);
                    business.UpdatePrintStatus(reqID);
                }                
                if (msg != null)
                {
                    lblMessage.Text = msg;
                    lblMessage.Visible = true;
                }
                BindGrid(status, false);
            }
        }

        protected void grd_CheckOutList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (drpUserRoles.SelectedIndex > 0)
                BindGrid((Convert.ToInt32(drpUserRoles.SelectedValue)), true);
            else
            {
                userID = Session[sessionNames.userID_Karbar].ToString();
                userdt = lngB.Get_UserRoles(userID);
                int status = business.GetStatusOfRole(Convert.ToInt32(userdt.Rows[0][1]));
                BindGrid(status, true);
            }
        }
    }
}