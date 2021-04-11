using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.DTO.University.Faculty;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using System.IO;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ArzeshyabiAsatid : System.Web.UI.Page
    {
        
        public static TeacherEvalutionDTO TED = new TeacherEvalutionDTO();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        EducationReportBusiness ERB = new EducationReportBusiness();
        DataTable dtGroup = new DataTable();
        DataTable dtTerm = new DataTable();
        DataTable dtDars = new DataTable();
        DataTable dt = new DataTable();
        CommonBusiness CB = new CommonBusiness();
        int Order;
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.StiWebViewer1.ResetReport();
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
                if (Session[sessionNames.roleID].ToString() == "51" || Session[sessionNames.roleID].ToString() == "17")
                    iddanesh = 1;
                if (Session[sessionNames.roleID].ToString() == "52" || Session[sessionNames.roleID].ToString() == "16")
                    iddanesh = 3;
                if (Session[sessionNames.roleID].ToString() == "53" || Session[sessionNames.roleID].ToString() == "15")
                    iddanesh = 2;
                 
                        
                
                dtGroup = FRB.GetAllGroup(iddanesh);
                ddl_CodeGroup.DataTextField = "namegroup";
                ddl_CodeGroup.DataValueField = "id";
                ddl_CodeGroup.DataSource = dtGroup;
                ddl_CodeGroup.DataBind();
                ddl_CodeGroup.Items.Add(new ListItem("انتخاب کنید","0"));
                ddl_CodeGroup.Items[ddl_CodeGroup.Items.Count -1].Selected=true;
                if (Request.QueryString["Departman"]!= null)
                {
                    ddl_CodeGroup.SelectedValue = Request.QueryString["Departman"].ToString();

                }
                dtDars = ERB.GetListDorus();
                ddl_CodeDras.DataTextField = "namedars";
                ddl_CodeDras.DataValueField = "dcode";
                ddl_CodeDras.DataSource = dtDars;
                ddl_CodeDras.DataBind();
                ddl_CodeDras.Items.Add(new ListItem("انتخاب کنید","0"));
                ddl_CodeDras.Items[ddl_CodeDras.Items.Count - 1].Selected = true;
                if (Request.QueryString["Lesson"] != null)
                {
                    ddl_CodeDras.SelectedValue = Request.QueryString["Lesson"].ToString();

                }
            }
            if (Session["code_ostad"] != null)
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
        }

        protected void btn_Select_Click(object sender, EventArgs e)
        {
            if (ddl_CodeGroup.SelectedValue == null)
            {
                ddl_CodeGroup.SelectedValue = "0";
            }
            if (ddl_CodeDras.SelectedValue == null)
            {
                ddl_CodeDras.SelectedValue = "0";
            }
            if (ddl_Term.SelectedValue == null)
            {
               ddl_Term.SelectedValue = "0";
            }
            Session["page"] = 2;
            //Response.Redirect("SearchProfByParams.aspx?"+"Term"+"="+ddl_Term.SelectedValue+"&"+"Lesson"+"="+ddl_CodeDras.SelectedValue+"&"+"Departman"+"="+ddl_CodeGroup.SelectedValue);
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = ("../../../university/Faculty/CMS/SearchProfByParams.aspx?" + "Term" + "=" + ddl_Term.SelectedValue + "&" + "Lesson" + "=" + ddl_CodeDras.SelectedValue + "&" + "Departman" + "=" + ddl_CodeGroup.SelectedValue);
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
            TED.Term = ddl_Term.SelectedValue;
        }

        protected void ddl_CodeDras_SelectedIndexChanged(object sender, EventArgs e)
        {
            TED.Lesson = ddl_CodeDras.SelectedValue;
        }

        protected void ddl_CodeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            TED.Departman = ddl_CodeGroup.SelectedValue;
        }

        protected void btn_EvalutionProf_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            img_ExportToExcel3.Visible = false;
            img_ExportToExcel4.Visible = false;
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue=="0")   
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_CodeOstad.Text == "")
                {
                    txt_CodeOstad.Text = "0";
                }
                if (ddl_CodeGroup.SelectedValue == null)
                {
                    ddl_CodeGroup.SelectedValue = "0";
                }
                if (ddl_CodeDras.SelectedValue == null)
                {
                    ddl_CodeDras.SelectedValue = "0";
                }
                Order = 1;
                dt = FRB.GetEvalutionProfDividedDid(ddl_Term.SelectedValue, int.Parse(txt_CodeOstad.Text), int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_CodeDras.SelectedValue), Order);
               if (dt.Rows.Count == 0)
               {
                   RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
               }
               else
               {
                   img_ExportToExcel1.Visible = true;
                   this.StiWebViewer1.ResetReport();
                   StiReport rpt = new StiReport();
                   rpt.Load(Server.MapPath("../Report/ReportEvalutionProf.mrt"));
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.Dictionary.Databases.Clear();
                   rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                   rpt.Compile();
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Lesson"].ParameterValue = int.Parse(ddl_CodeDras.SelectedValue);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_CodeGroup.SelectedValue);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Order"].ParameterValue = Order;
                   ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"]).CommandTimeout = 30000;
                   rpt.RegData(dt);
                   rpt.Dictionary.Synchronize();
                   //rpt.Show();
                   StiWebViewer1.Report = rpt;
                   StiWebViewer1.Visible = true;
                   //rpt.Print(true);
               }
               if (txt_CodeOstad.Text =="0" || txt_CodeOstad.Text == "" )
               {
                   txt_CodeOstad.Text = "";
                   Session["code_ostad"] = null;
               }
               
            }
            //txt_CodeOstad.Text = "";
            Session["code_ostad"]= null;
            
        }

        protected void btn_EvalutionProfByLesson_Click(object sender, EventArgs e)
        {
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue=="0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                }
                if (ddl_CodeGroup.SelectedValue == null)
                {
                    ddl_CodeGroup.SelectedValue = "0";
                }
                if (ddl_CodeDras.SelectedValue == null)
                {
                    ddl_CodeDras.SelectedValue = "0";
                }
                Order = 1;
                dt = FRB.GetEvalutionProfDividedODD(ddl_Term.SelectedValue, int.Parse(txt_CodeOstad.Text), int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_CodeDras.SelectedValue), Order);
               if (dt.Rows.Count == 0)
               {
                   RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
               }
               else
               {
                   this.StiWebViewer1.ResetReport();
                   StiReport rpt = new StiReport();
                   rpt.Load(Server.MapPath("../Report/ReportEvalutionProfDividedDid].mrt"));
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.Dictionary.Databases.Clear();
                   rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                   rpt.Compile();
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@CodeOstad"].ParameterValue = txt_CodeOstad.Text;
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Lesson"].ParameterValue = int.Parse(ddl_CodeDras.SelectedValue);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_CodeGroup.SelectedValue);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Order"].ParameterValue = Order;
                   ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"]).CommandTimeout = 10000;
                   rpt.RegData(dt);
                   rpt.Dictionary.Synchronize();
                   //rpt.Show();
                   StiWebViewer1.Report = rpt;
                   StiWebViewer1.Visible = true;
                   //rpt.Print(true);
               }
               if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
               {
                   txt_CodeOstad.Text = "";
                   Session["code_ostad"] = null;
               }
            }
            //txt_CodeOstad.Text = "";
            Session["code_ostad"] = null;
         }

        protected void btn_EvalutionProfbyItem_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            img_ExportToExcel3.Visible = false;
            img_ExportToExcel4.Visible = false;
            //this.StiWebViewer1.ResetReport();
             if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue=="0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                }
                if (ddl_CodeGroup.SelectedValue == null)
                {
                    ddl_CodeGroup.SelectedValue = "0";
                }
                if (ddl_CodeDras.SelectedValue == null)
                {
                    ddl_CodeDras.SelectedValue = "0";
                }
                Order = 1;
                dt = FRB.GetEvalutionProfDividedODR(ddl_Term.SelectedValue,int.Parse(txt_CodeOstad.Text), int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_CodeDras.SelectedValue), Order);
               if (dt.Rows.Count == 0)
               {
                   RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
               }
               else
               {
                   img_ExportToExcel2.Visible = true;
                   this.StiWebViewer1.ResetReport();
                   StiReport rpt = new StiReport();
                   rpt.Load(Server.MapPath("../Report/ReportEvalutionProfDividedODR.mrt"));
                   rpt.Dictionary.Databases.Clear();
                   rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.Compile();
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"].Parameters["@CodeOstad"].ParameterValue =int.Parse( txt_CodeOstad.Text);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"].Parameters["@Lesson"].ParameterValue = int.Parse(ddl_CodeDras.SelectedValue);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_CodeGroup.SelectedValue);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"].Parameters["@Order"].ParameterValue = Order;
                   ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"]).CommandTimeout = 30000;
                   rpt.RegData(dt);
                   rpt.Dictionary.Synchronize();
                   //rpt.Show();
                   StiWebViewer1.Report = rpt;
                   StiWebViewer1.Visible = true;
                   //rpt.Print(true);
               }
               if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
               {
                   txt_CodeOstad.Text = string.Empty;
               }
            }
             //txt_CodeOstad.Text = string.Empty;
        }

        protected void btn_EvalutionProfbyItemLesson_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            img_ExportToExcel3.Visible = false;
            img_ExportToExcel4.Visible = false;
            //this.StiWebViewer1.ResetReport();
             if (ddl_Term.SelectedValue == "0" || ddl_Term.SelectedValue==null)
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                }
                if (ddl_CodeGroup.SelectedValue == null)
                {
                    ddl_CodeGroup.SelectedValue= "0";
                }
                if (ddl_CodeDras.SelectedValue == null)
                {
                   ddl_CodeDras.SelectedValue = "0";
                }
                Order = 1;
                dt = FRB.GetEvalutionProfDividedODD(ddl_Term.SelectedValue,int.Parse(txt_CodeOstad.Text), int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_CodeDras.SelectedValue), Order);
               if (dt.Rows.Count == 0)
               {
                   RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
               }
               else
               {
                   img_ExportToExcel3.Visible = true;
                   this.StiWebViewer1.ResetReport();
                   StiReport rpt = new StiReport();
                   rpt.Load(Server.MapPath("../Report/ReportEvalutionProfDividedODD.mrt"));
                   rpt.Dictionary.Databases.Clear();
                   rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.Compile();
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@CodeOstad"].ParameterValue =int.Parse( txt_CodeOstad.Text);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Lesson"].ParameterValue = int.Parse(ddl_CodeDras.SelectedValue);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_CodeGroup.SelectedValue);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Order"].ParameterValue = Order;
                   ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"]).CommandTimeout = 10000;
                   rpt.RegData(dt);
                   rpt.Dictionary.Synchronize();
                   //rpt.Show();
                   StiWebViewer1.Report = rpt;
                   StiWebViewer1.Visible = true;
                   //rpt.Print(true); 
               }
               if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
               {
                   txt_CodeOstad.Text = "";
                   Session["code_ostad"] = null;
               }
            }
             //txt_CodeOstad.Text = "";
             Session["code_ostad"] = null;
        }

        protected void btn_EvalutionAll_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            img_ExportToExcel3.Visible = false;
            img_ExportToExcel4.Visible = false;
            //this.StiWebViewer1.ResetReport();
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue=="0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                }
                if (ddl_CodeGroup.SelectedValue == null)
                {
                    ddl_CodeGroup.SelectedValue = "0";
                }
                if (ddl_CodeDras.SelectedValue == null)
                {
                    ddl_CodeDras.SelectedValue = "0";
                }
                Order = 1;
               dt = FRB.GetEvalutionProfDividedODDQ(ddl_Term.SelectedValue ,int.Parse(txt_CodeOstad.Text),int.Parse(ddl_CodeGroup.SelectedValue),int.Parse(ddl_CodeDras.SelectedValue),Order);
               if (dt.Rows.Count == 0)
               {
                   RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
               }
               else
               {
                   img_ExportToExcel4.Visible = true;
                   this.StiWebViewer1.ResetReport();
                   StiReport rpt = new StiReport();
                   rpt.Load(Server.MapPath("../Report/ReportEvalutionProfDividedODDQ.mrt"));
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.Dictionary.Databases.Clear();
                   rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                   rpt.Compile();
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"].Parameters["@CodeOstad"].ParameterValue =int.Parse(txt_CodeOstad.Text);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"].Parameters["@Lesson"].ParameterValue = int.Parse(ddl_CodeDras.SelectedValue);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_CodeGroup.SelectedValue);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"].Parameters["@Order"].ParameterValue = Order;
                   ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"]).CommandTimeout = 30000;
                   rpt.RegData(dt);
                   rpt.Dictionary.Synchronize();
                   //rpt.Show();
                   StiWebViewer1.Report = rpt;
                   StiWebViewer1.Visible = true;
                   //rpt.Print(true); 
               }
               if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
               {
                   txt_CodeOstad.Text = "";
                   Session["code_ostad"] = null;
               }

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
            Order = 1;
            int idostad = 0;
            if (txt_CodeOstad.Text != "")
                idostad = int.Parse(txt_CodeOstad.Text);
            DataTable dt = FRB.GetEvalutionProfDividedDid(ddl_Term.SelectedValue, idostad, int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_CodeDras.SelectedValue), Order);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportEvalutionProf.xls");
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
            Order = 1;
            DataTable dt = FRB.GetEvalutionProfDividedODR(ddl_Term.SelectedValue, int.Parse(txt_CodeOstad.Text), int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_CodeDras.SelectedValue), Order);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportEvalutionProfDividedODR.xls");
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

        protected void img_ExportToExcel3_Click(object sender, ImageClickEventArgs e)
        {
            Order = 1;
            DataTable dt = FRB.GetEvalutionProfDividedODD(ddl_Term.SelectedValue, int.Parse(txt_CodeOstad.Text), int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_CodeDras.SelectedValue), Order);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView3.DataSource = dt;
                GridView3.DataBind();
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
                    foreach (TableCell cell in GridView3.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView3.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView3.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView3.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView3.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView3.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void img_ExportToExcel4_Click(object sender, ImageClickEventArgs e)
        {
            Order = 1;
            dt = FRB.GetEvalutionProfDividedODDQ(ddl_Term.SelectedValue, int.Parse(txt_CodeOstad.Text), int.Parse(ddl_CodeGroup.SelectedValue), int.Parse(ddl_CodeDras.SelectedValue), Order);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView4.DataSource = dt;
                GridView4.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportEvalutionProfDividedODDQ.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView4.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView4.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView4.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView4.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView4.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView4.RenderControl(hw);

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
