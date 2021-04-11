using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.GraduateAffair;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Education;

namespace IAUEC_Apps.UI.University.Education.CMS
{
    public partial class RegistrationOfStudent : System.Web.UI.Page
    {
        public static RegisterOfStudentDTO RSD = new RegisterOfStudentDTO();
        UniversityBusiness UB = new UniversityBusiness();
        GraduateReportBusiness GraduateBusiness = new GraduateReportBusiness();
        DataTable dt = new DataTable();
        //ramezanian
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(Request.QueryString["Degree"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Field"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Education"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Sex"].ToString()) || (!string.IsNullOrWhiteSpace(Request.QueryString["salvorud"].ToString()))))
            {
                lbl_Degree.Text = Request.QueryString["Degree"].ToString();
                lbl_Education.Text = Request.QueryString["Education"].ToString();
                lbl_Field.Text = Request.QueryString["Field"].ToString();
                lbl_Sex.Text = Request.QueryString["Sex"].ToString();
                lbl_Salvorud.Text=Request.QueryString["salvorud"].ToString();
            }
        }

        //ramezanian
        //از متد خانم بهرامی استفاده کردم
        protected void grd_Student_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = UB.GetTop50Student();
            grd_Student.DataSource = dt;
        }
        //ramezanian
        protected void grd_Student_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName =="Select")
            {
                
                    Session["stcode"] =e.CommandArgument;
                    ClientScript.RegisterStartupScript(Page.GetType(), "Select", "CloseAndRebind();", true);
            }
        }
        public string generaterandomstr()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        }
    }
