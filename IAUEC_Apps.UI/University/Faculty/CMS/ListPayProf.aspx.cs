using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.Business.university.Faculty;

using IAUEC_Apps.DTO.University.Education;
using IAUEC_Apps.DTO.University.Faculty;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ListPayProf : System.Web.UI.Page
    {
        UniversityBusiness UB = new UniversityBusiness();
        public static FacultyDTO FD = new FacultyDTO();
        
        public static BarnameHaftegiDTO BHD = new BarnameHaftegiDTO();
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
                dtDaneshkade = CB.SelectAllDaneshkade();
                ddl_Daneshkade.DataTextField = "namedanesh";
                ddl_Daneshkade.DataValueField = "id";
                ddl_Daneshkade.DataSource = dtDaneshkade;
                ddl_Daneshkade.DataBind();
                ddl_Daneshkade.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;
                if (Request.QueryString["Daneshkade"] != null)
                {
                    ddl_Daneshkade.SelectedValue = Request.QueryString["Daneshkade"].ToString();

                }
                dtDepartman = CB.GetAllDepartman();
                ddl_Departman.DataTextField = "namegroup";
                ddl_Departman.DataValueField = "id";
                ddl_Departman.DataSource = dtDepartman;
                ddl_Departman.DataBind();
                ddl_Departman.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Departman.Items[ddl_Departman.Items.Count - 1].Selected = true;
                if (Request.QueryString["Departman"] != null)
                {
                    ddl_Departman.SelectedValue = Request.QueryString["Departman"].ToString();

                }
                dtField = ERB.SelectAllField();
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
                //dtCooperation = CB.GetAllTypeCooperation();
                //ddl_Cooperation.DataTextField = "name_nahveh";
                //ddl_Cooperation.DataValueField = "nahveh_hamk";
                //ddl_Cooperation.DataSource = dtCooperation;
                //ddl_Cooperation.DataBind();
                ddl_Cooperation.Items.Add(new ListItem("تمام وقت 32 ساعت", "1"));
                ddl_Cooperation.Items.Add(new ListItem("نیمه وقت", "2"));
                ddl_Cooperation.Items.Add(new ListItem("ساعتی-حق التدریس", "3"));
                ddl_Cooperation.Items.Add(new ListItem("تمام وقت طرح مشمولان", "4"));
                ddl_Cooperation.Items.Add(new ListItem("بورسیه دکترا", "5"));
                ddl_Cooperation.Items.Add(new ListItem("کارمند", "6"));
                ddl_Cooperation.Items.Add(new ListItem("تمام وقت 44 ساعت", "7"));
                ddl_Cooperation.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Cooperation.Items[ddl_Cooperation.Items.Count - 1].Selected = true;
                if (Request.QueryString["Cooperation"] != null)
                {
                    ddl_Cooperation.SelectedValue = Request.QueryString["Cooperation"].ToString();

                }
            }
            if (Session["code_ostad"] != null)
            {
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
            }
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            FD.Term = ddl_Term.SelectedValue.ToString();
        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            FD.Daneshkade = ddl_Daneshkade.SelectedValue;
            DataTable dt = new DataTable();
            if (ddl_Daneshkade.SelectedValue == "0")
            {
                dtDepartman = CB.GetAllDepartman();
                ddl_Departman.DataTextField = "namegroup";
                ddl_Departman.DataValueField = "id";
                ddl_Departman.DataSource = dtDepartman;
                ddl_Departman.DataBind();
                ddl_Departman.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Departman.Items[ddl_Departman.Items.Count - 1].Selected = true;
                FD.Departman = ddl_Departman.SelectedValue;
                dtField = ERB.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
                FD.Field = ddl_Field.SelectedValue;
            }
            else
            {
                //Session["Daneshkade"] = ddl_Daneshkade.SelectedValue;
                //FD.Daneshkade = ddl_Daneshkade.SelectedValue;
                dt = CB.GetAllDepartman(int.Parse(FD.Daneshkade));
                ddl_Departman.DataSource = dt;
                ddl_Departman.DataTextField = "namegroup";
                ddl_Departman.DataValueField = "id";
                ddl_Departman.DataBind();
                ddl_Departman.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Departman.Items[ddl_Departman.Items.Count - 1].Selected = true;
                //FD.Departman = ddl_Departman.SelectedValue;
                DataTable dtField = new DataTable();
                dtField = ERB.GetReshByDaneshkade(int.Parse(FD.Daneshkade));
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
                //FD.Field = ddl_Field.SelectedValue;
            }
        }
        //protected void ddl_Departman_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FD.Departman = ddl_Departman.SelectedValue.ToString();
        //}

        protected void ddl_Departman_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddl_Departman.SelectedValue == "0")
            {
                FD.Departman = ddl_Departman.SelectedValue.ToString();
                dtField = ERB.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
            }
            else
            {
                FD.Departman = ddl_Departman.SelectedValue.ToString();
                dtField=UB.GetFieldByDepartman(int.Parse(FD.Departman));
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
            }
        }

        protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            FD.Field = ddl_Field.SelectedValue.ToString();
        }

        protected void ddl_Cooperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            FD.Cooperation = ddl_Cooperation.SelectedValue.ToString();
        }

        protected void btn_SelectCodeOstad_Click(object sender, EventArgs e)
        {
            Session["page"] = 7;
            if (ddl_Daneshkade.SelectedValue == null)
            {
                ddl_Daneshkade.SelectedValue = "0";
            }
            if (ddl_Departman.SelectedValue == null)
            {
                ddl_Departman.SelectedValue = "0";
            }
            if (ddl_Cooperation.SelectedValue == null)
            {
                ddl_Cooperation.SelectedValue = "0";
            }
            if (ddl_Term.SelectedValue == null)
            {
                ddl_Term.SelectedValue = "0";
            }
            if (ddl_Field.SelectedValue == null)
            {
                ddl_Field.SelectedValue = "0";
            }
           
            //Response.Redirect("SearchProfByParams.aspx?" + "Term" + "=" + ddl_Term.SelectedValue + "&" + "Daneshkade" + "=" + ddl_Daneshkade.SelectedValue + "&" + "Departman" + "=" + ddl_Departman.SelectedValue + "&" + "Cooperation" + "=" + ddl_Cooperation.SelectedValue + "&" + "Field" + "=" + ddl_Field.SelectedValue + "&" + "Zarib" +"="+ txt_Zarib.Text);
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = ("../../../university/Faculty/CMS/SearchProfByParams.aspx?" + "Term" + "=" + ddl_Term.SelectedValue + "&" + "Daneshkade" + "=" + ddl_Daneshkade.SelectedValue + "&" + "Departman" + "=" + ddl_Departman.SelectedValue + "&" + "Cooperation" + "=" + ddl_Cooperation.SelectedValue + "&" + "Field" + "=" + ddl_Field.SelectedValue + "&" + "Zarib" + "=" + txt_Zarib.Text);
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

        protected void btn_ShowReport_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب نمایید", 0, 100, "پیام ", "");
            }
            else
            {

                if (ddl_Daneshkade.SelectedValue == null)
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                if (ddl_Departman.SelectedValue == null)
                {
                    ddl_Departman.SelectedValue = "0";
                }
                if (ddl_Cooperation.SelectedValue == null)
                {
                    ddl_Cooperation.SelectedValue = "0";
                }
                if (ddl_Term.SelectedValue == null)
                {
                    ddl_Term.SelectedValue = "0";
                }
                if (ddl_Field.SelectedValue == null)
                {
                    ddl_Field.SelectedValue = "0";
                }
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                }
                dtResault = FRB.GetListPayProf(ddl_Term.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue), int.Parse(txt_CodeOstad.Text), int.Parse(txt_Zarib.Text));
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
                    rpt.Load(Server.MapPath("../Report/ReportListPayProf.mrt"));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    StiOptions.Engine.ReportCache.AmountOfProcessedPagesForStartGCCollect = 20000; // default - 20
                    StiOptions.Engine.ReportCache.AmountOfQuickAccessPages = 10000; // default - 50
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ListPayTuition]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ListPayTuition]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ListPayTuition]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ListPayTuition]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_Departman.SelectedValue);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ListPayTuition]"].Parameters["@Cooperation"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ListPayTuition]"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ListPayTuition]"].Parameters["@Zarib"].ParameterValue = int.Parse(txt_Zarib.Text);
                    //((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_ConflictClassbyNumberClass]"]).CommandTimeout = 30000;
                    rpt.RegData(dtResault);
                    rpt.Render();
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                }
            }
           if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
           {
               txt_CodeOstad.Text = "";
               Session["code_ostad"] = null; ;
           }
           //txt_CodeOstad.Text = "";
           Session["code_ostad"] = null;
        }
        protected void RadAjaxManager1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_CodeOstad.Text == string.Empty)
            {
                txt_CodeOstad.Text = "0";
            }
            dtResault = FRB.GetListPayProf(ddl_Term.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue), int.Parse(txt_CodeOstad.Text), int.Parse(txt_Zarib.Text));
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView1.DataSource = dtResault;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportListPayProf.xls");
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