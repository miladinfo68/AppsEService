using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.DTO.University.Education;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.IO;
using IAUEC_Apps.Business.Common;


namespace IAUEC_Apps.UI.University.Education.CMS
{
    public partial class ListDorusGhabuli : System.Web.UI.Page
    {
       // CommonDAO dao = new CommonDAO();
        CommonBusiness cb = new CommonBusiness();
        public static ListDorusGhabuliDTO LDD = new ListDorusGhabuliDTO();
        EducationReportBusiness ERB = new EducationReportBusiness();
        DataTable dtTerm = new DataTable();
        DataTable dtField = new DataTable();
        DataTable dtDars = new DataTable(); 
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
                dtTerm = cb.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;

                dtField = cb.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
                //dtDars = ERB.GetListDorus();
                //ddl_CodeDras.DataTextField = "namedars";
                //ddl_CodeDras.DataValueField = "dcode";
                //ddl_CodeDras.DataSource = dtDars;
                //ddl_CodeDras.DataBind();
                //ddl_CodeDras.Items.Add(new ListItem("انتخاب کنید","0"));
                //ddl_CodeDras.Items[ddl_CodeDras.Items.Count - 1].Selected = true;
                ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "7"));
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;
                //ddl_Geraesh.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_Geraesh.Items[ddl_Geraesh.Items.Count - 1].Selected = true;
            }
        }

        protected void btn_ShowReport_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            DataTable dt = new DataTable();
            if (ddl_Term.SelectedValue == "0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب کنید", 0, 100, "پیغام", "0");
            }
            else if (ddl_Degree.SelectedValue == "0")
            {
                RadWindowManager1.RadAlert("لطفا مقطع را انتخاب کنید", 0, 100, "پیغام", "0");
            }
            if (ddl_Field.SelectedValue == "0")
            {
                RadWindowManager1.RadAlert("لطفا رشته را انتخاب کنید", 0, 100, "پیغام", "0");
            }
            else if (ddl_Field.SelectedValue != "0")
            {

                if (ddl_CodeDras.Items.Count == 1)
                {
                    RadWindowManager1.RadAlert("برای رشته انتخاب شده درسی تعریف نشده است", 0, 100, "پیغام", "0");
                }
                else
                {
                    //if (ddl_Geraesh.Items.Count > 1 && ddl_Geraesh.SelectedValue == "0")
                    //{
                    //    RadWindowManager1.RadAlert("لطفا گرایش را انتخاب کنید", 0, 100, "پیغام", "");
                    //}
                    //else
                    if (ddl_CodeDras.Items.Count > 1 && ddl_CodeDras.SelectedValue == "0")
                    {
                        RadWindowManager1.RadAlert("لطفا درس را انتخاب کنید", 0, 100, "پیغام", "");
                    }
                    else if (ddl_Field.SelectedValue == null || ddl_CodeDras.SelectedValue == null || ddl_Field.SelectedValue == "0" || ddl_CodeDras.SelectedValue == "0")
                    {
                        RadWindowManager1.RadAlert("لطفا رشته و درس را انتخاب بفرمایید", 0, 100, "پیغام", "");
                    }
                    else
                    {
                        if (chk_ListStu.Checked == true)
                        {
                            dt = ERB.GetListDorusGhabuli(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_CodeDras.SelectedValue.ToString()), int.Parse(ddl_Geraesh.SelectedValue), int.Parse(ddl_Degree.SelectedValue));

                            if (dt.Rows.Count == 0)
                            {
                                RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیغام", "");
                            }
                            else
                            {
                                img_ExportToExcel2.Visible = true;
                                this.StiWebViewer1.ResetReport();
                                StiWebViewer1.Visible = true;
                                StiReport rpt = new StiReport();
                                rpt.Load(Server.MapPath("../Report/ReportAcceptedListLesson.mrt"));
                                rpt.Dictionary.Databases.Clear();
                                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", cb.ReportConnection.ToString()));
                                rpt.Compile();
                                rpt.CompiledReport.DataSources["[Education].[SP_AcceptedListLesson]"].Parameters["@Lesson"].ParameterValue = int.Parse(ddl_CodeDras.SelectedValue);
                                rpt.CompiledReport.DataSources["[Education].[SP_AcceptedListLesson]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                                rpt.CompiledReport.DataSources["[Education].[SP_AcceptedListLesson]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                                rpt.CompiledReport.DataSources["[Education].[SP_AcceptedListLesson]"].Parameters["@Graesh"].ParameterValue = int.Parse(ddl_Geraesh.SelectedValue);
                                rpt.CompiledReport.DataSources["[Education].[SP_AcceptedListLesson]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                                ((StiSqlSource)rpt.Dictionary.DataSources["[Education].[SP_AcceptedListLesson]"]).CommandTimeout = 300000;
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
                            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
                            {
                                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
                            }
                            else
                            {

                                dt = ERB.GetListDorusGhabuliBarAsasTerm(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_CodeDras.SelectedValue), int.Parse(ddl_Geraesh.SelectedValue), int.Parse(ddl_Degree.SelectedValue));
                                DataTable dtNameField = ERB.GetNameField(int.Parse(ddl_Field.SelectedValue));
                                DataTable dtNameLesson = ERB.GetNameLesson(int.Parse(ddl_CodeDras.SelectedValue));
                                if (dt.Rows.Count == 0)
                                {
                                    RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیغام", "");
                                }
                                else
                                {
                                    img_ExportToExcel1.Visible = true;
                                    this.StiWebViewer1.ResetReport();
                                    StiWebViewer1.Visible = true;
                                    StiReport rpt = new StiReport();
                                    rpt.Load(Server.MapPath("../Report/ReportAcceptedListLessonByTerm.mrt"));
                                    rpt.Dictionary.Databases.Clear();
                                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", cb.ReportConnection.ToString()));
                                    rpt.Compile();
                                    rpt.CompiledReport.DataSources["[Education].[SP_AcceptedListLessonByTerm]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                                    rpt.CompiledReport.DataSources["[Education].[SP_AcceptedListLessonByTerm]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                                    rpt.CompiledReport.DataSources["[Education].[SP_AcceptedListLessonByTerm]"].Parameters["@Graesh"].ParameterValue = int.Parse(ddl_Geraesh.SelectedValue);
                                    rpt.CompiledReport.DataSources["[Education].[SP_AcceptedListLessonByTerm]"].Parameters["@Lesson"].ParameterValue = int.Parse(ddl_CodeDras.SelectedValue);
                                    rpt.CompiledReport.DataSources["[Education].[SP_AcceptedListLessonByTerm]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                                    rpt.CompiledReport.DataSources["[Education].[SP_GetNameField]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                                    rpt.CompiledReport.DataSources["[Education].[GetNameLesson]"].Parameters["@Lesson"].ParameterValue = int.Parse(ddl_CodeDras.SelectedValue);
                                    rpt.RegData(dt);
                                    rpt.RegData(dtNameField);
                                    rpt.RegData(dtNameLesson);
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
            }
        }

        protected void ddl_CodeField_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (ddl_Field.SelectedValue == "0")
            {
                LDD.Field = ddl_Field.SelectedValue;
                dt = ERB.GetListDorus();
                ddl_CodeDras.DataSource = dt;
                ddl_CodeDras.DataTextField = "namedars";
                ddl_CodeDras.DataValueField = "dcode";
                ddl_CodeDras.DataBind();
                ddl_CodeDras.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_CodeDras.Items[ddl_CodeDras.Items.Count - 1].Selected = true;
            }
            else
            {
                //Session["Field"] = ddl_Field.SelectedValue;
                LDD.Field = ddl_Field.SelectedValue;
                DataTable dt2 = ERB.GetLessonByField(int.Parse(ddl_Field.SelectedValue));
                ddl_CodeDras.DataTextField = "namedars"; 
                ddl_CodeDras.DataValueField = "dcode";
                ddl_CodeDras.DataSource = dt2;
                ddl_CodeDras.DataBind();
                ddl_CodeDras.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_CodeDras.Items[ddl_CodeDras.Items.Count - 1].Selected = true;
                DataTable dt1;
                dt1 = ERB.GetGeraeshByField(int.Parse(ddl_Field.SelectedValue));
                ddl_Geraesh.DataTextField = "namegeraesh";
                ddl_Geraesh.DataValueField = "id";
                ddl_Geraesh.DataSource = dt1;
                ddl_Geraesh.DataBind();
                ddl_Geraesh.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Geraesh.Items[ddl_Geraesh.Items.Count - 1].Selected = true;              
            }
        }

        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt6;
            if (ddl_Degree.SelectedValue == "0")
            {
                dt6 = cb.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dt6;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;

                DataTable dtDars2 = ERB.GetListDorus();
                ddl_CodeDras.DataTextField = "namedars";
                ddl_CodeDras.DataValueField = "dcode";
                ddl_CodeDras.DataSource = dtDars2;
                ddl_CodeDras.DataBind();
                ddl_CodeDras.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_CodeDras.Items[ddl_CodeDras.Items.Count - 1].Selected = true;
            }
            else
            {
                DataTable dt5 = ERB.SelectFieldByDegree(int.Parse(ddl_Degree.SelectedValue));
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "idresh";
                ddl_Field.DataSource = dt5;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
                while (ddl_CodeDras.Items.Count > 0)
                {
                    ddl_CodeDras.Items.Remove(ddl_CodeDras.Items.FindByValue(ddl_CodeDras.SelectedValue));
                }
                while (ddl_Geraesh.Items.Count > 0)
                {
                    ddl_Geraesh.Items.Remove(ddl_Geraesh.Items.FindByValue(ddl_Geraesh.SelectedValue));
                }
            }
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            LDD.Term = ddl_Term.SelectedValue;
        }

        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = ERB.GetListDorusGhabuliBarAsasTerm(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_CodeDras.SelectedValue), int.Parse(ddl_Geraesh.SelectedValue), int.Parse(ddl_Degree.SelectedValue));
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportAcceptedListLessonByTerm.xls");
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
          DataTable  dt = ERB.GetListDorusGhabuli(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_CodeDras.SelectedValue.ToString()), int.Parse(ddl_Geraesh.SelectedValue), int.Parse(ddl_Degree.SelectedValue));
          if (dt.Rows.Count == 0)
          {
          }
          else
          {
              GridView2.DataSource = dt;
              GridView2.DataBind();
              Response.Clear();
              Response.Buffer = true;
              Response.AddHeader("content-disposition", "attachment;filename=ReportAcceptedListLesson.xls");
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

        protected void ddl_CodeDras_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CodeDars = ddl_CodeDras.SelectedValue;
        }

        protected void ddl_Geraesh_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Geraesh = ddl_Geraesh.SelectedValue;
        }
    }
}