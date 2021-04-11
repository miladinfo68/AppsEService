using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Faculty;
using Stimulsoft.Report;

using Stimulsoft.Report.Dictionary;
using Telerik.Web.UI;
using System.IO;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ListHoursAndAbsencesOfCountervailing : System.Web.UI.Page
    {
        
        DataTable dt = new DataTable();
        DataTable dtTerm = new DataTable();
        DataTable dtGroup = new DataTable();
        DataTable dtDaneshkade = new DataTable();
        EducationReportBusiness ERB = new EducationReportBusiness();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        public static ListHoursAndAbsenceDTO LAD = new ListHoursAndAbsenceDTO();
        CommonBusiness CB = new CommonBusiness();
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
                int iddanesh = 0;
                if (Session["RoleID"].ToString() == "51" || Session["RoleID"].ToString() == "17")
                    iddanesh = 1;
                if (Session["RoleID"].ToString() == "52" || Session["RoleID"].ToString() == "16")
                    iddanesh = 3;
                if (Session["RoleID"].ToString() == "53" || Session["RoleID"].ToString() == "15")
                    iddanesh = 2;
                 
                        
                dtGroup = FRB.GetAllGroup(iddanesh);
                ddl_GroupOstad.DataTextField = "namegroup";
                ddl_GroupOstad.DataValueField = "id";
                ddl_GroupOstad.DataSource = dtGroup;
                ddl_GroupOstad.DataBind();
                ddl_GroupOstad.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_GroupOstad.Items[ddl_GroupOstad.Items.Count - 1].Selected = true;
                if (Request.QueryString["Departman"]!= null)
                {
                    ddl_GroupOstad.SelectedValue = Request.QueryString["Departman"].ToString();
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
                if (Request.QueryString["AzD"] != null)
                {
                    txt_FromDate.Text = Request.QueryString["AzD"].ToString();
                }
                if (Request.QueryString["TaD"] != null)
                {
                    txt_ToDate.Text = Request.QueryString["TaD"].ToString();
                }
                if (Request.QueryString["AzJ"] != null)
                {
                    txt_AzJobrani.Text = Request.QueryString["AzJ"].ToString();
                }
                if (Request.QueryString["TaJ"] != null)
                {
                    txt_ToJobrani.Text = Request.QueryString["TaJ"].ToString();
                }
                if (Request.QueryString["NumberAbsence"] != null)
                {
                    txt_NumberAbsence.Text = Request.QueryString["NumberAbsence"].ToString();
                }
            }

            if (Session["code_ostad"] != null)
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
        }

        protected void btn_Select_Click(object sender, EventArgs e)
        {
            if (ddl_Term.SelectedValue == null)
            {
                ddl_Term.SelectedValue = "0";
            }
            if (ddl_GroupOstad.SelectedValue == null)
            {
                ddl_GroupOstad.SelectedValue = "0";
            }
            if (ddl_Daneshkade.SelectedValue == null)
            {
                ddl_Daneshkade.SelectedValue = "0";
            }
            if (txt_AzJobrani.Text == string.Empty)
            {
                txt_AzJobrani.Text = "0";
            }
            if (txt_ToJobrani.Text == string.Empty)
            {
                txt_ToJobrani.Text = "0";
            }
            if (txt_FromDate.Text==string.Empty)
            {
                txt_FromDate.Text = "0";
            }
            if (txt_ToDate.Text == string.Empty)
            {
                txt_ToDate.Text = "0";
            }
            if (txt_NumberAbsence.Text == string.Empty)
            {
                txt_NumberAbsence.Text = "0";
            }
            
            Session["page"] = 5;
            //Response.Redirect("SearchProfByParams.aspx?"+"Term"+"="+ddl_Term.SelectedValue+"&"+"Departman"+"="+ddl_GroupOstad.SelectedValue  +"&"+"Daneshkade"+"="+ ddl_Daneshkade.SelectedValue +"&"+"AzJ"+"="+txt_AzJobrani.Text+"&"+"TaJ"+"="+txt_ToJobrani.Text+"&"+"AzD"+"="+txt_FromDate.Text+"&"+"TaD"+"="+txt_ToDate.Text+"&"+"NumberAbsence"+"="+txt_NumberAbsence.Text);
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = ("../../../university/Faculty/CMS/SearchProfByParams.aspx?" + "Term" + "=" + ddl_Term.SelectedValue + "&" + "Departman" + "=" + ddl_GroupOstad.SelectedValue + "&" + "Daneshkade" + "=" + ddl_Daneshkade.SelectedValue + "&" + "AzJ" + "=" + txt_AzJobrani.Text + "&" + "TaJ" + "=" + txt_ToJobrani.Text + "&" + "AzD" + "=" + txt_FromDate.Text + "&" + "TaD" + "=" + txt_ToDate.Text + "&" + "NumberAbsence" + "=" + txt_NumberAbsence.Text);
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
            LAD.Term = ddl_Term.SelectedValue;
        }

        protected void ddl_GroupOstad_SelectedIndexChanged(object sender, EventArgs e)
        {
            LAD.Departman = ddl_GroupOstad.SelectedValue;
        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (ddl_Daneshkade.SelectedValue == "0")
            {
                LAD.Daneshkade = ddl_Daneshkade.SelectedValue;
                DataTable dtDepartman = CB.GetAllDepartman();
                ddl_GroupOstad.DataTextField = "namegroup";
                ddl_GroupOstad.DataValueField = "id";
                ddl_GroupOstad.DataSource = dtDepartman;
                ddl_GroupOstad.DataBind();
                ddl_GroupOstad.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_GroupOstad.Items[ddl_GroupOstad.Items.Count - 1].Selected = true;
                LAD.Departman = ddl_GroupOstad.SelectedValue;
            }
            else
            {
                //Session["Daneshkade"] = ddl_Daneshkade.SelectedValue;
                LAD.Daneshkade = ddl_Daneshkade.SelectedValue;
                dt = CB.GetAllDepartman(int.Parse(LAD.Daneshkade));
                ddl_GroupOstad.DataSource = dt;
                ddl_GroupOstad.DataTextField = "namegroup";
                ddl_GroupOstad.DataValueField = "id";
                ddl_GroupOstad.DataBind();
                ddl_GroupOstad.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_GroupOstad.Items[ddl_GroupOstad.Items.Count - 1].Selected = true;
            }
        }

        protected void btn_ShowList_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            if (ddl_Term.SelectedValue==null || ddl_Term.SelectedValue=="0")
            {
                RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
            }
            else
            {
                LAD.CodeOstad = txt_CodeOstad.Text;
                LAD.FromDate=  txt_FromDate.Text;
                LAD.ToDate = txt_ToDate.Text;
                LAD.AzJobrani = txt_AzJobrani.Text;
                LAD.TaJobrani = txt_ToJobrani.Text;
                if (txt_AzJobrani.Text == "")
                {
                    txt_AzJobrani.Text = "  /  /  ";
                }
                if (txt_ToJobrani.Text == "")
                {
                    txt_ToJobrani.Text = "  /  /  ";
                }
                if (txt_FromDate.Text == "")
                {
                    txt_FromDate.Text = "  /  /  ";
                }
                if (txt_ToDate.Text == "")
                {
                    txt_ToDate.Text = "  /  /  ";
                }
                if ( ddl_GroupOstad.SelectedValue == null)
                {
                    ddl_GroupOstad.SelectedValue = "0";
                }
                if (ddl_Daneshkade.SelectedValue == null)
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                if (txt_AzJobrani.Text == string.Empty)
                {
                    txt_AzJobrani.Text = "0";
                }
                if (txt_ToJobrani.Text == string.Empty)
                {
                    txt_ToJobrani.Text = "0";
                }
                if (txt_FromDate.Text == string.Empty)
                {
                    txt_FromDate.Text = "0";
                }
                if (txt_FromDate.Text == string.Empty)
                {
                    txt_FromDate.Text = "0";
                }
                if (txt_NumberAbsence.Text == string.Empty)
                {
                    txt_NumberAbsence.Text = "0";
                }
                if (txt_CodeOstad.Text == null || txt_CodeOstad.Text == "")
                {
                    txt_CodeOstad.Text = "0";
                }
                if (rdb_YesAbsencesNoCountervailing.Checked==true)
                {
                    if ((txt_FromDate.Text == "  /  /  ") || (txt_ToDate.Text == "  /  /  "))
                    {
                        RadWindowManager1.RadAlert("لطفا از تاریخ ، تا تاریخ را پر کنید", 0, 100, "پیام", "");
                        
                    }
                    else
                    {
                        DataTable dtResault = FRB.GetAbsenceButNoCompensationProf(ddl_Term.SelectedValue, int.Parse(ddl_GroupOstad.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(txt_CodeOstad.Text), txt_FromDate.Text, txt_ToDate.Text, int.Parse(txt_NumberAbsence.Text));
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
                            rpt.Load(Server.MapPath("../Report/ReportAbsenceButNoCompensation.mrt"));
                            rpt.ReportCacheMode = StiReportCacheMode.On;
                            rpt.Dictionary.Databases.Clear();
                            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                            rpt.Compile();
                            rpt.CompiledReport.DataSources["[Faculty].[SP_AbsenceButNoCompensation]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                            rpt.CompiledReport.DataSources["[Faculty].[SP_AbsenceButNoCompensation]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_GroupOstad.SelectedValue);
                            rpt.CompiledReport.DataSources["[Faculty].[SP_AbsenceButNoCompensation]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                            rpt.CompiledReport.DataSources["[Faculty].[SP_AbsenceButNoCompensation]"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                            rpt.CompiledReport.DataSources["[Faculty].[SP_AbsenceButNoCompensation]"].Parameters["@FromDate"].ParameterValue = txt_FromDate.Text;
                            rpt.CompiledReport.DataSources["[Faculty].[SP_AbsenceButNoCompensation]"].Parameters["@ToDate"].ParameterValue = txt_ToDate.Text;
                            rpt.CompiledReport.DataSources["[Faculty].[SP_AbsenceButNoCompensation]"].Parameters["@CountAbsence"].ParameterValue = int.Parse(txt_NumberAbsence.Text);
                            rpt.RegData(dtResault);
                            rpt.Dictionary.Synchronize();
                            //rpt.Show();
                            StiWebViewer1.Report = rpt;
                            StiWebViewer1.Visible = true;
                            //rpt.Print(true);
                        }
                    }
                }
                if (rdb_YesAbsenceYesCountervailing.Checked==true)
                {
                    if (txt_FromDate.Text == "  /  /  " || txt_ToDate.Text == "  /  /  " )
                    {
                        RadWindowManager1.RadAlert("لطفا از تاریخ تا تاریخ  پر کنید", 0, 100, "پیام", "");
                        
                    }
                    if (txt_AzJobrani.Text == "  /  /  " || txt_ToJobrani.Text == "  /  /  ")
                    {
                        RadWindowManager1.RadAlert("لطفا از تاریخ جبرانی تا تاریخ جبرانی را پر کنید", 0, 100, "پیام", "");
                    }
                    else
                    {
                        DataTable dtResault = FRB.GetAbsenceAndCompensationProf(ddl_Term.SelectedValue, int.Parse(ddl_GroupOstad.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(txt_CodeOstad.Text), txt_FromDate.Text, txt_ToDate.Text, int.Parse(txt_NumberAbsence.Text), txt_AzJobrani.Text, txt_ToJobrani.Text);
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
                            rpt.Load(Server.MapPath("../Report/ReportAbsenceAndCompensation.mrt"));
                            rpt.ReportCacheMode = StiReportCacheMode.On;
                            rpt.Dictionary.Databases.Clear();
                            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                            rpt.Compile();
                            rpt.CompiledReport.DataSources["[Faculty].[Sp_AbsenceAndCompensation]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                            rpt.CompiledReport.DataSources["[Faculty].[Sp_AbsenceAndCompensation]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_GroupOstad.SelectedValue);
                            rpt.CompiledReport.DataSources["[Faculty].[Sp_AbsenceAndCompensation]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                            rpt.CompiledReport.DataSources["[Faculty].[Sp_AbsenceAndCompensation]"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                            rpt.CompiledReport.DataSources["[Faculty].[Sp_AbsenceAndCompensation]"].Parameters["@AzJobrani"].ParameterValue = txt_AzJobrani.Text;
                            rpt.CompiledReport.DataSources["[Faculty].[Sp_AbsenceAndCompensation]"].Parameters["@TaJobrani"].ParameterValue = txt_ToJobrani.Text;
                            rpt.CompiledReport.DataSources["[Faculty].[Sp_AbsenceAndCompensation]"].Parameters["@FromDate"].ParameterValue = txt_FromDate.Text;
                            rpt.CompiledReport.DataSources["[Faculty].[Sp_AbsenceAndCompensation]"].Parameters["@ToDate"].ParameterValue = txt_ToDate.Text;
                            rpt.CompiledReport.DataSources["[Faculty].[Sp_AbsenceAndCompensation]"].Parameters["@CountAbsence"].ParameterValue = int.Parse(txt_NumberAbsence.Text);
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
            DataTable dtResault = FRB.GetAbsenceButNoCompensationProf(ddl_Term.SelectedValue, int.Parse(ddl_GroupOstad.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(txt_CodeOstad.Text), txt_FromDate.Text, txt_ToDate.Text, int.Parse(txt_NumberAbsence.Text));
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView1.DataSource = dtResault;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportAbsenceButNoCompensation.xls");
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

        protected void img_ExportToExcel2_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtResault = FRB.GetAbsenceAndCompensationProf(ddl_Term.SelectedValue, int.Parse(ddl_GroupOstad.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(txt_CodeOstad.Text), txt_FromDate.Text, txt_ToDate.Text, int.Parse(txt_NumberAbsence.Text), txt_AzJobrani.Text, txt_ToJobrani.Text);
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView2.DataSource = dtResault;
                GridView2.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportAbsenceAndCompensation.xls");
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