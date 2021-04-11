using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.Adobe.ManagementPanel.Pages
{
    public partial class C_FindUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorGuide") != null)
                    lbl_ProfessorGuide.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorGuide");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorAdvisor") != null)
                    lbl_ProfessorAdvisor.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorAdvisor");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorRefereeOne") != null)
                    lbl_ProfessorRefereeOne.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorRefereeOne");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorRefereeTwo") != null)
                    lbl_ProfessorRefereeTwo.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorRefereeTwo");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("Student") != null)
                    lbl_Student.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("Student");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("Type") != null)
                    lbl_Type.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("Type");

                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClassName") != null)
                    lbl_ClassName.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClassName");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("LatinClassName") != null)
                    lbl_LatinClassName.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("LatinClassName");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("CountOfClass") != null)
                    lbl_CountOfClass.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("CountOfClass");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("CountOfUser") != null)
                    lbl_CountOfUser.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("CountOfUser");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateStart") != null)
                    lbl_DateStart.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateStart");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateEnd") != null)
                    lbl_DateEnd.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateEnd");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateDef") != null)
                    lbl_DateDef.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateDef");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClassType") != null)
                    lbl_ClassType.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClassType");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("UserType") != null)
                    lbl_UserType.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("UserType");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClasDayTime") != null)
                    lbl_ClasDayTime.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClasDayTime");

                lbl_FinderTitle.Text = "جستجوی: " + lbl_Type.Text;
            }
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            string Name = txtName.Text;
            string Family = txtFamily.Text;
            string NationalCode = txtNationalCode.Text;

            DataTable DTDate = new DataTable();
            DataTable DTSource = new DataTable();
            DTSource.Columns.Add("ProfessorGuide", typeof(string));
            DTSource.Columns.Add("ProfessorAdvisor", typeof(string));
            DTSource.Columns.Add("ProfessorRefereeOne", typeof(string));
            DTSource.Columns.Add("ProfessorRefereeTwo", typeof(string));
            DTSource.Columns.Add("Student", typeof(string));
            DTSource.Columns.Add("Type", typeof(string));

            DTSource.Columns.Add("ClassName", typeof(string));
            DTSource.Columns.Add("LatinClassName", typeof(string));
            DTSource.Columns.Add("CountOfClass", typeof(string));
            DTSource.Columns.Add("CountOfUser", typeof(string));
            DTSource.Columns.Add("DateStart", typeof(string));
            DTSource.Columns.Add("DateEnd", typeof(string));
            DTSource.Columns.Add("DateDef", typeof(string));
            DTSource.Columns.Add("ClassType", typeof(string));
            DTSource.Columns.Add("UserType", typeof(string));
            DTSource.Columns.Add("ClasDayTime", typeof(string));

            DTSource.Columns.Add("UserId", typeof(string));//StudentCode Or ProfCode
            DTSource.Columns.Add("Name", typeof(string)); // Name
            DTSource.Columns.Add("Family", typeof(string)); // Family
            DTSource.Columns.Add("FatherName", typeof(string)); // FatherName
            DTSource.Columns.Add("Id", typeof(string)); // شماره شناسنامه
            DTSource.Columns.Add("NationalCode", typeof(string)); // کدملی

            ManagementPanelBusiness MPB = new ManagementPanelBusiness();





            if (lbl_Type.Text == "Student")
                DTDate = MPB.Get_Student_ByName_Family_NationalCode(Name, Family, NationalCode);
            else if (lbl_Type.Text != "")
                DTDate = MPB.Get_Professor_ByName_Family_NationalCode(Name, Family, NationalCode);



            for (int i = 0; i < DTDate.Rows.Count; i++)
            {
                DataRow row = DTSource.NewRow();
                row["ProfessorGuide"] = lbl_ProfessorGuide.Text;
                row["ProfessorAdvisor"] = lbl_ProfessorAdvisor.Text;
                row["ProfessorRefereeOne"] = lbl_ProfessorRefereeOne.Text;
                row["ProfessorRefereeTwo"] = lbl_ProfessorRefereeTwo.Text;
                row["Student"] = lbl_Student.Text;
                row["Type"] = lbl_Type.Text;

                row["ClassName"] = lbl_ClassName.Text;
                row["LatinClassName"] = lbl_LatinClassName.Text;
                row["CountOfClass"] = lbl_CountOfClass.Text;
                row["CountOfUser"] = lbl_CountOfUser.Text;
                row["DateStart"] = lbl_DateStart.Text;
                row["DateEnd"] = lbl_DateEnd.Text;
                row["DateDef"] = lbl_DateDef.Text;
                row["ClassType"] = lbl_ClassType.Text;
                row["UserType"] = lbl_UserType.Text;
                row["ClasDayTime"] = lbl_ClasDayTime.Text;                

                row["UserId"] = DTDate.Rows[i]["Code"].ToString();
                row["Name"] = DTDate.Rows[i]["name"].ToString();
                row["Family"] = DTDate.Rows[i]["family"].ToString();
                row["FatherName"] = DTDate.Rows[i]["namep"].ToString();
                row["Id"] = DTDate.Rows[i]["id"].ToString();
                row["NationalCode"] = DTDate.Rows[i]["idd_meli"].ToString();

                DTSource.Rows.Add(row);
            }


            RadGrid1.DataSource = DTSource;
            RadGrid1.DataBind();
        }











    }
}