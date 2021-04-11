using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report;
using System.Data;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.DTO.University.Faculty;
using Stimulsoft.Report.Dictionary;

using System.IO;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ShowDateAndClockExamTheMaster : System.Web.UI.Page
    {
        //CommonDAO dao = new CommonDAO();
        ExamShowClockDateDTO FCD = new ExamShowClockDateDTO();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        DataTable dtTerm = new DataTable();
        DataTable dtResault = new DataTable();
        CommonBusiness CB = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
             StiWebViewer1.Visible = false;
             if (!IsPostBack)
             {
                 //
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
                     Session[sessionNames.menuID] = menuId;
                 }
                 AccessControl1.MenuId = Session[sessionNames.menuID].ToString(); 
                 AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
                 //
                 dtTerm = CB.SelectAllTerm();
                 ddl_NewType.DataTextField = "tterm";
                 ddl_NewType.DataSource = dtTerm;
                 ddl_NewType.DataBind();
                 ddl_NewType.Items.Add(new ListItem("انتخاب کنید", "0"));
                 ddl_NewType.Items[ddl_NewType.Items.Count - 1].Selected = true;
             }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            if(ddl_NewType.SelectedValue == null || ddl_NewType.SelectedValue=="0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را وارد نمایید", 0, 100, "پیام", "");
            }
            else
            {
                dtResault = FRB.ShowClockDateExam(ddl_NewType.SelectedValue);
                if (dtResault.Rows.Count == 0)
                {
                    RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    img_ExportToExcel1.Visible = true;
                    this.StiWebViewer1.ResetReport();
                    StiWebViewer1.Visible = true;
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportExamShowClockDate.mrt"));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Faculty].[SP_GetSaatDateExam]"].Parameters["@Term"].ParameterValue = ddl_NewType.SelectedValue;
                    rpt.RegData(dtResault);
                    rpt.Dictionary.Synchronize();
                    //rpt.Show();
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                    //rpt.Print(true);
                }
            }
        }

        protected void ddl_NewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FCD.Term = ddl_NewType.SelectedValue;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtResault = FRB.ShowClockDateExam(ddl_NewType.SelectedValue);
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView1.DataSource = dtResault;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportExamShowClockDate.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView1.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView1.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView1.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}