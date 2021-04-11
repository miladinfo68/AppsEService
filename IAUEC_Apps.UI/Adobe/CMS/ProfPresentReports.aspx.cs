using IAUEC_Apps.Business.Adobe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using System.Data;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class ProfPresentReports : System.Web.UI.Page
    {
        StuPresentBusiness StuPresentBusiness = new StuPresentBusiness();
        ProfPresentBusiness ProfPresentBusiness = new ProfPresentBusiness();
        CommonBusiness CB = new CommonBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtDaneshkade = new DataTable();
            if (!IsPostBack)
            {
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

            string Nimsal = ddl_Nimsal.Text;
            int DepartmentCode = 0;
            if (ddl_Daneshkade.SelectedIndex != 0)
                DepartmentCode = int.Parse(ddl_Daneshkade.SelectedValue);
            ProfPresentBusiness pb = new ProfPresentBusiness();
            grd_ProfPresentReport.DataSource = pb.GetProfessorListByDepartment_Term(Nimsal, DepartmentCode);
            grd_ProfPresentReport.DataBind();
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
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 19, "حضور غیاب اساتید");

            }
        }

        protected void grd_ProfPresentReports_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            
            string Nimsal = ddl_Nimsal.Text;
            int DepartmentCode = 0;
            if (ddl_Daneshkade.SelectedIndex != 0)
                DepartmentCode = int.Parse(ddl_Daneshkade.SelectedValue);
            ProfPresentBusiness pb = new ProfPresentBusiness();

            grd_ProfPresentReport.DataSource = pb.GetProfessorListByDepartment_Term( Nimsal, DepartmentCode);
            
            
        }



    }
}