using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Education;
using System.Data;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.DTO.University.Faculty;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using IAUEC_Apps.Business.Common;
using System.IO;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ListAsatidDarHarTerm : System.Web.UI.Page
    {
        
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        EducationReportBusiness ERB = new EducationReportBusiness();
        DataTable dtGroup = new DataTable();
        DataTable dtTerm = new DataTable();
        DataTable dtCooperation = new DataTable();
        static ListAsatidDarHarTermDTO LATD = new ListAsatidDarHarTermDTO();
        DataTable dt = new DataTable();
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
                int iddanesh = 0;
                if (Session["RoleID"].ToString() == "51" || Session["RoleID"].ToString() == "17")
                    iddanesh = 1;
                if (Session["RoleID"].ToString() == "52" || Session["RoleID"].ToString() == "16")
                    iddanesh = 3;
                if (Session["RoleID"].ToString() == "53" || Session["RoleID"].ToString() == "15")
                    iddanesh = 2;
                 
                        
                dtGroup = FRB.GetAllGroup(iddanesh);
                ddl_EducationGroup.DataTextField = "namegroup";
                ddl_EducationGroup.DataValueField = "id";
                ddl_EducationGroup.DataSource = dtGroup;
                ddl_EducationGroup.DataBind();
                ddl_EducationGroup.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_EducationGroup.Items[ddl_EducationGroup.Items.Count - 1].Selected = true;
                //dtCooperation = FRB.GetCooperation();
                //ddl_Cooperation.DataTextField = "name_nahveh";
                //ddl_Cooperation.DataValueField = "nahveh_hamk";
                //ddl_Cooperation.DataSource = dtCooperation;
                //ddl_Cooperation.DataBind();
                //ddl_Cooperation.Items.Add(new ListItem("انتخاب کنید","0"));
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
            }
        }

        protected void btn_AccessCards_Click(object sender, EventArgs e)
        {
            //img_ExportToExcel1.Visible = false;
            //img_ExportToExcel2.Visible = false;
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue =="0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (ddl_Cooperation.SelectedIndex == null)
                {
                    ddl_Cooperation.SelectedValue = "0";
                }
                if (ddl_EducationGroup.SelectedValue == null)
                {
                    ddl_EducationGroup.SelectedValue = "0";
                }
               dt = FRB.GetAccessCardsProf(ddl_Term.SelectedValue, int.Parse(ddl_EducationGroup.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue));
               if (dt.Rows.Count == 0)
               {
                   RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
               }
               else
               {
                   //img_ExportToExcel1.Visible = true;
                   this.StiWebViewer1.ResetReport();
                   StiWebViewer1.Visible = true;
                   StiReport rpt = new StiReport();
                   rpt.Load(Server.MapPath("../Report/ReportAccessCardsProf.mrt"));
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.Dictionary.Databases.Clear();
                   rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                   rpt.Compile();
                   rpt.CompiledReport.DataSources["[Faculty].[SP_AccessCardsProf]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_EducationGroup.SelectedValue);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_AccessCardsProf]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                   rpt.CompiledReport.DataSources["[Faculty].[SP_AccessCardsProf]"].Parameters["@Cooperation"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                   rpt.RegData(dt);
                   rpt.Dictionary.Synchronize();
                   //rpt.Show();
                   StiWebViewer1.Report = rpt;
                   StiWebViewer1.Visible = true;
                   //rpt.Print(true);
               }
            }
        }

        protected void btn_CurrentTerm_Click(object sender, EventArgs e)
        {
            //img_ExportToExcel1.Visible = false;
            //img_ExportToExcel2.Visible = false;
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue=="0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (ddl_Cooperation.SelectedValue == null)
                {
                    ddl_Cooperation.SelectedValue = "0";
                }
                if (ddl_EducationGroup.SelectedValue == null)
                {
                    ddl_EducationGroup.SelectedValue = "0";
                }
                dt = FRB.GetListProfinCurrentTerm(ddl_Term.SelectedValue, int.Parse(ddl_EducationGroup.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue));
                if (dt.Rows.Count == 0)
                {
                    RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    //img_ExportToExcel2.Visible = true;
                    DataTable dtDanesh = new DataTable();
                    dtDanesh = FRB.GetNameDepartmanAndGroup(int.Parse(ddl_EducationGroup.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue));
                    this.StiWebViewer1.ResetReport();
                    StiWebViewer1.Visible = true;
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportListProfCurrentTerm.mrt"));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ListProfCurrentTerm]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_EducationGroup.SelectedValue);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ListProfCurrentTerm]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                    rpt.CompiledReport.DataSources["[Faculty].[SP_ListProfCurrentTerm]"].Parameters["@Cooperation"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_NameGroupAndDaneshByID]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_EducationGroup.SelectedValue);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_NameGroupAndDaneshByID]"].Parameters["@Cooperation"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                    rpt.RegData(dt);
                    rpt.RegData(dtDanesh);
                    rpt.Dictionary.Synchronize();
                    //rpt.Show();
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                    //rpt.Print(true);
                }
            }
        }

        protected void ddl_Cooperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            LATD.Cooperation = ddl_Cooperation.SelectedValue;
        }

        protected void ddl_EducationGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LATD.Departman = ddl_EducationGroup.SelectedValue;
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            LATD.Term = ddl_Term.SelectedValue;
        }

        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
            dt = FRB.GetAccessCardsProf(ddl_Term.SelectedValue, int.Parse(ddl_EducationGroup.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue));
             if (dt.Rows.Count == 0)
             {
             }
             else
             {
                 GridView1.DataSource = dt;
                 GridView1.DataBind();
                 Response.Clear();
                 Response.Buffer = true;
                 Response.AddHeader("content-disposition", "attachment;filename=ReportAccessCardsProf.xls");
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
            dt = FRB.GetListProfinCurrentTerm(ddl_Term.SelectedValue, int.Parse(ddl_EducationGroup.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue));
            if (dt.Rows.Count > 0)
            
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportListProfCurrentTerm.xls");
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
                            
                           if(c<15)
                                    cell.CssClass = "textmode";
                              else 
                                    cell.CssClass = "num";
                            c++;
                            
                
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