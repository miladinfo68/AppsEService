using IAUEC_Apps.Business.Adobe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class TAPresentReports : System.Web.UI.Page
    {
        StuPresentBusiness StuPresentBusiness = new StuPresentBusiness();
        ProfPresentBusiness ProfPresentBusiness = new ProfPresentBusiness();
        TABusiness TABusiness = new TABusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                // تمام نیمسال های قابل قبول را بر می گرداند
                List<string> TermList = StuPresentBusiness.GetActiveTerm();
                ddl_Nimsal.Items.Add("93-94-2");
                //foreach (var item in TermList)
                //    ddl_Nimsal.Items.Add(item.ToString());


                // تمام دپارتمان ها را میخواند
                List<string> DepartmentList = ProfPresentBusiness.GetAllDepartment();
                foreach (var item in DepartmentList)
                    ddl_Department.Items.Add(item.ToString());
               

            }
           
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            int Leniency = int.Parse(txt_Leniency.Text);
            string Nimsal = ddl_Nimsal.Text;
            string Department = "";      
            if (ddl_Department.Text != "همه")
                Department = ddl_Department.Text;



            if (ddl_Department.Text != "همه")
            {
                DataTable DT = TABusiness.GetAllTimeOfEachClass(Leniency, Nimsal, Department);

                DataTable DT3 = new DataTable();
                DT3.Columns.Add("Department", typeof(string));
                DT3.Columns.Add("GroupName", typeof(string));
                DT3.Columns.Add("LOGIN", typeof(string));
                DT3.Columns.Add("TeacherName", typeof(string));
                DT3.Columns.Add("code", typeof(int));
                DT3.Columns.Add("LessonName", typeof(string));
                DT3.Columns.Add("SumOfTime", typeof(string));
                DT3.Columns.Add("TimeClass", typeof(string));
                DT3.Columns.Add("SessionCount", typeof(string));
                DT3.Columns.Add("Hozoor", typeof(string));
                DT3.Columns.Add("Leniency", typeof(string));
                DT3.Columns.Add("tterm", typeof(string));

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    string xxxx = DT.Rows[i]["Department"].ToString();

                    if (DT.Rows[i]["Department"].ToString() == ddl_Department.Text)
                    {
                        DataRow row = DT3.NewRow();
                        row["Department"] = DT.Rows[i]["Department"].ToString();
                        row["GroupName"] = DT.Rows[i]["GroupName"].ToString();
                        row["LOGIN"] = DT.Rows[i]["LOGIN"].ToString();
                        row["TeacherName"] = DT.Rows[i]["TeacherName"].ToString();
                        row["code"] = DT.Rows[i]["code"].ToString();
                        row["LessonName"] = DT.Rows[i]["LessonName"].ToString();
                        row["SumOfTime"] = DT.Rows[i]["SumOfTime"].ToString();
                        row["TimeClass"] = DT.Rows[i]["TimeClass"].ToString();
                        row["SessionCount"] = DT.Rows[i]["SessionCount"].ToString();
                        row["Hozoor"] = DT.Rows[i]["Hozoor"].ToString();
                        row["Leniency"] = DT.Rows[i]["Leniency"].ToString();
                        row["tterm"] = DT.Rows[i]["tterm"].ToString();
                        DT3.Rows.Add(row);
                    }
                }
                grd_TAPresentReport.DataSource = DT3;
            }
            else
                grd_TAPresentReport.DataSource = TABusiness.GetAllTimeOfEachClass(Leniency, Nimsal, Department);


            //grd_TAPresentReport.DataSource = TABusiness.GetAllTimeOfEachClass(Leniency, Nimsal, Department);
            grd_TAPresentReport.DataBind();
        }

        protected void ConvertExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (grd_TAPresentReport.MasterTableView.Items.Count > 0)
            {
                grd_TAPresentReport.ExportSettings.FileName = string.Format("ProfessorTime_{0}_{1}", "Teacher", DateTime.Now);
                grd_TAPresentReport.ExportSettings.IgnorePaging = true;
                grd_TAPresentReport.ExportSettings.OpenInNewWindow = true;
                grd_TAPresentReport.ExportSettings.ExportOnlyData = true;
                grd_TAPresentReport.MasterTableView.UseAllDataFields = true;
                grd_TAPresentReport.MasterTableView.ExportToExcel();
            }
        }







    }
}