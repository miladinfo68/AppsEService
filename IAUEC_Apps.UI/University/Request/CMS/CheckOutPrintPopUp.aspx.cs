using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Request;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutPrintPopUp : System.Web.UI.Page
    {
        CheckOutRequestBusiness business = new CheckOutRequestBusiness();
        CommonBusiness CB = new CommonBusiness();
        DataTable _signList = null;

        enum imagetype
        {
            mashmool,
            woman
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int reqID = Convert.ToInt32(Request.QueryString["r"]);
                DataTable request = business.GetCheckOutInfoByReqId(reqID);
                int s = Convert.ToInt32(request.Rows[0]["RequestLogId"]);
                issuerID = business.GetCheckOutStudentIDByReqID(reqID);
                
                CheckOutStatusEnum.FareghReqStatus status = new CheckOutStatusEnum.FareghReqStatus();
                status = (CheckOutStatusEnum.FareghReqStatus)s;
                _signList = business.GetAllSigns();
                //string stcode = business.GetCheckOutStudentIDByReqID(reqID);
                this.StiWebViewer1.ResetReport();
                StiWebViewer1.Visible = true;
                StiReport rpt = new StiReport();
                rpt.Load(Server.MapPath("../Reports/CheckOutPrint2.mrt"));
                rpt.ReportCacheMode = StiReportCacheMode.On;
                rpt.Dictionary.Databases.Clear();
                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));


                if (status >= CheckOutStatusEnum.FareghReqStatus.amoozesh_ok)
                {
                    StiImage amoozesh = rpt.GetComponents()["Table1_Cell10"] as StiImage;
                    amoozesh.Image = byteArrayToImage((int)CheckOutStatusEnum.FareghReqStatus.amoozesh_ok, reqID);
                }
                if (status >= CheckOutStatusEnum.FareghReqStatus.daneshjooyi_ok)
                {
                    StiImage daneshjooyi = rpt.GetComponents()["Table1_Cell16"] as StiImage;
                    daneshjooyi.Image = byteArrayToImage((int)CheckOutStatusEnum.FareghReqStatus.daneshjooyi_ok, reqID);
                }
                if (status >= CheckOutStatusEnum.FareghReqStatus.pajohesh_ok)
                {
                    StiImage pajoohesh = rpt.GetComponents()["Table1_Cell22"] as StiImage;
                    pajoohesh.Image = byteArrayToImage((int)CheckOutStatusEnum.FareghReqStatus.pajohesh_ok, reqID);
                }
                if (status >= CheckOutStatusEnum.FareghReqStatus.refah_ok)
                {
                    StiImage refah = rpt.GetComponents()["Table1_Cell28"] as StiImage;
                    refah.Image = byteArrayToImage((int)CheckOutStatusEnum.FareghReqStatus.refah_ok, reqID);
                }
                if (status >= CheckOutStatusEnum.FareghReqStatus.maali_ok)
                {
                    StiImage maali = rpt.GetComponents()["Table1_Cell7"] as StiImage;
                    maali.Image = byteArrayToImage((int)CheckOutStatusEnum.FareghReqStatus.maali_ok, reqID);
                }

                if (status >= CheckOutStatusEnum.FareghReqStatus.mashmulan_ok)
                {
                    StiImage mashmoolan = rpt.GetComponents()["Table1_Cell19"] as StiImage;
                    bool isMale = business.isMale(issuerID);
                    if (isMale)
                    {
                        bool ismashmool = isMashmool(issuerID);
                        if (ismashmool)
                        {
                            bool bayganiOk = Convert.ToBoolean(request.Rows[0]["BayganiOk"]);
                            if (bayganiOk)
                            {
                                mashmoolan.Image = byteArrayToImage((int)CheckOutStatusEnum.FareghReqStatus.mashmulan_ok, reqID);
                            }
                            else
                            {
                                mashmoolan.Image = GetImageFromFile(imagetype.mashmool);
                            }
                        }
                        else
                        {
                            mashmoolan.Image = byteArrayToImage((int)CheckOutStatusEnum.FareghReqStatus.mashmulan_ok, reqID);
                        }
                    }
                    else
                    {
                        mashmoolan.Image = GetImageFromFile(imagetype.woman);
                    }
                }

                if (status >= CheckOutStatusEnum.FareghReqStatus.fani_ok)
                {
                    StiImage fani = rpt.GetComponents()["Table1_Cell13"] as StiImage;
                    fani.Image = byteArrayToImage((int)CheckOutStatusEnum.FareghReqStatus.fani_ok, reqID);
                }
                if (status >= CheckOutStatusEnum.FareghReqStatus.archive_ok)
                {
                    DataTable dtUserID = new DataTable();
                    dtUserID = business.getArchiveUserSignByStudentStcode(issuerID);
                    decimal userID = 0;
                    if (dtUserID.Rows[0]["userID"] != DBNull.Value)
                    {
                        userID = Convert.ToDecimal(dtUserID.Rows[0]["userID"]);
                    }

                    StiImage archive = rpt.GetComponents()["Table1_Cell25"] as StiImage;
                    archive.Image = byteArrayToImageArchive((int)CheckOutStatusEnum.FareghReqStatus.archive_ok, userID, reqID);
                }

                rpt.Compile();
                rpt.CompiledReport.DataSources["[Request].[SP_GetStudentInfoForCheckOut2]"].Parameters["@stdcode"].ParameterValue = issuerID;
                rpt.CompiledReport.DataSources["[Request].[SP_Get_StudentLoanInfo]"].Parameters["@stcode"].ParameterValue = issuerID;
                //DataTable dtResault = new DataTable();
                //rpt.RegData(dtResault);
                StiWebViewer1.Report = rpt;
                StiWebViewer1.Visible = true;

            }
        }

        private System.Drawing.Image GetImageFromFile(Enum imagetype)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("../Files/" + imagetype.ToString() + ".jpg"));
            return img;
        }

        public System.Drawing.Image byteArrayToImage(int state,int reqID)
        {
            var dt= business.GetSignByLogDate(state, reqID);
            byte[] imgByte = null;
            if (dt !=null && dt.Rows.Count>0)
            {
                imgByte=(byte[])dt.Rows[0]["SignImage"];
            }

            //(from r in _signList.AsEnumerable()
            //                  where (r.Field<Int64>("RequestLogID") == stat)
            //                  //RequestLogID dar jadvale userlog eventesh fargh dare bayad 
            //                  //bebine ke oon event baraye oon modifyID dar che tarikhi rokh 
            //                  //dade bad logdate bayad dar jadvale emzaha dide shavad ke in 
            //                  //RequestLogID dar oon tarikhemzaye ki mioftade

            //                  select r.Field<byte[]>("SignImage")).SingleOrDefault();

            if (imgByte != null)
            {
                using (MemoryStream imgStream = new MemoryStream(imgByte))
                {
                    return System.Drawing.Image.FromStream(imgStream);
                }
            }
            return null;
        }

        public System.Drawing.Image byteArrayToImageArchive(int stat, decimal userId, int reqID)
        {


            var pdateTBL = business.GetLogDatesignByModifyID(reqID);
            string pdate = pdateTBL.Rows[0][0].ToString();
            byte[] imgByte = (from r in _signList.AsEnumerable()
                              where (r.Field<Int64>("RequestLogID") == stat && r.Field<decimal>("UserID") == userId
                              && r.Field<string>("fromDate").CompareTo(pdate) <= 0 && r.Field<string>("toDate").CompareTo(pdate) >= 0)
                              select r.Field<byte[]>("SignImage")).SingleOrDefault();

            if (imgByte != null)
            {
                using (MemoryStream imgStream = new MemoryStream(imgByte))
                {
                    return System.Drawing.Image.FromStream(imgStream);
                }
            }
            return null;
        }

        private bool isMashmool(string issuerID)
        {
            return business.isMashmool(issuerID);
        }

        public string issuerID { get; set; }
    }
}