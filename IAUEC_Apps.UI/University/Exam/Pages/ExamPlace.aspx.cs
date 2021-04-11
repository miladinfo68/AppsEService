using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using IAUEC_Apps.Business.Common;
using System.Text;

namespace IAUEC_Apps.UI.University.Exam.Pages
{
    public partial class ExamPlace : System.Web.UI.Page
    {

        ExamBusiness ExamBusiness = new ExamBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = null;//ExamBusiness.CardQuizStudents(Session[sessionNames.userID_StudentOstad].ToString());
            if (dt !=null && dt.Rows.Count > 0)
            {
                lblExam.Text = dt.Rows[0]["adres"].ToString();
            }
        }

        protected void grd_Class_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grd_Class.DataSource = null;//ExamBusiness.CardQuizStudents(Session[sessionNames.userID_StudentOstad].ToString());
        }
        
    }
}