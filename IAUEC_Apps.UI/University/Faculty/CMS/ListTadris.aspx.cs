using IAUEC_Apps.DTO.University.Faculty;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.Business.Common;
using Stimulsoft.Report;

using Stimulsoft.Report.Dictionary;
using Telerik.Web.UI;
using System.IO;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ListTadris : System.Web.UI.Page
    {
        
        DataTable dtTerm = new DataTable();
        DataTable dtDepartman = new DataTable();
        DataTable dtDaneshkade = new DataTable();
        DataTable dtCooperation = new DataTable();
        CommonBusiness CB = new CommonBusiness();
        EducationReportBusiness ERB = new EducationReportBusiness();
        public static ListTadrisDTO LTD = new ListTadrisDTO();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
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
                if (Request.QueryString["Term"] != null)
                {
                    ddl_Term.SelectedValue = Request.QueryString["Term"].ToString();

                }
                int iddanesh = 0;
                if (Session["RoleID"].ToString() == "51" || Session["RoleID"].ToString() == "17")
                    iddanesh = 1;
                if (Session["RoleID"].ToString() == "52" || Session["RoleID"].ToString() == "16")
                    iddanesh = 3;
                if (Session["RoleID"].ToString() == "53" || Session["RoleID"].ToString() == "15")
                    iddanesh = 2;
                 
                        
                dtDepartman = FRB.GetAllGroup(iddanesh);
                ddl_CodeGroup.DataTextField = "namegroup";
                ddl_CodeGroup.DataValueField = "id";
                ddl_CodeGroup.DataSource = dtDepartman;
                ddl_CodeGroup.DataBind();
                ddl_CodeGroup.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_CodeGroup.Items[ddl_CodeGroup.Items.Count - 1].Selected = true;
                if (Request.QueryString["Departman"] != null)
                {
                    ddl_CodeGroup.SelectedValue = Request.QueryString["Departman"].ToString();

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
                //dtCooperation = CB.GetAllTypeCooperation();
                //ddl_Cooperation.DataTextField = "name_nahveh";
                //ddl_Cooperation.DataValueField = "nahveh_hamk";
                //ddl_Cooperation.DataSource = dtCooperation;
                //ddl_Cooperation.DataBind();
                //ddl_Cooperation.Items.Add(new ListItem("انتخاب کنید", "2"));
                //ddl_Cooperation.Items[ddl_Cooperation.Items.Count - 1].Selected = true;
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
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            LTD.Term=ddl_Term.SelectedValue;
        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            LTD.Daneshkade = ddl_Daneshkade.SelectedValue;
            DataTable dt = new DataTable();
            if (ddl_Daneshkade.SelectedValue == "0")
            {
                LTD.Daneshkade = ddl_Daneshkade.SelectedValue;
                dtDepartman = CB.GetAllDepartman();
                ddl_CodeGroup.DataTextField = "namegroup";
                ddl_CodeGroup.DataValueField = "id";
                ddl_CodeGroup.DataSource = dtDepartman;
                ddl_CodeGroup.DataBind();
                ddl_CodeGroup.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_CodeGroup.Items[ddl_CodeGroup.Items.Count - 1].Selected = true;
                LTD.Departman = ddl_CodeGroup.SelectedValue;
            }
            else
            {
                //Session["Daneshkade"] = ddl_Daneshkade.SelectedValue;
                LTD.Daneshkade = ddl_Daneshkade.SelectedValue;
                dt = CB.GetAllDepartman(int.Parse(LTD.Daneshkade));
                ddl_CodeGroup.DataSource = dt;
                ddl_CodeGroup.DataTextField = "namegroup";
                ddl_CodeGroup.DataValueField = "id";
                ddl_CodeGroup.DataBind();
                ddl_CodeGroup.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_CodeGroup.Items[ddl_CodeGroup.Items.Count - 1].Selected = true;
            }
        }

        protected void btn_Select_Click(object sender, EventArgs e)
        {
                if (ddl_Term.SelectedValue == null)
                {
                    ddl_Term.SelectedValue = "0";
                }
                if (ddl_CodeGroup.SelectedValue ==null)
                {
                    ddl_CodeGroup.SelectedValue = "0";
                }
                if (ddl_Cooperation.SelectedValue == null)
                {
                    ddl_Cooperation.SelectedValue = "0";
                }
                if (ddl_Daneshkade.SelectedValue == null)
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                
            Session["page"] = 6;
            //Response.Redirect("SearchProfByParams.aspx?"+"Term"+"="+ddl_Term.SelectedValue+"&"+"Departman"+"="+ddl_CodeGroup.SelectedValue+"&"+"Cooperation"+"="+ddl_Cooperation.SelectedValue+"&"+"Daneshkade"+"="+ddl_Daneshkade.SelectedValue);
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = ("../../../university/Faculty/CMS/SearchProfByParams.aspx?" + "Term" + "=" + ddl_Term.SelectedValue + "&" + "Departman" + "=" + ddl_CodeGroup.SelectedValue + "&" + "Cooperation" + "=" + ddl_Cooperation.SelectedValue + "&" + "Daneshkade" + "=" + ddl_Daneshkade.SelectedValue);
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

        //protected void btn_Exit_Click(object sender, EventArgs e)
        //{
        //    Session["code_ostad"]=null;
        //    Response.Redirect("FacultyReports.aspx");
        //}
        protected void btn_ShowList_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue=="0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (ddl_Daneshkade.SelectedValue == null)
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                    LTD.CodeOstad = txt_CodeOstad.Text;
                }
                else
                {
                    LTD.CodeOstad = txt_CodeOstad.Text;
                }
                if (txt_CodeOstad.Text == null || txt_CodeOstad.Text=="")
                {
                    txt_CodeOstad.Text = "0";
                }
                if (ddl_CodeGroup.SelectedValue ==null)
                {
                    ddl_CodeGroup.SelectedValue = "0";
                }
                if (ddl_Cooperation.SelectedValue == null)
                {
                    ddl_Cooperation.SelectedValue = "0";
                }
                if (rdb_Tuition.Checked == true)
                {
                    DataTable dt = FRB.GetListTuition(ddl_Term.SelectedValue, int.Parse(ddl_Cooperation.SelectedValue), int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(txt_CodeOstad.Text));
                    if (dt.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                    }
                    else
                    {
                        img_ExportToExcel1.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportTuition.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["Faculty.SP_Tuition"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["Faculty.SP_Tuition"].Parameters["@Cooperation"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                        rpt.CompiledReport.DataSources["Faculty.SP_Tuition"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_CodeGroup.SelectedValue);
                        rpt.CompiledReport.DataSources["Faculty.SP_Tuition"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                        rpt.CompiledReport.DataSources["Faculty.SP_Tuition"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                        rpt.RegData(dt);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }
                }
                else
                {
                    DataTable dt = FRB.GetListTuition2(ddl_Term.SelectedValue, int.Parse(ddl_Cooperation.SelectedValue), int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(txt_CodeOstad.Text));
                    if (dt.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                    }
                    else
                    {
                        img_ExportToExcel1.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportListTaition.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Faculty].[SP_ListTuition]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_ListTuition]"].Parameters["@Cooperation"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_ListTuition]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_CodeGroup.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_ListTuition]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_ListTuition]"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                        rpt.RegData(dt);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
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
        }

        protected void ddl_CodeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LTD.Departman = ddl_CodeGroup.SelectedValue;
        }

        protected void ddl_Cooperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            LTD.Cooperation = ddl_Cooperation.SelectedValue;
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
            if (txt_CodeOstad.Text == null || txt_CodeOstad.Text == "")
            {
                txt_CodeOstad.Text = "0";
            }
            DataTable dt = new DataTable();
            if (rdb_Tuition.Checked == true)
            {
                dt = FRB.GetListTuition(ddl_Term.SelectedValue, int.Parse(ddl_Cooperation.SelectedValue), int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(txt_CodeOstad.Text));
                if (dt.Rows.Count > 0)
                {

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
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
                        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                dt = FRB.GetListTuition2(ddl_Term.SelectedValue, int.Parse(ddl_Cooperation.SelectedValue), int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(txt_CodeOstad.Text));
                if (dt.Rows.Count > 0)
                {

                    GridView2.DataSource = dt;
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
                            cell.BackColor = GridView1.HeaderStyle.BackColor;
                        }
                        foreach (GridViewRow row in GridView2.Rows)
                        {
                            int c = 0;
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
                                if (c == 15)
                                    cell.CssClass = "textmode";
                                else
                                    cell.CssClass = "textmode";
                            }
                        }

                        GridView2.RenderControl(hw);

                        //style to format numbers to string
                        string style = @"<style> .textmode { }
.num {
  mso-number-format:\#\,\#\#0\.00;}</style>";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            
        }
        
    }
}