using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using System.Data;
using IAUEC_Apps.Business.university.Education;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class ProfPresentReport : System.Web.UI.Page
    {
        StuPresentBusiness StuPresentBusiness = new StuPresentBusiness();
        CommonBusiness CB = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtDaneshkade = new DataTable();
            if (!IsPostBack)
            {
                // تمام نیمسال های قابل قبول را بر می گرداند
                List<string> TermList = StuPresentBusiness.GetActiveTerm();
                foreach (var item in TermList)
                    ddl_Nimsal.Items.Add(item.ToString());
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
                }
                Session[sessionNames.menuID] = menuId;
                AccessControl1.MenuId = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();


                dtDaneshkade = CB.SelectAllDaneshkade();
                ddl_Daneshkade.DataTextField = "namedanesh";
                ddl_Daneshkade.DataValueField = "id";
                ddl_Daneshkade.DataSource = dtDaneshkade;
                ddl_Daneshkade.DataBind();
                ddl_Daneshkade.Items.Insert(0, "انتخاب کنید");
                ddl_Daneshkade.Items.Add(new ListItem("همه", "0"));
                ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;
                if (Session[sessionNames.roleID].ToString() == "15" || Session[sessionNames.roleID].ToString() == "26")
                {
                    ddl_Daneshkade.SelectedValue = "2";
                    ddl_Daneshkade.Enabled = false;
                  
                                  }
                else if (Session[sessionNames.roleID].ToString() == "17" || Session[sessionNames.roleID].ToString() == "28")
                {
                    ddl_Daneshkade.SelectedValue = "1";
                    ddl_Daneshkade.Enabled = false;
                                 }
                else if (Session[sessionNames.roleID].ToString() == "16" || Session[sessionNames.roleID].ToString() == "27")
                {
                    ddl_Daneshkade.SelectedValue = "3";
                    ddl_Daneshkade.Enabled = false;
                  
                }
                else if (Session[sessionNames.roleID].ToString() == "66" || Session[sessionNames.roleID].ToString() == "67")
                {
                    ddl_Daneshkade.SelectedValue = "8";
                    ddl_Daneshkade.Enabled = false;

                }
                else
                {
                    ddl_Daneshkade.SelectedValue = "0";

                }

            }
           
        }

        protected void btn2_Click(object sender, EventArgs e)
        {

            if (txt_Leniency.Text == "-")
            {
                RadWindowManager1.RadAlert("مدت ارفاق وارد شود", null, null, "پیام", "");
            }
            if (txt_Leniency.Text != "-")
            {
                int Leniency = int.Parse(txt_Leniency.Text);
                ProfessorPresentBusiness pb = new ProfessorPresentBusiness();
                grd_ProfPresentReport.DataSource = pb.GetAllTimeOfEachClass(pb.SortedDataTableBothServer(ddl_Nimsal.SelectedValue.ToString(), int.Parse(ddl_Daneshkade.SelectedValue)), Leniency);
                grd_ProfPresentReport.DataBind();
            }
            else
            {
                RadWindowManager1.RadAlert("مدت ارفاق وارد شود", null, null, "پیام", "");
            }

        }

        protected void ConvertExcel_Click(object sender, ImageClickEventArgs e)
        {
            
            CommonBusiness cmnb = new CommonBusiness();

            if (grd_ProfPresentReport.MasterTableView.Items.Count > 0)
            {
                grd_ProfPresentReport.ExportSettings.FileName = string.Format("ProfessorTime_{0}_{1}", "Teacher", DateTime.Now);
                grd_ProfPresentReport.ExportSettings.IgnorePaging = true;
                grd_ProfPresentReport.ExportSettings.OpenInNewWindow = true;
                grd_ProfPresentReport.ExportSettings.ExportOnlyData = true;
                grd_ProfPresentReport.MasterTableView.UseAllDataFields = true;
                grd_ProfPresentReport.MasterTableView.ExportToExcel();
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 19, "");

            }
            
        }

        protected void grd_ProfPresentReport_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
//*******************************************************
            if (txt_Leniency.Text != "-")
            {
                int Leniency = int.Parse(txt_Leniency.Text);
                ProfessorPresentBusiness pb = new ProfessorPresentBusiness();

                grd_ProfPresentReport.DataSource = pb.GetAllTimeOfEachClass(pb.SortedDataTableBothServer(ddl_Nimsal.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue)), Leniency);


            }
//******************************************************           
           
            
        }

       

    }
}