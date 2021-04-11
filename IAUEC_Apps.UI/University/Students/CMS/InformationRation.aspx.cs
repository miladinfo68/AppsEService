using IAUEC_Apps.Business.university.Education;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Students;
using System.IO;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Students.CMS
{
    public partial class InformationRation : System.Web.UI.Page
    {
        //CommonDAO dao = new CommonDAO();
        CommonBusiness CB = new CommonBusiness();
        InformationRationBusiness IRB = new InformationRationBusiness();
        EducationReportBusiness ERB = new EducationReportBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            StiWebViewer1.Visible = false;
            DataTable dtTerm = new DataTable();
            if (!IsPostBack)
            {
                dtTerm = CB.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;

                ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "7"));
                //ddl_Degree.Items.Insert(0, "انتخاب کنید");
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;

                ddl_Sex.Items.Add(new ListItem("مرد", "1"));
                ddl_Sex.Items.Add(new ListItem("زن", "2"));
                ddl_Sex.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Sex.Items[ddl_Sex.Items.Count - 1].Selected = true;

                ddl_StatusStu.Items.Add(new ListItem("نامعلوم" , "0"));
                ddl_StatusStu.Items.Add(new ListItem("عادی", "1"));
                ddl_StatusStu.Items.Add(new ListItem("مهمان از", "2"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف تغییر رشته", "3"));
                ddl_StatusStu.Items.Add(new ListItem("انتقال از", "4"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف با اطلاع", "5"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف ماده 51", "6"));
                ddl_StatusStu.Items.Add(new ListItem("فارغ التحصیل", "7"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج آموزشی", "8"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج انضباطی", "9"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج از واحد های تهران", "10"));
                ddl_StatusStu.Items.Add(new ListItem("محروم", "11"));
                ddl_StatusStu.Items.Add(new ListItem("فوت", "12"));
                ddl_StatusStu.Items.Add(new ListItem("شهید", "13"));
                ddl_StatusStu.Items.Add(new ListItem("مهمان سازمان", "14"));
                ddl_StatusStu.Items.Add(new ListItem("عدم مراجعه", "15"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج از کل واحدها", "16"));
                ddl_StatusStu.Items.Add(new ListItem("تسویه حساب مدرک معادل", "17"));
                ddl_StatusStu.Items.Add(new ListItem("انتخاب کنید", "18"));
                ddl_StatusStu.Items[ddl_StatusStu.Items.Count - 1].Selected = true;
            }

        }

        protected void ddl_Sex_SelectedIndexChanged(object sender, EventArgs e)
        {
           string Sex = ddl_Sex.SelectedValue;
        }

        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Degree = ddl_Sex.SelectedValue;
        }

        protected void ddl_StatusStu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string StatusStu = ddl_StatusStu.SelectedValue;
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Term = ddl_Term.SelectedValue;
        }

        protected void btn_Show_Click(object sender, EventArgs e)
        {
            img_ExportToExcel.Visible = false;
            img_ExportToExcel1.Visible = false;
            imgExportToExcelPermitted.Visible = false;
            if (ddl_Term.SelectedValue == "0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب کنید", 0, 100, "پیام", "");
            }
            if (ddl_StatusStu.SelectedValue == "18")
            {
                RadWindowManager1.RadAlert("لطفا وضعیت دانشجو را انتخاب کنید", 0, 100, "پیام", "");
            }
            else
            {
                if (rdb_Sahmie.Checked == true)
                {
                    int Id = 1;
                    DataTable dt = IRB.GetInfoSahmie(ddl_Term.SelectedValue, int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Degree.SelectedValue));
                    if (dt.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                    }
                    else
                    {
                        //Report .............
                        img_ExportToExcel.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportInfoRation.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmie]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmie]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmie]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmie]"].Parameters["@StatusStu"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
                        rpt.RegData(dt);
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                    }
                }

                if (rdb_EntekhabvahedSahmie.Checked == true)
                {
                    img_ExportToExcel.Visible = false;
                    img_ExportToExcel1.Visible = false;

                        DataTable dt = IRB.GetInfoSahmie2(ddl_Term.SelectedValue, int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue));
                        if (dt.Rows.Count == 0)
                        {
                            RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                        }
                        else
                        {
                            //Report .............
                            img_ExportToExcel1.Visible = true;
                            this.StiWebViewer1.ResetReport();
                            StiWebViewer1.Visible = true;
                            StiReport rpt = new StiReport();
                            rpt.Load(Server.MapPath("../Report/Report.mrt"));
                            rpt.ReportCacheMode = StiReportCacheMode.On;
                            rpt.Dictionary.Databases.Clear();
                            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                            rpt.Compile();
                            rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmie2]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                            rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmie2]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmie2]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmie2]"].Parameters["@StatusStu"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
                            rpt.RegData(dt);
                            StiWebViewer1.Report = rpt;
                            StiWebViewer1.Visible = true;
                        }
                }

                if (rdbPermitted.Checked)
                {
                    var dt = IRB.GetInfoSahmiePermitted(ddl_Term.SelectedValue, int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue));
                    if (dt.Rows.Count == 0)
                        RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                    else
                    {
                        imgExportToExcelPermitted.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportInfoSahmiePermitted.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmiePermitted]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmiePermitted]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmiePermitted]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_InfoSahmiePermitted]"].Parameters["@StatusStu"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
                        rpt.RegData(dt);
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                    }
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
            DataTable dt = IRB.GetInfoSahmie(ddl_Term.SelectedValue, int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Degree.SelectedValue));
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                grd_Show.DataSource = dt;
                grd_Show.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=InfoSahmie.xls");
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

       

        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = IRB.GetInfoSahmie2(ddl_Term.SelectedValue, int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue));
            if (dt.Rows.Count == 0)
            {
               
            }
            else
            {
                grd_Show2.DataSource = dt;
                grd_Show2.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=InfoSahmie.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    foreach (TableCell cell in grd_Show2.HeaderRow.Cells)
                    {
                        cell.BackColor = grd_Show2.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in grd_Show2.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grd_Show2.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grd_Show2.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    grd_Show2.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void imgExportToExcelPermitted_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = IRB.GetInfoSahmiePermitted(ddl_Term.SelectedValue, int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue));
            if(dt.Rows.Count > 0)
            {
                grdPermitted.DataSource = dt;
                grdPermitted.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=InfoSahmie.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    foreach (TableCell cell in grdPermitted.HeaderRow.Cells)
                    {
                        cell.BackColor = grdPermitted.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in grdPermitted.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grdPermitted.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grdPermitted.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    grdPermitted.RenderControl(hw);

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