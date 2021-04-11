using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using System.IO;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class displayThesisFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            checkUserAccess();
            if (!IsPostBack)
            {
                setDaneshSource();
                setGridThesisSource();
                grdThesis.DataBind();

            }
        }

        private void checkUserAccess()
        {
            if (Request.QueryString["id"] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");

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
            AccessControl.MenuId = menuId;
            Session[sessionNames.menuID] = menuId;
            AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
        }
        protected void exportExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdThesis.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            grdThesis.ExportSettings.IgnorePaging = true;
            grdThesis.ExportSettings.ExportOnlyData = true;
            grdThesis.ExportSettings.OpenInNewWindow = true;
            grdThesis.ExportSettings.UseItemStyles = true;
            grdThesis.ExportSettings.FileName = "لیست پایان نامه آپلود شده";
            grdThesis.MasterTableView.ExportToExcel();
        }

        protected void grdThesis_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            setGridThesisSource();

        }

        private string getDaneshFilter()
        {
            string filter = "";
            foreach (var d in ddlDaneshkade.CheckedItems)
            {
                if (d.Checked)
                {
                    filter += "|" + d.Value;
                }
            }
            if (filter.Length > 0)
                filter += "|";
            return filter;
        }

        private string getFieldFilter()
        {
            string filter = "";
            foreach (var f in ddlField.CheckedItems)
            {
                if (f.Checked)
                {
                    filter += "|" + f.Value;
                }
            }
            if (filter.Length > 0)
                filter += "|";
            return filter;
        }

        private void setFieldSource()
        {
            Business.Common.CommonBusiness cb = new Business.Common.CommonBusiness();
            DataTable fields = new DataTable();
            if (ddlDaneshkade.CheckedItems.Count > 0)
            {

                foreach (var a in ddlDaneshkade.CheckedItems)
                {
                    if (a.Checked)
                        fields.Merge(cb.SelectAllField(Convert.ToInt32(a.Value)));
                }
            }
            ddlField.DataSource = fields;
            ddlField.DataValueField = "id";
            ddlField.DataTextField = "nameresh";
            ddlField.DataBind();
            //if(ddlField.CheckedItems.Count==0)
            //    ddlField.ch
        }

        private void setDaneshSource()
        {
            Business.Common.CommonBusiness cb = new Business.Common.CommonBusiness();
            var danesh = cb.SelectAllDaneshkade();
            ddlDaneshkade.DataSource = danesh;
            ddlDaneshkade.DataValueField = "id";
            ddlDaneshkade.DataTextField = "namedanesh";
            ddlDaneshkade.DataBind();
        }

        protected void ddlDaneshkade_ItemChecked(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            setFieldSource();
        }

        private void setGridThesisSource()
        {
            Business.university.Request.CheckOutRequestBusiness chrb = new Business.university.Request.CheckOutRequestBusiness();
            DataTable dt = chrb.getThesisByFiltering(getDaneshFilter(), getFieldFilter(), txtFamily.Text.Trim(), txtStcode.Text.Trim());
            grdThesis.DataSource = dt;
        }

        protected void grdThesis_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "showThesis")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    string thesisPath = "../../request/pages/" + e.CommandArgument;

                    thesisPath = Server.MapPath(thesisPath);
                    if (System.IO.File.Exists(thesisPath))
                    {
                        Response.ContentType = GetMimeType(e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf('.')));// "pdf";// doc.DOCUMENT_TYPE;
                        
                        Response.Clear();
                        Response.AddHeader("content-disposition", "attachment; filename=" + thesisPath);
                        byte[] file = File.ReadAllBytes(thesisPath);
                        Response.BinaryWrite(file);
                        Response.Flush();
                        Response.End();
                        //var fileByteArray = System.IO.File.ReadAllBytes(thesisPath);
                        //Session["fileUnprintable_Byte"] = fileByteArray;
                        //Session["fileUnprintable_Path"] = thesisPath;
                        //var c_type = GetMimeType(".pdf");

                        //Session["contentTypeUnprintable"] = c_type;
                        //var nocash = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openWin('" + nocash + "')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('فایل پایان نامه موجود نمی باشد. لطفا با مدیر سیستم تماس بگیرید.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "alert('خطا در دریافت فایل پایان نامه رخ داده است.لطفا مجددا تلاش فرمایید.')", true);

                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            setGridThesisSource();
            grdThesis.DataBind();
        }

        protected void ddlDaneshkade_CheckAllCheck(object sender, Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs e)
        {
            setFieldSource();

        }
        private string GetMimeType(string ext)
        {
            return ext.Substring(1);
            string mimeType = "application/unknown";
            // ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;

            switch (ext)
            {
                case ".pdf":
                    return "application/pdf";
                case ".jpeg":
                case ".jpg":
                    return "image/jpeg";
                default: return string.Empty;
            }
        }

        protected void grdThesis_ExcelMLWorkBookCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExcelMLWorkBookCreatedEventArgs e)
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
    }
}