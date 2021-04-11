//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Data;
//using System.Web.UI.WebControls;
//using IAUEC_Apps.Business.university.Request;
//using IAUEC_Apps.DTO.University.Request;
//using IAUEC_Apps.Business.Common;
//using Telerik.Web.UI;
//using xi = Telerik.Web.UI.ExportInfrastructure;
//using Telerik.Web.UI.GridExcelBuilder;
//using System.Drawing;

//namespace IAUEC_Apps.UI.University.Request.CMS
//{
//    public partial class MadarekReport : System.Web.UI.Page
//    {
//        MadarekReportBusiness business = new MadarekReportBusiness();
//        MadarekReportDTO dto = new MadarekReportDTO();
//        DataTable dt = new DataTable();

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//            //    string mId = Request.QueryString["id"].ToString();
//            //    string[] id = mId.ToString().Split(new char[] { '@' });
//            //    string menuId = "";
//            //    for (int i = 0; i < id[1].Length; i++)
//            //    {
//            //        string s = id[1].Substring(i + 1, 1);
//            //        if (s != "-")
//            //            menuId += s;
//            //        else
//            //            break;
//            //        Session["MenuId"] = menuId;
//            //    }
//            //    AccessControl1.MenuId = Session["MenuId"].ToString();
//            //    AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
//            }
//        }

//        protected void btnSearch_Click(object sender, EventArgs e)
//        {
//            if (!chkRiz.Checked && !chkGovahi.Checked && !chkDanesh.Checked)
//            {
//                valMadarek.IsValid = false;
//                grdResult.Visible = false;
//            }
//            else
//            {
//                valMadarek.IsValid = true;
//                grdResult.Visible = true;
//                setData();
//                dt = business.GetInfo(dto);
//                //if (dt.Rows.Count != 0)
//                //{
//                //    btnExcel.Visible = true;
//                //}
//                //else
//                //{
//                //    btnExcel.Visible = false;
//                //}
//                grdResult.DataSource = dt;
//                grdResult.DataBind();
//                GridFilterMenu menu = grdResult.FilterMenu;
//                if (menu.Items.Count > 3)
//                {
//                    int im = 0;
//                    while (im < menu.Items.Count)
//                    {
//                        if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
//                        {
//                            im++;
//                        }
//                        else
//                        {
//                            menu.Items.RemoveAt(im);
//                        }
//                    }
//                    foreach (RadMenuItem item in menu.Items)
//                    {    //change the text for the "StartsWith" menu item  
//                        if (item.Text == "NoFilter")
//                        {
//                            item.Text = "حذف فیلتر";
//                            //item.Remove();
//                        }
//                        if (item.Text == "Contains")
//                        {
//                            item.Text = "شامل";
//                            //item.Remove();
//                        }
//                        if (item.Text == "EqualTo")
//                        {
//                            item.Text = "مساوی با";
//                            //item.Remove();
//                        }

//                    }
//                }
//            }
//        }

//        protected void grdResult_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
//        {

//        }

//        protected void grdResult_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
//        {
//            setData();
//            if (dto.eDate != string.Empty && dto.eDate != string.Empty)
//            {
//                dt = business.GetInfo(dto);
//                grdResult.DataSource = dt;

//                //if (dt.Rows.Count != 0)
//                //{
//                //    btnExcel.Visible = true;
//                //}
//                //else
//                //{
//                //    btnExcel.Visible = false;
//                //}
//                GridFilterMenu menu = grdResult.FilterMenu;
//                if (menu.Items.Count > 3)
//                {
//                    int im = 0;
//                    while (im < menu.Items.Count)
//                    {
//                        if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
//                        {
//                            im++;
//                        }
//                        else
//                        {
//                            menu.Items.RemoveAt(im);
//                        }
//                    }
//                    foreach (RadMenuItem item in menu.Items)
//                    {    //change the text for the "StartsWith" menu item  
//                        if (item.Text == "NoFilter")
//                        {
//                            item.Text = "حذف فیلتر";
//                            //item.Remove();
//                        }
//                        if (item.Text == "Contains")
//                        {
//                            item.Text = "شامل";
//                            //item.Remove();
//                        }
//                        if (item.Text == "EqualTo")
//                        {
//                            item.Text = "مساوی با";
//                            //item.Remove();
//                        }
//                    }
//                }
//            }

