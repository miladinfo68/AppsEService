using IAUEC_Apps.Business.university.Faculty;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class ShowPicture : System.Web.UI.Page
    {
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(Request.QueryString["ID"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["TypePic"].ToString())))
            {
                DataTable dt4 = FRB.GetInfoPeoByFilterPDF(int.Parse(Request.QueryString["ID"].ToString()), int.Parse(Request.QueryString["TypePic"].ToString()));
                if (dt4.Rows.Count > 0)
                {
                    if (dt4.Rows[0]["scan_document"] != DBNull.Value)
                    {

                        byte[] image = (byte[])(dt4.Rows[0]["scan_document"]);
                        RadBinaryImage1.DataValue = image;
                    }
                }
            }
        }
    }
}