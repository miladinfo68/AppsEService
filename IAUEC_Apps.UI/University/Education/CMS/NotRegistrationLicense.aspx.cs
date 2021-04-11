using IAUEC_Apps.Business.university.Education;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.University.Education;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Telerik.Web.UI;
using System.IO;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Education.CMS
{
    public partial class NotRegistrationLicense : System.Web.UI.Page
    {
        //CommonDAO dao = new CommonDAO();
        CommonBusiness cb = new CommonBusiness();
        public static ListDaneshjuianAdamMojavezDTO LDD = new ListDaneshjuianAdamMojavezDTO();
        EducationReportBusiness ERB = new EducationReportBusiness();
        DataTable dtTerm = new DataTable();
        DataTable dtDegree = new DataTable();
        DataTable dtField = new DataTable();
        DataTable dtEducation = new DataTable();
        DataTable dtSex = new DataTable();
        DataTable dtResault = new DataTable();
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
                //dtDegree = ERB.SelectAllDegree();
                //ddl_Degree.DataTextField = "name";
                //ddl_Degree.DataValueField = "id_sida";
                //ddl_Degree.DataSource = dtDegree;
                //ddl_Degree.DataBind();
                ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "7"));
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;
                if (Request.QueryString["Degree"]!= null)
                {
                    ddl_Degree.SelectedValue = Request.QueryString["Degree"].ToString();
                }
                dtField = cb.SelectAllField();
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
                ddl_Education.Items.Add(new ListItem("دوره ای", "1"));
                ddl_Education.Items.Add(new ListItem("پاره وقت", "2"));
                ddl_Education.Items.Add(new ListItem("طرح معلمان", "3"));
                ddl_Education.Items.Add(new ListItem("قراردادی", "4"));
                ddl_Education.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Education.Items[ddl_Education.Items.Count - 1].Selected = true;
                if (Request.QueryString["Education"] != null)
                {
                   ddl_Education.SelectedValue=Request.QueryString["Education"].ToString();
                }
                ddl_Sex.Items.Add(new ListItem("مرد", "1"));
                ddl_Sex.Items.Add(new ListItem("زن", "2"));
                ddl_Sex.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Sex.Items[ddl_Sex.Items.Count - 1].Selected = true;
                if (Request.QueryString["Sex"] != null)
                {
                    ddl_Sex.SelectedValue = Request.QueryString["Sex"].ToString();
                }
                if (LDD.salvorud == "0")
                {
                    txt_SalVorud.Text = string.Empty;
                }
                else
                {
                    txt_SalVorud.Text = LDD.salvorud;
                }
                
            }

            if (Session["stcode"] != null)
            {
                txt_StCode.Text = Session["stcode"].ToString();
            }
            LDD.salvorud = txt_SalVorud.Text;
        }


        protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            LDD.Field = ddl_Field.SelectedValue;         
        }

        protected void btn_ShowStCode_Click(object sender, EventArgs e)
        {
            if (ddl_Term.SelectedValue == null)
            {
                ddl_Term.SelectedValue = "0";
            }
            if (ddl_Degree.SelectedValue == null)
            {
                ddl_Degree.SelectedValue = "0";
            }
            if (ddl_Sex.SelectedValue == null)
            {
                ddl_Sex.SelectedValue = "0";
            }
            if (txt_SalVorud.Text == null || txt_SalVorud.Text == "")
            {
                txt_SalVorud.Text = "0";
            }
            if (ddl_Education.SelectedValue == null)
            {
                ddl_Education.SelectedValue = "0";
            }
            if (ddl_Field.SelectedValue == null)
            {
                ddl_Field.SelectedValue = "0";
            }
            //if (LDD.salvorud == null)
            //{
            //    LDD.salvorud = "0";
            //}

            Session["Page"] = 1;

            //Response.Redirect("RegistrationOfStudent.aspx?" + "Degree" + "=" + ddl_Degree.SelectedValue + "&" + "Field" + "=" + ddl_Field.SelectedValue + "&" + "Education" + "=" + ddl_Education.SelectedValue + "&" + "Sex" + "=" + ddl_Sex.SelectedValue + "&" + "salvorud" + "=" + txt_SalVorud.Text);

            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = ("../../../university/Education/CMS/SearchOfStudent.aspx?" + "Degree" + "=" + ddl_Degree.SelectedValue + "&" + "Field" + "=" + ddl_Field.SelectedValue + "&" + "Education" + "=" + ddl_Education.SelectedValue + "&" + "Sex" + "=" + ddl_Sex.SelectedValue + "&" + "salvorud" + "=" + txt_SalVorud.Text);
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code 
            widnow1.Height = 600;
            widnow1.Width = 1100;
            windowManager.Windows.Add(widnow1);
            windowManager.Height = 600;
            windowManager.Width = 1100;
            ContentPlaceHolder mpContentPlaceHolder;
            mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            mpContentPlaceHolder.Controls.Add(widnow1);
        }

        protected void ddl_Education_SelectedIndexChanged(object sender, EventArgs e)
        {
            LDD.Education = ddl_Education.SelectedValue;
        }

        protected void ddl_Sex_SelectedIndexChanged(object sender, EventArgs e)
        {
            LDD.sex = ddl_Sex.SelectedValue;
        }

        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            LDD.Degree = ddl_Degree.SelectedValue;

        }

        protected void Btn_Show_Click(object sender, EventArgs e)
        {
                if (ddl_Term.SelectedValue == null)
                {
                    ddl_Term.SelectedValue = "0";
                }
                if (ddl_Degree.SelectedValue == null)
                {
                    ddl_Degree.SelectedValue = "0";
                }
                if (ddl_Sex.SelectedValue == null)
                {
                    ddl_Sex.SelectedValue = "0";
                }
                if (txt_SalVorud.Text == null || txt_SalVorud.Text == "")
                {
                    txt_SalVorud.Text = "0";
                }
                if (ddl_Education.SelectedValue == null)
                {
                    ddl_Education.SelectedValue = "0";
                }
                if (txt_StCode.Text == string.Empty || txt_StCode.Text=="")
                {
                    txt_StCode.Text = "0";
                    Session["stcode"] = txt_StCode.Text;
                }
                if (LDD.Field == null)
                {
                    LDD.Field = "0";
                }

                dtResault = ERB.GetLisAdamSabtenam(ddl_Term.SelectedValue.ToString(), txt_StCode.Text, int.Parse(ddl_Degree.SelectedValue.ToString()), int.Parse(ddl_Education.SelectedValue.ToString()), int.Parse(ddl_Sex.SelectedValue.ToString()), txt_SalVorud.Text.ToString(), int.Parse(ddl_Field.SelectedValue));
                if (dtResault.Rows.Count == 0)
                {
                    RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیغام", "");
                }
                else
                {
                    img_ExportToExcel1.Visible = true;
                    this.StiWebViewer1.ResetReport();
                    StiWebViewer1.Visible = true;
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportNotRegistrationLicense.mrt"));
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", cb.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Education].[SP_NotRegistrationLicense]"].Parameters["@StCode"].ParameterValue = txt_StCode.Text;
                    rpt.CompiledReport.DataSources["[Education].[SP_NotRegistrationLicense]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                    rpt.CompiledReport.DataSources["[Education].[SP_NotRegistrationLicense]"].Parameters["@Education"].ParameterValue = int.Parse(ddl_Education.SelectedValue);
                    rpt.CompiledReport.DataSources["[Education].[SP_NotRegistrationLicense]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue);
                    rpt.CompiledReport.DataSources["[Education].[SP_NotRegistrationLicense]"].Parameters["@SalVorud"].ParameterValue = txt_SalVorud.Text;
                    rpt.CompiledReport.DataSources["[Education].[SP_NotRegistrationLicense]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                    rpt.CompiledReport.DataSources["[Education].[SP_NotRegistrationLicense]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                    rpt.RegData(dtResault);
                    rpt.Dictionary.Synchronize();
                    //rpt.Show();
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.ProcessExcel2007Request();
                    StiWebViewer1.ShowExportToExcel.ToString();
                    StiWebViewer1.Visible = true;
                    //rpt.Print(true);
                }
                Session["stcode"] = null;
            }

        protected void RadAjaxManager1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                txt_StCode.Text = Session["stcode"].ToString();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
         if (txt_StCode.Text == string.Empty || txt_StCode.Text == "")
         {
           txt_StCode.Text = "0";
          Session["stcode"] = txt_StCode.Text;
         }
         DataTable  dt = ERB.GetLisAdamSabtenam(ddl_Term.SelectedValue, txt_StCode.Text, int.Parse(ddl_Degree.SelectedValue.ToString()), int.Parse(ddl_Education.SelectedValue.ToString()), int.Parse(ddl_Sex.SelectedValue.ToString()), txt_SalVorud.Text.ToString(), int.Parse(ddl_Field.SelectedValue));
         if (dt.Rows.Count == 0)
         {
         }
         else
         {
             GridView1.DataSource = dt;
             GridView1.DataBind();
             Response.Clear();
             Response.Buffer = true;
             Response.AddHeader("content-disposition", "attachment;filename=ReportNotRegistrationLicense.xls");
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

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Term = ddl_Term.SelectedValue;
        }
      }
    }
