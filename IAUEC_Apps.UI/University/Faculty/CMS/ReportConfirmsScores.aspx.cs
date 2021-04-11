using IAUEC_Apps.Business.Common;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Faculty;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using System.IO;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ReportConfirmsScores : System.Web.UI.Page
    {
        
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        CommonBusiness CB = new CommonBusiness();
        System.Data.DataTable dtTerm = new System.Data.DataTable();
        //DataTable dtTerm = new DataTable();
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
                ddlDanesh.DataSource = CB.SelectAllDaneshkade();
                ddlDanesh.DataTextField = "namedanesh";
                ddlDanesh.DataValueField = "id";

                ddlDanesh.DataBind();
                ddlDanesh.Items.Insert(0,new ListItem("همه دانشکده ها", "0"));
            }
            StiWebViewer1.Visible = false;
            if (Session["code_ostad"] != null)
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
        }

        protected void btn_Select_Click(object sender, EventArgs e)
        {
            if (ddl_Term.SelectedValue == null)
            {
                ddl_Term.SelectedValue = "0";
            }
            Session["page"] = 8;
            //Response.Redirect("SearchProfByParams.aspx?"+"Term"+"="+ddl_Term.SelectedValue+"&"+"Lesson"+"="+ddl_CodeDras.SelectedValue+"&"+"Departman"+"="+ddl_CodeGroup.SelectedValue);
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = ("../../../university/Faculty/CMS/SearchProfByParams.aspx?" + "Term" + "=" + ddl_Term.SelectedValue + "&" + "did" + "=" +txt_did.Text );
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

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
           string Term = ddl_Term.SelectedValue;
        }

        protected void btn_ShowReport_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            if (ddl_Term.SelectedValue == "0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب کنید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_did.Text == string.Empty)
                {
                    txt_did.Text = "0";
                }
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                }
                System.Data.DataTable dt = FRB.GetAcceptMark(ddl_Term.SelectedValue, int.Parse(txt_did.Text), int.Parse(txt_CodeOstad.Text),Convert.ToInt32(ddlDanesh.SelectedValue));
                if (dt.Rows.Count == 0)
                {
                    RadWindowManager1.RadAlert("رکوردی وجود ندارد", 100, 0, "پیام", "");
                }
                else
                {
                    //Report
                    img_ExportToExcel1.Visible = true;
                    StiWebViewer1.Visible = true;
                    this.StiWebViewer1.ResetReport();
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportFinal.mrt")); 
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ReportAcceptMark]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ReportAcceptMark]"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ReportAcceptMark]"].Parameters["@Did"].ParameterValue = int.Parse(txt_did.Text);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ReportAcceptMark]"].Parameters["@daneshID"].ParameterValue = int.Parse(ddlDanesh.SelectedValue);
                    rpt.RegData(dt);
                    //rpt.Dictionary.Synchronize();
                    //rpt.Show();
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                }
                if (txt_did.Text == "0")
                {
                    txt_did.Text = string.Empty;
                }
                if (txt_CodeOstad.Text == "0")
                {
                    txt_CodeOstad.Text = string.Empty;
                }
            }
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
            if (txt_CodeOstad.Text == string.Empty || txt_CodeOstad.Text == "")
            {
                txt_CodeOstad.Text = "0";
                //Session["stcode"] = txt_CodeOstad.Text;
            }
            if (txt_did.Text == string.Empty)
            {
                txt_did.Text = "0";
            }
            System.Data.DataTable dt2 = FRB.GetAcceptMark(ddl_Term.SelectedValue, int.Parse(txt_did.Text), int.Parse(txt_CodeOstad.Text),Convert.ToInt32(ddlDanesh.SelectedValue));
            if (dt2.Rows.Count == 0)
            {
                
            }
            else
            {
                GridView1.DataSource = dt2;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportConfirmScores.xls");
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