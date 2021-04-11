using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.DTO.University.Faculty;
using Stimulsoft.Report;
using IAUEC_Apps.Business.Common;
using Stimulsoft.Report.Dictionary;

using Telerik.Web.UI;
using System.IO;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ListTadakholKlass : System.Web.UI.Page
    {
        
        DataTable dtTerm = new DataTable();
        DataTable dtNumClass = new DataTable();
        DataTable dtResault = new DataTable();
        DataTable dtLesson = new DataTable();
        EducationReportBusiness ERB = new EducationReportBusiness();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        CommonBusiness CB = new CommonBusiness();
        static ListTadakholKlassDTO LTKD = new ListTadakholKlassDTO();
        int Sort;
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
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;
                if (Request.QueryString["Term"]!= null)
                {
                    ddl_Term.SelectedValue = Request.QueryString["Term"].ToString();

                }
                dtNumClass = FRB.GetNumberClass();
                ddl_NumberClass.DataTextField = "shom_klas";
                ddl_NumberClass.DataValueField = "id";
                ddl_NumberClass.DataSource = dtNumClass;
                ddl_NumberClass.DataBind();
                ddl_NumberClass.Items.Add(new ListItem("انتخاب کنید", "."));
                ddl_NumberClass.Items[ddl_NumberClass.Items.Count - 1].Selected = true;
                if (Request.QueryString["NumberClass"] != null)
                {
                    ddl_NumberClass.SelectedValue = Request.QueryString["NumberClass"].ToString();
                }
                //dtLesson = CB.GetLesson();
                //ddl_Lesson.DataTextField = "namedars";
                //ddl_Lesson.DataValueField = "dcode";
                //ddl_Lesson.DataSource = dtLesson;
                //ddl_Lesson.DataBind();
                //ddl_Lesson.Items.Add(new ListItem("انتخاب کنید", "."));
                //ddl_Lesson.Items[ddl_Lesson.Items.Count - 1].Selected = true;
                ddl_Day.Items.Add(new ListItem("شنبه", "1"));
                ddl_Day.Items.Add(new ListItem("یکشنبه", "2"));
                ddl_Day.Items.Add(new ListItem("دوشنبه", "3"));
                ddl_Day.Items.Add(new ListItem("سه شنبه", "4"));
                ddl_Day.Items.Add(new ListItem("چهارشنبه", "5"));
                ddl_Day.Items.Add(new ListItem("پنج شنبه", "6"));
                ddl_Day.Items.Add(new ListItem("جمعه", "7"));
                ddl_Day.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Day.Items[ddl_Day.Items.Count - 1].Selected = true;
            }

            if (Session["code_ostad"] != null)
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
        }

        protected void btn_CodeOstad_Click(object sender, EventArgs e)
        {
            if (ddl_NumberClass.SelectedValue == null)
            {
                ddl_NumberClass.SelectedValue = "0";
            }
            if (ddl_Term.SelectedValue == null)
            {
                ddl_Term.SelectedValue = "0";
            }
            Session["page"] = 3;
            //Response.Redirect("SearchProfByParams.aspx?" + "Term" + "=" + ddl_Term.SelectedValue + "&" + "NumberClass" + "=" + ddl_NumberClass + "&" + "Day" + "=" + ddl_Day.SelectedValue);
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = ("../../../university/Faculty/CMS/SearchProfByParams.aspx?" + "Term" + "=" + ddl_Term.SelectedValue + "&" + "NumberClass" + "=" + ddl_NumberClass + "&" + "Day" + "=" + ddl_Day.SelectedValue);
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code 
            widnow1.Height = 600;
            widnow1.Width = 1200;
            windowManager.Windows.Add(widnow1);
            windowManager.Height = 600;
            windowManager.Width = 1200;
            ContentPlaceHolder mpContentPlaceHolder;
            mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            mpContentPlaceHolder.Controls.Add(widnow1); 
        }

        protected void btn_ShowList_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            if (ddl_Term.SelectedValue == null)
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب کنید", 0, 100, "پیام", "");
            }
            if (ddl_Day.SelectedValue==null || ddl_Day.SelectedValue=="0")
            {
                RadWindowManager1.RadAlert("لطفا روز را انتخاب کنید", 0, 100, "پیام", ""); 
            }
            if (ddl_NumberClass.SelectedValue == null || ddl_NumberClass.SelectedValue == ".")
            {
                RadWindowManager1.RadAlert("لطفا شماره کلاس را انتخاب کنید", 0, 100, "پیام", "");
            }
            else
            {

                if (txt_CodeOstad.Text == string.Empty || txt_CodeOstad.Text=="")
                {
                    txt_CodeOstad.Text = "0";
                }
                else
                {
                    LTKD.CodeOstad = txt_CodeOstad.Text;
                }

                if (rdb_ListByOstad.Checked == true)
                {
                    Sort = 0;
                    dtResault = FRB.GetConflictClassByCodeOstad(ddl_Term.SelectedValue, int.Parse(txt_CodeOstad.Text), Sort, int.Parse(ddl_Day.SelectedValue), int.Parse(ddl_NumberClass.SelectedValue));
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
                        rpt.Load(Server.MapPath("../Report/ReportConflictClassbyCodeOstad.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Faculty].[SP_ConflictClassbyCodeOstad]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_ConflictClassbyCodeOstad]"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_ConflictClassbyCodeOstad]"].Parameters["@Sort"].ParameterValue = Sort;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_ConflictClassbyCodeOstad]"].Parameters["@Day"].ParameterValue = int.Parse(ddl_Day.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_ConflictClassbyCodeOstad]"].Parameters["@NumberClass"].ParameterValue = (ddl_NumberClass.SelectedValue);
                        ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_ConflictClassbyCodeOstad]"]).CommandTimeout = 30000;
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }
                    if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
                    {
                        txt_CodeOstad.Text = "";
                        Session["code_ostad"] = null; ;
                    }
                    txt_CodeOstad.Text = "";
                    Session["code_ostad"] = null;
                }
                if (rdb_ListByNumberClass.Checked == true)
                {
                    Sort = 2;
                    if (ddl_Day.SelectedValue == null || ddl_Day.SelectedValue == "")
                    {
                        RadWindowManager1.RadAlert("لطفا روز را انتخاب کنید", 0, 100, "پیام", "");
                    }
                    else
                    {
                        dtResault = FRB.GetConflictClassByNumberClass(ddl_Term.SelectedValue, ddl_NumberClass.SelectedValue, Sort, int.Parse(ddl_Day.SelectedValue));
                        if (dtResault.Rows.Count == 0)
                        {
                            RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                        }
                        else
                        {
                            img_ExportToExcel2.Visible = true;
                            this.StiWebViewer1.ResetReport();
                            StiWebViewer1.Visible = true;
                            StiReport rpt = new StiReport();
                            rpt.Load(Server.MapPath("../Report/ReportConflictClassbyNumberClass.mrt"));
                            rpt.ReportCacheMode = StiReportCacheMode.On;
                            StiOptions.Engine.ReportCache.AmountOfProcessedPagesForStartGCCollect = 20000; // default - 20
                            StiOptions.Engine.ReportCache.AmountOfQuickAccessPages = 10000; // default - 50
                            rpt.Dictionary.Databases.Clear();
                            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                            rpt.Compile();
                            rpt.CompiledReport.DataSources["[Faculty].[SP_ConflictClassbyNumberClass]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                            rpt.CompiledReport.DataSources["[Faculty].[SP_ConflictClassbyNumberClass]"].Parameters["@NumberClass"].ParameterValue = ddl_NumberClass.SelectedValue;
                            rpt.CompiledReport.DataSources["[Faculty].[SP_ConflictClassbyNumberClass]"].Parameters["@Sort"].ParameterValue = Sort;
                            rpt.CompiledReport.DataSources["[Faculty].[SP_ConflictClassbyNumberClass]"].Parameters["@Day"].ParameterValue = int.Parse(ddl_Day.SelectedValue);
                            ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_ConflictClassbyNumberClass]"]).CommandTimeout = 30000;
                            rpt.RegData(dtResault);
                            rpt.Render();
                            StiWebViewer1.Report = rpt;
                            StiWebViewer1.Visible = true;
                        }
                        if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
                        {
                            txt_CodeOstad.Text = "";
                            Session["code_ostad"] = null; ;
                        }
                        txt_CodeOstad.Text = "";
                        Session["code_ostad"] = null;
                    }
                }
            }
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            LTKD.Term = ddl_Term.SelectedValue;
        }
        protected void ddl_NumberClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LTKD.NumberClass = ddl_NumberClass.SelectedValue;
        }

        protected void ddl_Day_SelectedIndexChanged(object sender, EventArgs e)
        {
            LTKD.Day = ddl_Day.SelectedValue;
        }
        protected void RadAjaxManager1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
            }
        }

        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {

            if (txt_CodeOstad.Text == string.Empty || txt_CodeOstad.Text == "")
            {
                txt_CodeOstad.Text = "0";
            }
            Sort = 0;
            dtResault = FRB.GetConflictClassByCodeOstad(ddl_Term.SelectedValue, int.Parse(txt_CodeOstad.Text), Sort, int.Parse(ddl_Day.SelectedValue), Convert.ToInt32(ddl_NumberClass.SelectedValue));
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView1.DataSource = dtResault;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportConflictClassbyCodeOstad.xls");
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void img_ExportToExcel2_Click(object sender, ImageClickEventArgs e)
        {
            Sort = 2;
            dtResault = FRB.GetConflictClassByNumberClass(ddl_Term.SelectedValue, ddl_NumberClass.SelectedValue, Sort, int.Parse(ddl_Day.SelectedValue));
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView2.DataSource = dtResault;
                GridView2.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportConflictClassbyNumberClass.xls");
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
      } 
   }