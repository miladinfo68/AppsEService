using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.Imaging;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using ResourceControl.BLL;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Export;


namespace IAUEC_Apps.UI.ResourceControl.Forms
{

    public partial class SessionPrint : System.Web.UI.Page
    {
        CommonBusiness CB = new CommonBusiness();
   

        //protected virtual void OnProcessExport(object sender)
        //{
        //    StiExportService service = sender as StiExportService;
        //    service.Export(CurrentReport, null, false);
        //}
        protected void Page_Load(object sender, EventArgs e)
        {

            //StiWebViewer1.ShowExportToBmp = false;
            //StiWebViewer1.ShowExportToCsv = false;
            //StiWebViewer1.ShowExportToDbf = false;
            //StiWebViewer1.ShowExportToDif = false;
            //StiWebViewer1.ShowExportToDocument = false;
            //StiWebViewer1.ShowExportToExcel = false;
            //StiWebViewer1.ShowExportToExcel2007 = false;
            //StiWebViewer1.ShowExportToExcelXml = false;
            //StiWebViewer1.ShowExportToGif = false;
            //StiWebViewer1.ShowExportToHtml = false;
            //StiWebViewer1.ShowExportToJpeg = false;
            //StiWebViewer1.ShowExportToPng = false;
            //StiWebViewer1.ShowExportToMetafile = false;
            //StiWebViewer1.ShowExportToText = false;
            //StiWebViewer1.ShowExportToXps = false;
            //StiWebViewer1.ShowExportToPowerPoint = false;
            //StiWebViewer1.ShowExportToMht = false;
            //StiWebViewer1.ShowExportToTiff = false;
            //StiWebViewer1.ShowExportToWord2007 = false;
            //StiWebViewer1.ShowExportToPcx = false;
            //StiWebViewer1.ShowExportToSvg = false;
            //StiWebViewer1.ShowExportToSvgz = false;
            //StiWebViewer1.ShowExportToSvgz = false;
            //StiWebViewer1.ShowExportToXps = false;
            //StiWebViewer1.ShowExportToXml = false;
            //StiWebViewer1.ShowExportToRtf = false;
            //StiWebViewer1.ShowExportToOdt = false;
            //StiWebViewer1.ShowExportToOds = false;


            try
            {
                var splitedDefenceSessionInformation = Session["PrintDefenceSession"]?.ToString()?.Split('-');
                if (splitedDefenceSessionInformation.Count() == 5)
                {
                    if (!(splitedDefenceSessionInformation[0] == null && splitedDefenceSessionInformation[1] == null &&
                      splitedDefenceSessionInformation[2] == null && splitedDefenceSessionInformation[3] == null
                      ))
                    {
                        if (splitedDefenceSessionInformation[4] == null)
                        {
                            splitedDefenceSessionInformation[4] = "false";
                        }
                        //StiReport rpt = CreateDefenceSessionReport(splitedDefenceSessionInformation[0], splitedDefenceSessionInformation[1], Convert.ToInt32(splitedDefenceSessionInformation[2]));
                        StiReport rpt = CreateDefenceSessionReport_Manual(splitedDefenceSessionInformation[0], splitedDefenceSessionInformation[1], 0, splitedDefenceSessionInformation[3],bool.Parse(splitedDefenceSessionInformation[4].ToString()));
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        StiWebViewer1.PdfEmbeddedFonts = true;
                        //StiWebViewer1.Font=new FontInfo();
                    }
                }
                else 
                if (!(splitedDefenceSessionInformation[0] == null && splitedDefenceSessionInformation[1] == null))
                {
                    if(splitedDefenceSessionInformation[3]==null)
                    {
                        splitedDefenceSessionInformation[3] = "false";
                    }    
                    //StiReport rpt = CreateDefenceSessionReport(splitedDefenceSessionInformation[0], splitedDefenceSessionInformation[1], Convert.ToInt32(splitedDefenceSessionInformation[2]));
                    StiReport rpt = CreateDefenceSessionReport(splitedDefenceSessionInformation[0], splitedDefenceSessionInformation[1], 0,bool.Parse(splitedDefenceSessionInformation[3].ToString()));
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                    StiWebViewer1.PdfEmbeddedFonts = true;
                    //StiWebViewer1.Font=new FontInfo();

                }
            }
            catch(Exception x)
            {
                // ignored
                //throw x;
            }
        }

