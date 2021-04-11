using IAUEC_Apps.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Exam.CMS.TestForms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            byte[] strPath = Convert.FromBase64String(TextBox1.Text);
            string pass = EncryptionClass.DecryptRJ256(strPath);
            Response.Write(pass);
        }
    }
}