using IAUEC_Apps.Business.university.Faculty;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ListAllProf : System.Web.UI.Page
    {
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void img_ExportToExcel3_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = FRB.ListAllProf();
            if (dt.Rows.Count > 0)
           
            {
                GridView3.DataSource = dt;
                GridView3.DataBind();
                 try
                {

                    var pck = new OfficeOpenXml.ExcelPackage();
                    var ws = pck.Workbook.Worksheets.Add("ProfInfoList");
                    //foreach (var dt. in collection)
                    //{
                        
                    //}
                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=ProfInfoList.xlsx");
                    
                    Response.BinaryWrite(pck.GetAsByteArray());
                    //dt.Columns.Add("StudentID", typeof(int));
                    //dt.Columns.Add("StudentName", typeof(string));
                    //dt.Columns.Add("RollNumber", typeof(int));
                    //dt.Columns.Add("TotalMarks", typeof(int));
                }
                catch (Exception ex)
                {
                    //log error
                }
                Response.End();

            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=ReportListAllProf.xls");
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.ms-excel";
            //    using (StringWriter sw = new StringWriter())
            //    {
            //        HtmlTextWriter hw = new HtmlTextWriter(sw);

            //        //To Export all pages
            //        ////gv_Show.AllowPaging = false;
            //        ////this.BindGrid();

            //        //gv_Show.HeaderRow.BackColor = Color.White;
            //        foreach (TableCell cell in GridView3.HeaderRow.Cells)
            //        {
            //            cell.BackColor = GridView3.HeaderStyle.BackColor;
            //        }
            //        foreach (GridViewRow row in GridView3.Rows)
            //        {
            //            //row.BackColor = Color.White;
            //            foreach (TableCell cell in row.Cells)
            //            {
            //                if (row.RowIndex % 2 == 0)
            //                {
            //                    cell.BackColor = GridView3.AlternatingRowStyle.BackColor;
            //                }
            //                else
            //                {
            //                    cell.BackColor = GridView3.RowStyle.BackColor;
            //                }
            //                cell.CssClass = "textmode";
            //            }
            //        }

            //        GridView3.RenderControl(hw);

            //        //style to format numbers to string
            //        string style = @"<style> .textmode { } </style>";
            //        Response.Write(style);
            //        Response.Output.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }
            }
        }
    }
}