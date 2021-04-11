using System;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Request;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class checkOutPrintClassPresent : System.Web.UI.Page
    {
        CheckOutRequestBusiness business = new CheckOutRequestBusiness();
        CommonBusiness CB = new CommonBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int reqID = Convert.ToInt32(Request.QueryString["r"]);
                int s = Convert.ToInt32(Request.QueryString["s"]);
                CheckOutStatusEnum.FareghReqStatus status = new CheckOutStatusEnum.FareghReqStatus();
                status = (CheckOutStatusEnum.FareghReqStatus)s;
                string stcode = business.GetCheckOutStudentIDByReqID(reqID);
                this.StiWebViewer1.ResetReport();
                StiWebViewer1.Visible = true;
                StiReport rpt = new StiReport();
                rpt.Load(Server.MapPath("../Reports/CheckOutBankInfo.mrt"));
                rpt.ReportCacheMode = StiReportCacheMode.On;
                rpt.Dictionary.Databases.Clear();
                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                rpt.Compile();
                rpt.CompiledReport.DataSources["[Request].[SP_GetBankAcountInfoCheckOut]"].Parameters["@stcode"].ParameterValue = stcode;
                rpt.CompiledReport.DataSources["[Request].[SP_GetStudentInfoByStCode]"].Parameters["@stcode"].ParameterValue = stcode;
                DataTable dtResault = new DataTable();
                rpt.RegData(dtResault);
                StiWebViewer1.Report = rpt;
                StiWebViewer1.Visible = true;
            }
        }
    }
}