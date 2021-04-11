using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using System.Data;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class StuPresent_StudentClassInfo : System.Web.UI.Page
    {
        public DateTime TermTimeStart = new DateTime(2014, 09, 15);
        public DateTime TermTimeEnd = new DateTime(2015, 01, 06);
        StuPresentBusiness StuPresentBusiness = new StuPresentBusiness();

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
                List<string> TermList = StuPresentBusiness.GetActiveTerm();
                foreach (var item in TermList)
                    ddl_Nimsal.Items.Add(item.ToString());

                string stcode = "";
                string ClassCode = "";
                string tterm = "";
                stcode = HttpUtility.ParseQueryString(this.ClientQueryString).Get("stcode");
                ClassCode = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClassCode");
                tterm = HttpUtility.ParseQueryString(this.ClientQueryString).Get("tterm");
                txt_Stcode.Text = stcode;
                txt_ClassCode.Text = ClassCode;
                ddl_Nimsal.SelectedValue = tterm;
                if (stcode != "" && stcode != null && ClassCode != "" && ClassCode != null)
                    Result(stcode, ClassCode, tterm);
                
            }
           
        }

        protected void btn_StudentFinder_Click(object sender, EventArgs e)
        {
            string Stcode="0";
            if (txt_Stcode.Text != "")
                Stcode = txt_Stcode.Text;
            string ClassCode = "0";
            if (txt_ClassCode.Text != "")
                ClassCode = txt_ClassCode.Text;

            Result(Stcode, ClassCode, ddl_Nimsal.SelectedValue.ToString());           
        }


        public void Result(string StudentCode, string ClassCode, string tterm)
        {
            
            DataTable DT = StuPresentBusiness.TimeResult(StudentCode, ClassCode, TermTimeStart, TermTimeEnd,tterm);

            if (DT.Rows.Count > 0)
            {
                DataTable DT3 = new DataTable();
                DT3.Columns.Add("PersianDate", typeof(string));
                DT3.Columns.Add("TimeStart", typeof(string));
                DT3.Columns.Add("TimeEND", typeof(string));
                DT3.Columns.Add("SumOfTime", typeof(string));
                DT3.Columns.Add("TimeClass", typeof(string));

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DataRow row = DT3.NewRow();
                    row["PersianDate"] = DT.Rows[i]["PersianDate"].ToString();
                    row["TimeStart"] = DT.Rows[0]["TimeStart"].ToString();
                    row["TimeEND"] = DT.Rows[0]["TimeEND"].ToString();
                    row["SumOfTime"] = DT.Rows[i]["SumOfTime"].ToString();
                    row["TimeClass"] = DT.Rows[i]["TimeClass"].ToString();

                    DT3.Rows.Add(row);
                }

                lbl_StudentName.Text = "نام دانشجو: " + DT.Rows[0]["StudentName"].ToString();
                lbl_ClassCode.Text = "مشخصه کلاس: " + DT.Rows[0]["code"].ToString();
                lbl_LessonName.Text = "نام درس: " + DT.Rows[0]["LessonName"].ToString();
                lbl_ProfName.Text = "نام استاد: " + DT.Rows[0]["TeacherName"].ToString();


                grd_StuPeresentStuClassInfo.DataSource = DT3;
                grd_StuPeresentStuClassInfo.DataBind();
            }
            else
            {
                lbl_StudentName.Text = "";
                lbl_ClassCode.Text = "";
                lbl_LessonName.Text = "";
                lbl_ProfName.Text = "";
                string script = "alert(\" هیچ اطلاعاتی پیدا نشد \");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                DataTable DT0 = new DataTable();
                grd_StuPeresentStuClassInfo.DataSource = DT0;
                grd_StuPeresentStuClassInfo.DataBind();
            }


            
            
        }

        protected void ConvertExcel_Click(object sender, ImageClickEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            if (grd_StuPeresentStuClassInfo.MasterTableView.Items.Count > 0)
            {
                string Time = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
                    + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString()
                    + DateTime.Now.Second.ToString();
                grd_StuPeresentStuClassInfo.ExportSettings.FileName = string.Format("StudentTime_{0}_{1}", txt_Stcode.Text, Time);
                grd_StuPeresentStuClassInfo.ExportSettings.IgnorePaging = true;
                grd_StuPeresentStuClassInfo.ExportSettings.OpenInNewWindow = true;
                grd_StuPeresentStuClassInfo.ExportSettings.ExportOnlyData = true;
                grd_StuPeresentStuClassInfo.MasterTableView.UseAllDataFields = true;
                grd_StuPeresentStuClassInfo.MasterTableView.ExportToExcel();
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 19, "مدت زمان حضور دانشجو در یک  کلاس");

            }
        }



    }
}