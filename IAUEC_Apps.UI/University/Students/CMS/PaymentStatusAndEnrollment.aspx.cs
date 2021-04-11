using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Education;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Students;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

using System.IO;

namespace IAUEC_Apps.UI.University.Students.CMS
{
    public partial class PaymentStatusAndEnrollment : System.Web.UI.Page
    {
        PaymentStatusAbdEnrollment PSAE = new PaymentStatusAbdEnrollment();
        DataTable dtTerm = new DataTable();
        DataTable dtDaneshkade = new DataTable();
        CommonBusiness CB = new CommonBusiness();
        DataTable dtField = new DataTable();
        EducationReportBusiness ERB = new EducationReportBusiness();
        //CommonDAO dao = new CommonDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            StiWebViewer1.Visible = false;
            if (!IsPostBack)
            {
                dtTerm = CB.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;

                dtDaneshkade = CB.SelectAllDaneshkade();
                ddl_Daneshkade.DataTextField = "namedanesh";
                ddl_Daneshkade.DataValueField = "id";
                ddl_Daneshkade.DataSource = dtDaneshkade;
                ddl_Daneshkade.DataBind();
                ddl_Daneshkade.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;

                dtField = CB.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;

                ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "6"));
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;

                ddl_StatusStu.Items.Add(new ListItem("نامعلوم", "0"));
                ddl_StatusStu.Items.Add(new ListItem("عادی", "1"));
                ddl_StatusStu.Items.Add(new ListItem("مهمان از", "2"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف تغییر رشته", "3"));
                ddl_StatusStu.Items.Add(new ListItem("انتقال از", "4"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف با اطلاع", "5"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف ماده 51", "6"));
                ddl_StatusStu.Items.Add(new ListItem("فارغ التحصیل", "7"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج آموزشی", "8"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج انضباطی", "9"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج از واحد های تهران", "10"));
                ddl_StatusStu.Items.Add(new ListItem("محروم", "11"));
                ddl_StatusStu.Items.Add(new ListItem("فوت", "12"));
                ddl_StatusStu.Items.Add(new ListItem("شهید", "13"));
                ddl_StatusStu.Items.Add(new ListItem("مهمان سازمان", "14"));
                ddl_StatusStu.Items.Add(new ListItem("عدم مراجعه", "15"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج از کل واحدها", "16"));
                ddl_StatusStu.Items.Add(new ListItem("تسویه حساب مدرک معادل", "17"));
                ddl_StatusStu.Items.Add(new ListItem("انتخاب کنید", "18"));
                ddl_StatusStu.Items[ddl_StatusStu.Items.Count - 1].Selected = true;

            }
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Term = ddl_Term.SelectedValue;
        }

        protected void ddl_StatusStu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string StatusStu = ddl_StatusStu.SelectedValue;
        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Daneshkade.SelectedValue == "0")
            {
                dtField = CB.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
            }
            else
            {
                DataTable dtField = new DataTable();
                dtField = ERB.GetReshByDaneshkade(int.Parse(ddl_Daneshkade.SelectedValue.ToString()));
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
            string Field = ddl_Field.SelectedValue;
        }

        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Degree = ddl_Degree.SelectedValue;
        }

        protected void btn_Show_Click(object sender, EventArgs e)
        {
            img_ExportToExcel.Visible=false;
            img_ExportToExcel1.Visible=false;

            if (ddl_Term.SelectedValue == "0")
            {
                rwm.RadAlert("لطفا ترم را انتخاب کنید", 0, 100, "پیام", "");                   
            }
            else if (ddl_StatusStu.SelectedValue == "18")
            {
                rwm.RadAlert("لطفا وضعیت دانشجو را انتخاب کنید", 0, 100, "پیام", "");
            }
            else if (rdb_Payment.Checked == false)
            {
                img_ExportToExcel.Visible = false;
                img_ExportToExcel1.Visible = false;
               DataTable dt= PSAE.GetStatusRegistrationStu(ddl_Term.SelectedValue, int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Degree.SelectedValue));
               if (dt.Rows.Count == 0)
               {
                   rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
               }
               else
               {
                   //Report.............
                   img_ExportToExcel.Visible = true;
                   this.StiWebViewer1.ResetReport();
                   StiWebViewer1.Visible = true;
                   StiReport rpt = new StiReport();
                   rpt.Load(Server.MapPath("../Report/ReportNotPaymentStu.mrt"));
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.Dictionary.Databases.Clear();
                   rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                   rpt.Compile();
                   rpt.CompiledReport.DataSources["[Students].[SP_InfoStatusRegistartionStu]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                   rpt.CompiledReport.DataSources["[Students].[SP_InfoStatusRegistartionStu]"].Parameters["@StatusStu"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
                   rpt.CompiledReport.DataSources["[Students].[SP_InfoStatusRegistartionStu]"].Parameters["@Danshkade"].ParameterValue =int.Parse(ddl_Daneshkade.SelectedValue);
                   rpt.CompiledReport.DataSources["[Students].[SP_InfoStatusRegistartionStu]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                   rpt.CompiledReport.DataSources["[Students].[SP_InfoStatusRegistartionStu]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                   rpt.RegData(dt);
                   StiWebViewer1.Report = rpt;
                   StiWebViewer1.Visible = true;
               }
            }
            else
            {
                img_ExportToExcel.Visible = false;
                img_ExportToExcel1.Visible = false;
               DataTable dt= PSAE.GetStatusPayStu(ddl_Term.SelectedValue, int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Degree.SelectedValue));
               if (dt.Rows.Count == 0)
               {
                   rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
               }
               else
               {
                   //Report..............
                   img_ExportToExcel1.Visible = true;
                   this.StiWebViewer1.ResetReport();        
                   StiWebViewer1.Visible = true;
                   StiReport rpt = new StiReport();
                   rpt.Load(Server.MapPath("../Report/ReportPaymentStu.mrt"));
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.Dictionary.Databases.Clear();
                   rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                   rpt.Compile();
                   rpt.CompiledReport.DataSources["[Students].[SP_InfoStatusPaymentStu]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                   rpt.CompiledReport.DataSources["[Students].[SP_InfoStatusPaymentStu]"].Parameters["@StatusStu"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
                   rpt.CompiledReport.DataSources["[Students].[SP_InfoStatusPaymentStu]"].Parameters["@Danshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                   rpt.CompiledReport.DataSources["[Students].[SP_InfoStatusPaymentStu]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                   rpt.CompiledReport.DataSources["[Students].[SP_InfoStatusPaymentStu]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                   rpt.RegData(dt);
                   StiWebViewer1.Report = rpt;
                   StiWebViewer1.Visible = true;

               }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
             DataTable dt= PSAE.GetStatusRegistrationStu(ddl_Term.SelectedValue, int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Degree.SelectedValue));
             if (dt.Rows.Count == 0)
             {
                 
             }
             else
             {
                grd_Show1.DataSource = dt;
                grd_Show1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=GetStatusRegistrationStu.xls");
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

        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
              DataTable dt= PSAE.GetStatusPayStu(ddl_Term.SelectedValue, int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Degree.SelectedValue));
              if (dt.Rows.Count == 0)
              {

              }
              else
              {
                  grd_Show2.DataSource = dt;
                  grd_Show2.DataBind();
                  Response.Clear();
                  Response.Buffer = true;
                  Response.AddHeader("content-disposition", "attachment;filename=GetStatusPayStu.xls");
                  Response.Charset = "";
                  Response.ContentType = "application/vnd.ms-excel";
                  using (StringWriter sw = new StringWriter())
                  {
                      HtmlTextWriter hw = new HtmlTextWriter(sw);

                      //To Export all pages
                      ////gv_Show.AllowPaging = false;
                      ////this.BindGrid();

                      //gv_Show.HeaderRow.BackColor = Color.White;
                      foreach (TableCell cell in grd_Show2.HeaderRow.Cells)
                      {
                          cell.BackColor = grd_Show2.HeaderStyle.BackColor;
                      }
                      foreach (GridViewRow row in grd_Show2.Rows)
                      {
                          //row.BackColor = Color.White;
                          foreach (TableCell cell in row.Cells)
                          {
                              if (row.RowIndex % 2 == 0)
                              {
                                  cell.BackColor = grd_Show2.AlternatingRowStyle.BackColor;
                              }
                              else
                              {
                                  cell.BackColor = grd_Show2.RowStyle.BackColor;
                              }
                              cell.CssClass = "textmode";
                          }
                      }

                      grd_Show2.RenderControl(hw);

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