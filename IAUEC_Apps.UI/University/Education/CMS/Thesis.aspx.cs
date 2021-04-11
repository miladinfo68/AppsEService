using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Education;
using OfficeOpenXml.Style;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Education.CMS
{
    public partial class Thesis : System.Web.UI.Page
    {
        //CommonDAO dao = new CommonDAO();
        CommonBusiness cb = new CommonBusiness();
        DataTable dtTerm = new DataTable();
        DataTable dtDaneshkade = new DataTable();
        EducationReportBusiness ERB = new EducationReportBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtTerm = cb.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;

                dtDaneshkade = cb.SelectAllDaneshkade();
                ddl_Daneshkade.DataTextField = "namedanesh";
                ddl_Daneshkade.DataValueField = "id";
                ddl_Daneshkade.DataSource = dtDaneshkade;
                ddl_Daneshkade.DataBind();
                ddl_Daneshkade.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;

                if (Session["RoleID"].ToString() == "15" || Session["RoleID"].ToString() == "26")
                {
                    ddl_Daneshkade.SelectedValue = "2";
                    ddl_Daneshkade.Enabled = false;                  
                }
                else if (Session["RoleID"].ToString() == "17" || Session["RoleID"].ToString() == "28")
                {
                    ddl_Daneshkade.SelectedValue = "1";
                    ddl_Daneshkade.Enabled = false;
                }
                else if (Session["RoleID"].ToString() == "16" || Session["RoleID"].ToString() == "27")
                {
                    ddl_Daneshkade.SelectedValue = "3";
                    ddl_Daneshkade.Enabled = false;
                }
                else
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                StiWebViewer1.Visible = false;
            }
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
           string Term = ddl_Term.SelectedValue;
        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Daneshkade = ddl_Daneshkade.SelectedValue;
        }

        protected void btn_ShowReport_Click(object sender, EventArgs e)
        {
            img_ExportToExcel.Visible = false;

            if (ddl_Term.SelectedValue == null)
            {
                rwd.RadAlert("لطفا ترم را انتخاب نمایید", 100, 0, "پیام", "");
            }
            else
            {
                DataTable dtResault = ERB.Getthesis(ddl_Term.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue));
                if (dtResault.Rows.Count == 0)
                {
                    rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    img_ExportToExcel.Visible = true;
                    this.StiWebViewer1.ResetReport();
                    StiWebViewer1.Visible = true;
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportThesis3.mrt"));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", cb.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Education].[SP_Thesis]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                    rpt.CompiledReport.DataSources["[Education].[SP_Thesis]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                    rpt.RegData(dtResault);
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                }
            }
        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtResault = ERB.Getthesis(ddl_Term.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue));
            if (dtResault.Rows.Count == 0)
            {
                rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
            }
            else
            {
                try
                {
                    var pck = new OfficeOpenXml.ExcelPackage();
                    var ws = pck.Workbook.Worksheets.Add("Thesis");
                    //gv_Show.HeaderStyle.Font.Bold = true;     // SET HEADER AS BOLD.
                    using (var rng = ws.Cells["A1:F1"])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.WrapText = true;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(237, 237, 237));
                        rng.Style.Font.Size = 12;
                    }
                    ws.Cells["A1"].Value = "نام";
                    ws.Cells["B1"].Value = "نام خانوادگی";
                    ws.Cells["C1"].Value = "شماره دانشجویی";
                    ws.Cells["D1"].Value = "دانشکده";
                    ws.Cells["E1"].Value = "رشته تحصیلی";
                    ws.Cells["F1"].Value = "تاریخ";
                    ws.Row(200).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Row(200).Style.Font.Name = "B Nazanin";
                    ws.Cells["A2"].LoadFromDataTable(dtResault, false);
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=Thesis.xlsx");
                    Response.BinaryWrite(pck.GetAsByteArray());
                }
                catch (Exception)
                {
                    throw;
                }
                Response.End();
            }
        }
    }
}