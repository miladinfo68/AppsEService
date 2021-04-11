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
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class ShowDetailFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var classCode = Request.QueryString["ClassCode"].ToString();
            string fileDate = Request.QueryString["Date"].ToString();
      
            string term = Request.QueryString["t"].ToString();


            RecordsBusiness recBusiness = new RecordsBusiness();
            List<RecordsDTO> recDTO = new List<RecordsDTO>();
            recDTO = recBusiness.LinkOfClassWithLessonCodeByCodeClassAndTime(classCode, fileDate, term);
            //  recDTO = recBusiness.MakeOffLineClassWithLessonCodeByCodeClassAndTime(classCode, fileDate);
      
            lstView.DataSource = recDTO;
            lstView.DataBind();
         
            
            //string mId = Request.QueryString["id"].ToString();
            //string[] id = mId.ToString().Split(new char[] { '@' });
            //string menuId = "";
            //for (int i = 0; i < id[1].Length; i++)
            //{
            //    string s = id[1].Substring(i + 1, 1);
            //    if (s != "-")
            //        menuId += s;
            //    else
            //        break;
            //}
            //AccessControl1.MenuId = menuId;
            //AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
        }
    }
}