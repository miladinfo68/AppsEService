using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class OpenScanImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code_ostad = Request.QueryString["o"].ToString();
                string file_Name = Request.QueryString["f"].ToString();
                string filePath = "~/University/CooperationRequest/Teachers/ScanMadarek/"  + file_Name.Replace(@"\","/");
                if (file_Name.ToLower().EndsWith("jpg".ToLower())|| file_Name.ToLower().EndsWith("jpeg".ToLower()))
                {
                    filePath = filePath.Replace("(plusSign)", "");
                    filePath = filePath.Replace("(percentSign)", "");

                    Image imgScan = new Image();
                    imgScan.ImageUrl = filePath;
                    imgparent.Controls.Add(imgScan);
                    // Response.ContentType = "application/jpg";// doc.DOCUMENT_TYPE;
                }
                else
                {

                    filePath = filePath.Replace("(plusSign)", "+");
                    filePath = filePath.Replace("(percentSign)", "%");
                    Response.ContentType = "pdf";// doc.DOCUMENT_TYPE;
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment; filename=" + file_Name);
                    byte[] file = File.ReadAllBytes(MapPath(filePath));
                    Response.BinaryWrite(file);
                    Response.Flush();
                    Response.End();
                }
                //;
            }
        }
    }
}