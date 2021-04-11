using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using System.Data;

namespace IAUEC_Apps.UI.University.Exam.MasterPages
{

    public partial class ExamShowClockDate : System.Web.UI.Page
    {
        ExamShowClockBusiness ClockDateBusiness = new ExamShowClockBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt = ClockDateBusiness.ShowTerm();
                ddl_Term.DataSource = dt;
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataBind();
            }

        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            DataTable dt_ShowInfo = new DataTable();
            ddl_Term.DataTextField = "tterm";
            dt_ShowInfo = ClockDateBusiness.SelectTerm(ddl_Term.SelectedValue);
            grd_ShowExam.DataSource = dt_ShowInfo;
            grd_ShowExam.DataBind();
        }
    }
}