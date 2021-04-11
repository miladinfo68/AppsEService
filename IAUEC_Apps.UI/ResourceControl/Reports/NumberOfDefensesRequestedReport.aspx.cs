using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.ResourceControlClasses;
using IAUEC_Apps.UI.University.GraduateAffair.CMS;
using ResourceControl.BLL;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.ResourceControl.Reports
{
    public partial class NumberOfDefensesRequestedReport : System.Web.UI.Page
    {
        private RequestHandler _requestHandler = new RequestHandler();

        private string _daneshId;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
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
                AccessControl.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
            }

            _daneshId = Session["DaneshId"].ToString();
            if (_daneshId != 0.ToString())
            {
                foreach (ListItem item in CollegeIddrp.Items)
                {
                    if (_daneshId != item.Value)
                        item.Enabled = false;
                }
            }
        }

        protected void SearchBtn_OnClick(object sender, EventArgs e)
        {
            grdResualtList.Visible = true;
            grdResualtList.DataSource = null;
            grdResualtList.Rebind();

        }

        protected void ExcleExportBtn_OnClick(object sender, EventArgs e)
        {

            var allAcceptedRequest = _requestHandler.NumberOfDefensesRequestedReport(Convert.ToInt32(CollegeIddrp.SelectedValue)
                , StartDateTxt.Text
                , EndDateTxt.Text
                , Convert.ToInt32(drpDateType.SelectedValue));

            if (allAcceptedRequest.Rows.Count <= 0) return;
            try
            {

                var pck = new OfficeOpenXml.ExcelPackage();
                var ws = pck.Workbook.Worksheets.Add("ProfInfoList");

                ws.Cells["A1"].LoadFromDataTable(allAcceptedRequest, true);
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=DefenceList.xlsx");

                Response.BinaryWrite(pck.GetAsByteArray());

            }
            catch (Exception ex)
            {
                //log error
            }
            Response.End();
        }

        protected void ClearBtn_OnClick(object sender, EventArgs e)
        {
            StartDateTxt.Text = "";
            EndDateTxt.Text = "";
            CollegeIddrp.SelectedValue = 0.ToString();
            grdResualtList.Visible = false;
        }

        protected void grdResualtList_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdResualtList.DataSource = _requestHandler.NumberOfDefensesRequestedReport(Convert.ToInt32(CollegeIddrp.SelectedValue),
                  StartDateTxt.Text, EndDateTxt.Text, Convert.ToInt32(drpDateType.SelectedValue));


            GridFilterMenu menu = grdResualtList.FilterMenu;
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
            CurrentPage.CurrentPageNumberValue = grdResualtList.MasterTableView.CurrentPageIndex;
            CurrentPage.PageSizeValue = grdResualtList.MasterTableView.PageSize;


        }

        public static class CurrentPage
        {
            public static int CurrentPageNumberValue { get; set; }
            public static int PageSizeValue { get; set; }
        }


    }
}