﻿using IAUEC_Apps.Business.university.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.DTO.University.Faculty;
using System.Configuration;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Support;
using Telerik.Web.UI.Grid;
using Telerik.Web.UI;
using System.Drawing;
using Telerik.Web.UI.GridExcelBuilder;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class NotacceptableRequest : System.Web.UI.Page
    {
        public static PassProfessorDTO PPD = new PassProfessorDTO();
        InsertToSida ITS = new InsertToSida();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();

        
        CommonBusiness ProfCommonBusiness = new CommonBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = FRB.SelectInfoPeopleBystatus(2);
            grd_Show.DataSource = dt;
            PersiaFiltering();
        }

        protected void PersiaFiltering()
        {
            GridFilterMenu menu = grd_Show.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" ||
                        menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {
                    //change the text for the "StartsWith" menu item  
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

        protected void grd_Show_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Session["RoleID"].ToString() == "1" || Session["RoleID"].ToString() == "7")
            {
                DataTable dt = FRB.SelectInfoPeopleBystatus(2);
                grd_Show.DataSource = dt;
                PersiaFiltering();
            }
            else if (Session["RoleID"].ToString() == "9" || Session["RoleID"].ToString() == "10")
            {
                // اگر پژوهش بود
                DataTable dt = FRB.SelectInfoPeopleBystatus(2);
                grd_Show.DataSource = dt;
                PersiaFiltering();
            }
            else
            {
                //اگر آموزش بود هم آموزش هم پژوهش
                DataTable dt = FRB.SelectInfoPeopleBystatus(2);
                grd_Show.DataSource = dt;
                PersiaFiltering();
            }

        }

        protected void grd_Show_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Session["Page"] = 2;
                string index = e.CommandArgument.ToString();
                ViewState.Add("index", index);
                if (e.CommandName == "Detail")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "radModal();", true);
                }
            }
        }

        protected void grd_Show_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            
           

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem) e.Item;
                if (Session["RoleID"].ToString() == "1")
                {
                }
            }
        }

        protected void rbConfirm_OK1_Click(object sender, EventArgs e)
        {
            
        }
        protected void closeCustomConfirm1(object sender, EventArgs e)
        {
            Response.Redirect("NotacceptableRequest.aspx");
        }
        protected void RadButton1_Click(object sender, EventArgs e)
        {
            FRB.UpdateInfoPeopleStatus(ViewState["index"].ToString(), 0);
            rwd.RadAlert("درخواست ثبت شد گردید", 0, 100, "پیام", "");
            Response.Redirect("NotacceptableRequest.aspx");
        }

        protected void grd_Show_ExcelMLWorkBookCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExcelMLWorkBookCreatedEventArgs e)
        {
            int r = 0;
            foreach (RowElement row in e.WorkBook.Worksheets[0].Table.Rows)
            {


                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (r != 0)
                    {
                        if (r % 2 == 0)
                            row.Cells[i].StyleValue = "Style1";
                        else
                            row.Cells[i].StyleValue = "Style2";
                    }
                    else
                        row.Cells[i].StyleValue = "styleHeader";
                }
                r++;

            }
            StyleElement styleHeader = new StyleElement("styleHeader");
            styleHeader.InteriorStyle.Pattern = InteriorPatternType.Solid;
            styleHeader.InteriorStyle.Color = System.Drawing.Color.White;
            styleHeader.FontStyle.FontName = "Tahoma";
            styleHeader.FontStyle.Bold = true;
            styleHeader.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            e.WorkBook.Styles.Add(styleHeader);
            StyleElement style = new StyleElement("Style1");
            style.InteriorStyle.Pattern = InteriorPatternType.Solid;
            style.InteriorStyle.Color = System.Drawing.Color.FromArgb(162, 226, 255);
            style.FontStyle.FontName = "Tahoma";
            style.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            e.WorkBook.Styles.Add(style);
            StyleElement style2 = new StyleElement("Style2");
            style2.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            style2.InteriorStyle.Pattern = InteriorPatternType.Solid;
            style2.InteriorStyle.Color = System.Drawing.Color.FromArgb(217, 243, 255);
            style2.FontStyle.FontName = "Tahoma";
            e.WorkBook.Styles.Add(style2);
        }

        protected void btn_excel_Click(object sender, EventArgs e)
        {
            grd_Show.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            grd_Show.ExportSettings.IgnorePaging = false;
            grd_Show.ExportSettings.ExportOnlyData = true;
            grd_Show.ExportSettings.OpenInNewWindow = true;
            grd_Show.ExportSettings.UseItemStyles = true;
            grd_Show.MasterTableView.ExportToExcel();
        }
    }
}