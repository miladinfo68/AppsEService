using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Education;
using System.Data;
using IAUEC_Apps.Business.Common;
//using Microsoft.Reporting.WebForms;
//using System.Windows.Forms;

using IAUEC_Apps.DTO.University.Education;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

using System.IO;

namespace IAUEC_Apps.UI.University.Education.CMS
{
    
    public partial class ListNumberClass : System.Web.UI.Page
    {
        
        public static ListNumberClassDTO LNCDT = new ListNumberClassDTO();
        EducationReportBusiness ERB =new EducationReportBusiness();
        CommonBusiness CB = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

            StiWebViewer1.Visible = false;
            DataTable dtLocationClass = new DataTable();
            DataTable dtTerm = new DataTable();

            //ramezanian-940331
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
                dtLocationClass = ERB.SelectAllLocatoionClass();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;
                ddl_LocationClass.DataTextField = "name_mahal";
                ddl_LocationClass.DataValueField = "id";
                ddl_LocationClass.DataSource = dtLocationClass;
                ddl_LocationClass.DataBind();
                //ddl_NumberClass.Items.Insert(0, "انتخاب کنید");
                ddl_LocationClass.Items.Add(new ListItem("انتخاب کنید ", "0"));
                ddl_LocationClass.Items[ddl_LocationClass.Items.Count - 1].Selected = true;
               
            }

        }
        //ramezanian
        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            LNCDT.Term = ddl_Term.SelectedValue.ToString();
            //Session["Term"] = ddl_Term.SelectedValue;      
        }
        

        //ramezanian
        protected void ddl_LocationClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LNCDT.LocationClass = ddl_LocationClass.SelectedValue;
            int LocationClass = int.Parse(LNCDT.LocationClass.ToString());
            DataTable dt = ERB.SelectRowLocationClass(LocationClass);
            ddl_NumberClass.DataTextField = "shom_klas";
            ddl_NumberClass.DataValueField = "id";
            ddl_NumberClass.DataSource = dt;
            ddl_NumberClass.DataBind();
            ddl_NumberClass.Items.Add(new ListItem("انتخاب کنید", "0"));
            ddl_NumberClass.Items[ddl_NumberClass.Items.Count - 1].Selected = true;      
        }
      //ramezanian
       protected void ddl_NumberClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Session["NumberClass"] = Convert.ToInt32(ddl_NumberClass.SelectedValue);
            LNCDT.NumberClass = ddl_NumberClass.SelectedValue;
        }
       //ramezanian
       protected void btn_PrintList_Click(object sender, EventArgs e)
       {
                img_ExportToExcel1.Visible = false;
               if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue=="0")
               {
                   RadWindowManager1.RadAlert("لطفا ترم را انتخاب فرمایید", 0, 100, "پیغام", "");
               }
               else
               {
                   int NumberClass = 0;
                   int LocationClass = 0;
                   if (ddl_NumberClass.SelectedValue != null && ddl_NumberClass.SelectedValue!="")
                   {
                       NumberClass = int.Parse(ddl_NumberClass.SelectedValue);
                   }
                   if (ddl_LocationClass.SelectedValue != null && ddl_NumberClass.SelectedValue!="")
                   {
                       LocationClass = int.Parse(ddl_LocationClass.SelectedValue.ToString());
                   }
                   
                   string Term = ddl_Term.SelectedValue.ToString();                  
                   DataTable dt = new DataTable();
                   dt = ERB.SelectListClass(NumberClass, Term, LocationClass);
                   DataTable dt1 = ERB.GetNameClass(NumberClass);
                   if (dt.Rows.Count == 0)
                   {
                       RadWindowManager1.RadAlert("رکوردی یافت نشد", 0, 100, "پیغام", "");
                   }
                   else
                   {
                       img_ExportToExcel1.Visible = true;
                       this.StiWebViewer1.ResetReport();
                       StiWebViewer1.Visible = true;
                       StiReport rpt = new StiReport();
                       rpt.CacheAllData = false;
                       rpt.ClearAllStates();
                       rpt.Load(Server.MapPath("../Report/ReportّFinal.mrt"));
                       rpt.Dictionary.Databases.Clear();
                       rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                       rpt.Compile();
                       rpt.CompiledReport.DataSources["[Education].[SP_PrintListClass]"].Parameters["@Number"].ParameterValue = NumberClass;
                       rpt.CompiledReport.DataSources["[Education].[SP_PrintListClass]"].Parameters["@Term"].ParameterValue = Term;
                       rpt.CompiledReport.DataSources["[Education].[SP_PrintListClass]"].Parameters["@Location"].ParameterValue = LocationClass;
                       rpt.CompiledReport.DataSources["[Education].[SP_GetNameClass]"].Parameters["@id"].ParameterValue = NumberClass;
                       rpt.RegData(dt);
                       rpt.RegData(dt1);
                       rpt.Dictionary.Synchronize();
                       //rpt.Show();
                       StiWebViewer1.Report = rpt;
                       StiWebViewer1.Visible = true;
                       //rpt.Print(true);
                   }
               
           }       
       }
       public override void VerifyRenderingInServerForm(Control control)
       {
           /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
              server control at run time. */
       }
       protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
       {
           DataTable dt;
           string Number;
           if (ddl_NumberClass.SelectedValue == "")
           {
               Number = "0";
               dt = ERB.SelectListClass(int.Parse(Number), ddl_Term.SelectedValue.ToString(), int.Parse(ddl_LocationClass.SelectedValue.ToString())); 
           }
           else
           {
              dt = ERB.SelectListClass(int.Parse(ddl_NumberClass.SelectedValue), ddl_Term.SelectedValue.ToString(), int.Parse(ddl_LocationClass.SelectedValue.ToString()));
           }
           if (dt.Rows.Count == 0)
          {
          }
          else
          {
              GridView2.DataSource = dt;
              GridView2.DataBind();
              Response.Clear();
              Response.Buffer = true;
              Response.AddHeader("content-disposition", "attachment;filename=PrintListClass.xls");
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
