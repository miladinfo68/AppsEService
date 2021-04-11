using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.Business.university.Students;
using IAUEC_Apps.DTO.University.Students;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Students.CMS
{
    public partial class ReportExcellentStudents : System.Web.UI.Page
    {
        ReportExcellentStuBusiness RES = new ReportExcellentStuBusiness();
        UniversityBusiness UB = new UniversityBusiness();
        //CommonDAO dao = new CommonDAO();
        public static ReportExcellentStuDTO RESD = new ReportExcellentStuDTO();
        EducationReportBusiness ERB = new EducationReportBusiness();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        CommonBusiness CB = new CommonBusiness();
        DataTable dtTerm = new DataTable();
        DataTable dtDaneshkade = new DataTable();
        DataTable dtDepartman = new DataTable();
        DataTable dtCooperation = new DataTable();
        DataTable dtResault = new DataTable();
        DataTable dtField = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            StiWebViewer1.Visible = false;
            if (!IsPostBack)
            {
                dtDaneshkade = CB.SelectAllDaneshkade();
                ddl_Daneshkade.DataTextField = "namedanesh";
                ddl_Daneshkade.DataValueField = "id";
                ddl_Daneshkade.DataSource = dtDaneshkade;
                ddl_Daneshkade.DataBind();
                ddl_Daneshkade.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;

                dtField = ERB.SelectAllField();
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
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "6"));
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;

                ddl_Education.Items.Add(new ListItem("دوره ای", "1"));
                ddl_Education.Items.Add(new ListItem("پاره وقت", "2"));
                ddl_Education.Items.Add(new ListItem("طرح معلمان", "3"));
                ddl_Education.Items.Add(new ListItem("قراردادی", "4"));
                ddl_Education.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Education.Items[ddl_Education.Items.Count - 1].Selected = true;

                ddl_Sex.Items.Add(new ListItem("مرد", "1"));
                ddl_Sex.Items.Add(new ListItem("زن", "2"));
                ddl_Sex.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Sex.Items[ddl_Sex.Items.Count - 1].Selected = true;

                ddl_VaziatStu.Items.Add(new ListItem("عادی", "1"));
                ddl_VaziatStu.Items.Add(new ListItem("میهمان از", "2"));
                ddl_VaziatStu.Items.Add(new ListItem("انصراف - تغییر رشته", "3"));
                ddl_VaziatStu.Items.Add(new ListItem("انتقال به", "4"));
                ddl_VaziatStu.Items.Add(new ListItem("انصراف با اطلاع", "5"));
                ddl_VaziatStu.Items.Add(new ListItem("انصراف دو ترم", "6"));
                ddl_VaziatStu.Items.Add(new ListItem("فارغ التحصیل", "7"));
                ddl_VaziatStu.Items.Add(new ListItem("اخراج آموزشی", "8"));
                ddl_VaziatStu.Items.Add(new ListItem("اخراج انصباطی از واحد", "9"));
                ddl_VaziatStu.Items.Add(new ListItem("اخراج از واحد های تهران", "10"));
                ddl_VaziatStu.Items.Add(new ListItem("اخراج از کل واحد ها", "11"));
                ddl_VaziatStu.Items.Add(new ListItem("محروم", "12"));
                ddl_VaziatStu.Items.Add(new ListItem("فوت", "13"));
                ddl_VaziatStu.Items.Add(new ListItem("شهید", "14"));
                ddl_VaziatStu.Items.Add(new ListItem("میهمان از - سازمان", "15"));
                ddl_VaziatStu.Items.Add(new ListItem("عدم مراجعه", "16"));
                ddl_VaziatStu.Items.Add(new ListItem("در شرف فارغ التحصیلی", "17"));
                ddl_VaziatStu.Items.Add(new ListItem("تسویه حساب - مدرک معادل", "18"));
                ddl_VaziatStu.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_VaziatStu.Items[ddl_VaziatStu.Items.Count - 1].Selected = true;
            }

        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            RESD.Daneshkade = int.Parse(ddl_Daneshkade.SelectedValue);
        }

        protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            RESD.Field = int.Parse(ddl_Field.SelectedValue);
        }

        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            RESD.Degree = int.Parse(ddl_Degree.SelectedValue);
        }

        protected void ddl_Education_SelectedIndexChanged(object sender, EventArgs e)
        {
            RESD.Education = int.Parse(ddl_Education.SelectedValue);
        }

        protected void ddl_Sex_SelectedIndexChanged(object sender, EventArgs e)
        {
            RESD.Sex = int.Parse(ddl_Sex.SelectedValue);
        }

        protected void btn_ShowInfo_Click(object sender, EventArgs e)
        {
            img_ExportToExcel.Visible = false;
            if (txt_NimsalVorod.Text == string.Empty || txt_NimsalVorod.Text == "0")
            {
                rwd.RadAlert("لطفا نیمسال ورود را وارد نمایید", 0, 100, "پیام", "");
            }
            if (txt_SalVorod.Text == string.Empty || txt_SalVorod.Text == "0")
            {
                rwd.RadAlert("لطفا سال ورود را وارد نمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_FromDateFeraghat.Text == string.Empty)
                {
                    lbl_FromDateFeraghat1.Text = "  /  /  ";
                }
                else
                {
                    lbl_FromDateFeraghat1.Text = txt_FromDateFeraghat.Text;
                }
                if (txt_ToDateFeraghat.Text == string.Empty)
                {
                    lbl_ToDateFeraghat1.Text = "  /  /  ";
                }
                else
                {
                    lbl_ToDateFeraghat1.Text = txt_FromDateFeraghat.Text;
                }
                DataTable dtResault = RES.GetInfoSTU(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Education.SelectedValue), txt_SalVorod.Text, int.Parse(ddl_Degree.SelectedValue), int.Parse(txt_NimsalVorod.Text), int.Parse(ddl_VaziatStu.SelectedValue), int.Parse(ddl_Sex.SelectedValue), lbl_FromDateFeraghat1.Text, lbl_ToDateFeraghat1.Text);
                if (dtResault.Rows.Count == 0)
                {
                    rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    //Report
                    img_ExportToExcel.Visible = true;
                    this.StiWebViewer1.ResetReport();
                    StiWebViewer1.Visible = true;
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportGraduateStudents.mrt"));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Students].[SP_GraduateStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                    rpt.CompiledReport.DataSources["[Students].[SP_GraduateStudents]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                    rpt.CompiledReport.DataSources["[Students].[SP_GraduateStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Education.SelectedValue);
                    rpt.CompiledReport.DataSources["[Students].[SP_GraduateStudents]"].Parameters["@sal_v"].ParameterValue = txt_SalVorod.Text;
                    rpt.CompiledReport.DataSources["[Students].[SP_GraduateStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                    rpt.CompiledReport.DataSources["[Students].[SP_GraduateStudents]"].Parameters["@Vazkol"].ParameterValue = int.Parse(ddl_VaziatStu.SelectedValue);
                    rpt.CompiledReport.DataSources["[Students].[SP_GraduateStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(txt_NimsalVorod.Text);
                    rpt.CompiledReport.DataSources["[Students].[SP_GraduateStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue);
                    rpt.CompiledReport.DataSources["[Students].[SP_GraduateStudents]"].Parameters["@FromDate"].ParameterValue = lbl_FromDateFeraghat1.Text;
                    rpt.CompiledReport.DataSources["[Students].[SP_GraduateStudents]"].Parameters["@ToDate"].ParameterValue = lbl_ToDateFeraghat1.Text;
                    rpt.RegData(dtResault);
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                }
            }
        }

        protected void ddl_VaziatStu_SelectedIndexChanged(object sender, EventArgs e)
        {
            RESD.StatusStu = int.Parse(ddl_VaziatStu.SelectedValue);
        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_NimsalVorod.Text == string.Empty)
            {
                txt_NimsalVorod.Text = "0";
            }
            if (txt_SalVorod.Text == string.Empty)
            {
                txt_SalVorod.Text = "0";
            }
            DataTable dtResault = RES.GetInfoSTU(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Education.SelectedValue), txt_SalVorod.Text, int.Parse(ddl_Degree.SelectedValue), int.Parse(txt_NimsalVorod.Text), int.Parse(ddl_VaziatStu.SelectedValue), int.Parse(ddl_Sex.SelectedValue), lbl_FromDateFeraghat1.Text, lbl_ToDateFeraghat1.Text);

            if (dtResault.Rows.Count == 0)
            {

            }
            else
            {
                GridView.DataSource = dtResault;
                GridView.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportEvalutionProfDividedODD.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView.RenderControl(hw);

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