        private StiReport CreateDefenceSessionReport(string requestId, string studentCode, int defenceSessionDate, bool bySign=false)
        {
            CheckOutRequestBusiness _reqBusiness = new CheckOutRequestBusiness();
            var entryYear = _reqBusiness.GetSaleVoroodByStCode(studentCode);

            StiReport rpt = new StiReport();
          

            if (!bySign)
            {
                if (Convert.ToUInt32(entryYear) < 95)
                {
                    //کمی
                    rpt.Load(Server.MapPath("../report/QuantityDefenceSession.mrt"));
                }
                else
                {
                    //کیفی
                    rpt.Load(Server.MapPath("../report/QualityDefenceSession.mrt"));
                }
                rpt.Dictionary.Databases.Clear();

                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.SupplementaryReportConnection));
                rpt.Compile();
                rpt.CompiledReport
                        .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                        .Parameters["@stcode"]
                        .ParameterValue =
                    studentCode;
                rpt.CompiledReport
                    .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                    .Parameters["@requestId"]
                    .ParameterValue = Convert.ToInt32(requestId);

                rpt.CompiledReport
                    .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                    .Parameters["@defenceSessionDate"]
                    .ParameterValue = defenceSessionDate;
                return rpt;
            }
            else
            {

                if (Convert.ToUInt32(entryYear) < 95)
                {
                    //کمی
                    rpt.Load(Server.MapPath("../report/QuantityDefenceSession_BySign.mrt"));
                }
                else
                {
                    //کیفی
                    rpt.Load(Server.MapPath("../report/QualityDefenceSession_BySign.mrt"));
                   
                }
                rpt.Dictionary.Databases.Clear();

                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.SupplementaryReportConnection));
                rpt.Compile();
                rpt.CompiledReport
                        .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                        .Parameters["@stcode"]
                        .ParameterValue =
                    studentCode;
                rpt.CompiledReport
                    .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                    .Parameters["@requestId"]
                    .ParameterValue = Convert.ToInt32(requestId);

                rpt.CompiledReport
                    .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                    .Parameters["@defenceSessionDate"]
                    .ParameterValue = defenceSessionDate;

                RequestHandler _requestHandler = new RequestHandler();
                var address = Request.Url.OriginalString.Replace(Request.Url.PathAndQuery, "") + Request.ApplicationPath; //Server.MapPath("../../").ToString();
                var signs = _requestHandler.GetSignutreOstadByImage(studentCode, address).OrderBy(c => c.IdTypeOs);
                var score = _requestHandler.GetScoreForDefence(Convert.ToInt32(requestId));
             
                rpt["modirSign"]= signs.Where(c => c.IdTypeOs == 1)?.FirstOrDefault()?.singAddress;
                rpt["osMoshSign"]= signs.Where(c => c.IdTypeOs == 2)?.FirstOrDefault()?.singAddress;
                rpt["osRahSign"] = signs.Where(c => c.IdTypeOs == 3)?.FirstOrDefault()?.singAddress;
                rpt["osDavInSign"]= signs.Where(c => c.IdTypeOs == 4)?.FirstOrDefault()?.singAddress;
                rpt["osDOSign"]= signs.Where(c => c.IdTypeOs == 5)?.FirstOrDefault()?.singAddress;
           
                if (Convert.ToUInt32(entryYear) < 95)
                {
                    //کمی
                    rpt["Score"] = (score.Score);
                    rpt["ScoreLetters"] = score.ScoreLetters;
                }
                else
                {
                    //کیفی
                    rpt["Degree"] = UtilityFunction.ConvertScoreToDegree((score.Score == null ? -1 : score.Score.Value));

                }
               


                return rpt;

            }
        }
        private StiReport CreateDefenceSessionReport_Manual(string requestId, string studentCode, int defenceSessionDate, string defenceDate, bool bySign = false)
        {
            CheckOutRequestBusiness _reqBusiness = new CheckOutRequestBusiness();
            var entryYear = _reqBusiness.GetSaleVoroodByStCode(studentCode);

            StiReport rpt = new StiReport();
            if (!bySign)
            {

                if (Convert.ToUInt32(entryYear) < 95)
                {
                    //کمی
                    rpt.Load(Server.MapPath("../report/QuantityDefenceSession_Manual.mrt"));
                }
                else
                {
                    //کیفی
                    rpt.Load(Server.MapPath("../report/QualityDefenceSession_Manual.mrt"));
                }
                rpt.Dictionary.Databases.Clear();

                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.SupplementaryReportConnection));
                rpt.Compile();
                rpt.CompiledReport
                        .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                        .Parameters["@stcode"]
                        .ParameterValue =
                    studentCode;
                rpt.CompiledReport
                    .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                    .Parameters["@requestId"]
                    .ParameterValue = Convert.ToInt32(requestId);

                rpt.CompiledReport
                    .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                    .Parameters["@defenceSessionDate"]
                    .ParameterValue = defenceSessionDate;

                rpt["defenceDate"] = defenceDate;
                return rpt;
            }
            else
            {
                if (Convert.ToUInt32(entryYear) < 95)
                {
                    //کمی
                    rpt.Load(Server.MapPath("../report/QuantityDefenceSession_Manual_BySign.mrt"));
                }
                else
                {
                    //کیفی
                    rpt.Load(Server.MapPath("../report/QualityDefenceSession_Manual_BySign.mrt"));
                }
                rpt.Dictionary.Databases.Clear();

                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.SupplementaryReportConnection));
                rpt.Compile();
                rpt.CompiledReport
                        .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                        .Parameters["@stcode"]
                        .ParameterValue =
                    studentCode;
                rpt.CompiledReport
                    .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                    .Parameters["@requestId"]
                    .ParameterValue = Convert.ToInt32(requestId);

                rpt.CompiledReport
                    .DataSources["[Resource_Control].[GetDefenceSessionInformation]"]
                    .Parameters["@defenceSessionDate"]
                    .ParameterValue = defenceSessionDate;

                rpt["defenceDate"] = defenceDate;
                RequestHandler _requestHandler = new RequestHandler();
                var address = Request.Url.OriginalString.Replace(Request.Url.PathAndQuery, "") + Request.ApplicationPath; //Server.MapPath("../../").ToString();
                var signs = _requestHandler.GetSignutreOstadByImage(studentCode, address).OrderBy(c => c.IdTypeOs);
                var score = _requestHandler.GetScoreForDefence(Convert.ToInt32(requestId));

                rpt["modirSign"] = signs.Where(c => c.IdTypeOs == 1)?.FirstOrDefault()?.singAddress;
                rpt["osMoshSign"] = signs.Where(c => c.IdTypeOs == 2)?.FirstOrDefault()?.singAddress;
                rpt["osRahSign"] = signs.Where(c => c.IdTypeOs == 3)?.FirstOrDefault()?.singAddress;
                rpt["osDavInSign"] = signs.Where(c => c.IdTypeOs == 4)?.FirstOrDefault()?.singAddress;
                rpt["osDOSign"] = signs.Where(c => c.IdTypeOs == 5)?.FirstOrDefault()?.singAddress;
       
                if (Convert.ToUInt32(entryYear) < 95)
                {
                    //کمی
                    rpt["Score"] = (score.Score);
                    rpt["ScoreLetters"] = score.ScoreLetters;
                }
                else
                {
                    //کیفی
                    rpt["Degree"] = UtilityFunction.ConvertScoreToDegree((score.Score == null ? -1 : score.Score.Value));

                }
                return rpt;
            }
        }
        }
      
       // public Image Getimage
    }

