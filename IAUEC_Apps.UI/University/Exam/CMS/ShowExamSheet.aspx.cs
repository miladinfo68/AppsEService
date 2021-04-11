using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.Business.university.Request;
using System.Data;


namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ShowExamSheet : System.Web.UI.Page
    {
        ExamBusiness EBusiness = new ExamBusiness();
        RequestStudentCartBusiness CBusiness = new RequestStudentCartBusiness();
        CommonBusiness CB = new CommonBusiness();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string did = Request.QueryString["did"].ToString();

            string whichBtn = Request.QueryString["whichBtn"].ToString();

            CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 68, " پرینت پاسخنامه مشخصه " + did);
            this.StiWebViewer1.ResetReport();
            DataTable dt1 = new DataTable();
            dt1 = EBusiness.ExamAnswerSheetbyDid(did, int.Parse(Session[sessionNames.userID_Karbar].ToString()));
            StiWebViewer1.Visible = true;
            StiReport rpt = new StiReport();

            string targetReport = "";
            if (whichBtn == "btn_printExamSheet")
            {
                targetReport = Server.MapPath("../Reports/AnswerSheet1.mrt");
            }
            else if (whichBtn == "btn_printExamSheetByA4Format")
            {
                targetReport = Server.MapPath("../Reports/AnswerSheetA4.mrt");
            }

            //rpt.Load(Server.MapPath("../Reports/AnswerSheet1.mrt"));
            rpt.Load(targetReport);
            rpt.Dictionary.Databases.Clear();
            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Supplementary", CB.ReportConnection.ToString()));
            rpt.Compile();
            rpt.CompiledReport.DataSources["[Exam].[SP_ExamAnswerSheetbyDid]"].Parameters["@did"].ParameterValue = int.Parse(did);
            rpt.CompiledReport.DataSources["[Exam].[SP_ExamAnswerSheetbyDid]"].Parameters["@ExaminerID"].ParameterValue = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            rpt.RegData(dt1);
            rpt.ReportCacheMode = StiReportCacheMode.On;
            StiWebViewer1.Report = rpt;
        }

    }
}