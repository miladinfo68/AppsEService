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
using IAUEC_Apps.Business.Common;
using Stimulsoft.Report.Dictionary;
using System.IO;

namespace IAUEC_Apps.UI.University.Education
{
    public partial class ContrastScore : System.Web.UI.Page
    {
            
            public static ListMoghayeratDTO LMD = new ListMoghayeratDTO();
            EducationReportBusiness ERB = new EducationReportBusiness();
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
                    DataTable dtField = new DataTable();
                    DataTable dtTerm = new DataTable();
                    dtField = ERB.SelectAllField();
                    ddl_Field.DataTextField = "nameresh";
                    ddl_Field.DataValueField = "id";
                    ddl_Field.DataSource = dtField;
                    ddl_Field.DataBind();
                    //ddl_Field.Items.Insert(0, "انتخاب کنید");
                    ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                    ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
                    dtTerm = CB.SelectAllTerm();
                    ddl_Term.DataTextField = "tterm";
                    ddl_Term.DataSource = dtTerm;
                    ddl_Term.DataBind();
                    //ddl_Term.Items.Insert(0, "انتخاب کنید");
                    ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                    ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;
                    
                }

            }

            protected void Btn_Show_Click(object sender, EventArgs e)
            {
                img_ExportToExcel.Visible = false;
                DataTable dtResault = new DataTable();
                if (txt_AzMoshakhase.Text == string.Empty || txt_TaMoshakase.Text == string.Empty)
                {
                    RadWindowManager1.RadAlert("لطفا از مشخصه ، تا مشخصه  را وارد نمایید ", 0, 100, "پیام", "");
                }
                else if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
                {
                    RadWindowManager1.RadAlert("لطفا ترم  را وارد نمایید ", 0, 100, "پیام", "");
                }
                else
                {
                    int AzMoshakhase = int.Parse(txt_AzMoshakhase.Text);
                    int TaMoshakhase = int.Parse(txt_TaMoshakase.Text);
                    if (ddl_Field.SelectedValue == null || ddl_Field.SelectedValue == "")
                    {
                        ddl_Field.SelectedValue = "0";
                    }
                    dtResault = ERB.GetListMoghayerat(AzMoshakhase, TaMoshakhase, Convert.ToInt32(ddl_Field.SelectedValue), ddl_Term.SelectedValue.ToString());
                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیغام", "");
                    }
                    else
                    {
                        img_ExportToExcel.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportDatesContrastScore.mrt"));
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Education].[SP_DatesContrastScore]"].Parameters["@AzMoshakhase"].ParameterValue = AzMoshakhase;
                        rpt.CompiledReport.DataSources["[Education].[SP_DatesContrastScore]"].Parameters["@TaMoshakhase"].ParameterValue = TaMoshakhase;
                        rpt.CompiledReport.DataSources["[Education].[SP_DatesContrastScore]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                        rpt.CompiledReport.DataSources["[Education].[SP_DatesContrastScore]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }
                    if (txt_TaMoshakase.Text == "0")
                        txt_TaMoshakase.Text = string.Empty;
                    if (txt_AzMoshakhase.Text == "0")
                        txt_AzMoshakhase.Text = string.Empty;
                    
                }
            }

            protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
            {
                
                LMD.Field = ddl_Field.SelectedValue;
            }

            protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
            {
                
                LMD.Term = ddl_Term.SelectedValue;              
            }
            public override void VerifyRenderingInServerForm(Control control)
            {
                /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
                   server control at run time. */
            }
            protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
            {
                DataTable dt = ERB.GetListMoghayerat(int.Parse(txt_AzMoshakhase.Text), int.Parse(txt_TaMoshakase.Text), Convert.ToInt32(ddl_Field.SelectedValue), ddl_Term.SelectedValue.ToString());
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    gv_Show.DataSource = dt;
                    gv_Show.DataBind();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=ContrastScore.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);

                        //To Export all pages
                        ////gv_Show.AllowPaging = false;
                        ////this.BindGrid();

                        //gv_Show.HeaderRow.BackColor = Color.White;
                        foreach (TableCell cell in gv_Show.HeaderRow.Cells)
                        {
                            cell.BackColor = gv_Show.HeaderStyle.BackColor;
                        }
                        foreach (GridViewRow row in gv_Show.Rows)
                        {
                            //row.BackColor = Color.White;
                            foreach (TableCell cell in row.Cells)
                            {
                                if (row.RowIndex % 2 == 0)
                                {
                                    cell.BackColor = gv_Show.AlternatingRowStyle.BackColor;
                                }
                                else
                                {
                                    cell.BackColor = gv_Show.RowStyle.BackColor;
                                }
                                cell.CssClass = "textmode";
                            }
                        }

                        gv_Show.RenderControl(hw);

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