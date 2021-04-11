using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.University.Faculty;
using System.Data;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Faculty;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

using System.IO;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class PercentageOfFallenStudents : System.Web.UI.Page
    {
        
        public static PercentageStuProbationDTO PSD = new PercentageStuProbationDTO();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        CommonBusiness CB = new CommonBusiness();
        DataTable dtResault = new DataTable();
        DataTable dtTerm = new DataTable();
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
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataValueField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;
                ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "6"));
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;
            }
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSD.Term = ddl_Term.SelectedValue;
        }


        protected void btn_ShowInfo_Click(object sender, EventArgs e)
        {
           
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب نمایید", 0, 100, "پیام", "");
            }
            else if (ddl_Degree.SelectedValue == null || ddl_Degree.SelectedValue == "0")
            {
                RadWindowManager1.RadAlert("لطفا مقطع را انتخاب نمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (rdb_Mardudi.Checked == true)
                {
                   
                    PSD.Percent = txt_Percent.Text;
                    if (txt_Percent.Text == null || txt_Percent.Text == "")
                    {
                        txt_Percent.Text = "0";
                    }
                    dtResault = FRB.GetStudentsProbation(ddl_Term.SelectedValue, int.Parse(ddl_Degree.SelectedValue), int.Parse(txt_Percent.Text));
                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                    }
                    else
                    {
                        //Report
                        img_ExportToExcel1.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportStudentProbation.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Faculty].[SP_StudentsProbation]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_StudentsProbation]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_StudentsProbation]"].Parameters["@Percent"].ParameterValue = int.Parse(txt_Percent.Text);
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }
                }
                else
                {
                    
                    PSD.Percent = txt_PercentGhabul.Text;
                    if (txt_PercentGhabul.Text == null || txt_PercentGhabul.Text == "")
                    {
                        txt_PercentGhabul.Text = "0";
                    }
                    dtResault = FRB.GetStudentsProbationAcceptance(ddl_Term.SelectedValue, int.Parse(ddl_Degree.SelectedValue), int.Parse(txt_PercentGhabul.Text));
                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                    }
                    else
                    {
                        //Report
                        img_ExportToExcel2.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportStudentProbationAcceptance.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Faculty].[SP_StudentsProbationAcceptance]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_StudentsProbationAcceptance]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_StudentsProbationAcceptance]"].Parameters["@Percent"].ParameterValue = int.Parse(txt_PercentGhabul.Text);
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }
                }
            }
        }

        //protected void btn_Exit_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("FacultyReports.aspx");
        //}

        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSD.Degree = ddl_Degree.SelectedValue;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = FRB.GetStudentsProbation(ddl_Term.SelectedValue, int.Parse(ddl_Degree.SelectedValue), int.Parse(txt_Percent.Text));
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportStudentProbation.xls");
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

        //protected void rdb_Ghabuli_CheckedChanged(object sender, EventArgs e)
        //{

        //}

        protected void img_ExportToExcel2_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = FRB.GetStudentsProbationAcceptance(ddl_Term.SelectedValue, int.Parse(ddl_Degree.SelectedValue), int.Parse(txt_PercentGhabul.Text));
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportStudentProbationAcceptance.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView2.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView2.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView2.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView2.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView2.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void rdb_Ghabuli_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Ghabuli.Checked == true)
            {
                lbl_Percent.Visible = false;
                txt_Percent.Visible = false;
                lbl_PercentGhabul.Visible = true;
                txt_PercentGhabul.Visible = true;
            }
            else
            {
                lbl_Percent.Visible = true;
                txt_Percent.Visible = true;
                lbl_PercentGhabul.Visible = false;
                txt_PercentGhabul.Visible = false;
            }
        }
    }
}