using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Students;
using IAUEC_Apps.DTO.University.Students;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;

namespace IAUEC_Apps.UI.University.Students.CMS
{
    public partial class ListStudentsUnderWelfare : System.Web.UI.Page
    {
        //CommonDAO dao = new CommonDAO();
        ListStudentsUnderWelfareDTO LSWD = new ListStudentsUnderWelfareDTO();
        CommonBusiness CB = new CommonBusiness();
        DataTable dtField = new DataTable();
        ListStudentsBasedOnIGRDBusiness LSBIB = new ListStudentsBasedOnIGRDBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            StiWebViewer1.Visible = false;
            if (!IsPostBack)
            {
                dtField = CB.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
                if (Request.QueryString["Field"] != null)
                {
                    ddl_Field.SelectedValue = Request.QueryString["Field"].ToString();
                }
                ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "6"));
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;
                if (Request.QueryString["Degree"] != null)
                {
                    ddl_Degree.SelectedValue = Request.QueryString["Degree"].ToString();
                }
                ddl_Dorpar.Items.Add(new ListItem("دوره ای", "1"));
                ddl_Dorpar.Items.Add(new ListItem("پاره وقت", "2"));
                ddl_Dorpar.Items.Add(new ListItem("طرح معلمان", "3"));
                ddl_Dorpar.Items.Add(new ListItem("قراردادی", "4"));
                ddl_Dorpar.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Dorpar.Items[ddl_Dorpar.Items.Count - 1].Selected = true;
                if (Request.QueryString["Education"] != null)
                {
                    ddl_Dorpar.SelectedValue = Request.QueryString["Education"].ToString();
                }
                ddl_Sex.Items.Add(new ListItem("مرد", "1"));
                ddl_Sex.Items.Add(new ListItem("زن", "2"));
                ddl_Sex.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Sex.Items[ddl_Sex.Items.Count - 1].Selected = true;
                if (Request.QueryString["Sex"] != null)
                {
                    ddl_Sex.SelectedValue = Request.QueryString["Sex"].ToString();
                }
                ddl_VaziatStu.Items.Add(new ListItem("نامعلوم", "0"));
                ddl_VaziatStu.Items.Add(new ListItem("عادی", "1"));
                ddl_VaziatStu.Items.Add(new ListItem("میهمان از", "2"));
                ddl_VaziatStu.Items.Add(new ListItem("انصراف - تغییر رشته", "3"));
                ddl_VaziatStu.Items.Add(new ListItem("انتقال به", "4"));
                ddl_VaziatStu.Items.Add(new ListItem("انصراف با اطلاع", "5"));
                ddl_VaziatStu.Items.Add(new ListItem("انصراف ماده 51", "6"));
                ddl_VaziatStu.Items.Add(new ListItem("فارغ التحصیل", "7"));
                ddl_VaziatStu.Items.Add(new ListItem("اخراج آموزشی", "8"));
                ddl_VaziatStu.Items.Add(new ListItem("اخراج انضباطی از واحدها", "9"));
                ddl_VaziatStu.Items.Add(new ListItem("اخراج از واحدهای تهران", "10"));
                ddl_VaziatStu.Items.Add(new ListItem("اخراج از کل واحد ها", "11"));
                ddl_VaziatStu.Items.Add(new ListItem("محروم", "12"));
                ddl_VaziatStu.Items.Add(new ListItem("فوت", "13"));
                ddl_VaziatStu.Items.Add(new ListItem("شهید", "14"));
                ddl_VaziatStu.Items.Add(new ListItem("میهمان از- سازمان", "15"));
                ddl_VaziatStu.Items.Add(new ListItem("عدم مراجعه", "16"));
                ddl_VaziatStu.Items.Add(new ListItem("در شرف فارغ التحصیلی", "17"));
                ddl_VaziatStu.Items.Add(new ListItem("تسویه حساب - مدرک معادل", "18"));
                ddl_VaziatStu.Items.Add(new ListItem("انتخاب کنید", "19"));
                ddl_VaziatStu.Items[ddl_VaziatStu.Items.Count - 1].Selected = true;
                if (Request.QueryString["StatusStu"] != null)
                {
                    ddl_VaziatStu.SelectedValue = Request.QueryString["StatusStu"].ToString();
                }
            }
            if (Session["stcode"] != null)
            {
                txt_Students.Text = Session["stcode"].ToString();
            }
        }

        protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSWD.Field = int.Parse(ddl_Field.SelectedValue.ToString());
        }

        protected void btn_Click_Click(object sender, EventArgs e)
        {
            Session["page"] = "2";

            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = ("../../../university/Students/CMS/SearchOfStudent.aspx?" + "Field" + "=" + ddl_Field.SelectedValue + "&" + "Degree" + "=" + ddl_Degree.SelectedValue + "&" + "Education" + "=" + ddl_Dorpar.SelectedValue + "&" + "Sex" + "=" + ddl_Sex.SelectedValue + "&" + "StatusStu" + "=" + ddl_VaziatStu.SelectedValue);
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code 
            widnow1.Height = 600;
            widnow1.Width = 1100;
            windowManager.Windows.Add(widnow1);
            windowManager.Height = 600;
            windowManager.Width = 1100;
            ContentPlaceHolder mpContentPlaceHolder;
            mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            mpContentPlaceHolder.Controls.Add(widnow1);


        }

        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSWD.Degree = int.Parse(ddl_Degree.SelectedValue.ToString());
        }

        protected void ddl_Dorpar_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSWD.Education = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        }

        protected void ddl_Sex_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSWD.Sex = int.Parse(ddl_Sex.SelectedValue.ToString());
        }

        protected void ddl_VaziatStu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSWD.StatusStu = int.Parse(ddl_VaziatStu.SelectedValue.ToString());
        }
        protected void RadAjaxManager1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                txt_Students.Text = Session["stcode"].ToString();
            }
        }
        protected void btn_Show_Click(object sender, EventArgs e)
        {
            img_ExportToExcel.Visible = false;
            if (txt_Students.Text == string.Empty)
            {
                txt_Students.Text = "0";
            }
            if (txt_SalVorod.Text == string.Empty)
            {
                txt_SalVorod.Text = "0";
            }
            if (rdb_Behzisti.Checked == true)
            {
                DataTable dtResault = LSBIB.GetStudentBehzisti(txt_Students.Text, int.Parse(ddl_Degree.SelectedValue.ToString()), int.Parse(ddl_Sex.SelectedValue.ToString()), int.Parse(ddl_Dorpar.SelectedValue.ToString()), int.Parse(ddl_Field.SelectedValue.ToString()), txt_SalVorod.Text, int.Parse(ddl_VaziatStu.SelectedValue.ToString()));
                if (dtResault.Rows.Count == 0)
                {
                    rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    //Report
                }
            }
            else
            {
                DataTable dtResault = LSBIB.GetStudentIsargar(txt_Students.Text, int.Parse(ddl_Degree.SelectedValue.ToString()), int.Parse(ddl_Sex.SelectedValue.ToString()), int.Parse(ddl_Dorpar.SelectedValue.ToString()), int.Parse(ddl_Field.SelectedValue.ToString()), txt_SalVorod.Text, int.Parse(ddl_VaziatStu.SelectedValue.ToString()));
                if (dtResault.Rows.Count == 0)
                {
                    rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    //Report
                    img_ExportToExcel.Visible = true;
                    this.StiWebViewer1.ResetReport();
                    StiWebViewer1.Visible = true;
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportStudentsUnderWelfare.mrt"));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Students].[SP_Martyr]"].Parameters["@stcode"].ParameterValue = txt_Students.Text;
                    rpt.CompiledReport.DataSources["[Students].[SP_Martyr]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                    rpt.CompiledReport.DataSources["[Students].[SP_Martyr]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
                    rpt.CompiledReport.DataSources["[Students].[SP_Martyr]"].Parameters["@Education"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
                    rpt.CompiledReport.DataSources["[Students].[SP_Martyr]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                    rpt.CompiledReport.DataSources["[Students].[SP_Martyr]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
                    rpt.CompiledReport.DataSources["[Students].[SP_Martyr]"].Parameters["@StatusStu"].ParameterValue = int.Parse(ddl_VaziatStu.SelectedValue.ToString());
                    rpt.RegData(dtResault);
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                }
            }
            if (txt_SalVorod.Text == "0")
            {
                txt_SalVorod.Text = string.Empty;
            }
            if (txt_Students.Text == "0")
            {
                txt_Students.Text = string.Empty;
            }
            if (Session["stcode"] != null)
            {
                Session["stcode"] = null;
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_Students.Text == string.Empty)
            {
                txt_Students.Text = "0";
            }
            if (txt_SalVorod.Text == string.Empty)
            {
                txt_SalVorod.Text = "0";
            }
            DataTable dt = LSBIB.GetStudentIsargar(txt_Students.Text, int.Parse(ddl_Degree.SelectedValue.ToString()), int.Parse(ddl_Sex.SelectedValue.ToString()), int.Parse(ddl_Dorpar.SelectedValue.ToString()), int.Parse(ddl_Field.SelectedValue.ToString()), txt_SalVorod.Text, int.Parse(ddl_VaziatStu.SelectedValue.ToString()));
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                grd_Show1.DataSource = dt;
                grd_Show1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=StudentIsargar.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in grd_Show1.HeaderRow.Cells)
                    {
                        cell.BackColor = grd_Show1.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in grd_Show1.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grd_Show1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grd_Show1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    grd_Show1.RenderControl(hw);

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