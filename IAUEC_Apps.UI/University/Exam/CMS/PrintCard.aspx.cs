using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using IAUEC_Apps.Business.university.Exam;
using System.Web.UI.WebControls;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report;

using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class PrintCard : System.Web.UI.Page
    {
        ExamBusiness ebusiness = new ExamBusiness();
      
        CommonBusiness CB = new CommonBusiness();
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        public static double bedehi;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            DataTable dtbedehi = new DataTable();
            dtbedehi = GovahiBusiness.GetBedehkar(txt_StNo.Text);
            bedehi = Convert.ToDouble((dtbedehi.Rows[0]["bedehi"].ToString()));
            DataTable dtmojazcart = new DataTable();
            dtmojazcart = ebusiness.StInMojazCart(txt_StNo.Text);
            if (dtmojazcart.Rows.Count == 0 && bedehi > 0)
            {
                rwm.RadAlert("به علت بدهکاری دانشجو، امکان دریافت کارت مقدور نمی باشد", null, 100, "پیام", "");
            }
            else
            {
                DataTable dt = new DataTable();
                dt = ebusiness.CardQuizStudents(txt_StNo.Text);
                this.StiWebViewer1.ResetReport();
                StiWebViewer1.Visible = true;
                StiReport rpt = new StiReport();
                rpt.Load(Server.MapPath("../Reports/Exam123.mrt"));
                rpt.Dictionary.Databases.Clear();
                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Supplementary", CB.ReportConnection.ToString()));
                rpt.Compile();
                rpt.CompiledReport.DataSources["[Exam].[SP_CardQuiz]"].Parameters["@stcode"].ParameterValue = txt_StNo.Text;
                rpt.RegData(dt);
                StiWebViewer1.Report = rpt;
                StiWebViewer1.Visible = true;
                CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 45, "چاپ کارت امتحان دانشجو");


            }


        }
    }
}