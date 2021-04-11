using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.DTO.AdobeClasses;

namespace IAUEC_Apps.UI.Adobe.Pages
{
    public partial class ListDownloadedFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<ReportDownloadReqDTO> lstDto = new List<ReportDownloadReqDTO>();
            DownloadRequestBusiness PB = new DownloadRequestBusiness();
            lstDto = PB.Get_DownloadedFiles_ByStcode(Session[sessionNames.userID_StudentOstad].ToString());
            grd_ListDownloadedFile.DataSource = lstDto;
            grd_ListDownloadedFile.DataBind();
        }
    }
}