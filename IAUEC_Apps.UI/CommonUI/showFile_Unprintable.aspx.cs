using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class showFile_Unprintable : System.Web.UI.Page
    {
        public string EmbedSrc { get; set; } = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["fileUnprintable_Byte"] != null)
            {
                EmbedSrc = "";
                var buf = Session["fileUnprintable_Byte"] as byte[];
                string base64String = Convert.ToBase64String(buf, 0, buf.Length);
                var content_type = Session["contentTypeUnprintable"].ToString();
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