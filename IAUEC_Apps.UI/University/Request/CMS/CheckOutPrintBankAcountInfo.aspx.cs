using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Request;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutPrintBankAcountInfo : System.Web.UI.Page
    {
        readonly CheckOutRequestBusiness _business = new CheckOutRequestBusiness();
        CommonBusiness CB = new CommonBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var reqId = Convert.ToInt32(Request.QueryString["r"]);
            var stcode = _business.GetCheckOutStudentIDByReqID(reqId);
            var report = new StiReport();
            report.Load(Server.MapPath("../Reports/CheckOutBankInfo.mrt"));
            report.Dictionary.Databases.Clear();
            report.Dictionary.Databases.AddRange(new StiDatabase[2] { new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()), new StiSqlDatabase("Connection2", CB.ReportConnection.ToString()) });
            report.Dictionary.Variables["StCode"].Value = stcode;
            StiWebViewer1.ResetReport();
            StiWebViewer1.Report = report;
            StiWebViewer1.Visible = true;

            // int reqID = Convert.ToInt32(Request.QueryString["r"]);
            // int s = Convert.ToInt32(Request.QueryString["s"]);
            // CheckOutStatusEnum.FareghReqStatus status = new CheckOutStatusEnum.FareghReqStatus();
            // status = (CheckOutStatusEnum.FareghReqStatus)s;
            // string stcode = business.GetCheckOutStudentIDByReqID(reqID);
            // this.StiWebViewer1.ResetReport();
            // StiWebViewer1.Visible = true;
            // StiReport rpt = new StiReport();
            // rpt.Load(Server.MapPath("../Reports/CheckOutBankInfo.mrt"));
            // rpt.ReportCacheMode = StiReportCacheMode.On;
            // rpt.Dictionary.Databases.Clear();
            //// rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
            // rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", "Data Source=192.168.1.21;Initial Catalog=Supplementary_test;Password=t123*t456;User ID=teamuser"));
            // rpt.Compile();
            // rpt.CompiledReport.DataSources["[Request].[SP_GetBankAcountInfoCheckOut]"].Parameters["@stcode"].ParameterValue = 99900999;
            // //rpt.CompiledReport.DataSources["[Request].[SP_GetStudentInfoByStCode]"].Parameters["@stcode"].ParameterValue = 99900999;
            // DataTable dtResault = new DataTable();
            // rpt.RegData(dtResault);
            // StiWebViewer1.Report = rpt;
            // StiWebViewer1.Visible = true;
        }
    }
}