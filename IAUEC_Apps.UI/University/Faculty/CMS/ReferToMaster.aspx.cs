using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.DTO.University.Faculty;
using IAUEC_Apps.Business.university.Faculty;
using Stimulsoft.Report;
using IAUEC_Apps.Business.Common;
using Stimulsoft.Report.Dictionary;
using Telerik.Web.UI;
using System.IO;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ReferToMaster : System.Web.UI.Page
    {
        //CommonDAO dao = new CommonDAO();
        EducationReportBusiness ERB = new EducationReportBusiness();
        DataTable dtDaneshkade = new DataTable();
        DataTable dtTerm = new DataTable();
        CommonBusiness CB = new CommonBusiness();
        public static ReferToMasterDTO RMD = new ReferToMasterDTO();
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
                if (Request.QueryString["FromDate"] != null)
                {
                    txt_FromDate.Text = Request.QueryString["FromDate"].ToString();

                }
                if (Request.QueryString["ToDate"] != null)
                {
                    txt_ToDate.Text = Request.QueryString["ToDate"].ToString();

                }
            }
            if (Session["code_ostad"] != null)
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
        }

        protected void btn_Select_Click(object sender, EventArgs e)
        {
            if (txt_FromDate.Text == "")
            {
                txt_FromDate.Text = "13  /  /  ";
            }
            if (txt_ToDate.Text == "")
            {
                txt_ToDate.Text = "13  /  /  ";
            }
            RMD.FromDate = txt_FromDate.Text;
            RMD.ToDate = txt_ToDate.Text;
            if (ddl_Term.SelectedValue == null)
            {
                ddl_Term.SelectedValue = "0";
            }
            if (txt_ToDate.Text == string.Empty)
            {
                txt_ToDate.Text = "0";
            }
            if (txt_FromDate.Text == string.Empty)
            {
                txt_FromDate.Text = "0";
            }
            Session["page"] = 4;
            //Response.Redirect("SearchProfByParams.aspx?" + "Term" + "=" + RMD.Term + "&" + "ToDate" + "=" + txt_ToDate.Text + "&" + "FromDate" + "=" + txt_FromDate.Text);
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = ("../../../university/Faculty/CMS/SearchProfByParams.aspx?" + "Term" + "=" + ddl_Term.SelectedValue + "&" + "ToDate" + "=" + txt_ToDate.Text + "&" + "FromDate" + "=" + txt_FromDate.Text);
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
            RMD.FromDate= txt_FromDate.Text;
            RMD.ToDate = txt_ToDate.Text;
            if (txt_FromDate.Text == "  /  /  " && txt_ToDate.Text == "  /  /  ")
            {
                RadWindowManager1.RadAlert("لطفا تاریخ را وارد نمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (ddl_Term.SelectedValue == null || chk_Term.Checked==true)
                {
                    ddl_Term.SelectedValue = "0";
                }
                if (txt_CodeOstad.Text == "")
               {
                   txt_CodeOstad.Text = "0";
               }
                DataTable dtResault = FRB.ReferToProf(ddl_Term.SelectedValue, txt_CodeOstad.Text, txt_FromDate.Text, txt_ToDate.Text);
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
                   rpt.Load(Server.MapPath("../Report/ReportReferToMaster.mrt"));
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.Dictionary.Databases.Clear();
                   rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                   rpt.Compile();
                   rpt.CompiledReport.DataSources["[Faculty].[SP_ReferToMaster]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                   rpt.CompiledReport.DataSources["[Faculty].[SP_ReferToMaster]"].Parameters["@CodeOstad"].ParameterValue = txt_CodeOstad.Text;
                   rpt.CompiledReport.DataSources["[Faculty].[SP_ReferToMaster]"].Parameters["@FromDate"].ParameterValue = txt_FromDate.Text;
                   rpt.CompiledReport.DataSources["[Faculty].[SP_ReferToMaster]"].Parameters["@ToDate"].ParameterValue = txt_ToDate.Text;
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
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            RMD.Term = ddl_Term.SelectedValue;
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
            DataTable dtResault = FRB.ReferToProf(ddl_Term.SelectedValue, txt_CodeOstad.Text, txt_FromDate.Text, txt_ToDate.Text);
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView1.DataSource = dtResault;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportReferToMaster.xls");
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