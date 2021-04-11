using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.Business.Adobe;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Configuration;


namespace IAUEC_Apps.UI.Adobe.Pages
{
    public partial class ViewFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_User.Text = Session[sessionNames.userID_StudentOstad].ToString();
            var classCode = (Request.QueryString["ClassCode"].ToString());
            string fileDate = Request.QueryString["Date"].ToString();
            string assetId = Request.QueryString["Ast"].ToString();
            string term = Request.QueryString["t"].ToString();
            if (term!=ConfigurationManager.AppSettings["Term"].ToString())
            {
                btn_Select.Visible = false;
            }
            ClassName.InnerText = classCode.ToString();
            SessionName.InnerText = fileDate.ToString();
            txt_AssetTxt.Text = assetId.ToString();
            RecordsBusiness recBusiness = new RecordsBusiness();
            List<RecordsDTO> recDTO = new List<RecordsDTO>();
            recDTO = recBusiness.LinkOfClassWithLessonCodeByCodeClassAndTime(classCode, fileDate, term);
            //  recDTO = recBusiness.MakeOffLineClassWithLessonCodeByCodeClassAndTime(classCode, fileDate);
            if (recDTO.Count == 0)
                btn_Select.Visible = false;
            lstView.DataSource = recDTO;
            lstView.DataBind();
            DownloadRequestBusiness dnlreq = new DownloadRequestBusiness();
            DataTable dt = new DataTable();
            dt = dnlreq.Check_PayedAsset(Session[sessionNames.userID_StudentOstad].ToString(), Convert.ToInt32(txt_AssetTxt.Text));
            if (dt.Rows.Count > 0)
                btn_Select.Enabled = false;
            string fd = fileDate.ToString().Replace('/', '-');

           // string pathmp3 = Server.MapPath("../content/" + term + "/" + classCode.ToString() + "/" + fd );
            string pathflv = Server.MapPath("../content/" + term + "/" + classCode.ToString() + "/" + fd );
            bool viewmp3 = false, viewFlv = false, viewavi=false;
            if (Directory.Exists(pathflv)==false)
                btn_Select.Visible = false;
            else
            {
                DirectoryInfo di = new DirectoryInfo(Server.MapPath("../content/" + term + "/" + classCode.ToString() + "/" + fd + "/"));
                // Get a reference to each file in that directory.
                FileInfo[] fiArr = di.GetFiles("mp3.zip");
                foreach (FileInfo f in fiArr)
                    if (f.Length > 5000000)
                    {
                        viewmp3 = true;
                    }

                // Get a reference to each file in that directory.
                FileInfo[] fiArrflv = di.GetFiles("flv.zip");
                foreach (FileInfo f in fiArrflv)
                    if (f.Length > 10000000)
                    {
                        viewFlv = true;
                    }
                FileInfo[] fiArravi = di.GetFiles("avi.zip");
                foreach (FileInfo f in fiArravi)
                    if (f.Length > 5000000)
                    {
                        viewavi = true;
                    }
                if (viewFlv == false && viewmp3 == false && viewavi== false)
                {
                    btn_Select.Visible = false;
                }
            }
            Session[sessionNames.userID_StudentOstad] = lbl_User.Text;
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            DownloadRequestBusiness dnlreq = new DownloadRequestBusiness();
            DownloadRequestDTO dnlreqDTO = new DownloadRequestDTO();
            dnlreqDTO.StCode = lbl_User.Text;
            dnlreqDTO.Class_Code = txt_AssetTxt.Text;
            dnlreqDTO.Link_Click = false;
            dnlreq.Create_DownloadRequest(dnlreqDTO);
            MasterPage mp = this.Master;
            Reports rpt = new Reports();
            List<ReportDownloadReqDTO> req = new List<ReportDownloadReqDTO>();
            req = rpt.Get_SelectedAsset_NotPay(lbl_User.Text);
            //MastePage.MasterPage msp = new MastePage.MasterPage();
            RadGrid lst = (RadGrid)mp.FindControl("grdShopping");
            lst.DataSource = req;
            lst.Rebind();
            Response.Redirect("ClassList.aspx");
        }
    }
}