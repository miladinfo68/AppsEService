using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ShowFilePrevious : System.Web.UI.Page
    {
        ExamBusiness examBusiness = new ExamBusiness();
        CommonBusiness CB = new CommonBusiness();
        public string EmbedSrc { get; set; } = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["pdfPath"] != null)
            {
                EmbedSrc = "";
                var buf = Session["pdfPath"] as byte[];
                string base64String = Convert.ToBase64String(buf, 0, buf.Length);
                var content_type = Session["contentType"].ToString();
                EmbedSrc = "data:" + content_type + ";base64," + base64String;
            }
            else
            {
                EmbedSrc = null;
                lblAlert.Visible = true;
                pnlEmbed.Visible = false;
            }
        }
    }
}