using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.Business.university.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class RequestDistributionOfStudent : System.Web.UI.Page
    {
        RequestDistributionOfStudentBusiness RequestDistributionOfStudentBusiness = new RequestDistributionOfStudentBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> ProvincetList = RequestDistributionOfStudentBusiness.GetAllProvince();
                foreach (var item in ProvincetList)
                    ddl_Province.Items.Add(item.ToString());
            }
        }


        protected void btn2_Click(object sender, EventArgs e)
        {
            string Province = "";
            if (ddl_Province.Text != "همه")

                Province = ddl_Province.Text;
            DataTable dt = new DataTable();
            dt = RequestDistributionOfStudentBusiness.GetProvinceStudent(Province);
            grd_StudentPerProvince.DataSource = dt;
            grd_StudentPerProvince.DataBind();
        }

        protected void ConvertExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (grd_StudentPerProvince.MasterTableView.Items.Count > 0)
            {
                grd_StudentPerProvince.ExportSettings.FileName = string.Format("DistributionOfStudentIn-" + DateTime.Now.ToString());
                grd_StudentPerProvince.ExportSettings.IgnorePaging = true;
                grd_StudentPerProvince.ExportSettings.OpenInNewWindow = true;
                grd_StudentPerProvince.ExportSettings.ExportOnlyData = true;
                grd_StudentPerProvince.MasterTableView.UseAllDataFields = true;
                grd_StudentPerProvince.MasterTableView.ExportToExcel();
            }
        }






    }
}