//        }

//        private void setData()
//        {
//            if (chkRiz.Checked)
//            {
//                dto.rizNomarat = 1;
//            }
//            else
//            {
//                dto.rizNomarat = 0;
//            }
//            if (chkGovahi.Checked)
//            {
//                dto.GovahiMovaghat = 1;
//            }
//            else
//            {
//                dto.GovahiMovaghat = 0;
//            }
//            if (chkDanesh.Checked)
//            {
//                dto.DaneshName = 1;
//            }
//            else
//            {
//                dto.DaneshName = 0;
//            }

//            dto.sDate = txtSdate.Text;
//            dto.eDate = txtEdate.Text;
//            if (dto.eDate != string.Empty && dto.eDate != string.Empty)
//            {
//                dto.sDate = dto.sDate.Substring(2, dto.sDate.Length - 2);
//                dto.eDate = dto.eDate.Substring(2, dto.eDate.Length - 2);
//            }
//        }

//        protected void grdResult_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
//        {
//            if (e.CommandName == "History")
//            {
//                int reqID = Convert.ToInt32(e.CommandArgument);
//                GridDataItem itemAmount = (GridDataItem)e.Item;
//                string stcode = itemAmount["stcode"].Text;
//                string stName = itemAmount["name"].Text;
//                string reqDate = itemAmount["CreateDate"].Text;

//                CommonBusiness cmb = new CommonBusiness();

//                lst_history.DataSource = cmb.GetUserLogByModifyId(int.Parse(e.CommandArgument.ToString()), 12);
//                lst_history.DataBind();

//                info1.InnerText = "نام دانشجو:" + stName;
//                info2.InnerText = "شماره درخواست:" + reqID;
//                info3.InnerText = "تاریخ درخواست:" + reqDate;
                
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
//            }
//        }

//        protected void grdResult_ExcelMLWorkBookCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExcelMLWorkBookCreatedEventArgs e)
//        {
//            int r = 0;
//            foreach (RowElement row in e.WorkBook.Worksheets[0].Table.Rows)
//            {


//                for (int i = 0; i < row.Cells.Count; i++)
//                {
//                    if (r != 0)
//                    {
//                        if (r % 2 == 0)
//                            row.Cells[i].StyleValue = "Style1";
//                        else
//                            row.Cells[i].StyleValue = "Style2";
//                    }
//                    else
//                        row.Cells[i].StyleValue = "styleHeader";
//                }
//                r++;

//            }
//            StyleElement styleHeader = new StyleElement("styleHeader");
//            styleHeader.InteriorStyle.Pattern = InteriorPatternType.Solid;
//            styleHeader.InteriorStyle.Color = System.Drawing.Color.White;
//            styleHeader.FontStyle.FontName = "Tahoma";
//            styleHeader.FontStyle.Bold = true;
//            styleHeader.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
//            e.WorkBook.Styles.Add(styleHeader);
//            StyleElement style = new StyleElement("Style1");
//            style.InteriorStyle.Pattern = InteriorPatternType.Solid;
//            style.InteriorStyle.Color = System.Drawing.Color.FromArgb(27, 198, 255);
//            style.FontStyle.FontName = "Tahoma";
//            style.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
//            e.WorkBook.Styles.Add(style);
//            StyleElement style2 = new StyleElement("Style2");
//            style2.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
//            style2.InteriorStyle.Pattern = InteriorPatternType.Solid;
//            style2.InteriorStyle.Color = System.Drawing.Color.FromArgb(217, 243, 255);
//            style2.FontStyle.FontName = "Tahoma";
//            e.WorkBook.Styles.Add(style2);
//        }

//        protected void btnExcel_Click(object sender, EventArgs e)
//        {
//            grdResult.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
//            grdResult.ExportSettings.IgnorePaging = false;
//            grdResult.ExportSettings.ExportOnlyData = true;
//            grdResult.ExportSettings.OpenInNewWindow = true;
//            grdResult.ExportSettings.UseItemStyles = true;
//            grdResult.MasterTableView.ExportToExcel();
//        }
//    }
//}