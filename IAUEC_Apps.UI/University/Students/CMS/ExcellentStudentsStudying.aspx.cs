using IAUEC_Apps.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.university.Students;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.IO;

namespace IAUEC_Apps.UI.University.Students.CMS
{
    public partial class ExcellentStudentsStudying : System.Web.UI.Page
    {
        //CommonDAO dao = new CommonDAO();
        CommonBusiness CB = new CommonBusiness();
        DataTable dtTerm = new DataTable();
        DataTable dtField = new DataTable();
        ReportExcellentStuBusiness RES = new ReportExcellentStuBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtTerm = CB.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;

                dtField = CB.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;

                ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "7"));
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;

                ddl_NimsalVorod.Items.Add(new ListItem("مهر", "1"));
                ddl_NimsalVorod.Items.Add(new ListItem("بهمن", "2"));
                ddl_NimsalVorod.Items.Add(new ListItem("تابستان", "3"));
                ddl_NimsalVorod.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_NimsalVorod.Items[ddl_NimsalVorod.Items.Count - 1].Selected = true;             
            }
            StiWebViewer1.Visible = false;
        }

        protected void ddl_NimsalVorod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NimsalVorod = ddl_NimsalVorod.SelectedValue;
        }

        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Degree = ddl_Degree.SelectedValue;
        }

        protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Field = ddl_Field.SelectedValue;
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Term = ddl_Term.SelectedValue;
        }

        protected void btn_ShowList_Click(object sender, EventArgs e)
        {
            img_ExportToExcel.Visible = false;

            if (ddl_Term.SelectedValue == "0")
            {
                rwd.RadAlert("لطفا ترم را انتخاب نمایید", 0, 100, "پیام", "");
            }
            else if (ddl_Field.SelectedValue == "0")
            {
                rwd.RadAlert("لطفا رشته را انتخاب نمایید", 0, 100, "پیام", "");
            }
            else if (ddl_Degree.SelectedValue == "0")
            {
                rwd.RadAlert("لطفا مقطع تحصیلی را انتخاب نمایید", 0, 100, "پیام", "");
            }
            else if (ddl_NimsalVorod.SelectedValue == "0")
            {
                rwd.RadAlert("لطفا نیمسال ورودی را انتخاب نمایید", 0, 100, "پیام", "");
            }
            else if (txt_SalVorod.Text == null || txt_SalVorod.Text == string.Empty)
            {
                rwd.RadAlert("لطفا سال ورود را وارد نمایید", 0, 100, "پیام", "");
            }
            else
            {
               DataTable dt = RES.ExcellentStudents(ddl_Term.SelectedValue, txt_SalVorod.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Field.SelectedValue));
               if (dt.Rows.Count == 0)
               {
                   rwd.RadAlert("رکوردی وجود ندارد" , 0 , 100 , "پیام" , "");
               }
               else
               {
                   //Report
                   img_ExportToExcel.Visible = true;
                   this.StiWebViewer1.ResetReport();
                   StiWebViewer1.Visible = true;
                   StiReport rpt = new StiReport();
                   rpt.Load(Server.MapPath("../Report/ReportExcellentStudents.mrt"));
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.Dictionary.Databases.Clear();
                   rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                   rpt.Compile();
                   rpt.CompiledReport.DataSources["[Students].[SP_ExcellentStudents]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                   rpt.CompiledReport.DataSources["[Students].[SP_ExcellentStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
                   rpt.CompiledReport.DataSources["[Students].[SP_ExcellentStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
                   rpt.CompiledReport.DataSources["[Students].[SP_ExcellentStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                   rpt.CompiledReport.DataSources["[Students].[SP_ExcellentStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                   rpt.RegData(dt);
                   StiWebViewer1.Report = rpt;
                   StiWebViewer1.Visible = true;

               }          
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = RES.ExcellentStudents(ddl_Term.SelectedValue, txt_SalVorod.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Field.SelectedValue));
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                grd_Show.DataSource = dt;
                grd_Show.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ExcellentStudents.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in grd_Show.HeaderRow.Cells)
                    {
                        cell.BackColor = grd_Show.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in grd_Show.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grd_Show.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grd_Show.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    grd_Show.RenderControl(hw);

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